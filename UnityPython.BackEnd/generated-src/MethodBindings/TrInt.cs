using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrInt
    {
        [Mark(Initialization.TokenBuiltinInit)]
        static void BindMethods()
        {
            Traffy.Objects.TrObject __bind___new__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(Traffy.Objects.TrInt.__new__(_0,_1));
                    }
                    default:
                        throw new ValueError("__new__() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["__new__"] = TrStaticMethod.Bind(CLASS.Name + "." + "__new__", __bind___new__);
            Traffy.Objects.TrObject __bind_from_bytes(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<System.Byte[]>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        return Box.Apply(Traffy.Objects.TrInt.from_bytes(_0,_1));
                    }
                    default:
                        throw new ValueError("from_bytes() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["from_bytes"] = TrStaticMethod.Bind(CLASS.Name + "." + "from_bytes", __bind_from_bytes);
        }
    }
}
