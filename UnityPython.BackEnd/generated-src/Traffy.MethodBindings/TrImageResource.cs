using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
#if !NOT_UNITY
namespace Traffy.Unity2D
{
    public sealed partial class TrImageResource
    {
        internal static void generated_BindMethods()
        {
            CLASS["__new__"] = TrStaticMethod.Bind(CLASS.Name + "." + "__new__", __new_sprite_resource__);
            static  Traffy.Objects.TrObject __bind_load(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<string>.Unique,__args[0]);
                        return Box.Apply(Traffy.Unity2D.TrImageResource.load(_0));
                    }
                    default:
                        throw new ValueError("load() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["load"] = TrStaticMethod.Bind(CLASS.Name + "." + "load", __bind_load);
            static  Traffy.Objects.TrObject __bind_destory(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrImageResource>.Unique,__args[0]);
                        _0.destory();
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("destory() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["destory"] = TrSharpFunc.FromFunc("destory", __bind_destory);
        }
    }
}
#endif

