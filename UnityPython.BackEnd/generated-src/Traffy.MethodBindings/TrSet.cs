using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrSet
    {
        public override int __hash__() => throw new TypeError($"unhashable type: {CLASS.Name.Escape()}");
        public override bool __ne__(TrObject o) => !__eq__(o);
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind_add(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        _0.add(_1);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("add() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["add"] = TrSharpFunc.FromFunc("add", __bind_add);
        }
    }
}

