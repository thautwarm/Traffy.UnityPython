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
        }
    }
}

