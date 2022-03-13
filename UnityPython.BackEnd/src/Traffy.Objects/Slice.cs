using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    [Serializable]
    public class TrSlice : TrObject
    {
        public TrObject start;
        public TrObject stop;
        public TrObject step;

        public static TrClass CLASS;
        TrClass TrObject.Class => CLASS;

        List<TrObject> TrObject.__array__ => null;

        string TrObject.__repr__() => $"slice({start.__repr__()}:{stop.__repr__()}:{step.__repr__()})";

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            RTS.arg_check_positional_range(args, 1, 4);
            int narg = args.Count;
            if (narg == 1)
            {
                return new TrSlice { start = TrNone.Unique, stop = TrNone.Unique, step = TrNone.Unique };
            }
            if (narg == 2)
            {
                return new TrSlice { start = TrNone.Unique, stop = args[1], step = TrNone.Unique };
            }
            if (narg == 3)
            {
                return new TrSlice { start = args[1], stop = args[2], step = TrNone.Unique };
            }
            if (narg == 4)
            {
                return new TrSlice { start = args[1], stop = args[2], step = args[3] };
            }
            throw new TypeError($"invalid invocation of {args[0].AsClass.Name}");
        }


        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrSlice>("slice");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("slice.__new__", TrSlice.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrSlice)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrSlice))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public (int, int, int) mkslice(int count)
        {
            var start = this.start;
            var stop = this.stop;
            var step = this.step;
            int istart, istop, istep;
            if (step is TrInt istep_o)
            {
                istep = unchecked((int)istep_o.value);
                if (istep == 0)
                    throw new ValueError("slice step cannot be zero");
            }
            else
            {
                if (!step.IsNone())
                    throw new TypeError($"slice step must be an integer, not '{step.AsClass.Name}'");
                istep = 1;
            }
            if (start is TrInt istart_o)
            {
                istart = unchecked((int)istart_o.value);
                if (istart < 0)
                {
                    istart = count + istart;
                }
            }
            else
            {
                if (!start.IsNone())
                    throw new TypeError($"slice start must be an integer, not '{start.AsClass.Name}'");
                istart = istep > 0 ? 0 : count - 1;
            }

            if (stop is TrInt istop_o)
            {
                istop = unchecked((int)istop_o.value);
                if (istop < 0)
                {
                    istop = count + istop;
                }
            }
            else
            {
                if (!stop.IsNone())
                    throw new TypeError($"slice stop must be an integer, not '{stop.AsClass.Name}'");
                istop = istep > 0 ? count : -1;
            }
            istop = Math.Max(-1, Math.Min(istop, count));
            istart = Math.Max(Math.Min(istart, count - 1), 0);
            return (istart, istop, istep);
        }
    }

}