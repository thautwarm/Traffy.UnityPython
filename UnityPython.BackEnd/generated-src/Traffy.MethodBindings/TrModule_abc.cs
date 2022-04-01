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
            static  Traffy.Objects.TrObject __bind_abstractmethod(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Modules.TrModule_abc.abstractmethod(_0));
                    }
                    default:
                        throw new ValueError("abstractmethod() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["abstractmethod"] = TrStaticMethod.Bind(CLASS.Name + "." + "abstractmethod", __bind_abstractmethod);
            CLASS["ABC"] = Traffy.Box.Apply(Traffy.Modules.TrModule_abc.ABC);
            CLASS["ABCMeta"] = Traffy.Box.Apply(Traffy.Modules.TrModule_abc.ABCMeta);
        }
    }
}

