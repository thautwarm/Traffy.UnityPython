using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
#if !NOT_UNITY
namespace Traffy.Unity2D
{
    public sealed partial class TrTraffyBehaviour
    {
        internal static void generated_BindMethods()
        {
            CLASS["datanew"] = TrStaticMethod.Bind(CLASS.Name + "." + "datanew", datanew);
            static  Traffy.Objects.TrObject __bind___init_subclass__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        Traffy.Unity2D.TrTraffyBehaviour.__init_subclass__(_0,_1);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("__init_subclass__() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["__init_subclass__"] = TrStaticMethod.Bind(CLASS.Name + "." + "__init_subclass__", __bind___init_subclass__);
        }
    }
}
#endif

