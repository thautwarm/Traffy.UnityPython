using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrEnum
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind___init_subclass__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        Traffy.Objects.TrEnum.__init_subclass__(_0,_1);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("__init_subclass__() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["__init_subclass__"] = TrStaticMethod.Bind(CLASS.Name + "." + "__init_subclass__", __bind___init_subclass__);
            static  Traffy.Objects.TrObject __read_name(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrEnum)_arg).name);
            }
            Action<TrObject, TrObject> __write_name = null;
            CLASS["name"] = TrProperty.Create(CLASS.Name + ".name", __read_name, __write_name);
            static  Traffy.Objects.TrObject __read_value(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrEnum)_arg).value);
            }
            Action<TrObject, TrObject> __write_value = null;
            CLASS["value"] = TrProperty.Create(CLASS.Name + ".value", __read_value, __write_value);
        }
    }
}

