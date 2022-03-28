using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Modules
{
    public sealed partial class TrModule_types
    {
        internal static void generated_BindMethods()
        {
            CLASS["BuiltinFunctionType"] = Traffy.Box.Apply(Traffy.Modules.TrModule_types.BuiltinFunctionType);
            CLASS["FunctionType"] = Traffy.Box.Apply(Traffy.Modules.TrModule_types.FunctionType);
            CLASS["ModuleType"] = Traffy.Box.Apply(Traffy.Modules.TrModule_types.ModuleType);
        }
    }
}

