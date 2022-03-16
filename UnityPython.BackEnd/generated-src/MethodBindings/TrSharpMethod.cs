using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrSharpMethod
    {
        [Mark(Initialization.TokenBuiltinInit)]
        static void BindMethods()
        {
            static  Traffy.Objects.TrObject __read___func__(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrSharpMethod)_arg).__func__);
            }
            static  void __write___func__(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Objects.TrSharpMethod)_arg).__func__ = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["__func__"] = TrProperty.Create(CLASS.Name + ".__func__", __read___func__, __write___func__);
            static  Traffy.Objects.TrObject __read___self__(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrSharpMethod)_arg).__self__);
            }
            static  void __write___self__(Traffy.Objects.TrObject _arg,Traffy.Objects.TrObject _value)
            {
                ((Traffy.Objects.TrSharpMethod)_arg).__self__ = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,_value);
            }
            CLASS["__self__"] = TrProperty.Create(CLASS.Name + ".__self__", __read___self__, __write___self__);
        }
    }
}
