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
                return Box.Apply(__args.Count switch
                {
                    2 => Traffy.Objects.TrInt.__new__(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]),Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1])),
                    _ => throw new ValueError("requires 2 argument(s), got " + __args.Count)
                }) ;
            }
            CLASS["__new__"] = TrStaticMethod.Bind(CLASS.Name + "." + "__new__", __bind___new__);
            Traffy.Objects.TrObject __bind_from_bytes(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    2 => Traffy.Objects.TrInt.from_bytes(Unbox.Apply(THint<System.Byte[]>.Unique,__args[0]),Unbox.Apply(THint<string>.Unique,__args[1])),
                    _ => throw new ValueError("requires 2 argument(s), got " + __args.Count)
                }) ;
            }
            CLASS["from_bytes"] = TrStaticMethod.Bind(CLASS.Name + "." + "from_bytes", __bind_from_bytes);
        }
    }
}
