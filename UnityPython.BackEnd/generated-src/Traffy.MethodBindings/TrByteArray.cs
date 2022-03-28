using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrByteArray
    {
        public override int __hash__() => throw new TypeError($"unhashable type: {CLASS.Name.Escape()}");
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind___new__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        Traffy.Objects.TrObject _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("buffer"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__keyword__1);
                        else
                            _1 = null;
                        string _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("encoding"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<string>.Unique,__keyword__2);
                        else
                            _2 = null;
                        return Box.Apply(Traffy.Objects.TrByteArray.__new__(_0,buffer : _1,encoding : _2));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        string _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("encoding"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<string>.Unique,__keyword__2);
                        else
                            _2 = null;
                        return Box.Apply(Traffy.Objects.TrByteArray.__new__(_0,_1,encoding : _2));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<string>.Unique,__args[2]);
                        return Box.Apply(Traffy.Objects.TrByteArray.__new__(_0,_1,_2));
                    }
                    default:
                        throw new ValueError("__new__() requires 1 to 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["__new__"] = TrStaticMethod.Bind(CLASS.Name + "." + "__new__", __bind___new__);
            static  Traffy.Objects.TrObject __bind_index(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        System.Int32 _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("start"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__2);
                        else
                            _2 = 0;
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = -1;
                        bool _4;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("noraise"),out var __keyword__4)))
                            _4 = Unbox.Apply(THint<bool>.Unique,__keyword__4);
                        else
                            _4 = false;
                        return Box.Apply(_0.index(_1,start : _2,end : _3,noraise : _4));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = -1;
                        bool _4;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("noraise"),out var __keyword__4)))
                            _4 = Unbox.Apply(THint<bool>.Unique,__keyword__4);
                        else
                            _4 = false;
                        return Box.Apply(_0.index(_1,_2,end : _3,noraise : _4));
                    }
                    case 4:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        var _3 = Unbox.Apply(THint<System.Int32>.Unique,__args[3]);
                        bool _4;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("noraise"),out var __keyword__4)))
                            _4 = Unbox.Apply(THint<bool>.Unique,__keyword__4);
                        else
                            _4 = false;
                        return Box.Apply(_0.index(_1,_2,_3,noraise : _4));
                    }
                    default:
                        throw new ValueError("index() requires 2 to 4 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["index"] = TrSharpFunc.FromFunc("index", __bind_index);
            static  Traffy.Objects.TrObject __bind_count(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        System.Int32 _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("start"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__2);
                        else
                            _2 = 0;
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = -1;
                        return Box.Apply(_0.count(_1,start : _2,end : _3));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = -1;
                        return Box.Apply(_0.count(_1,_2,end : _3));
                    }
                    case 4:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        var _3 = Unbox.Apply(THint<System.Int32>.Unique,__args[3]);
                        return Box.Apply(_0.count(_1,_2,_3));
                    }
                    default:
                        throw new ValueError("count() requires 2 to 4 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["count"] = TrSharpFunc.FromFunc("count", __bind_count);
            static  Traffy.Objects.TrObject __bind_append(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        _0.append(_1);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("append() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["append"] = TrSharpFunc.FromFunc("append", __bind_append);
            static  Traffy.Objects.TrObject __bind_extend(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        _0.extend(_1);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("extend() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["extend"] = TrSharpFunc.FromFunc("extend", __bind_extend);
            static  Traffy.Objects.TrObject __bind_insert(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[2]);
                        _0.insert(_1,_2);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("insert() requires 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["insert"] = TrSharpFunc.FromFunc("insert", __bind_insert);
            static  Traffy.Objects.TrObject __bind_remove(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        _0.remove(_1);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("remove() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["remove"] = TrSharpFunc.FromFunc("remove", __bind_remove);
            static  Traffy.Objects.TrObject __bind_pop(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        Traffy.Objects.TrObject _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("index"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__keyword__1);
                        else
                            _1 = null;
                        return Box.Apply(_0.pop(index : _1));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(_0.pop(_1));
                    }
                    default:
                        throw new ValueError("pop() requires 1 to 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["pop"] = TrSharpFunc.FromFunc("pop", __bind_pop);
            static  Traffy.Objects.TrObject __bind_clear(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        _0.clear();
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("clear() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["clear"] = TrSharpFunc.FromFunc("clear", __bind_clear);
            static  Traffy.Objects.TrObject __bind_reverse(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        _0.reverse();
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("reverse() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["reverse"] = TrSharpFunc.FromFunc("reverse", __bind_reverse);
            static  Traffy.Objects.TrObject __bind_upper(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        return Box.Apply(_0.upper());
                    }
                    default:
                        throw new ValueError("upper() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["upper"] = TrSharpFunc.FromFunc("upper", __bind_upper);
            static  Traffy.Objects.TrObject __bind_lower(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        return Box.Apply(_0.lower());
                    }
                    default:
                        throw new ValueError("lower() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["lower"] = TrSharpFunc.FromFunc("lower", __bind_lower);
            static  Traffy.Objects.TrObject __bind_copy(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        return Box.Apply(_0.copy());
                    }
                    default:
                        throw new ValueError("copy() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["copy"] = TrSharpFunc.FromFunc("copy", __bind_copy);
            static  Traffy.Objects.TrObject __bind_decode(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        string _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("encoding"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<string>.Unique,__keyword__1);
                        else
                            _1 = "utf-8";
                        return Box.Apply(_0.decode(encoding : _1));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        return Box.Apply(_0.decode(_1));
                    }
                    default:
                        throw new ValueError("decode() requires 1 to 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["decode"] = TrSharpFunc.FromFunc("decode", __bind_decode);
            static  Traffy.Objects.TrObject __bind_islower(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        return Box.Apply(_0.islower());
                    }
                    default:
                        throw new ValueError("islower() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["islower"] = TrSharpFunc.FromFunc("islower", __bind_islower);
            static  Traffy.Objects.TrObject __bind_isupper(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrByteArray>.Unique,__args[0]);
                        return Box.Apply(_0.isupper());
                    }
                    default:
                        throw new ValueError("isupper() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["isupper"] = TrSharpFunc.FromFunc("isupper", __bind_isupper);
        }
    }
}

