using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Traffy.Objects;

namespace Traffy
{
    public static partial class SeqUtils
    {

        internal static IEnumerable<int> SimpleRange(this int i)
        {
            for (int j = 0; j < i; j++)
            {
                yield return j;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void NormalizeStartEnd(ref int start, ref int end, int count)
        {
            start = Traffy.Compatibility.IronPython.PythonOps.FixSliceIndex(start, count);
            end = Traffy.Compatibility.IronPython.PythonOps.FixSliceIndex(end, count);
        }


        internal static int IndexEltGenericSimple<TList, T>(this TList seq1, T elt, int start, int end) where T : IEquatable<T> where TList : IList<T>
        {
            NormalizeStartEnd(ref start, ref end, seq1.Count);
            for(int i = start; i < end; i++)
            {
                if (seq1[i].Equals(elt))
                {
                    return i;
                }
            }
            return -1;
        }


        internal static long CountGenericSimple<TList, T>(this TList seq, T element, int start, int end) where T : IEquatable<T> where TList : IList<T>
        {
            long cnt = 0;
            NormalizeStartEnd(ref start, ref end, seq.Count);
            for (int i = start; i < end; i++)
            {
                if (seq[i].Equals(element))
                    cnt++;
            }
            return cnt;
        }

        
        // internal static bool StartswithI<TList1, TList2, T>(this TList1 xs, TList2 content, int start) where T : IEquatable<T> where TList1 : IList<T> where TList2 : IList<T>
        // {
        //     start = NormalizeStart(start, xs.Count);

        //     var n1 = xs.Count;
        //     var n2 = content.Count;
        //     if (start + n2 > n1)
        //     {
        //         return false;
        //     }

        //     for (int i = 0, j = start; i < n2; i++, j++)
        //     {

        //         if (!xs[j].Equals(content[i]))
        //         {
        //             return false;
        //         }
        //     }
        //     return true;
        // }

        internal static int ByteSequenceHash<TList>(this TList xs, int seed, int primSeed) where TList : IList<byte>
        {
            unchecked
            {
                int hash = seed;
                for (int i = 0; i < xs.Count; i++)
                {
                    hash = hash * primSeed + xs[i];
                }
                return hash;
            }
        }


        public static void DeleteItemsSupportSlice<T>(List<T> seq, TrObject item, TrClass Class)
        {
            switch (item)
            {
                case TrInt oitem:
                {
                    var i = checked((int)oitem.value);
                    if (Traffy.Compatibility.IronPython.PythonOps.TryFixIndex(ref i, seq.Count))
                    {
                        seq.RemoveAt(i);
                        return;
                    }
                    throw new IndexError($"{Class.Name} assignment index out of range");
                }
                case TrSlice slice:
                {
                    IronPython.Runtime.Operations.ListOps.DelSlice(seq, slice);
                    return;
                }
                default:
                    throw new TypeError($"{Class.Name} indices must be integers, not '{item.Class.Name}'");
            }
        }

        // length equal
        internal static bool SeqEq<Col1, Col2, T>(this Col1 seq1, Col2 seq2) where Col1 : IList<T> where Col2 : IList<T> where T : IEquatable<T>
        {
            if (seq1.Count != seq2.Count)
                return false;
            for (int i = 0; i < seq1.Count; i++)
            {
                if (!seq1[i].Equals(seq2[i]))
                    return false;
            }
            return true;
        }

        internal static bool SeqNe<Col1, Col2, T>(this Col1 seq1, Col2 seq2) where Col1 : IList<T> where Col2 : IList<T> where T : IEquatable<T>
        {
            if (seq1.Count != seq2.Count)
                return true;
            for (int i = 0; i < seq1.Count; i++)
            {
                if (!seq1[i].Equals(seq2[i]))
                    return true;
            }
            return false;
        }


        internal static bool SeqLtE<Col1, Col2, T>(this Col1 seq1, Col2 seq2) where Col1 : IList<T> where Col2 : IList<T> where T : IComparable<T>
        {
            return SeqLtE<Col1, Col2, T>(seq1, seq2, out var _);
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        internal static bool SeqLtE<Col1, Col2, T>(this Col1 seq1, Col2 seq2, out bool seqIsEqual) where Col1 : IList<T> where Col2 : IList<T> where T : IComparable<T>
        {
            var commonLen = Math.Min(seq1.Count, seq2.Count);
            int cmp;
            for (int i = 0; i < commonLen; i++)
            {
                cmp = seq1[i].CompareTo(seq2[i]);
                if (cmp < 0)
                {
                    seqIsEqual = false;
                    return true;
                }
                else if (cmp == 0)
                {
                    continue;
                }
                else
                {
                    seqIsEqual = false;
                    return false;
                }
            }
            if (seq1.Count < seq2.Count)
            {
                seqIsEqual = false;
                return true;
            }
            else if (seq1.Count == seq2.Count)
            {
                seqIsEqual = true;
                return true;
            }
            seqIsEqual = false;
            return false;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        internal static bool SeqLt<Col1, Col2, T>(this Col1 seq1, Col2 seq2) where Col1 : IList<T> where Col2 : IList<T> where T : IComparable<T>
        {
            return seq1.SeqLtE<Col1, Col2, T>(seq2, out var isEqual) && !isEqual;
        }

        internal static bool SeqGtE<Col1, Col2, T>(this Col1 seq1, Col2 seq2) where Col1 : IList<T> where Col2 : IList<T> where T : IComparable<T>
        {
            return SeqGtE<Col1, Col2, T>(seq1, seq2, out var _);
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        internal static bool SeqGtE<Col1, Col2, T>(this Col1 seq1, Col2 seq2, out bool seqIsEqual) where Col1 : IList<T> where Col2 : IList<T> where T : IComparable<T>
        {
            var commonLen = Math.Min(seq1.Count, seq2.Count);
            for (int i = 0; i < commonLen; i++)
            {
                var cmp = seq1[i].CompareTo(seq2[i]);
                if (cmp > 0)
                {
                    seqIsEqual = false;
                    return true;
                }
                else if (cmp == 0)
                {
                    continue;
                }
                else
                {
                    seqIsEqual = false;
                    return false;
                }
            }
            if (seq1.Count > seq2.Count)
            {
                seqIsEqual = false;
                return true;
            }
            else if (seq1.Count == seq2.Count)
            {
                seqIsEqual = true;
                return true;
            }
            seqIsEqual = false;
            return false;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        internal static bool SeqGt<Col1, Col2, T>(this Col1 seq1, Col2 seq2) where Col1 : IList<T> where Col2 : IList<T> where T : IComparable<T>
        {
            return seq1.SeqGtE<Col1, Col2, T>(seq2, out var isEqual) && !isEqual;
        }

        internal static T[] ConcatArray<T>(this T[] self, T[] other)
        {
            var xs = new T[self.Length + other.Length];
            for (int i = 0; i < self.Length; i++)
                xs[i] = self[i];
            for (int i = 0, j = self.Length; i < other.Length; i++, j++)
                xs[j] = other[i];
            return xs;
        }

    }

}