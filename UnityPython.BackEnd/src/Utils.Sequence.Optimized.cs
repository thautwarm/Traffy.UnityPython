// We might optimize string searching using this module in the future.

namespace Traffy
{
    public static partial class SeqUtils
    {
        // internal static int IndexSubSeqGenericSimple<TList1, TList2, T>(this TList1 seq1, TList2 seq2, int start = 0, int end = 0) where T : IEquatable<T> where TList1 : IList<T> where TList2 : IList<T>
        // {
        //     Dictionary<T, int> dict = null;
        //     (start, end) = NormalizeStartEnd(start, end, seq1.Count);
        //     return _BMHSearch<TList1, TList2, T>(seq1, seq2, start, end, ref dict);
        // }

        // internal static int CountSubSeqGenericSimple<TList1, TList2, T>(this TList1 seq1, TList2 seq2, int start = 0, int end = 0) where T : IEquatable<T> where TList1 : IList<T> where TList2 : IList<T>
        // {
        //     Dictionary<T, int> dict = null;
        //     (start, end) = NormalizeStartEnd(start, end, seq1.Count);
        //     int cnt = 0;
        //     var i = start;
        //     while(i < end)
        //     {
        //         i = _BMHSearch<TList1, TList2, T>(seq1, seq2, i, end, ref dict);
        //         if (i < 0)
        //             return cnt;
        //         cnt++;
        //         i++;
        //     }
        //     return cnt;
        // }

        // TODO: use BMH search and cache 'badMatchTable'
        // https://swimburger.net/blog/dotnet/generic-boyer-moore-horspool-algorithm-in-csharp-dotnet
        // public static int _BMHSearch<TList1, TList2, T>(
        //     TList1 haystack,
        //     TList2 needle,
        //     int start,
        //     int end,
        //     ref Dictionary<T, int> badMatchTable)
        // where T : IEquatable<T>
        // where TList1 : IList<T>
        // where TList2 : IList<T>
        // {
        //     var needleLength = needle.Count;

        //     int shiftAmountIfMissing = needleLength;
        //     if (start + needleLength > end)
        //     {
        //         return -1;
        //     }
        //     if (needleLength == 0)
        //         return start;

        //     if (badMatchTable == null)
        //     {
        //         badMatchTable = new Dictionary<T, int>();
        //         for (int i = 0; i < needleLength - 1; i++)
        //         {
        //             badMatchTable[needle[i]] = needleLength - i - 1;
        //         }
        //     }
        //     int listIndex = start;
        //     while (listIndex <= end - needleLength)
        //     {
        //         int matchIndex = needleLength - 1;
        //         while (true)
        //         {
        //             if (haystack[listIndex + matchIndex].Equals(needle[matchIndex]))
        //             {
        //                 matchIndex--;
        //             }
        //             else
        //             {
        //                 break;
        //             }

        //             if (matchIndex <= 0)
        //             {
        //                 return listIndex;
        //             }
        //         }

        //         if (badMatchTable.TryGetValue(haystack[listIndex + needleLength - 1], out int amountToShift))
        //         {
        //             listIndex += amountToShift;
        //         }
        //         else
        //         {
        //             listIndex += shiftAmountIfMissing;
        //         }
        //     }
        //     return -1;
        // }

    }
}