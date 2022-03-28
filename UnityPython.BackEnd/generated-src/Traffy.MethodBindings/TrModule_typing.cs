using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
namespace Traffy.Modules
{
    public sealed partial class TrModule_typing
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind_runtime_checkable(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Modules.TrModule_typing.runtime_checkable(_0));
                    }
                    default:
                        throw new ValueError("runtime_checkable() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["runtime_checkable"] = TrStaticMethod.Bind(CLASS.Name + "." + "runtime_checkable", __bind_runtime_checkable);
            CLASS["TypeVar"] = TrStaticMethod.Bind(CLASS.Name + "." + "TypeVar", TypeVar);
            CLASS["AnyStr"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.AnyStr);
            CLASS["Annotated"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Annotated);
            CLASS["Generic"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Generic);
            CLASS["TypedDict"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.TypedDict);
        }
    }
}

