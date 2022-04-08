using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
#if !NOT_UNITY
namespace Traffy.Unity2D
{
    public sealed partial class TrScrollRect
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
                        return Box.Apply(Traffy.Unity2D.TrScrollRect.__new__(_0,_1));
                    }
                    default:
                        throw new ValueError("__new__() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["__new__"] = TrStaticMethod.Bind(CLASS.Name + "." + "__new__", __bind___new__);
            static  Traffy.Objects.TrObject __bind_onValueChanged(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrScrollRect>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        _0.onValueChanged(_1);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("onValueChanged() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["onValueChanged"] = TrSharpFunc.FromFunc("onValueChanged", __bind_onValueChanged);
            static  Traffy.Objects.TrObject __bind_on(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrScrollRect>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Unity2D.TrEventTriggerType>.Unique,__args[1]);
                        return Box.Apply(_0.on(_1));
                    }
                    default:
                        throw new ValueError("on() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["on"] = TrSharpFunc.FromFunc("on", __bind_on);
            static  Traffy.Objects.TrObject __bind_AddComponent(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrScrollRect>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[2]);
                        Traffy.Objects.TrObject _3;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("parameter"),out var __keyword__3)))
                            _3 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__keyword__3);
                        else
                            _3 = null;
                        return Box.Apply(_0._AddComponent(_1,_2,parameter : _3));
                    }
                    case 4:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrScrollRect>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[2]);
                        var _3 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[3]);
                        return Box.Apply(_0._AddComponent(_1,_2,_3));
                    }
                    default:
                        throw new ValueError("AddComponent() requires 3 to 4 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["AddComponent"] = TrSharpFunc.FromFunc("AddComponent", __bind_AddComponent);
            static  Traffy.Objects.TrObject __bind_TryGetComponent(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrScrollRect>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrRef>.Unique,__args[2]);
                        return Box.Apply(_0._TryGetComponent(_1,_2));
                    }
                    default:
                        throw new ValueError("TryGetComponent() requires 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["TryGetComponent"] = TrSharpFunc.FromFunc("TryGetComponent", __bind_TryGetComponent);
            static  Traffy.Objects.TrObject __bind_TryGetComponents(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrScrollRect>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrRef>.Unique,__args[2]);
                        return Box.Apply(_0._TryGetComponents(_1,_2));
                    }
                    default:
                        throw new ValueError("TryGetComponents() requires 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["TryGetComponents"] = TrSharpFunc.FromFunc("TryGetComponents", __bind_TryGetComponents);
            static  Traffy.Objects.TrObject __bind_GetComponent(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrScrollRect>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(_0._GetComponent(_1));
                    }
                    default:
                        throw new ValueError("GetComponent() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["GetComponent"] = TrSharpFunc.FromFunc("GetComponent", __bind_GetComponent);
            static  Traffy.Objects.TrObject __bind_Destroy(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrScrollRect>.Unique,__args[0]);
                        _0.Destroy();
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("Destroy() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["Destroy"] = TrSharpFunc.FromFunc("Destroy", __bind_Destroy);
            static  Traffy.Objects.TrObject __read_elasticity(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrScrollRect)_arg).elasticity);
            }
            static  void __write_elasticity(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrScrollRect)_arg).elasticity = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["elasticity"] = TrProperty.Create(CLASS.Name + ".elasticity", __read_elasticity, __write_elasticity);
            static  Traffy.Objects.TrObject __read_decelerationRate(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrScrollRect)_arg).decelerationRate);
            }
            static  void __write_decelerationRate(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrScrollRect)_arg).decelerationRate = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["decelerationRate"] = TrProperty.Create(CLASS.Name + ".decelerationRate", __read_decelerationRate, __write_decelerationRate);
            static  Traffy.Objects.TrObject __read_verticalScrollbarSpacing(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrScrollRect)_arg).verticalScrollbarSpacing);
            }
            static  void __write_verticalScrollbarSpacing(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrScrollRect)_arg).verticalScrollbarSpacing = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["verticalScrollbarSpacing"] = TrProperty.Create(CLASS.Name + ".verticalScrollbarSpacing", __read_verticalScrollbarSpacing, __write_verticalScrollbarSpacing);
            static  Traffy.Objects.TrObject __read_horizontalScrollbarSpacing(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrScrollRect)_arg).horizontalScrollbarSpacing);
            }
            static  void __write_horizontalScrollbarSpacing(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrScrollRect)_arg).horizontalScrollbarSpacing = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["horizontalScrollbarSpacing"] = TrProperty.Create(CLASS.Name + ".horizontalScrollbarSpacing", __read_horizontalScrollbarSpacing, __write_horizontalScrollbarSpacing);
            static  Traffy.Objects.TrObject __read_movementType(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrScrollRect)_arg).movementType);
            }
            static  void __write_movementType(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrScrollRect)_arg).movementType = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["movementType"] = TrProperty.Create(CLASS.Name + ".movementType", __read_movementType, __write_movementType);
            static  Traffy.Objects.TrObject __read_inertia(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrScrollRect)_arg).inertia);
            }
            static  void __write_inertia(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrScrollRect)_arg).inertia = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["inertia"] = TrProperty.Create(CLASS.Name + ".inertia", __read_inertia, __write_inertia);
            static  Traffy.Objects.TrObject __read_content(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrScrollRect)_arg).content);
            }
            static  void __write_content(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrScrollRect)_arg).content = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["content"] = TrProperty.Create(CLASS.Name + ".content", __read_content, __write_content);
            static  Traffy.Objects.TrObject __read_width(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrScrollRect)_arg).width);
            }
            static  void __write_width(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrScrollRect)_arg).width = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["width"] = TrProperty.Create(CLASS.Name + ".width", __read_width, __write_width);
            static  Traffy.Objects.TrObject __read_height(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrScrollRect)_arg).height);
            }
            static  void __write_height(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrScrollRect)_arg).height = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["height"] = TrProperty.Create(CLASS.Name + ".height", __read_height, __write_height);
            static  Traffy.Objects.TrObject __read_base(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrScrollRect)_arg)._baseObject);
            }
            Action<TrObject, TrObject> __write_base = null;
            CLASS["base"] = TrProperty.Create(CLASS.Name + ".base", __read_base, __write_base);
            static  Traffy.Objects.TrObject __read_x(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrScrollRect)_arg).x);
            }
            static  void __write_x(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrScrollRect)_arg).x = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["x"] = TrProperty.Create(CLASS.Name + ".x", __read_x, __write_x);
            static  Traffy.Objects.TrObject __read_y(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrScrollRect)_arg).y);
            }
            static  void __write_y(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrScrollRect)_arg).y = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["y"] = TrProperty.Create(CLASS.Name + ".y", __read_y, __write_y);
            static  Traffy.Objects.TrObject __read_z(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrScrollRect)_arg).z);
            }
            static  void __write_z(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrScrollRect)_arg).z = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["z"] = TrProperty.Create(CLASS.Name + ".z", __read_z, __write_z);
        }
    }
}
#endif

