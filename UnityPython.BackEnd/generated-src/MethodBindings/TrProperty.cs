using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrProperty
    {
        [Mark(Initialization.TokenBuiltinInit)]
        static void BindMethods()
        {
            Traffy.Objects.TrObject __read_getter(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrProperty)_arg).getter) ;
            }
            Action<TrObject, TrObject> __write_getter = null;
            CLASS["getter"] = TrProperty.Create(CLASS.Name + ".getter", __read_getter, __write_getter);
            Traffy.Objects.TrObject __read_setter(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrProperty)_arg).setter) ;
            }
            Action<TrObject, TrObject> __write_setter = null;
            CLASS["setter"] = TrProperty.Create(CLASS.Name + ".setter", __read_setter, __write_setter);
        }
    }
}
