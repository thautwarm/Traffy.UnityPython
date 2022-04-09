using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
#if !NOT_UNITY
namespace Traffy.Unity2D
{
    public sealed partial class TrUI
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind_on(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrUI>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Unity2D.TrEventTriggerType>.Unique,__args[1]);
                        return Box.Apply(_0.on(_1));
                    }
                    default:
                        throw new ValueError("on() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["on"] = TrSharpFunc.FromFunc("on", __bind_on);
            CLASS["requireComponents"] = TrSharpFunc.FromFunc(CLASS.Name + "." + "requireComponents", (self, args, kwargs) => ((TrUI) self)._RequireComponents(args, kwargs));
            static  Traffy.Objects.TrObject __bind_destory(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrUI>.Unique,__args[0]);
                        _0.destory();
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("destory() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["destory"] = TrSharpFunc.FromFunc("destory", __bind_destory);
            static  Traffy.Objects.TrObject __read_width(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrUI)_arg).width);
            }
            static  void __write_width(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrUI)_arg).width = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["width"] = TrProperty.Create(CLASS.Name + ".width", __read_width, __write_width);
            static  Traffy.Objects.TrObject __read_height(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrUI)_arg).height);
            }
            static  void __write_height(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrUI)_arg).height = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["height"] = TrProperty.Create(CLASS.Name + ".height", __read_height, __write_height);
            static  Traffy.Objects.TrObject __read_enabled(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrUI)_arg).enabled);
            }
            static  void __write_enabled(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrUI)_arg).enabled = Unbox.Apply(THint<bool>.Unique,_value);
            }
            CLASS["enabled"] = TrProperty.Create(CLASS.Name + ".enabled", __read_enabled, __write_enabled);
            static  Traffy.Objects.TrObject __read_gameObject(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrUI)_arg)._baseObject);
            }
            Action<TrObject, TrObject> __write_gameObject = null;
            CLASS["gameObject"] = TrProperty.Create(CLASS.Name + ".gameObject", __read_gameObject, __write_gameObject);
            static  Traffy.Objects.TrObject __read_x(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrUI)_arg).x);
            }
            static  void __write_x(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrUI)_arg).x = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["x"] = TrProperty.Create(CLASS.Name + ".x", __read_x, __write_x);
            static  Traffy.Objects.TrObject __read_y(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrUI)_arg).y);
            }
            static  void __write_y(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrUI)_arg).y = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["y"] = TrProperty.Create(CLASS.Name + ".y", __read_y, __write_y);
            static  Traffy.Objects.TrObject __read_z(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrUI)_arg).z);
            }
            static  void __write_z(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrUI)_arg).z = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["z"] = TrProperty.Create(CLASS.Name + ".z", __read_z, __write_z);
        }
    }
}
#endif

