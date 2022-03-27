using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrGenerator
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind_send(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrGenerator>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        Traffy.Objects.TrRef _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("refval"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<Traffy.Objects.TrRef>.Unique,__keyword__2);
                        else
                            _2 = null;
                        return Box.Apply(_0.send(_1,refval : _2));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrGenerator>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrRef>.Unique,__args[2]);
                        return Box.Apply(_0.send(_1,_2));
                    }
                    default:
                        throw new ValueError("send() requires 2 to 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["send"] = TrSharpFunc.FromFunc("send", __bind_send);
        }
    }
}

