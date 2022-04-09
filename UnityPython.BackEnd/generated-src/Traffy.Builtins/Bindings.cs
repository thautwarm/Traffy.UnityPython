using Traffy;
using Traffy.Objects;
using System;
using System.Collections.Generic;
namespace Traffy
{
    public static partial class Builtins
    {
        internal static void InitBuiltins()
        {
            Initialization.Prelude(TrSharpFunc.FromFunc("map", map));
            Initialization.Prelude(TrSharpFunc.FromFunc("filter", filter));
            Initialization.Prelude(TrSharpFunc.FromFunc("range", range));
            Initialization.Prelude(TrSharpFunc.FromFunc("all", all));
            Initialization.Prelude(TrSharpFunc.FromFunc("any", any));
            Initialization.Prelude(TrSharpFunc.FromFunc("zip", zip));
            Initialization.Prelude(TrSharpFunc.FromFunc("enumerate", enumerate));
            static  Traffy.Objects.TrObject __bind_reversed(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Builtins.reversed(_0));
                    }
                    default:
                        throw new ValueError("reversed() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("reversed", __bind_reversed));
            static  Traffy.Objects.TrObject __bind_sorted(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        Traffy.Objects.TrObject _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("key"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__keyword__1);
                        else
                            _1 = null;
                        bool _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("reverse"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<bool>.Unique,__keyword__2);
                        else
                            _2 = false;
                        return Box.Apply(Traffy.Builtins.sorted(_0,key : _1,reverse : _2));
                    }
                    default:
                        throw new ValueError("sorted() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("sorted", __bind_sorted));
            static  Traffy.Objects.TrObject __bind_sum(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        Traffy.Objects.TrObject _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("start"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__keyword__1);
                        else
                            _1 = null;
                        return Box.Apply(Traffy.Builtins.sum(_0,start : _1));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(Traffy.Builtins.sum(_0,_1));
                    }
                    default:
                        throw new ValueError("sum() requires 1 to 2 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("sum", __bind_sum));
            Initialization.Prelude(TrSharpFunc.FromFunc("print", print));
            static  Traffy.Objects.TrObject __bind_stacktrace(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Builtins.stacktrace(_0));
                    }
                    default:
                        throw new ValueError("stacktrace() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("stacktrace", __bind_stacktrace));
            static  Traffy.Objects.TrObject __bind_abs(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Builtins.abs(_0));
                    }
                    default:
                        throw new ValueError("abs() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("abs", __bind_abs));
            static  Traffy.Objects.TrObject __bind_bin(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Builtins.bin(_0));
                    }
                    default:
                        throw new ValueError("bin() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("bin", __bind_bin));
            static  Traffy.Objects.TrObject __bind_chr(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Builtins.chr(_0));
                    }
                    default:
                        throw new ValueError("chr() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("chr", __bind_chr));
            static  Traffy.Objects.TrObject __bind_ord(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Builtins.ord(_0));
                    }
                    default:
                        throw new ValueError("ord() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("ord", __bind_ord));
            static  Traffy.Objects.TrObject __bind_oct(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Builtins.oct(_0));
                    }
                    default:
                        throw new ValueError("oct() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("oct", __bind_oct));
            static  Traffy.Objects.TrObject __bind_hex(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Builtins.hex(_0));
                    }
                    default:
                        throw new ValueError("hex() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("hex", __bind_hex));
            static  Traffy.Objects.TrObject __bind_hasattr(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(Traffy.Builtins.hasattr(_0,_1));
                    }
                    default:
                        throw new ValueError("hasattr() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("hasattr", __bind_hasattr));
            Initialization.Prelude(TrSharpFunc.FromFunc("getattr", getattr));
            static  Traffy.Objects.TrObject __bind_setattr(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[2]);
                        return Box.Apply(Traffy.Builtins.setattr(_0,_1,_2));
                    }
                    default:
                        throw new ValueError("setattr() requires 3 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("setattr", __bind_setattr));
            static  Traffy.Objects.TrObject __bind_len(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Builtins.len(_0));
                    }
                    default:
                        throw new ValueError("len() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("len", __bind_len));
            static  Traffy.Objects.TrObject __bind_hash(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Builtins.hash(_0));
                    }
                    default:
                        throw new ValueError("hash() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("hash", __bind_hash));
            static  Traffy.Objects.TrObject __bind_pow(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(Traffy.Builtins.pow(_0,_1));
                    }
                    default:
                        throw new ValueError("pow() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("pow", __bind_pow));
            static  Traffy.Objects.TrObject __bind_repr(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Builtins.repr(_0));
                    }
                    default:
                        throw new ValueError("repr() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("repr", __bind_repr));
            static  Traffy.Objects.TrObject __bind_round(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        Traffy.Objects.TrObject _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("n"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__keyword__1);
                        else
                            _1 = null;
                        return Box.Apply(Traffy.Builtins.round(_0,n : _1));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(Traffy.Builtins.round(_0,_1));
                    }
                    default:
                        throw new ValueError("round() requires 1 to 2 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("round", __bind_round));
            static  Traffy.Objects.TrObject __bind_isinstance(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(Traffy.Builtins.isinstance(_0,_1));
                    }
                    default:
                        throw new ValueError("isinstance() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("isinstance", __bind_isinstance));
            static  Traffy.Objects.TrObject __bind_issubclass(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(Traffy.Builtins.issubclass(_0,_1));
                    }
                    default:
                        throw new ValueError("issubclass() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("issubclass", __bind_issubclass));
            static  Traffy.Objects.TrObject __bind_next(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        Traffy.Objects.TrObject _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("__default"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__keyword__1);
                        else
                            _1 = null;
                        return Box.Apply(Traffy.Builtins.next(_0,__default : _1));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(Traffy.Builtins.next(_0,_1));
                    }
                    default:
                        throw new ValueError("next() requires 1 to 2 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("next", __bind_next));
            static  Traffy.Objects.TrObject __bind_dir(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Builtins.dir(_0));
                    }
                    default:
                        throw new ValueError("dir() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("dir", __bind_dir));
        }

    
    }
}

