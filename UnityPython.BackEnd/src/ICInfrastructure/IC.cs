using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Traffy.Objects;
namespace Traffy.InlineCache
{

    public class InlineCacheReceiver
    {
        public TrClass cls;
        public object Token;
        public Shape shape = null;
    }


    public class PolyIC_Inst
    {
        public InternedString Name => s_name;

        public InternedString s_name;
        int LastReceiver;
        InlineCacheReceiver[] Receivers;
        int MaxReceiverCount;

        public void AddReceiver(InlineCacheReceiver receiver)
        {
            Receivers[(LastReceiver++) % MaxReceiverCount] = receiver;
            LastReceiver %= MaxReceiverCount;
        }

        public PolyIC_Inst(InternedString name, int maxReceiverCount = 5)
        {
            MaxReceiverCount = maxReceiverCount;
            Receivers = new InlineCacheReceiver[maxReceiverCount];
            for (int i = 0; i < Receivers.Length; i++)
            {
                Receivers[i] = new InlineCacheReceiver();
                Receivers[i].cls = null;
            }
            LastReceiver = 0;
            this.s_name = name;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public bool ReadInst(TrObject obj, out TrObject value)
        {
            InlineCacheReceiver receiver;
            TrClass cls = obj.Class;
            for (int i = 0; i < MaxReceiverCount; i++)
            {
                receiver = Receivers[i];
                if (object.ReferenceEquals(receiver.cls, cls))
                {
                    if (object.ReferenceEquals(receiver.Token, cls.Token))
                    {
                        return obj.ReadInst(receiver.shape, out value);
                    }
                    else
                    {
                        // the class has been changed, so we need to update the receiver
                        receiver.Token = cls.Token;
                        if (cls.LoadCachedShape_ReadInst(Name.Value, out receiver.shape))
                        {
                            return obj.ReadInst(receiver.shape, out value);
                        }
                    }
                    break;
                }
            }

            receiver = new InlineCacheReceiver();
            receiver.cls = cls;
            receiver.Token = cls.Token;
            AddReceiver(receiver);
            if (cls.LoadCachedShape_ReadInst(Name.Value, out receiver.shape))
            {
                return obj.ReadInst(receiver.shape, out value);
            }
            value = null;
            return false;
        }

        public void WriteInst(TrObject obj, TrObject value)
        {
            InlineCacheReceiver receiver;
            TrClass cls = obj.Class;
            for (int i = 0; i < MaxReceiverCount; i++)
            {
                receiver = Receivers[i];
                if (object.ReferenceEquals(receiver.cls, cls))
                {
                    if (receiver.Token == cls.Token)
                    {
                        if (receiver.shape == null)
                            goto update;
                        PolyIC.WriteInst(obj, receiver.shape, value);
                        return;

                    }
                    else
                    {
                        // the class has been changed, so we need to update the receiver
                        receiver.Token = cls.Token;
                        if (cls.LoadCachedShape_WriteInst(Name.Value, out receiver.shape))
                        {
                            PolyIC.WriteInst(obj, receiver.shape, value);
                            return;
                        }
                    }
                    break;
                }
            }
            receiver = new InlineCacheReceiver();
        update:

            if (cls.IsInstanceFixed)
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
            PolyIC.WriteInst(obj, receiver.shape, value);
            return;
        }
    }

    public class PolyIC_Class
    {
        public InternedString Name => s_name;

        public InternedString s_name;

        int LastReceiver;
        public InlineCacheReceiver[] receivers = new InlineCacheReceiver[2];


        public void AddReceiver(InlineCacheReceiver receiver)
        {
            receivers[(LastReceiver++) % 2] = receiver;
            LastReceiver %= 2;
        }

        public PolyIC_Class(InternedString name)
        {
            for (int i = 0; i < receivers.Length; i++)
            {
                receivers[i] = new InlineCacheReceiver();
                receivers[i].cls = null;
            }
            LastReceiver = 0;
            this.s_name = name;
        }


        const int ZERO = 0;
        const int ONE = 1;
        public bool ReadClass(TrClass cls, out TrObject value)
        {
            InlineCacheReceiver receiver;

            receiver = receivers[ZERO];
            if (object.ReferenceEquals(receiver.cls, cls))
            {
                if (receiver.Token == cls.Token)
                {
                    return PolyIC.ReadClass(cls, receiver.shape, out value);
                }
                else
                {
                    // the class has been changed, so we need to update the receiver
                    receiver.Token = cls.Token;
                    if (cls.LoadCachedShape_ReadClass(Name.Value, out receiver.shape))
                    {
                        return PolyIC.ReadClass(cls, receiver.shape, out value);
                    }
                }
                goto end;
            }

            receiver = receivers[ONE];
            if (object.ReferenceEquals(receiver.cls, cls))
            {
                if (receiver.Token == cls.Token)
                {
                    return PolyIC.ReadClass(cls, receiver.shape, out value);
                }
                else
                {
                    // the class has been changed, so we need to update the receiver
                    receiver.Token = cls.Token;
                    if (cls.LoadCachedShape_ReadClass(Name.Value, out receiver.shape))
                    {
                        return PolyIC.ReadClass(cls, receiver.shape, out value);
                    }
                }
                goto end;
            }

        end:
            receiver = new InlineCacheReceiver();
            receiver.cls = cls;
            receiver.Token = cls.Token;
            AddReceiver(receiver);
            if (cls.LoadCachedShape_ReadClass(Name.Value, out receiver.shape))
            {
                return PolyIC.ReadClass(cls, receiver.shape, out value);
            }
            value = null;
            return false;
        }
    }

    public partial class PolyIC
    {
        public InternedString Name => s_name;
        public InternedString s_name;
        public PolyIC_Class ICClass;
        public PolyIC_Inst ICInstance;

        // 'attribute' is always interned
        public TrStr attribute;
        public PolyIC(TrStr name)
        {
            s_name = name.AsStr().ToIntern();
            ICClass = new PolyIC_Class(s_name);
            ICInstance = new PolyIC_Inst(s_name);
            if (!name.isInterned)
                name = name.Interned();
            attribute = name;
        }

        public PolyIC(InternedString name)
        {
            s_name = name;
            ICClass = new PolyIC_Class(s_name);
            ICInstance = new PolyIC_Inst(s_name);
            attribute = MK.IStr(name);
        }
    }

}