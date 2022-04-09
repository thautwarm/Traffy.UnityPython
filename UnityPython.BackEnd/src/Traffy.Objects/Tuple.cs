using System;
using System.Collections.Generic;
using System.Linq;
using InlineHelper;
using Traffy.Annotations;

namespace Traffy.Objects
{

    public static class ConverterTuple
    {
        public static TrObject ToTr(this TrObject[] arr) => RTS.tuple_construct(arr);

        public static TrTuple AsTuple(this TrObject o)
        {
            var tuple = o as TrTuple;
            if (tuple != null)
            {
                return tuple;
            }
            throw new TypeError("Expected tuple, got " + o.Class.Name);
        }
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
            CLASS.IsClassFixed = true;
            Initialization.Prelude(CLASS);
        }

        public override bool __bool__() => elts.UnList.Length != 0;
        public override IEnumerator<TrObject> __iter__()
        {
            for(int i = 0; i < elts.UnList.Length; i++)
            {
                yield return elts[i];
            }
        }

        public override bool __contains__(TrObject o)
        {
            for (int i = 0; i < elts.Count; i++)
            {
                if (elts[i].__eq__(o))
                    return true;
            }
            return false;
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
            throw new TypeError($"tuple() takes at most 1 argument ({narg} given)");
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
                Array.Copy(elts.UnList, xs, elts.Count);
                Array.Copy(otherTuple.elts.UnList, 0, xs, elts.Count, otherTuple.elts.Count);
                return MK.Tuple(xs);
            }
            throw new TypeError($"unsupported operand type(s) for +: '{Class.Name}' and '{other.Class.Name}'");
        }

        public override TrObject __mul__(TrObject a)
        {
            if (a is TrInt ai)
            {
                return MK.Tuple(IronPython.Runtime.Operations.ArrayOps.Multiply(elts.UnList, (int)ai.value));
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
            if (other is TrTuple otherTuple)
            {
                return elts.SeqNe<FArray<TrObject>, FArray<TrObject>, TrObject>(otherTuple.elts);
            }
            return false;
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
                    var i = unchecked((int)ith.value);
                    if(Traffy.Compatibility.IronPython.PythonOps.TryFixIndex(ref i, s_ContentCount))
                    {
                        return elts[i];
                    }
                    throw new IndexError($"list index out of range");
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
        public int index(TrObject x, int start = 0, [PyBind.SelfProp(nameof(s_ContentCount))] int end = 0, [PyBind.Keyword(Only = true)] bool noraise = false)
        {
            if (end == -1)
                end = elts.Count;
            for (int i = start; i < end; i++)
            {
                if (elts[i].__eq__(x))
                    return i;
            }
            if (noraise)
                return -1;
            throw new ValueError($"tuple.index(x): x not in tuple");
        }
    }

}