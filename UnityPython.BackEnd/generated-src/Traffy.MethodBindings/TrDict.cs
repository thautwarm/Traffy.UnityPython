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
            static  Traffy.Objects.TrObject __bind_fromkeys(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        Traffy.Objects.TrObject _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("value"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__keyword__1);
                        else
                            _1 = null;
                        return Box.Apply(Traffy.Objects.TrDict.fromkeys(_0,value : _1));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(Traffy.Objects.TrDict.fromkeys(_0,_1));
                    }
                    default:
                        throw new ValueError("fromkeys() requires 1 to 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["fromkeys"] = TrStaticMethod.Bind(CLASS.Name + "." + "fromkeys", __bind_fromkeys);
            static  Traffy.Objects.TrObject __bind_clear(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrDict>.Unique,__args[0]);
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
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrDict>.Unique,__args[0]);
                        return Box.Apply(_0.copy());
                    }
                    default:
                        throw new ValueError("copy() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["copy"] = TrSharpFunc.FromFunc("copy", __bind_copy);
            static  Traffy.Objects.TrObject __bind_get(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrDict>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        Traffy.Objects.TrObject _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("defaultVal"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__keyword__2);
                        else
                            _2 = null;
                        return Box.Apply(_0.get(_1,defaultVal : _2));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrDict>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[2]);
                        return Box.Apply(_0.get(_1,_2));
                    }
                    default:
                        throw new ValueError("get() requires 2 to 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["get"] = TrSharpFunc.FromFunc("get", __bind_get);
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
            static  Traffy.Objects.TrObject __bind_pop(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrDict>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        Traffy.Objects.TrObject _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("defaultVal"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__keyword__2);
                        else
                            _2 = null;
                        return Box.Apply(_0.pop(_1,defaultVal : _2));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrDict>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[2]);
                        return Box.Apply(_0.pop(_1,_2));
                    }
                    default:
                        throw new ValueError("pop() requires 2 to 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["pop"] = TrSharpFunc.FromFunc("pop", __bind_pop);
            static  Traffy.Objects.TrObject __bind_setdefault(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrDict>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[2]);
                        return Box.Apply(_0.setdefault(_1,_2));
                    }
                    default:
                        throw new ValueError("setdefault() requires 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["setdefault"] = TrSharpFunc.FromFunc("setdefault", __bind_setdefault);
            static  Traffy.Objects.TrObject __bind_update(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrDict>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
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

