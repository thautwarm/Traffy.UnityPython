using System;
using System.Collections.Generic;
using Traffy.Objects;
namespace Traffy.InlineCache
{

    public class InlineCacheReceiver
    {
        public TrClass cls;
        public object Token;
        public Shape shape = null;
        public const int MaxPolymorphicInlineCacheStore = 5;
    }
    public abstract partial class IC
    {
        public InternedString Name => s_name;

        public InternedString s_name;

        public abstract InlineCacheReceiver InstReadProto(TrClass cls);

        public abstract InlineCacheReceiver InstWriteProto(TrClass cls);

        public abstract InlineCacheReceiver ClassReadProto(TrClass cls);

    }


    public class PolyIC_Inst : IC
    {
        int LastReceiver;
        public InlineCacheReceiver[] receivers = new InlineCacheReceiver[InlineCacheReceiver.MaxPolymorphicInlineCacheStore];

        public void AddReceiver(InlineCacheReceiver receiver)
        {
            receivers[(LastReceiver++) % InlineCacheReceiver.MaxPolymorphicInlineCacheStore] = receiver;
            LastReceiver %= InlineCacheReceiver.MaxPolymorphicInlineCacheStore;
        }

        public PolyIC_Inst(InternedString name)
        {
            LastReceiver = 0;
            this.s_name = name;
        }

        public override InlineCacheReceiver InstReadProto(TrClass cls)
        {
            InlineCacheReceiver receiver;
            for (int i = 0; i < InlineCacheReceiver.MaxPolymorphicInlineCacheStore; i++)
            {
                receiver = receivers[i];
                if (receiver == null)
                    break;
                if (object.ReferenceEquals(receiver.cls, cls))
                {
                    if (receiver.Token == cls.Token)
                    {

                        return receiver;
                    }
                    else
                    {
                        // the class has been changed, so we need to update the receiver
                        receiver.Token = cls.Token;
                        if (cls.LoadCachedShape_ReadInst(Name.Value, out receiver.shape))
                        {
                            return receiver;
                        }
                    }
                    break;
                }
            }

            receiver = new InlineCacheReceiver();
            receiver.cls = cls;
            receiver.Token = cls.Token;
            AddReceiver(receiver);
            cls.LoadCachedShape_ReadInst(Name.Value, out receiver.shape);
            return receiver;
        }

        public override InlineCacheReceiver InstWriteProto(TrClass cls)
        {
            InlineCacheReceiver receiver;
            for (int i = 0; i < InlineCacheReceiver.MaxPolymorphicInlineCacheStore; i++)
            {
                receiver = receivers[i];
                if (receiver == null)
                    break;
                if (object.ReferenceEquals(receiver.cls, cls))
                {
                    if (receiver.Token == cls.Token)
                    {

                        if (receiver.shape == null)
                            goto update;
                        return receiver;
                    }
                    else
                    {
                        // the class has been changed, so we need to update the receiver
                        receiver.Token = cls.Token;
                        if (cls.LoadCachedShape_WriteInst(Name.Value, out receiver.shape))
                        {
                            return receiver;
                        }
                    }
                    break;
                }
            }
            receiver = new InlineCacheReceiver();
        update:

            if (cls.IsFixed)
                throw new TypeError($"{cls.Name} object is frozen, can't set attribute {Name}");
            receiver.cls = cls;
            receiver.Token = cls.Token;
            AddReceiver(receiver);
            if (!cls.LoadCachedShape_WriteInst(Name.Value, out receiver.shape))
            {
                cls.AddFieldOrFind(Name.Value, out var fieldShape);
                receiver.shape = fieldShape.Get;
                receiver.Token = cls.Token;
            }
            return receiver;
        }



        public override InlineCacheReceiver ClassReadProto(TrClass cls)
        {
            throw new InvalidProgramException($"class {cls.Name} is using instance-specific IC");
        }
    }

    public class PolyIC_Class : IC
    {
        int LastReceiver;
        public InlineCacheReceiver[] receivers = new InlineCacheReceiver[2];

        public void AddReceiver(InlineCacheReceiver receiver)
        {
            receivers[(LastReceiver++) % 2] = receiver;
            LastReceiver %= 2;
        }

        public PolyIC_Class(InternedString name)
        {
            LastReceiver = 0;
            this.s_name = name;
        }


        const int ZERO = 0;
        const int ONE = 1;
        public override InlineCacheReceiver ClassReadProto(TrClass cls)
        {
            InlineCacheReceiver receiver;

            receiver = receivers[ZERO];
            if (receiver == null)
                goto end;
            if (object.ReferenceEquals(receiver.cls, cls))
            {
                if (receiver.Token == cls.Token)
                {
                    return receiver;
                }
                else
                {
                    // the class has been changed, so we need to update the receiver
                    receiver.Token = cls.Token;
                    if (cls.LoadCachedShape_ReadClass(Name.Value, out receiver.shape))
                    {
                        return receiver;
                    }
                }
                goto end;
            }


            receiver = receivers[ONE];
            if (receiver == null)
                goto end;
            if (object.ReferenceEquals(receiver.cls, cls))
            {
                if (receiver.Token == cls.Token)
                {
                    if (ONE != 0)
                    {
                        // move the receiver to the previous position
                        receivers[ONE] = receivers[ZERO];
                        receivers[ZERO] = receiver;
                    }
                    return receiver;
                }
                else
                {
                    // the class has been changed, so we need to update the receiver
                    receiver.Token = cls.Token;
                    if (cls.LoadCachedShape_ReadClass(Name.Value, out receiver.shape))
                    {
                        return receiver;
                    }
                }
                goto end;
            }

        end:
            receiver = new InlineCacheReceiver();
            receiver.cls = cls;
            receiver.Token = cls.Token;
            AddReceiver(receiver);
            cls.LoadCachedShape_ReadClass(Name.Value, out receiver.shape);
            return receiver;
        }

        public override InlineCacheReceiver InstReadProto(TrClass cls)
        {
            throw new InvalidProgramException($"{cls.Name} object is using class-specific IC");
        }

        public override InlineCacheReceiver InstWriteProto(TrClass cls)
        {
            throw new InvalidProgramException($"{cls.Name} object is using class-specific IC");
        }
    }

    public class PolyIC : IC
    {
        public PolyIC_Class ICClass;
        public PolyIC_Inst ICInstance;
        public TrStr attribute;
        public PolyIC(TrStr name)
        {
            s_name = name.AsString().ToIntern();
            ICClass = new PolyIC_Class(s_name);
            ICInstance = new PolyIC_Inst(s_name);
            if (!name.isInterned)
                name = name.Interned();
            this.attribute = name;
        }

        public PolyIC(InternedString name)
        {
            s_name = name;
            ICClass = new PolyIC_Class(s_name);
            ICInstance = new PolyIC_Inst(s_name);
            this.attribute = MK.Str(name);
        }

        public override InlineCacheReceiver ClassReadProto(TrClass cls)
        {
            return ICClass.ClassReadProto(cls);
        }

        public override InlineCacheReceiver InstReadProto(TrClass cls)
        {
            return ICInstance.InstReadProto(cls);
        }

        public override InlineCacheReceiver InstWriteProto(TrClass cls)
        {
            return ICInstance.InstWriteProto(cls);
        }
    }


    public class MonoInlineCacheInstance : IC
    {
        public InlineCacheReceiver receiver = null;

        public MonoInlineCacheInstance(InternedString name)
        {
            this.s_name = name;
        }

        public override InlineCacheReceiver ClassReadProto(TrClass cls)
        {
            throw new InvalidProgramException($"class {cls.Name} is using instance-specific IC");
        }

        public override InlineCacheReceiver InstReadProto(TrClass cls)
        {
            if (receiver != null)
            {
                if (object.ReferenceEquals(receiver.cls, cls))
                {
                    if (receiver.Token == cls.Token)
                    {
                        return receiver;
                    }
                    else
                    {
                        // the class has been changed, so we need to update the receiver
                        receiver.Token = cls.Token;
                        if (cls.LoadCachedShape_ReadInst(Name.Value, out receiver.shape))
                        {
                            return receiver;
                        }
                    }
                }
            }
            receiver = new InlineCacheReceiver();
            receiver.cls = cls;
            receiver.Token = cls.Token;
            cls.LoadCachedShape_ReadInst(Name.Value, out receiver.shape);
            return receiver;
        }

        public override InlineCacheReceiver InstWriteProto(TrClass cls)
        {
            if (receiver != null)
            {
                if (object.ReferenceEquals(receiver.cls, cls))
                {
                    if (receiver.Token == cls.Token)
                    {
                        if (receiver.shape == null)
                            goto update;
                        return receiver;
                    }
                    else
                    {
                        // the class has been changed, so we need to update the receiver
                        receiver.Token = cls.Token;
                        if (cls.LoadCachedShape_WriteInst(Name.Value, out receiver.shape))
                        {
                            return receiver;
                        }
                    }
                }
            }
            receiver = new InlineCacheReceiver();
        update:
            if (cls.IsFixed)
                throw new TypeError($"{cls.Name} object is frozen, can't set attribute {Name}");
            cls.AddFieldOrFind(Name.Value, out var fieldshape);
            receiver.shape = fieldshape.Get;
            receiver.Token = cls.Token;
            return receiver;
        }
    }

    public class MonoInlineCacheClass : IC
    {
        public InlineCacheReceiver receiver = null;

        public MonoInlineCacheClass(InternedString name)
        {
            this.s_name = name;
        }

        public override InlineCacheReceiver InstReadProto(TrClass cls)
        {
            throw new InvalidProgramException($"{cls.Name} object is using class-specific IC");
        }

        public override InlineCacheReceiver InstWriteProto(TrClass cls)
        {
            throw new InvalidProgramException($"{cls.Name} object is using class-specific IC");
        }

        public override InlineCacheReceiver ClassReadProto(TrClass cls)
        {
            if (receiver != null && object.ReferenceEquals(receiver.cls, cls))
            {
                if (receiver.Token == cls.Token)
                {
                    return receiver;
                }
                else
                {
                    // the class has been changed, so we need to update the receiver
                    receiver.Token = cls.Token;
                    if (cls.LoadCachedShape_ReadClass(Name.Value, out receiver.shape))
                    {
                        return receiver;
                    }
                }
            }
            receiver = new InlineCacheReceiver();
            receiver.cls = cls;
            receiver.Token = cls.Token;
            cls.LoadCachedShape_ReadClass(Name.Value, out receiver.shape);
            return receiver;
        }
    }

    public class MonoIC : IC
    {
        public MonoInlineCacheClass ICClass;
        public MonoInlineCacheInstance ICInstance;
        public TrStr attribute;
        public MonoIC(TrStr name)
        {
            name = name.Interned();
            s_name = name.AsString().ToIntern();
            ICClass = new MonoInlineCacheClass(s_name);
            ICInstance = new MonoInlineCacheInstance(s_name);
            this.attribute = name;
        }
        public MonoIC(InternedString istr)
        {
            s_name = istr;
            ICClass = new MonoInlineCacheClass(istr);
            ICInstance = new MonoInlineCacheInstance(istr);
            this.attribute = MK.Str(istr);
        }

        public override InlineCacheReceiver ClassReadProto(TrClass cls)
        {
            return ICClass.ClassReadProto(cls);
        }

        public override InlineCacheReceiver InstReadProto(TrClass cls)
        {
            return ICInstance.InstReadProto(cls);
        }

        public override InlineCacheReceiver InstWriteProto(TrClass cls)
        {
            return ICInstance.InstWriteProto(cls);
        }
    }


}