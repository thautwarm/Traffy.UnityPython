using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrSharpFunc
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __read___name__(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrSharpFunc)_arg).__name__);
            }
            Action<TrObject, TrObject> __write___name__ = null;
            CLASS["__name__"] = TrProperty.Create(CLASS.Name + ".__name__", __read___name__, __write___name__);
        }
    }
}

