using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
namespace Traffy.Modules
{
    public sealed partial class TrModule_abc
    {
        internal static void generated_BindMethods()
        {
            CLASS["Awaitable"] = Traffy.Box.Apply(Traffy.Modules.TrModule_abc.Awaitable);
            CLASS["Callable"] = Traffy.Box.Apply(Traffy.Modules.TrModule_abc.Callable);
            CLASS["Collection"] = Traffy.Box.Apply(Traffy.Modules.TrModule_abc.Collection);
            CLASS["Comparable"] = Traffy.Box.Apply(Traffy.Modules.TrModule_abc.Comparable);
            CLASS["Container"] = Traffy.Box.Apply(Traffy.Modules.TrModule_abc.Container);
            CLASS["Hashable"] = Traffy.Box.Apply(Traffy.Modules.TrModule_abc.Hashable);
            CLASS["Iterable"] = Traffy.Box.Apply(Traffy.Modules.TrModule_abc.Iterable);
            CLASS["Reversible"] = Traffy.Box.Apply(Traffy.Modules.TrModule_abc.Reversible);
            CLASS["Sequence"] = Traffy.Box.Apply(Traffy.Modules.TrModule_abc.Sequence);
            CLASS["Sized"] = Traffy.Box.Apply(Traffy.Modules.TrModule_abc.Sized);
        }
    }
}

