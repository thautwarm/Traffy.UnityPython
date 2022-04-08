using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
#if !NOT_UNITY
namespace Traffy.Unity2D
{
    public sealed partial class TrColor
    {
        public override bool __ne__(TrObject o) => !__eq__(o);
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind___new__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 5:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrClass>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<float>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<float>.Unique,__args[2]);
                        var _3 = Unbox.Apply(THint<float>.Unique,__args[3]);
                        var _4 = Unbox.Apply(THint<float>.Unique,__args[4]);
                        return Box.Apply(Traffy.Unity2D.TrColor.__new__(_0,_1,_2,_3,_4));
                    }
                    default:
                        throw new ValueError("__new__() requires 5 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["__new__"] = TrStaticMethod.Bind(CLASS.Name + "." + "__new__", __bind___new__);
            static  Traffy.Objects.TrObject __read_r(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrColor)_arg).r);
            }
            static  void __write_r(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrColor)_arg).r = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["r"] = TrProperty.Create(CLASS.Name + ".r", __read_r, __write_r);
            static  Traffy.Objects.TrObject __read_g(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrColor)_arg).g);
            }
            static  void __write_g(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrColor)_arg).g = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["g"] = TrProperty.Create(CLASS.Name + ".g", __read_g, __write_g);
            static  Traffy.Objects.TrObject __read_b(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrColor)_arg).b);
            }
            static  void __write_b(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrColor)_arg).b = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["b"] = TrProperty.Create(CLASS.Name + ".b", __read_b, __write_b);
            static  Traffy.Objects.TrObject __read_a(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrColor)_arg).a);
            }
            static  void __write_a(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Unity2D.TrColor)_arg).a = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["a"] = TrProperty.Create(CLASS.Name + ".a", __read_a, __write_a);
        }
    }
}
#endif

