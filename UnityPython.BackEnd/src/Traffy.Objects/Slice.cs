using System;
using System.Collections.Generic;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [Serializable]
    [PyBuiltin]
    public sealed partial class TrSlice : TrObject
    {
        public TrObject _start;
        public TrObject _stop;
        public TrObject _step;

        [PyBind]
        public TrObject start => _start;

        [PyBind]
        public TrObject stop => _stop;

        [PyBind]
        public TrObject step => _step;

        

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;

        public override string __repr__() => $"slice({_start.__repr__()}:{_stop.__repr__()}:{_step.__repr__()})";

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            RTS.arg_check_positional_range(args, 1, 4);
            int narg = args.Count;
            if (narg == 1)
            {
                return new TrSlice { _start = TrNone.Unique, _stop = TrNone.Unique, _step = TrNone.Unique };
            }
            if (narg == 2)
            {
                return new TrSlice { _start = TrNone.Unique, _stop = args[1], _step = TrNone.Unique };
            }
            if (narg == 3)
            {
                return new TrSlice { _start = args[1], _stop = args[2], _step = TrNone.Unique };
            }
            if (narg == 4)
            {
                return new TrSlice { _start = args[1], _stop = args[2], _step = args[3] };
            }
            throw new TypeError($"invalid invocation of {args[0].AsClass.Name}");
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrSlice>("slice");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("slice.__new__", TrSlice.datanew);
            CLASS.IsSealed = true;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }
        public override bool __eq__(TrObject other)
        {
            if (other is TrSlice s)
            {
                return _start.__eq__(s._start) && _stop.__eq__(s._stop) && _step.__eq__(s._step);
            }
            return false;
        }

        [PyBind(Name = "__hash__")]
        public static TrObject __hash => RTS.object_none;

        [PyBind]
        public TrObject indices(int len)
        {
            var (istart, istop, iend) = _indices(len);
            return MK.NTuple(MK.Int(istart), MK.Int(istop), MK.Int(iend));
        }

        public (int start, int stop, int step) _indices(int count)
        {
            return Traffy.Compatibility.IronPython.PythonOps.FixSlice(count, start, stop, step);
        }
        public (int start, int step, int nstep) resolveSlice(int count)
        {
            var (istart, istop, istep) = _indices(count);
            int nstep = (istop - istart) / istep;
            return (istart, istep, nstep);
        }
    }

}