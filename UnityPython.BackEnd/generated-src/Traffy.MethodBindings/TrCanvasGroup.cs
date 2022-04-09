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
            CLASS["requireComponents"] = TrSharpFunc.FromFunc(CLASS.Name + "." + "requireComponents", (self, args, kwargs) => ((TrCanvasGroup) self)._RequireComponents(args, kwargs));
            static  Traffy.Objects.TrObject __bind_destory(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrCanvasGroup>.Unique,__args[0]);
                        _0.destory();
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("destory() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["destory"] = TrSharpFunc.FromFunc("destory", __bind_destory);
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
            static  Traffy.Objects.TrObject __read_gameObject(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrCanvasGroup)_arg)._baseObject);
            }
            Action<TrObject, TrObject> __write_gameObject = null;
            CLASS["gameObject"] = TrProperty.Create(CLASS.Name + ".gameObject", __read_gameObject, __write_gameObject);
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

