using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
#if !NOT_UNITY
namespace Traffy.Unity2D
{
    public sealed partial class TrPolygonCollider2D
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind___new__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrClass>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Unity2D.TrGameObject>.Unique,__args[1]);
                        return Box.Apply(Traffy.Unity2D.TrPolygonCollider2D.__new__(_0,_1));
                    }
                    default:
                        throw new ValueError("__new__() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["__new__"] = TrStaticMethod.Bind(CLASS.Name + "." + "__new__", __bind___new__);
            static  Traffy.Objects.TrObject __bind_ResetShape(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrPolygonCollider2D>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Unity2D.TrSprite>.Unique,__args[1]);
                        float _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("tolerance"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<float>.Unique,__keyword__2);
                        else
                            _2 = 0.05f;
                        _0.ResetShape(_1,tolerance : _2);
                        return Traffy.MK.None();
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrPolygonCollider2D>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Unity2D.TrSprite>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<float>.Unique,__args[2]);
                        _0.ResetShape(_1,_2);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("ResetShape() requires 2 to 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["ResetShape"] = TrSharpFunc.FromFunc("ResetShape", __bind_ResetShape);
            static  Traffy.Objects.TrObject __bind_on(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrPolygonCollider2D>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Unity2D.TrEventTriggerType>.Unique,__args[1]);
                        return Box.Apply(_0.on(_1));
                    }
                    default:
                        throw new ValueError("on() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["on"] = TrSharpFunc.FromFunc("on", __bind_on);
            CLASS["requireComponents"] = TrSharpFunc.FromFunc(CLASS.Name + "." + "requireComponents", (self, args, kwargs) => ((TrPolygonCollider2D) self)._RequireComponents(args, kwargs));
            static  Traffy.Objects.TrObject __bind_destory(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrPolygonCollider2D>.Unique,__args[0]);
                        _0.destory();
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("destory() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["destory"] = TrSharpFunc.FromFunc("destory", __bind_destory);
            static  Traffy.Objects.TrObject __read_gameObject(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrPolygonCollider2D)_arg)._baseObject);
            }
            Action<TrObject, TrObject> __write_gameObject = null;
            CLASS["gameObject"] = TrProperty.Create(CLASS.Name + ".gameObject", __read_gameObject, __write_gameObject);
            static  Traffy.Objects.TrObject __read_x(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrPolygonCollider2D)_arg).x);
            }
            static  void __write_x(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrPolygonCollider2D)_arg).x = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["x"] = TrProperty.Create(CLASS.Name + ".x", __read_x, __write_x);
            static  Traffy.Objects.TrObject __read_y(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrPolygonCollider2D)_arg).y);
            }
            static  void __write_y(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrPolygonCollider2D)_arg).y = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["y"] = TrProperty.Create(CLASS.Name + ".y", __read_y, __write_y);
            static  Traffy.Objects.TrObject __read_z(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrPolygonCollider2D)_arg).z);
            }
            static  void __write_z(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrPolygonCollider2D)_arg).z = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["z"] = TrProperty.Create(CLASS.Name + ".z", __read_z, __write_z);
        }
    }
}
#endif

