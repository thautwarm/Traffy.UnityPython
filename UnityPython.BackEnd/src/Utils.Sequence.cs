using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Traffy.Objects;

namespace Traffy
{
    public static class SeqUtils
    {

        internal static IEnumerable<int> SimpleRange(this int i)
        {
            for (int j = 0; j < i; j++)
            {
                yield return j;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int NormalizeStart(int start, int count)
        {
            if (start < 0)
            {
                start += count;
                start = Math.Max(0, start);
            }
            return start;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static (int, int) NormalizeStartEnd(int start, int end, int count)
        {
            start = NormalizeStart(start, count);
            if (end == -1)
            {
                end = count;
            }
            else if (end < 0)
            {
                end += count;
                end = Math.Min(count, end);
            }
            return (start, end);

        }
        internal static int IndexSubSeqGenericSimple<TList1, TList2, T>(this TList1 seq1, TList2 seq2, int start = 0, int end = -1) where T : IEquatable<T> where TList1 : IList<T> where TList2 : IList<T>
        {
            Dictionary<T, int> dict = null;
            (start, end) = NormalizeStartEnd(start, end, seq1.Count);
            return _BMHSearch<TList1, TList2, T>(seq1, seq2, start, end, ref dict);
        }

        internal static int IndexEltGenericSimple<TList, T>(this TList seq1, T elt, int start = 0, int end = -1) where T : IEquatable<T> where TList : IList<T>
        {
            (start, end) = NormalizeStartEnd(start, end, seq1.Count);
            for(int i = start; i < end; i++)
            {
                if (seq1[i].Equals(elt))
                {
                    return i;
                }
            }
            return -1;
        }

        internal static int CountSubSeqGenericSimple<TList1, TList2, T>(this TList1 seq1, TList2 seq2, int start = 0, int end = -1) where T : IEquatable<T> where TList1 : IList<T> where TList2 : IList<T>
        {
            Dictionary<T, int> dict = null;
            (start, end) = NormalizeStartEnd(start, end, seq1.Count);
            int cnt = 0;
            var i = start;
            while(i < end)
            {
                i = _BMHSearch<TList1, TList2, T>(seq1, seq2, i, end, ref dict);
                if (i < 0)
                    return cnt;
                cnt++;
                i++;
            }
            return cnt;
        }

        internal static int CountGenericSimple<TList, T>(this TList seq, T element, int start = 0, int end = -1) where T : IEquatable<T> where TList : IList<T>
        {
            int cnt = 0;
            (start, end) = NormalizeStartEnd(start, end, seq.Count);
            for (int i = start; i < end; i++)
            {
                if (seq[i].Equals(element))
                    cnt++;
            }
            return cnt;
        }

        // TODO: use BMH search and cache 'badMatchTable'
        // https://swimburger.net/blog/dotnet/generic-boyer-moore-horspool-algorithm-in-csharp-dotnet
        public static int _BMHSearch<TList1, TList2, T>(
            TList1 haystack,
            TList2 needle,
            int start,
            int end,
            ref Dictionary<T, int> badMatchTable)
        where T : IEquatable<T>
        where TList1 : IList<T>
        where TList2 : IList<T>
        {
            var needleLength = needle.Count;

            int shiftAmountIfMissing = needleLength;
            if (start + needleLength > end)
            {
                return -1;
            }
            if (needleLength == 0)
                return start;

            if (badMatchTable == null)
            {
                badMatchTable = new Dictionary<T, int>();
                for (int i = 0; i < needleLength - 1; i++)
                {
                    badMatchTable[needle[i]] = needleLength - i - 1;
                }
            }
            int listIndex = start;
            while (listIndex <= end - needleLength)
            {
                int matchIndex = needleLength - 1;
                while (true)
                {
                    if (haystack[listIndex + matchIndex].Equals(needle[matchIndex]))
                    {
                        matchIndex--;
                    }
                    else
                    {
                        break;
                    }

                    if (matchIndex <= 0)
                    {
                        return listIndex;
                    }
                }

                if (badMatchTable.TryGetValue(haystack[listIndex + needleLength - 1], out int amountToShift))
                {
                    listIndex += amountToShift;
                }
                else
                {
                    listIndex += shiftAmountIfMissing;
                }
            }
            return -1;
        }

        internal static bool StartswithI<TList1, TList2, T>(this TList1 xs, TList2 content, int start) where T : IEquatable<T> where TList1 : IList<T> where TList2 : IList<T>
        {
            start = NormalizeStart(start, xs.Count);

            var n1 = xs.Count;
            var n2 = content.Count;
            if (start + n2 > n1)
            {
                return false;
            }

            for (int i = 0, j = start; i < n2; i++, j++)
            {

                if (!xs[j].Equals(content[i]))
                {
                    return false;
                }
            }
            return true;
        }

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


        public static void DeleteItemsSupportSlice<TList, T>(TList seq, TrObject item, TrClass Class) where TList: IList<T>
        {
            switch (item)
            {
                case TrInt oitem:
                {
                    var i = unchecked((int)oitem.value);
                    if (i < 0)
                        i += seq.Count;
                    if (i < 0 || i >= seq.Count)
                        throw new IndexError($"{Class.Name} assignment index out of range");
                    seq.RemoveAt((int)i);
                    return;
                }
                case TrSlice slice:
                {
                    var (istart, istep, nstep) = slice.resolveSlice(seq.Count);
                    // XXX: can optimize to O(n)
                    // we may iterate the list, remove the items in the slice, and add the remaining items to the new list
                    if (istep < 0)
                    {
                        for (int x = istart, i = 0; i < nstep; i++, x += istep)
                        {
                            seq.RemoveAt(x);
                        }
                    }
                    else
                    {
                        istart += (nstep - 1) * istep;
                        for (int x = istart, i = 0; i < nstep; i++, x -= istep)
                        {
                            seq.RemoveAt(x);
                        }
                    }
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