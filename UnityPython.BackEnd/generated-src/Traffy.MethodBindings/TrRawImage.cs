using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
namespace Traffy.Unity2D
{
    public sealed partial class TrRawImage
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __read_baseobject(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrRawImage)_arg).baseobject);
            }
            Action<TrObject, TrObject> __write_baseobject = null;
            CLASS["baseobject"] = TrProperty.Create(CLASS.Name + ".baseobject", __read_baseobject, __write_baseobject);
        }
    }
}

