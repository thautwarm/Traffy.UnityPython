using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrClassMethod
    {
        [Mark(Initialization.TokenBuiltinInit)]
        static void BindMethods()
        {
            Traffy.Objects.TrObject __read___func__(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrClassMethod)_arg).__func__);
            }
            void __write___func__(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Objects.TrClassMethod)_arg).__func__ = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["__func__"] = TrProperty.Create(CLASS.Name + ".__func__", __read___func__, __write___func__);
        }
    }
}
