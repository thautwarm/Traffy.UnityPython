using System;
using System.Collections;
using System.Collections.Generic;
using Traffy.Objects;

namespace Traffy.Compatibility.IronPython
{

    public static class IronPythonCompatExtras
    {
        public static TrObject CreateBytesLike(IList<byte> seq)
        {
            switch (seq)
            {
                case List<byte> bytearray:
                    return Traffy.MK.ByteArray(bytearray);
                case byte[] array:
                    return Traffy.MK.Bytes(array);
                default:
                    throw new TypeError($"Unsupported type {seq.GetType().Name} as bytearray or bytes");
            }
        }

        public static int ToIntUnchecked(this TrInt self)
        {
            return Convert.ToInt32(self.value);
            
        }
        public static bool AsInt32(this BigInteger fake_big_int, out int value)
        {
            var _int = (TrInt)fake_big_int;
            try
            {
                value = Convert.ToInt32(_int.value);
                return true;
            }
            catch (OverflowException)
            {
                value = 0;
                return false;
            }
        }
    }
    public static class PythonOps
    {
        public static Exception ValueError(string msg) => new Traffy.Objects.ValueError(msg);
        public static Exception ValueError(string msg, params object[] formatArgs) =>
            new Traffy.Objects.ValueError(String.Format(msg, formatArgs));
        public static Exception TypeError(string msg) => new Traffy.Objects.TypeError(msg);
        public static Exception TypeError(string msg, params object[] formatArgs) =>
            new Traffy.Objects.TypeError(String.Format(msg, formatArgs));

        public static Exception SystemError(string msg) => new Traffy.Objects.RuntimeError(msg);

        public static string GetPythonTypeName(this TrObject obj) => obj.Class.Name;

        public static TrList MakeEmptyList(int c)
        {
            return MK.List(RTS.barelist_create(c));
        }

        public static int FixSliceIndex(int index, int count)
        {
            if (index < 0)
            {
                index += count;
                if (index < 0)
                {
                    index = 0;
                }
            }
            else if (index > count)
            {
                index = count;
            }
            return index;
        }
    }

    public static class PythonTypeOps
    {
        public static string GetName(this TrObject obj) => PythonOps.GetPythonTypeName(obj);
    }

    public static class ContractUtils
    {
        public static void Requires(bool precondition, string msg)
        {
            if (!precondition)
            {
                throw PythonOps.ValueError(msg);
            }
        }

        public static void Requires(bool precondition, string msg, params object[] formatArgs)
        {
            if (!precondition)
            {
                throw PythonOps.ValueError(msg, formatArgs);
            }
        }

        public static void Requires(bool precondition)
        {
            if (!precondition)
            {
                throw PythonOps.ValueError("Precondition failed");
            }
        }

        public static void RequiresNotNull(object obj, string msg)
        {
            if (obj == null)
            {
                throw PythonOps.ValueError(msg);
            }
        }
    }

    public struct BigInteger
    {
        private TrInt _int;
        public BigInteger(TrInt i) => _int = i;
        public static implicit operator BigInteger(TrInt i) => new BigInteger(i);
        public static implicit operator TrInt(BigInteger i) => i._int;
    }

    public struct PythonTuple : IEnumerable<TrObject>
    {
        private TrTuple _tuple;
        public PythonTuple(TrTuple t) => _tuple = t;

        public static implicit operator PythonTuple(TrTuple t) => new PythonTuple(t);
        public static implicit operator TrTuple(PythonTuple t) => t._tuple;

        IEnumerator<TrObject> _GetEnumerator() => _tuple.elts.GetEnumerator();

        public IEnumerator<TrObject> GetEnumerator()
        {
            return _GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _GetEnumerator();
        }
    }
}