using System;
using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;
using InlineHelper;

namespace Traffy.Objects
{

    public static class ConverterTuple
    {
        public static TrObject ToTr(this TrObject[] arr) => RTS.tuple_construct(arr);

        public static TrTuple AsTuple(this TrObject tuple) => (TrTuple)tuple;
    }

    [Serializable]
    [PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Comparable), typeof(Traffy.Interfaces.Sequence))]
    public partial class TrTuple : TrObject
    {
        public FArray<TrObject> elts;
        internal int s_ContentCount => elts.Count;

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;
        public override string __repr__() =>
            elts.Count == 1 ? $"({elts[0].__repr__()},)" : "(" + String.Join(", ", elts.Select(x => x.__repr__())) + ")";

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrTuple>("tuple");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("tuple.__new__", TrTuple.datanew);
            CLASS.IsSealed = true;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public override IEnumerator<TrObject> __iter__()
        {
            return elts.AsEnumerable().GetEnumerator();
        }

        

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Tuple();
            if (narg == 2 && kwargs == null)
            {
                return MK.Tuple(RTS.object_as_array(args[1]));
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }

        public override int __hash__() => TrObject.ObjectSequenceHash<FArray<TrObject>>(
            elts,
            Initialization.HashConfig.TUPLE_HASH_SEED,
            Initialization.HashConfig.TUPLE_HASH_PRIME
        );
        public override TrObject __len__() => MK.Int(elts.Count);

        public override TrObject __add__(TrObject other)
        {
            if (other is TrTuple otherTuple)
            {
                var xs = new TrObject[elts.Count + otherTuple.elts.Count];
                for(int i = 0; i < elts.Count; i++)
                {
                    xs[i] = elts[i];
                }
                for(int i = 0; i < otherTuple.elts.Count; i++)
                {
                    xs[i + elts.Count] = otherTuple.elts[i];
                }
                return MK.Tuple(xs);
            }
            throw new TypeError($"unsupported operand type(s) for +: '{Class.Name}' and '{other.Class.Name}'");
        }

        public override TrObject __mul__(TrObject a)
        {
            if (a is TrInt ai)
            {
                var xs = new TrObject[elts.Count * ai.value];
                for (int i = 0; i < xs.Length; i++)
                {
                    xs[i] = elts[i % elts.Count];
                }
                return MK.Tuple(xs);
            }
            throw new TypeError($"unsupported operand type(s) for *: '{Class.Name}' and '{a.Class.Name}'");
        }

        public override bool __eq__(TrObject other)
        {
            if (other is TrTuple otherTuple)
            {
                return elts.SeqEq<FArray<TrObject>, FArray<TrObject>, TrObject>(otherTuple.elts);
            }
            return false;
        }

        public override bool __ne__(TrObject other)
        {
            return !__eq__(other);
        }

        public override bool __lt__(TrObject other)
        {
            if (other is TrTuple otherTuple)
            {
                return elts.SeqLt<FArray<TrObject>, FArray<TrObject>, TrObject>(otherTuple.elts);
            }
            throw new TypeError($"unsupported operand type(s) for <: '{Class.Name}' and '{other.Class.Name}'");
        }

        public override bool __le__(TrObject other)
        {
            if (other is TrTuple otherTuple)
            {
                return elts.SeqLtE<FArray<TrObject>, FArray<TrObject>, TrObject>(otherTuple.elts);
            }
            throw new TypeError($"unsupported operand type(s) for <=: '{Class.Name}' and '{other.Class.Name}'");
        }

        public override bool __gt__(TrObject other)
        {
            if (other is TrTuple otherTuple)
            {
                return elts.SeqGt<FArray<TrObject>, FArray<TrObject>, TrObject>(otherTuple.elts);
            }
            throw new TypeError($"unsupported operand type(s) for >: '{Class.Name}' and '{other.Class.Name}'");
        }

        public override bool __ge__(TrObject other)
        {
            if (other is TrTuple otherTuple)
            {
                return elts.SeqGtE<FArray<TrObject>, FArray<TrObject>, TrObject>(otherTuple.elts);
            }
            throw new TypeError($"unsupported operand type(s) for >=: '{Class.Name}' and '{other.Class.Name}'");
        }

        public override TrObject __getitem__(TrObject item)
        {
            switch (item)
            {
                case TrInt ith:
                    {
                        var i = unchecked((int) ith.value);
                        if (i < 0)
                            i += elts.Count;
                        if (i < 0 || i >= elts.Count)
                            throw new IndexError($"list index out of range");
                        return elts[i];
                    }
                case TrSlice slice:
                    {
                        return MK.Tuple(IronPython.Runtime.Operations.ArrayOps.GetSlice(elts.UnList, slice));
                    }
                default:
                    throw new TypeError($"list indices must be integers, not '{item.Class.Name}'");
            }
        }

        [PyBind]
        public long count(TrObject item)
        {
            long cnt = 0;
            for (int i = 0; i < elts.Count; i++)
            {
                if (elts[i].__eq__(item))
                    cnt++;
            }
            return cnt;
        }

        [PyBind]
        public TrObject index(TrObject x, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0)
        {
            if (end == -1)
                end = elts.Count;
            for (int i = start; i < end; i++)
            {
                if (elts[i].__eq__(x))
                    return MK.Int(i);
            }
            throw new ValueError($"tuple.index(x): x not in tuple");
        }
    }

}