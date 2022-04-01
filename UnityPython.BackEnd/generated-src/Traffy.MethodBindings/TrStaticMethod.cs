using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrStaticMethod
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __read___func__(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrStaticMethod)_arg).__func__);
            }
            Action<TrObject, TrObject> __write___func__ = null;
            CLASS["__func__"] = TrProperty.Create(CLASS.Name + ".__func__", __read___func__, __write___func__);
        }
    }
}

