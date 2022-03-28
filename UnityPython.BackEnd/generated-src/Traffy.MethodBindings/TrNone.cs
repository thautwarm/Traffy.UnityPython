using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrNone
    {
        public override bool __ne__(TrObject o) => !__eq__(o);
        internal static void generated_BindMethods()
        {
        }
    }
}

