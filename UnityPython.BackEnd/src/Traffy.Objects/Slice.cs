using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            switch (narg)
            {
                case 1:
                    return new TrSlice { _start = TrNone.Unique, _stop = TrNone.Unique, _step = TrNone.Unique };
                case 2:
                    return new TrSlice { _start = TrNone.Unique, _stop = args[1], _step = TrNone.Unique };
                case 3:
                    return new TrSlice { _start = args[1], _stop = args[2], _step = TrNone.Unique };
                case 4:
                    return new TrSlice { _start = args[1], _stop = args[2], _step = args[3] };
                default:
                    throw new TypeError($"slice() takes 0 to 3 positional arguments but {narg - 1} were given");
            }
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
            CLASS.IsClassFixed = true;
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

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public (int start, int stop, int step) _indices(int count)
        {
            return Traffy.Compatibility.IronPython.PythonOps.FixSlice(count, start, stop, step);
        }
    }

}