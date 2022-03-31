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
            static  Traffy.Objects.TrObject __bind_clear(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        _0.clear();
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("clear() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["clear"] = TrSharpFunc.FromFunc("clear", __bind_clear);
            static  Traffy.Objects.TrObject __bind_copy(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        return Box.Apply(_0.copy());
                    }
                    default:
                        throw new ValueError("copy() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["copy"] = TrSharpFunc.FromFunc("copy", __bind_copy);
            static  Traffy.Objects.TrObject __bind_difference_update(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Collections.Generic.HashSet<Traffy.Objects.TrObject>>.Unique,__args[1]);
                        _0.difference_update(_1);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("difference_update() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["difference_update"] = TrSharpFunc.FromFunc("difference_update", __bind_difference_update);
            static  Traffy.Objects.TrObject __bind_discard(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        _0.discard(_1);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("discard() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["discard"] = TrSharpFunc.FromFunc("discard", __bind_discard);
            static  Traffy.Objects.TrObject __bind_intersection(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Collections.Generic.HashSet<Traffy.Objects.TrObject>>.Unique,__args[1]);
                        return Box.Apply(_0.intersection(_1));
                    }
                    default:
                        throw new ValueError("intersection() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["intersection"] = TrSharpFunc.FromFunc("intersection", __bind_intersection);
            static  Traffy.Objects.TrObject __bind_intersection_update(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Collections.Generic.HashSet<Traffy.Objects.TrObject>>.Unique,__args[1]);
                        _0.intersection_update(_1);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("intersection_update() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["intersection_update"] = TrSharpFunc.FromFunc("intersection_update", __bind_intersection_update);
            static  Traffy.Objects.TrObject __bind_isdisjoint(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Collections.Generic.HashSet<Traffy.Objects.TrObject>>.Unique,__args[1]);
                        return Box.Apply(_0.isdisjoint(_1));
                    }
                    default:
                        throw new ValueError("isdisjoint() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["isdisjoint"] = TrSharpFunc.FromFunc("isdisjoint", __bind_isdisjoint);
            static  Traffy.Objects.TrObject __bind_issubset(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Collections.Generic.HashSet<Traffy.Objects.TrObject>>.Unique,__args[1]);
                        return Box.Apply(_0.issubset(_1));
                    }
                    default:
                        throw new ValueError("issubset() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["issubset"] = TrSharpFunc.FromFunc("issubset", __bind_issubset);
            static  Traffy.Objects.TrObject __bind_issuperset(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Collections.Generic.HashSet<Traffy.Objects.TrObject>>.Unique,__args[1]);
                        return Box.Apply(_0.issuperset(_1));
                    }
                    default:
                        throw new ValueError("issuperset() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["issuperset"] = TrSharpFunc.FromFunc("issuperset", __bind_issuperset);
            static  Traffy.Objects.TrObject __bind_pop(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        return Box.Apply(_0.pop());
                    }
                    default:
                        throw new ValueError("pop() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["pop"] = TrSharpFunc.FromFunc("pop", __bind_pop);
            static  Traffy.Objects.TrObject __bind_remove(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(_0.remove(_1));
                    }
                    default:
                        throw new ValueError("remove() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["remove"] = TrSharpFunc.FromFunc("remove", __bind_remove);
            static  Traffy.Objects.TrObject __bind_symmetric_difference(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Collections.Generic.HashSet<Traffy.Objects.TrObject>>.Unique,__args[1]);
                        return Box.Apply(_0.symmetric_difference(_1));
                    }
                    default:
                        throw new ValueError("symmetric_difference() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["symmetric_difference"] = TrSharpFunc.FromFunc("symmetric_difference", __bind_symmetric_difference);
            static  Traffy.Objects.TrObject __bind_symmetric_difference_update(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Collections.Generic.HashSet<Traffy.Objects.TrObject>>.Unique,__args[1]);
                        _0.symmetric_difference_update(_1);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("symmetric_difference_update() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["symmetric_difference_update"] = TrSharpFunc.FromFunc("symmetric_difference_update", __bind_symmetric_difference_update);
            static  Traffy.Objects.TrObject __bind_union(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Collections.Generic.HashSet<Traffy.Objects.TrObject>>.Unique,__args[1]);
                        return Box.Apply(_0.union(_1));
                    }
                    default:
                        throw new ValueError("union() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["union"] = TrSharpFunc.FromFunc("union", __bind_union);
            static  Traffy.Objects.TrObject __bind_update(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSet>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Collections.Generic.IEnumerator<Traffy.Objects.TrObject>>.Unique,__args[1]);
                        _0.update(_1);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("update() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["update"] = TrSharpFunc.FromFunc("update", __bind_update);
        }
    }
}

