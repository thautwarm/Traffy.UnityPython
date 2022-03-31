using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using IronPython.Runtime.Operations;
using Traffy.Objects;

namespace Traffy.Compatibility.IronPython
{

    public struct PythonContext
    {
        public static PythonContext Current => s_SharedContext;
        static PythonContext s_SharedContext = new PythonContext();
        public PythonContext SharedContext => s_SharedContext;
    }

    public static class Builtin
    {
        public static string format(PythonContext _, TrObject value, string formatSpec)
        {
            return value.__str__();
        }
    }
    public static class ScriptingRuntimeHelpers
    {
        public static TrObject Int32ToObject(int i)
        {
            return MK.Int(i);
        }

        public static TrObject BooleanToObject(bool b)
        {
            return MK.Bool(b);
        }
    }
    public static class Assert
    {
        public static void NotNull(params object[] xs)
        {
#if DEBUG
            foreach (var x in xs)
                Debug.Assert(x != null);
#endif
        }
    }
    public static class IronPythonCompatExtras
    {
        public static bool IsNone(TrObject self) => self.IsNone();
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

        public static string CharToString(char c)
        {
            return c.ToString(CultureInfo.InvariantCulture);
        }

        public static int ToIntChecked(this TrInt self)
        {
            return Convert.ToInt32(self.value);
        }

        public static int ToIntUnchecked(this TrObject self)
        {
            return unchecked((int)((TrInt)self).value);
        }

        public static long ToLongUnchecked(this TrObject self)
        {
            return unchecked(((TrInt)self).value);
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
        public static bool AsInt32(this TrInt _int, out int value)
        {
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
        public static TrObject GetBoundAttr(PythonContext _, TrObject self, string name)
        {
            return RTS.object_getattr(self, MK.Str(name));
        }

        public static TrObject GetIndex(PythonContext _, TrObject self, TrObject index)
        {
            return RTS.object_getitem(self, index);
        }

        public static Exception ValueError(string msg) => new Traffy.Objects.ValueError(msg);
        public static Exception ValueError(string msg, params object[] formatArgs) =>
            new Traffy.Objects.ValueError(String.Format(msg, formatArgs));
        public static Exception TypeError(string msg) => new Traffy.Objects.TypeError(msg);
        public static Exception TypeError(string msg, params object[] formatArgs) =>
            new Traffy.Objects.TypeError(String.Format(msg, formatArgs));

        public static Exception SystemError(string msg) => new Traffy.Objects.RuntimeError(msg);

        private static Exception IndexError(string msg, params object[] formatArgs)
        {
            return new IndexError(String.Format(msg, formatArgs));
        }

        // TODO: replace with OverflowError when it is implemented
        internal static Exception OverflowError(string v)
        {
            return new RuntimeError("OverflowError: " + v);
        }

        internal static Exception TypeErrorForTypeMismatch(string v, string s)
        {
            return new TypeError("expected " + v + ", got " + s);
        }
        public static string GetPythonTypeName(this TrObject obj) => obj.Class.Name;

        public static TrList MakeEmptyList(int c)
        {
            return MK.List(RTS.barelist_create(c));
        }



        /* ================================================================================================================================================================================================
        Modifications Copyright 3/30/2022 thautwarm(Taine Zhao)
            adopted for UnityPython use;
        See: https://github.com/IronLanguages/ironpython2/blob/aa0526d4b786ad1fabbedbf91459903b8dc01ba3/Src/IronPython/Runtime/Operations/PythonOps.cs#L1307
        Licensed to the .NET Foundation under one or more agreements.
        The .NET Foundation licenses this file to you under the Apache 2.0 License.
        See the LICENSE file in the project root for more information.
        */

        public static (int start, int stop, int step) FixSlice(int length, TrSlice slice)
        {
            return FixSlice(length, slice.start, slice.stop, slice.step);
        }
        public static (int start, int stop, int step) FixSlice(
            int length, TrObject start, TrObject stop, TrObject step
        )
        {
            int ostart, ostop, ostep;
            if (step.IsNone())
            {
                ostep = 1;
            }
            else
            {
                ostep = IronPythonCompatExtras.ToIntUnchecked(step);
                if (ostep == 0)
                {
                    throw PythonOps.ValueError("step cannot be zero");
                }
            }

            if (start.IsNone())
            {
                ostart = ostep > 0 ? 0 : length - 1;
            }
            else
            {
                ostart = IronPythonCompatExtras.ToIntUnchecked(start);
                if (ostart < 0)
                {
                    ostart += length;
                    if (ostart < 0)
                    {
                        ostart = ostep > 0 ? Math.Min(length, 0) : Math.Min(length - 1, -1);
                    }
                }
                else if (ostart >= length)
                {
                    ostart = ostep > 0 ? length : length - 1;
                }
            }

            if (stop.IsNone())
            {
                ostop = ostep > 0 ? length : -1;
            }
            else
            {
                ostop = IronPythonCompatExtras.ToIntUnchecked(stop);
                if (ostop < 0)
                {
                    ostop += length;
                    if (ostop < 0)
                    {
                        ostop = ostep > 0 ? Math.Min(length, 0) : Math.Min(length - 1, -1);
                    }
                }
                else if (ostop >= length)
                {
                    ostop = ostep > 0 ? length : length - 1;
                }
            }
            return (ostart, ostop, ostep);
        }

        internal static Exception NotImplementedError(string v)
        {
            return new NotImplementError(v);
        }

        public static int FixSliceIndex(int v, int len)
        {
            if (v < 0) v = len + v;
            if (v < 0) return 0;
            if (v > len) return len;
            return v;
        }

        public static void FixSliceIndex(ref int v, int len)
        {
            if (v < 0) v = len + v;
            if (v < 0) { v = 0; return; }
            if (v > len) { v = len; return; }
        }

        public static long FixSliceIndex(long v, long len)
        {
            if (v < 0) v = len + v;
            if (v < 0) return 0;
            if (v > len) return len;
            return v;
        }

        public static int FixIndex(int v, int len)
        {
            if (v < 0)
            {
                v += len;
                if (v < 0)
                {
                    throw PythonOps.IndexError("index out of range: {0}", v - len);
                }
            }
            else if (v >= len)
            {
                throw PythonOps.IndexError("index out of range: {0}", v);
            }
            return v;
        }

        public static bool TryFixIndex(ref int v, int len)
        {
            if (v < 0)
            {
                v += len;
                if (v < 0)
                {
                    return false;
                }
            }
            else if (v >= len)
            {
                return false;
            }
            return true;
        }


        // ================================================================================================================================================================================================
        public static TrStr BoxString(string s)
        {
            return MK.Str(s);
        }


        public static TrInt BoxInt(int s)
        {
            return MK.Int(s);
        }

        public static TrStr EmptyStr => MK.Str();


        [MethodImpl(MethodImplOptionsCompat.Best)]
        internal static void DoSliceAssign<TList1, TList2, T>(TList1 self, int start, int stop, int step, TList2 lst)
        where TList1 : IList<T>
        where TList2 : IList<T>
        {
            stop = step > 0 ? Math.Max(stop, start) : Math.Min(stop, start);
            int n = Math.Max(0, (step > 0 ? (stop - start + step - 1) : (stop - start + step + 1)) / step);
            if (lst.Count < n) throw PythonOps.ValueError("too few items in the enumerator. need {0} have {1}", n, lst.Count);
            else if (lst.Count != n) throw PythonOps.ValueError("too many items in the enumerator need {0} have {1}", n, lst.Count);
            for (int i = 0, index = start; i < n; i++, index += step)
            {
                self[index] = lst[i];
            }
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        internal static void DoSliceAssign<TList1, TList2, T>(TList1 self, TrSlice slice, TList2 lst)
        where TList1 : IList<T>
        where TList2 : IList<T>
        {
            var (ostart, ostop, ostep) = FixSlice(self.Count, slice.start, slice.stop, slice.step);
            DoSliceAssign<TList1, TList2, T>(self, ostart, ostop, ostep, lst);
        }

        public static string ToString(PythonContext _, TrObject s)
        {
            return s.__str__();
        }

        public static string Repr(PythonContext _, TrObject s)
        {
            return s.__repr__();
        }

        public static string Ascii(PythonContext _, TrObject s)
        {
            var orig = s.__str__();
            var xs = new System.Text.StringBuilder();
            foreach (var c in orig)
            {
                if (c > 255)
                {
                    xs.Append("\\u");
                    xs.Append(((int)c).ToString("X4"));
                }

                else if (c >= 32 && c < 127)
                {
                    xs.Append(c);
                }
                else
                {
                    xs.Append("\\x" + ((int)c).ToString("X2"));
                }
            }
            return xs.ToString();
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
        public TrInt Unbox;
        public BigInteger(TrInt i) => Unbox = i;
        public static implicit operator BigInteger(TrInt i) => new BigInteger(i);
        public static implicit operator TrInt(BigInteger i) => i.Unbox;
    }

    public struct PythonTuple : IEnumerable<TrObject>
    {
        public TrTuple Unbox;
        public PythonTuple(TrTuple t) => Unbox = t;

        public static implicit operator PythonTuple(TrTuple t) => new PythonTuple(t);
        public static implicit operator TrTuple(PythonTuple t) => t.Unbox;

        IEnumerator<TrObject> _GetEnumerator() => Unbox.elts.GetEnumerator();

        public IEnumerator<TrObject> GetEnumerator()
        {
            return _GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _GetEnumerator();
        }

        public static PythonTuple MakeTuple(params TrObject[] xs)
        {
            return MK.NTuple(xs);
        }
    }

    public static class ArrayUtils
    {
        public static class ArrayUtilsFactory<T>
        {
            internal static T[] Empty = new T[0];
        }
        public static T[] EmptyObjects<T>() => ArrayUtilsFactory<T>.Empty;
    }
}