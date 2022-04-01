using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
namespace Traffy.Modules
{
    public sealed partial class TrModule_enum
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind_auto(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 0:
                        return Box.Apply(Traffy.Modules.TrModule_enum.auto());
                    default:
                        throw new ValueError("auto() requires 0 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["auto"] = TrStaticMethod.Bind(CLASS.Name + "." + "auto", __bind_auto);
            CLASS["Enum"] = Traffy.Box.Apply(Traffy.Modules.TrModule_enum.Enum);
        }
    }
}

