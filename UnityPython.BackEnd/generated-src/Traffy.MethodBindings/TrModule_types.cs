using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
namespace Traffy.Modules
{
    public sealed partial class TrModule_types
    {
        internal static void generated_BindMethods()
        {
            CLASS["BuiltinFunctionType"] = Traffy.Box.Apply(Traffy.Modules.TrModule_types.BuiltinFunctionType);
            CLASS["BuiltinMethodType"] = Traffy.Box.Apply(Traffy.Modules.TrModule_types.BuiltinMethodType);
            CLASS["FunctionType"] = Traffy.Box.Apply(Traffy.Modules.TrModule_types.FunctionType);
            CLASS["ModuleType"] = Traffy.Box.Apply(Traffy.Modules.TrModule_types.ModuleType);
            CLASS["MethodType"] = Traffy.Box.Apply(Traffy.Modules.TrModule_types.MethodType);
            CLASS["__dict__"] = Traffy.Box.Apply(Traffy.Modules.TrModule_types.__dict__);
        }
    }
}

