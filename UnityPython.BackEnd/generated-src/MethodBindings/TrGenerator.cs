using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrGenerator
    {
        [Mark(Initialization.TokenBuiltinInit)]
        static void BindMethods()
        {
            Traffy.Objects.TrObject __bind_send(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    2 => ((Traffy.Objects.TrGenerator)__args[0]).send(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1])),
                    3 => ((Traffy.Objects.TrGenerator)__args[0]).send(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]),Unbox.Apply(THint<Traffy.Objects.TrRef>.Unique,__args[2])),
                    _ => throw new ValueError("requires 1 to 2 argument(s), got " + __args.Count)
                }) ;
            }
            CLASS["send"] = TrSharpFunc.FromFunc("send", __bind_send);
        }
    }
}
