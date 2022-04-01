using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
namespace Traffy.Modules
{
    public sealed partial class TrModule_json
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind_loads(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<string>.Unique,__args[0]);
                        return Box.Apply(Traffy.Modules.TrModule_json.loads(_0));
                    }
                    default:
                        throw new ValueError("loads() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["loads"] = TrStaticMethod.Bind(CLASS.Name + "." + "loads", __bind_loads);
            static  Traffy.Objects.TrObject __bind_dumps(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        System.Int32 _1;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("indent"),out var __keyword__1)))
                            _1 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__1);
                        else
                            _1 = 0;
                        return Box.Apply(Traffy.Modules.TrModule_json.dumps(_0,indent : _1));
                    }
                    default:
                        throw new ValueError("dumps() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["dumps"] = TrStaticMethod.Bind(CLASS.Name + "." + "dumps", __bind_dumps);
            CLASS["JSON"] = Traffy.Box.Apply(Traffy.Modules.TrModule_json.JSON);
        }
    }
}

