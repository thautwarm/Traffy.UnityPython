using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
#if !NOT_UNITY
namespace Traffy.Unity2D
{
    public sealed partial class TrComponentGroup
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind_add(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrComponentGroup>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        Traffy.Objects.TrObject _2;
                        if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("parameter"),out var __keyword__2)))
                            _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__keyword__2);
                        else
                            _2 = null;
                        return Box.Apply(_0.add(_1,parameter : _2));
                    }
                    case 3:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Unity2D.TrComponentGroup>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        var _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[2]);
                        return Box.Apply(_0.add(_1,_2));
                    }
                    default:
                        throw new ValueError("add() requires 2 to 3 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["add"] = TrSharpFunc.FromFunc("add", __bind_add);
            static  Traffy.Objects.TrObject __read_peek(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrComponentGroup)_arg).peek);
            }
            Action<TrObject, TrObject> __write_peek = null;
            CLASS["peek"] = TrProperty.Create(CLASS.Name + ".peek", __read_peek, __write_peek);
        }
    }
}
#endif

