using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrFunc
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __read___globals__(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrFunc)_arg).__globals__);
            }
            Action<TrObject, TrObject> __write___globals__ = null;
            CLASS["__globals__"] = TrProperty.Create(CLASS.Name + ".__globals__", __read___globals__, __write___globals__);
        }
    }
}

