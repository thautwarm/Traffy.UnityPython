using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
#if !NOT_UNITY
namespace Traffy.Unity2D
{
    public sealed partial class TrCanvasGroup
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
                        return Box.Apply(Traffy.Unity2D.TrCanvasGroup.__new__(_0,_1));
                    }
                    default:
                        throw new ValueError("__new__() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["__new__"] = TrStaticMethod.Bind(CLASS.Name + "." + "__new__", __bind___new__);
            static  Traffy.Objects.TrObject __bind_on(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrCanvasGroup>.Unique,__args[0]);
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
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrCanvasGroup>.Unique,__args[0]);
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
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrCanvasGroup>.Unique,__args[0]);
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
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrCanvasGroup>.Unique,__args[0]);
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
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrCanvasGroup>.Unique,__args[0]);
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
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrCanvasGroup>.Unique,__args[0]);
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
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrCanvasGroup>.Unique,__args[0]);
                        _0.Destroy();
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("Destroy() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["Destroy"] = TrSharpFunc.FromFunc("Destroy", __bind_Destroy);
            static  Traffy.Objects.TrObject __read_blocksRaycasts(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrCanvasGroup)_arg).blocksRaycasts);
            }
            static  void __write_blocksRaycasts(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrCanvasGroup)_arg).blocksRaycasts = Unbox.Apply(THint<bool>.Unique,_value);
            }
            CLASS["blocksRaycasts"] = TrProperty.Create(CLASS.Name + ".blocksRaycasts", __read_blocksRaycasts, __write_blocksRaycasts);
            static  Traffy.Objects.TrObject __read_alpha(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrCanvasGroup)_arg).alpha);
            }
            static  void __write_alpha(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrCanvasGroup)_arg).alpha = Unbox.Apply(THint<float>.Unique,_value);
            }
            CLASS["alpha"] = TrProperty.Create(CLASS.Name + ".alpha", __read_alpha, __write_alpha);
            static  Traffy.Objects.TrObject __read_ignoreParentGroups(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrCanvasGroup)_arg).ignoreParentGroups);
            }
            static  void __write_ignoreParentGroups(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrCanvasGroup)_arg).ignoreParentGroups = Unbox.Apply(THint<bool>.Unique,_value);
            }
            CLASS["ignoreParentGroups"] = TrProperty.Create(CLASS.Name + ".ignoreParentGroups", __read_ignoreParentGroups, __write_ignoreParentGroups);
            static  Traffy.Objects.TrObject __read_interactable(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrCanvasGroup)_arg).interactable);
            }
            static  void __write_interactable(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrCanvasGroup)_arg).interactable = Unbox.Apply(THint<bool>.Unique,_value);
            }
            CLASS["interactable"] = TrProperty.Create(CLASS.Name + ".interactable", __read_interactable, __write_interactable);
            static  Traffy.Objects.TrObject __read_base(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrCanvasGroup)_arg)._baseObject);
            }
            Action<TrObject, TrObject> __write_base = null;
            CLASS["base"] = TrProperty.Create(CLASS.Name + ".base", __read_base, __write_base);
            static  Traffy.Objects.TrObject __read_x(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrCanvasGroup)_arg).x);
            }
            static  void __write_x(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrCanvasGroup)_arg).x = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["x"] = TrProperty.Create(CLASS.Name + ".x", __read_x, __write_x);
            static  Traffy.Objects.TrObject __read_y(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrCanvasGroup)_arg).y);
            }
            static  void __write_y(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrCanvasGroup)_arg).y = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["y"] = TrProperty.Create(CLASS.Name + ".y", __read_y, __write_y);
            static  Traffy.Objects.TrObject __read_z(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrCanvasGroup)_arg).z);
            }
            static  void __write_z(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrCanvasGroup)_arg).z = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["z"] = TrProperty.Create(CLASS.Name + ".z", __read_z, __write_z);
        }
    }
}
#endif

