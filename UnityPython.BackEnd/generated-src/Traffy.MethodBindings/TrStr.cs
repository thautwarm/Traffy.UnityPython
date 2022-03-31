using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrStr
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind_capitalize(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.capitalize());
                    }
                    default:
                        throw new ValueError("capitalize() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["capitalize"] = TrSharpFunc.FromFunc("capitalize", __bind_capitalize);
            static  Traffy.Objects.TrObject __bind_casefold(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.casefold());
                    }
                    default:
                        throw new ValueError("casefold() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["casefold"] = TrSharpFunc.FromFunc("casefold", __bind_casefold);
            static  Traffy.Objects.TrObject __bind_center(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Int32>.Unique,__args[1]);
                        string _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("fillchar"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<string>.Unique,__keyword__2);
                        else
                            _2 = null;
                        return Box.Apply(_0.center(_1,fillchar : _2));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Int32>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<string>.Unique,__args[2]);
                        return Box.Apply(_0.center(_1,_2));
                    }
                    default:
                        throw new ValueError("center() requires 2 to 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["center"] = TrSharpFunc.FromFunc("center", __bind_center);
            static  Traffy.Objects.TrObject __bind_count(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        System.Int32 _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("start"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__2);
                        else
                            _2 = 0;
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = _0.s_ContentCount;
                        return Box.Apply(_0.count(_1,start : _2,end : _3));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = _0.s_ContentCount;
                        return Box.Apply(_0.count(_1,_2,end : _3));
                    }
                    case 4:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        var _3 = Unbox.Apply(THint<System.Int32>.Unique,__args[3]);
                        return Box.Apply(_0.count(_1,_2,_3));
                    }
                    default:
                        throw new ValueError("count() requires 2 to 4 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["count"] = TrSharpFunc.FromFunc("count", __bind_count);
            static  Traffy.Objects.TrObject __bind_encode(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        string _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("encoding"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<string>.Unique,__keyword__1);
                        else
                            _1 = "utf-8";
                        return Box.Apply(_0.encode(encoding : _1));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        return Box.Apply(_0.encode(_1));
                    }
                    default:
                        throw new ValueError("encode() requires 1 to 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["encode"] = TrSharpFunc.FromFunc("encode", __bind_encode);
            static  Traffy.Objects.TrObject __bind_endswith(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
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
                            _3 = _0.s_ContentCount;
                        return Box.Apply(_0.endswith(_1,start : _2,end : _3));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = _0.s_ContentCount;
                        return Box.Apply(_0.endswith(_1,_2,end : _3));
                    }
                    case 4:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        var _3 = Unbox.Apply(THint<System.Int32>.Unique,__args[3]);
                        return Box.Apply(_0.endswith(_1,_2,_3));
                    }
                    default:
                        throw new ValueError("endswith() requires 2 to 4 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["endswith"] = TrSharpFunc.FromFunc("endswith", __bind_endswith);
            static  Traffy.Objects.TrObject __bind_expandtabs(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        System.Int32 _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("tabsize"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__1);
                        else
                            _1 = 8;
                        return Box.Apply(_0.expandtabs(tabsize : _1));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Int32>.Unique,__args[1]);
                        return Box.Apply(_0.expandtabs(_1));
                    }
                    default:
                        throw new ValueError("expandtabs() requires 1 to 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["expandtabs"] = TrSharpFunc.FromFunc("expandtabs", __bind_expandtabs);
            static  Traffy.Objects.TrObject __bind_find(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        System.Int32 _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("start"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__2);
                        else
                            _2 = 0;
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = _0.s_ContentCount;
                        return Box.Apply(_0.find(_1,start : _2,end : _3));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = _0.s_ContentCount;
                        return Box.Apply(_0.find(_1,_2,end : _3));
                    }
                    case 4:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        var _3 = Unbox.Apply(THint<System.Int32>.Unique,__args[3]);
                        return Box.Apply(_0.find(_1,_2,_3));
                    }
                    default:
                        throw new ValueError("find() requires 2 to 4 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["find"] = TrSharpFunc.FromFunc("find", __bind_find);
            CLASS["format"] = TrStaticMethod.Bind(CLASS.Name + "." + "format", (self, args, kwargs) => ((TrStr) self).format(args, kwargs));
            static  Traffy.Objects.TrObject __bind_format_map(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Collections.Generic.Dictionary<Traffy.Objects.TrObject,Traffy.Objects.TrObject>>.Unique,__args[1]);
                        return Box.Apply(_0.format_map(_1));
                    }
                    default:
                        throw new ValueError("format_map() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["format_map"] = TrSharpFunc.FromFunc("format_map", __bind_format_map);
            static  Traffy.Objects.TrObject __bind_index(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        System.Int32 _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("start"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__2);
                        else
                            _2 = 0;
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = _0.s_ContentCount;
                        bool _4;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("noraise"),out var __keyword__4)))
                            _4 = Unbox.Apply(THint<bool>.Unique,__keyword__4);
                        else
                            _4 = false;
                        return Box.Apply(_0.index(_1,start : _2,end : _3,noraise : _4));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = _0.s_ContentCount;
                        bool _4;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("noraise"),out var __keyword__4)))
                            _4 = Unbox.Apply(THint<bool>.Unique,__keyword__4);
                        else
                            _4 = false;
                        return Box.Apply(_0.index(_1,_2,end : _3,noraise : _4));
                    }
                    case 4:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
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
            static  Traffy.Objects.TrObject __bind_isalnum(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.isalnum());
                    }
                    default:
                        throw new ValueError("isalnum() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["isalnum"] = TrSharpFunc.FromFunc("isalnum", __bind_isalnum);
            static  Traffy.Objects.TrObject __bind_isalpha(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.isalpha());
                    }
                    default:
                        throw new ValueError("isalpha() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["isalpha"] = TrSharpFunc.FromFunc("isalpha", __bind_isalpha);
            static  Traffy.Objects.TrObject __bind_isascii(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.isascii());
                    }
                    default:
                        throw new ValueError("isascii() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["isascii"] = TrSharpFunc.FromFunc("isascii", __bind_isascii);
            static  Traffy.Objects.TrObject __bind_isdecimal(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.isdecimal());
                    }
                    default:
                        throw new ValueError("isdecimal() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["isdecimal"] = TrSharpFunc.FromFunc("isdecimal", __bind_isdecimal);
            static  Traffy.Objects.TrObject __bind_isdigit(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.isdigit());
                    }
                    default:
                        throw new ValueError("isdigit() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["isdigit"] = TrSharpFunc.FromFunc("isdigit", __bind_isdigit);
            static  Traffy.Objects.TrObject __bind_isidentifier(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.isidentifier());
                    }
                    default:
                        throw new ValueError("isidentifier() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["isidentifier"] = TrSharpFunc.FromFunc("isidentifier", __bind_isidentifier);
            static  Traffy.Objects.TrObject __bind_islower(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.islower());
                    }
                    default:
                        throw new ValueError("islower() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["islower"] = TrSharpFunc.FromFunc("islower", __bind_islower);
            static  Traffy.Objects.TrObject __bind_isnumeric(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.isnumeric());
                    }
                    default:
                        throw new ValueError("isnumeric() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["isnumeric"] = TrSharpFunc.FromFunc("isnumeric", __bind_isnumeric);
            static  Traffy.Objects.TrObject __bind_isprintable(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.isprintable());
                    }
                    default:
                        throw new ValueError("isprintable() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["isprintable"] = TrSharpFunc.FromFunc("isprintable", __bind_isprintable);
            static  Traffy.Objects.TrObject __bind_isspace(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.isspace());
                    }
                    default:
                        throw new ValueError("isspace() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["isspace"] = TrSharpFunc.FromFunc("isspace", __bind_isspace);
            static  Traffy.Objects.TrObject __bind_istitle(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.istitle());
                    }
                    default:
                        throw new ValueError("istitle() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["istitle"] = TrSharpFunc.FromFunc("istitle", __bind_istitle);
            static  Traffy.Objects.TrObject __bind_isupper(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.isupper());
                    }
                    default:
                        throw new ValueError("isupper() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["isupper"] = TrSharpFunc.FromFunc("isupper", __bind_isupper);
            static  Traffy.Objects.TrObject __bind_join(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(_0.join(_1));
                    }
                    default:
                        throw new ValueError("join() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["join"] = TrSharpFunc.FromFunc("join", __bind_join);
            static  Traffy.Objects.TrObject __bind_ljust(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Int32>.Unique,__args[1]);
                        string _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("fillchar"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<string>.Unique,__keyword__2);
                        else
                            _2 = null;
                        return Box.Apply(_0.ljust(_1,fillchar : _2));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Int32>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<string>.Unique,__args[2]);
                        return Box.Apply(_0.ljust(_1,_2));
                    }
                    default:
                        throw new ValueError("ljust() requires 2 to 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["ljust"] = TrSharpFunc.FromFunc("ljust", __bind_ljust);
            static  Traffy.Objects.TrObject __bind_lower(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.lower());
                    }
                    default:
                        throw new ValueError("lower() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["lower"] = TrSharpFunc.FromFunc("lower", __bind_lower);
            static  Traffy.Objects.TrObject __bind_lstrip(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        string _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("chars"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<string>.Unique,__keyword__1);
                        else
                            _1 = null;
                        return Box.Apply(_0.lstrip(chars : _1));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        return Box.Apply(_0.lstrip(_1));
                    }
                    default:
                        throw new ValueError("lstrip() requires 1 to 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["lstrip"] = TrSharpFunc.FromFunc("lstrip", __bind_lstrip);
            static  Traffy.Objects.TrObject __bind_partition(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        return Box.Apply(_0.partition(_1));
                    }
                    default:
                        throw new ValueError("partition() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["partition"] = TrSharpFunc.FromFunc("partition", __bind_partition);
            static  Traffy.Objects.TrObject __bind_removeprefix(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        return Box.Apply(_0.removeprefix(_1));
                    }
                    default:
                        throw new ValueError("removeprefix() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["removeprefix"] = TrSharpFunc.FromFunc("removeprefix", __bind_removeprefix);
            static  Traffy.Objects.TrObject __bind_removesuffix(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        return Box.Apply(_0.removesuffix(_1));
                    }
                    default:
                        throw new ValueError("removesuffix() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["removesuffix"] = TrSharpFunc.FromFunc("removesuffix", __bind_removesuffix);
            static  Traffy.Objects.TrObject __bind_replace(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<string>.Unique,__args[2]);
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("count"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = -1;
                        return Box.Apply(_0.replace(_1,_2,count : _3));
                    }
                    case 4:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<string>.Unique,__args[2]);
                        var _3 = Unbox.Apply(THint<System.Int32>.Unique,__args[3]);
                        return Box.Apply(_0.replace(_1,_2,_3));
                    }
                    default:
                        throw new ValueError("replace() requires 3 to 4 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["replace"] = TrSharpFunc.FromFunc("replace", __bind_replace);
            static  Traffy.Objects.TrObject __bind_rfind(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        System.Int32 _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("start"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__2);
                        else
                            _2 = 0;
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = _0.s_ContentCount;
                        return Box.Apply(_0.rfind(_1,start : _2,end : _3));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = _0.s_ContentCount;
                        return Box.Apply(_0.rfind(_1,_2,end : _3));
                    }
                    case 4:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        var _3 = Unbox.Apply(THint<System.Int32>.Unique,__args[3]);
                        return Box.Apply(_0.rfind(_1,_2,_3));
                    }
                    default:
                        throw new ValueError("rfind() requires 2 to 4 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["rfind"] = TrSharpFunc.FromFunc("rfind", __bind_rfind);
            static  Traffy.Objects.TrObject __bind_rindex(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        System.Int32 _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("start"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__2);
                        else
                            _2 = 0;
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = _0.s_ContentCount;
                        return Box.Apply(_0.rindex(_1,start : _2,end : _3));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = _0.s_ContentCount;
                        return Box.Apply(_0.rindex(_1,_2,end : _3));
                    }
                    case 4:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        var _3 = Unbox.Apply(THint<System.Int32>.Unique,__args[3]);
                        return Box.Apply(_0.rindex(_1,_2,_3));
                    }
                    default:
                        throw new ValueError("rindex() requires 2 to 4 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["rindex"] = TrSharpFunc.FromFunc("rindex", __bind_rindex);
            static  Traffy.Objects.TrObject __bind_rjust(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Int32>.Unique,__args[1]);
                        string _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("fillchar"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<string>.Unique,__keyword__2);
                        else
                            _2 = null;
                        return Box.Apply(_0.rjust(_1,fillchar : _2));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Int32>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<string>.Unique,__args[2]);
                        return Box.Apply(_0.rjust(_1,_2));
                    }
                    default:
                        throw new ValueError("rjust() requires 2 to 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["rjust"] = TrSharpFunc.FromFunc("rjust", __bind_rjust);
            static  Traffy.Objects.TrObject __bind_rpartition(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        return Box.Apply(_0.rpartition(_1));
                    }
                    default:
                        throw new ValueError("rpartition() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["rpartition"] = TrSharpFunc.FromFunc("rpartition", __bind_rpartition);
            static  Traffy.Objects.TrObject __bind_rsplit(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        string _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("sep"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<string>.Unique,__keyword__1);
                        else
                            _1 = null;
                        System.Int32 _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("maxsplit"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__2);
                        else
                            _2 = -1;
                        return Box.Apply(_0.rsplit(sep : _1,maxsplit : _2));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        System.Int32 _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("maxsplit"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__2);
                        else
                            _2 = -1;
                        return Box.Apply(_0.rsplit(_1,maxsplit : _2));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        return Box.Apply(_0.rsplit(_1,_2));
                    }
                    default:
                        throw new ValueError("rsplit() requires 1 to 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["rsplit"] = TrSharpFunc.FromFunc("rsplit", __bind_rsplit);
            static  Traffy.Objects.TrObject __bind_rstrip(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        string _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("chars"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<string>.Unique,__keyword__1);
                        else
                            _1 = null;
                        return Box.Apply(_0.rstrip(chars : _1));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        return Box.Apply(_0.rstrip(_1));
                    }
                    default:
                        throw new ValueError("rstrip() requires 1 to 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["rstrip"] = TrSharpFunc.FromFunc("rstrip", __bind_rstrip);
            static  Traffy.Objects.TrObject __bind_split(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        string _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("sep"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<string>.Unique,__keyword__1);
                        else
                            _1 = null;
                        System.Int32 _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("maxsplit"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__2);
                        else
                            _2 = -1;
                        return Box.Apply(_0.split(sep : _1,maxsplit : _2));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        System.Int32 _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("maxsplit"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__2);
                        else
                            _2 = -1;
                        return Box.Apply(_0.split(_1,maxsplit : _2));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        return Box.Apply(_0.split(_1,_2));
                    }
                    default:
                        throw new ValueError("split() requires 1 to 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["split"] = TrSharpFunc.FromFunc("split", __bind_split);
            static  Traffy.Objects.TrObject __bind_splitlines(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        bool _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("keepends"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<bool>.Unique,__keyword__1);
                        else
                            _1 = false;
                        return Box.Apply(_0.splitlines(keepends : _1));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<bool>.Unique,__args[1]);
                        return Box.Apply(_0.splitlines(_1));
                    }
                    default:
                        throw new ValueError("splitlines() requires 1 to 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["splitlines"] = TrSharpFunc.FromFunc("splitlines", __bind_splitlines);
            static  Traffy.Objects.TrObject __bind_startswith(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
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
                            _3 = _0.s_ContentCount;
                        return Box.Apply(_0.startswith(_1,start : _2,end : _3));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        System.Int32 _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("end"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                        else
                            _3 = _0.s_ContentCount;
                        return Box.Apply(_0.startswith(_1,_2,end : _3));
                    }
                    case 4:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                        var _3 = Unbox.Apply(THint<System.Int32>.Unique,__args[3]);
                        return Box.Apply(_0.startswith(_1,_2,_3));
                    }
                    default:
                        throw new ValueError("startswith() requires 2 to 4 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["startswith"] = TrSharpFunc.FromFunc("startswith", __bind_startswith);
            static  Traffy.Objects.TrObject __bind_strip(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        string _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("chars"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<string>.Unique,__keyword__1);
                        else
                            _1 = null;
                        return Box.Apply(_0.strip(chars : _1));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        return Box.Apply(_0.strip(_1));
                    }
                    default:
                        throw new ValueError("strip() requires 1 to 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["strip"] = TrSharpFunc.FromFunc("strip", __bind_strip);
            static  Traffy.Objects.TrObject __bind_swapcase(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.swapcase());
                    }
                    default:
                        throw new ValueError("swapcase() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["swapcase"] = TrSharpFunc.FromFunc("swapcase", __bind_swapcase);
            static  Traffy.Objects.TrObject __bind_title(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.title());
                    }
                    default:
                        throw new ValueError("title() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["title"] = TrSharpFunc.FromFunc("title", __bind_title);
            static  Traffy.Objects.TrObject __bind_translate(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Collections.Generic.Dictionary<Traffy.Objects.TrObject,Traffy.Objects.TrObject>>.Unique,__args[1]);
                        return Box.Apply(_0.translate(_1));
                    }
                    default:
                        throw new ValueError("translate() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["translate"] = TrSharpFunc.FromFunc("translate", __bind_translate);
            static  Traffy.Objects.TrObject __bind_upper(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        return Box.Apply(_0.upper());
                    }
                    default:
                        throw new ValueError("upper() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["upper"] = TrSharpFunc.FromFunc("upper", __bind_upper);
            static  Traffy.Objects.TrObject __bind_zfill(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrStr>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Int32>.Unique,__args[1]);
                        return Box.Apply(_0.zfill(_1));
                    }
                    default:
                        throw new ValueError("zfill() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["zfill"] = TrSharpFunc.FromFunc("zfill", __bind_zfill);
        }
    }
}

