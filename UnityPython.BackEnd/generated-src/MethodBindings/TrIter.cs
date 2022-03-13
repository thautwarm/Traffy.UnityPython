using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrIter
    {
        [Mark(Initialization.TokenBuiltinInit)]
        static void BindMethods()
        {
            Traffy.Objects.TrObject __bind___new__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    2 => Traffy.Objects.TrIter.__new__(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]),Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1])),
                    _ => throw new ValueError("requires 2 argument(s), got " + __args.Count)
                }) ;
            }
            CLASS["__new__"] = TrStaticMethod.Bind(CLASS.Name + "." + "__new__", __bind___new__);
        }
    }
}
