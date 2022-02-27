using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrList
    {
        [Mark(Initialization.TokenBuiltinInit)]
        static void BindMethods()
        {
             Traffy.Objects.TrObject __bind_append(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    1 => ((Traffy.Objects.TrList)__args[0]).append(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1])),
                    _ => throw new ValueError("requires 1 argument(s), got " + __args.Count)
                }) ;
            }
            CLASS["append"] = TrSharpFunc.FromFunc("append", __bind_append);
        }
    }
}
