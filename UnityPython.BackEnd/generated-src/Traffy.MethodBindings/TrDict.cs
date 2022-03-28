using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrDict
    {
        public override int __hash__() => throw new TypeError($"unhashable type: {CLASS.Name.Escape()}");
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind_keys(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrDict>.Unique,__args[0]);
                        return Box.Apply(_0.keys());
                    }
                    default:
                        throw new ValueError("keys() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["keys"] = TrSharpFunc.FromFunc("keys", __bind_keys);
            static  Traffy.Objects.TrObject __bind_values(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrDict>.Unique,__args[0]);
                        return Box.Apply(_0.values());
                    }
                    default:
                        throw new ValueError("values() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["values"] = TrSharpFunc.FromFunc("values", __bind_values);
            static  Traffy.Objects.TrObject __bind_items(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrDict>.Unique,__args[0]);
                        return Box.Apply(_0.items());
                    }
                    default:
                        throw new ValueError("items() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["items"] = TrSharpFunc.FromFunc("items", __bind_items);
        }
    }
}

