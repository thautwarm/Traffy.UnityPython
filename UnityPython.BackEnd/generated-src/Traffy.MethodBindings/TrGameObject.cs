using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
#if !NOT_UNITY
namespace Traffy.Unity2D
{
    public sealed partial class TrGameObject
    {
        public override int __hash__() => throw new TypeError($"unhashable type: {CLASS.Name.Escape()}");
        public override bool __ne__(TrObject o) => !__eq__(o);
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind___new__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrClass>.Unique,__args[0]);
                        string _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("name"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<string>.Unique,__keyword__1);
                        else
                            _1 = "";
                        return Box.Apply(Traffy.Unity2D.TrGameObject.__new__(_0,name : _1));
                    }
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrClass>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<string>.Unique,__args[1]);
                        return Box.Apply(Traffy.Unity2D.TrGameObject.__new__(_0,_1));
                    }
                    default:
                        throw new ValueError("__new__() requires 1 to 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["__new__"] = TrStaticMethod.Bind(CLASS.Name + "." + "__new__", __bind___new__);
            CLASS["requireComponents"] = TrSharpFunc.FromFunc(CLASS.Name + "." + "requireComponents", (self, args, kwargs) => ((TrGameObject) self)._RequireComponents(args, kwargs));
            static  Traffy.Objects.TrObject __bind_destory(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrGameObject>.Unique,__args[0]);
                        _0.destory();
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("destory() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["destory"] = TrSharpFunc.FromFunc("destory", __bind_destory);
            static  Traffy.Objects.TrObject __bind_on(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrGameObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Unity2D.TrEventTriggerType>.Unique,__args[1]);
                        return Box.Apply(_0.on(_1));
                    }
                    default:
                        throw new ValueError("on() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["on"] = TrSharpFunc.FromFunc("on", __bind_on);
            static  Traffy.Objects.TrObject __read_name(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrGameObject)_arg).name);
            }
            static  void __write_name(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrGameObject)_arg).name = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["name"] = TrProperty.Create(CLASS.Name + ".name", __read_name, __write_name);
            static  Traffy.Objects.TrObject __read_x(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrGameObject)_arg).x);
            }
            static  void __write_x(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrGameObject)_arg).x = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["x"] = TrProperty.Create(CLASS.Name + ".x", __read_x, __write_x);
            static  Traffy.Objects.TrObject __read_y(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrGameObject)_arg).y);
            }
            static  void __write_y(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrGameObject)_arg).y = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["y"] = TrProperty.Create(CLASS.Name + ".y", __read_y, __write_y);
            static  Traffy.Objects.TrObject __read_z(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrGameObject)_arg).z);
            }
            static  void __write_z(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrGameObject)_arg).z = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["z"] = TrProperty.Create(CLASS.Name + ".z", __read_z, __write_z);
            static  Traffy.Objects.TrObject __read_parent(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrGameObject)_arg).parent);
            }
            static  void __write_parent(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrGameObject)_arg).parent = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["parent"] = TrProperty.Create(CLASS.Name + ".parent", __read_parent, __write_parent);
        }
    }
}
#endif

