using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrModule
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __read___name__(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrModule)_arg).__name__);
            }
            Action<TrObject, TrObject> __write___name__ = null;
            CLASS["__name__"] = TrProperty.Create(CLASS.Name + ".__name__", __read___name__, __write___name__);
            static  Traffy.Objects.TrObject __read___dict__(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrModule)_arg).__dict__);
            }
            Action<TrObject, TrObject> __write___dict__ = null;
            CLASS["__dict__"] = TrProperty.Create(CLASS.Name + ".__dict__", __read___dict__, __write___dict__);
        }
    }
}

