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
        public const int PolymorphicInlineCacheMax = 5;
    }
    public abstract partial class IC
    {
        public InternedString Name => s_name;

        public InternedString s_name;

        public abstract InlineCacheReceiver InstReadProto(TrClass cls);

        public abstract InlineCacheReceiver InstWriteProto(TrClass cls);

        public abstract InlineCacheReceiver ClassReadProto(TrClass cls);


        public static bool NoICReadShapeInst(TrClass cls, string name, out Shape ad)
        {
            if (name == "__new__")
            {
                ad = null;
                return false;
            }

            for (int i = 0; i < cls.__mro.Length; i++)
            {
                var other_cls = cls.__mro[i];
                if (cls.__mro[i].__prototype__.TryGetValue(name, out ad))
                {
                    if (ad.Kind == AttributeKind.Field && other_cls != cls)
                    {
                        // fields from parent classes are not inherited
                        // should create new ones in the current class
                        continue;
                    }
                    return true;
                }
            }
            ad = null;
            return false;
        }
        public static bool NoICOverwriteShapeInst(TrClass cls, string name, out Shape ad)
        {
            return cls.__prototype__.TryGetValue(name, out ad);
        }

        public static bool NOICReadShapeClass(TrClass cls, string name, out Shape ad)
        {
            for (int i = 0; i < cls.__mro.Length; i++)
            {
                var other_cls = cls.__mro[i];
                if (cls.__mro[i].__prototype__.TryGetValue(name, out ad))
                {
                    if (ad.Kind == AttributeKind.Field)
                    {
                        // classes cannot access instance fields
                        continue;
                    }
                    return true;
                }
            }
            ad = null;
            return false;
        }

        public static bool NOICOverwriteShapeClass(TrClass cls, string name, out Shape ad)
        {
            return cls.__prototype__.TryGetValue(name, out ad) && ad.Kind != AttributeKind.Field;
        }
    }



    public class InlineCacheInstance: IC
    {
        int LastReceiver;
        public InlineCacheReceiver[] receivers = new InlineCacheReceiver[InlineCacheReceiver.PolymorphicInlineCacheMax];

        public void AddReceiver(InlineCacheReceiver receiver)
        {
            receivers[(LastReceiver++) % InlineCacheReceiver.PolymorphicInlineCacheMax] = receiver;
            LastReceiver %= InlineCacheReceiver.PolymorphicInlineCacheMax;
        }

        public InlineCacheInstance(InternedString name)
        {
            LastReceiver = 0;
            this.s_name = name;
        }

        public override InlineCacheReceiver InstReadProto(TrClass cls)
        {
            InlineCacheReceiver receiver;
            for (int i = 0; i < InlineCacheReceiver.PolymorphicInlineCacheMax; i++)
            {
                receiver = receivers[i];
                if (receiver == null)
                    break;
                if (object.ReferenceEquals(receiver.cls, cls))
                {
                    if (receiver.Token == cls.Token)
                    {
                        if (i != 0)
                        {
                            // move the receiver to the previous position
                            receivers[i] = receivers[i - 1];
                            receivers[i - 1] = receiver;
                        }
                        return receiver;
                    }
                    else
                    {
                        // the class has been changed, so we need to update the receiver
                        receiver.Token = cls.Token;
                        if (IC.NoICReadShapeInst(cls, Name, out receiver.shape))
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
            IC.NoICReadShapeInst(cls, Name, out receiver.shape);
            return receiver;
        }

        public override InlineCacheReceiver InstWriteProto(TrClass cls)
        {
            InlineCacheReceiver receiver;
            for (int i = 0; i < InlineCacheReceiver.PolymorphicInlineCacheMax; i++)
            {
                receiver = receivers[i];
                if (receiver == null)
                    break;
                if (object.ReferenceEquals(receiver.cls, cls))
                {
                    if (receiver.Token == cls.Token)
                    {
                        if (i != 0)
                        {
                            // move the receiver to the previous position
                            receivers[i] = receivers[i - 1];
                            receivers[i - 1] = receiver;
                        }
                        if (receiver.shape == null)
                            goto update;
                        return receiver;
                    }
                    else
                    {
                        // the class has been changed, so we need to update the receiver
                        receiver.Token = cls.Token;
                        if (IC.NoICOverwriteShapeInst(cls, Name, out receiver.shape))
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
            IC.NoICOverwriteShapeInst(cls, Name, out receiver.shape);

            if (object.ReferenceEquals(receiver.shape, null))
            {
                var Token = cls.UpdatePrototype(); // mark the class as changed
                int field_index;
                field_index = cls.fieldCnt++;
                var shape = Shape.MKField(Name, field_index);
                cls.__prototype__.Add(Name, shape);
                receiver.Token = Token;
                receiver.shape = shape;
            }
            return receiver;
        }



        public override InlineCacheReceiver ClassReadProto(TrClass cls)
        {
            throw new InvalidProgramException($"class {cls.Name} is using instance-specific IC");
        }
    }

    public class InlineCacheClass: IC
    {
        int LastReceiver;
        public InlineCacheReceiver[] receivers = new InlineCacheReceiver[2];

        public void AddReceiver(InlineCacheReceiver receiver)
        {
            receivers[(LastReceiver++) % 2] = receiver;
            LastReceiver %= 2;
        }

        public InlineCacheClass(InternedString name)
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
                    if (IC.NOICReadShapeClass(cls, Name, out receiver.shape))
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
                    if (IC.NOICReadShapeClass(cls, Name, out receiver.shape))
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
            IC.NOICReadShapeClass(cls, Name, out receiver.shape);
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

    public class PolyIC: IC
    {
        public InlineCacheClass ICClass;
        public InlineCacheInstance ICInstance;
        public TrStr attribute;
        public PolyIC(TrStr name)
        {
            name = name.Interned();
            s_name = name.AsString().ToIntern();
            ICClass = new InlineCacheClass(s_name);
            ICInstance = new InlineCacheInstance(s_name);
            this.attribute = name;
        }

        public PolyIC(InternedString name)
        {
            s_name = name;
            ICClass = new InlineCacheClass(s_name);
            ICInstance = new InlineCacheInstance(s_name);
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


    public class MonoInlineCacheInstance: IC
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
                        if (IC.NoICReadShapeInst(cls, Name, out receiver.shape))
                        {
                            return receiver;
                        }
                    }
                }
            }
            receiver = new InlineCacheReceiver();
            receiver.cls = cls;
            receiver.Token = cls.Token;
            IC.NoICReadShapeInst(cls, Name, out receiver.shape);
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
                        if (IC.NoICOverwriteShapeInst(cls, Name, out receiver.shape))
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
            receiver.cls = cls;
            receiver.Token = cls.Token;
            IC.NoICOverwriteShapeInst(cls, Name, out receiver.shape);
            if (object.ReferenceEquals(receiver.shape, null))
            {
                var Token = cls.UpdatePrototype(); // mark the class as changed
                int field_index;
                field_index = cls.fieldCnt++;
                var shape = Shape.MKField(Name, field_index);
                receiver.shape = shape;
                cls.__prototype__.Add(Name, shape);
                receiver.Token = Token;
            }
            return receiver;
        }
    }

    public class MonoInlineCacheClass: IC
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
                    if (IC.NOICReadShapeClass(cls, Name, out receiver.shape))
                    {
                        return receiver;
                    }
                }
            }
            receiver = new InlineCacheReceiver();
            receiver.cls = cls;
            receiver.Token = cls.Token;
            IC.NOICReadShapeClass(cls, Name, out receiver.shape);
            return receiver;
        }
    }

    public class MonoIC: IC
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