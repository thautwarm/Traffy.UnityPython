using System;
using System.Collections.Generic;
using Traffy.Annotations;
namespace Traffy.Objects
{
    public sealed partial class TrSlice
    {
        public override int __hash__() => throw new TypeError($"unhashable type: {CLASS.Name.Escape()}");
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind_indices(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrSlice>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<System.Int32>.Unique,__args[1]);
                        return Box.Apply(_0.indices(_1));
                    }
                    default:
                        throw new ValueError("indices() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["indices"] = TrSharpFunc.FromFunc("indices", __bind_indices);
            static  Traffy.Objects.TrObject __read_start(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrSlice)_arg).start);
            }
            Action<TrObject, TrObject> __write_start = null;
            CLASS["start"] = TrProperty.Create(CLASS.Name + ".start", __read_start, __write_start);
            static  Traffy.Objects.TrObject __read_stop(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrSlice)_arg).stop);
            }
            Action<TrObject, TrObject> __write_stop = null;
            CLASS["stop"] = TrProperty.Create(CLASS.Name + ".stop", __read_stop, __write_stop);
            static  Traffy.Objects.TrObject __read_step(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Objects.TrSlice)_arg).step);
            }
            Action<TrObject, TrObject> __write_step = null;
            CLASS["step"] = TrProperty.Create(CLASS.Name + ".step", __read_step, __write_step);
            CLASS["__hash__"] = Traffy.Box.Apply(Traffy.Objects.TrSlice.__hash);
        }
    }
}

