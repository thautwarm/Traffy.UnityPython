/* ===================================================================
Modification Copyright 2022
Adapted for use with the Traffy.UnityPython project by:
  thautwarm(Taine Zhao)
The original source can be found at:
    https://github.com/IronLanguages/ironpython2/blob/master/Src/IronPython/Runtime/Operations/ArrayOps.cs
Specifically, the function 'DelSlice' is abstracted from
    shttps://github.com/IronLanguages/ironpython2/blob/aa0526d4b786ad1fabbedbf91459903b8dc01ba3/Src/IronPython/Runtime/ByteArray.cs#L1155
// ================================================================
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Traffy.Compatibility.IronPython;
using InlineHelper;

namespace IronPython.Runtime.Operations {
    public static class ListOps {
        #region Python APIs

        public static List<T> Add<T>(List<T> data1, List<T> data2) {
            List<T> ret = new List<T>(data1.Count + data2.Count);
            for(int i = 0; i < data1.Count; i++)
            {
                ret.Add(data1[i]);
            }
            for(int i = 0; i < data2.Count; i++)
            {
                ret.Add(data2[i]);
            }
            return ret;
        }

        
        /// <summary>
        /// Multiply two object[] arrays - slow version, we need to get the type, etc...
        /// </summary>
        public static List<T> Multiply<T>(List<T> data, int count) {
            if (count <= 0) return new List<T>();
            var xs = new List<T>(count * data.Count);
            for (int i = 0; i < count; i++)
            {
                xs.AddRange(data);
            }
            return xs;
        }

        
        public static List<T> GetItem<T>(List<T> data, Traffy.Objects.TrSlice slice) {
            return GetSlice(data, slice);
        }
        
        public static void SetItem<T, TList>(List<T> a, Traffy.Objects.TrSlice slice, TList lst)
        where T: IEquatable<T>
        where TList: IList<T>
        {
            PythonOps.DoSliceAssign<InlineHelper.FList<T>, TList, T>(
                a, 
                slice,
                lst
            );
        }

        #endregion

        #region Internal APIs
        internal static List<T> GetSlice<T>(List<T> data, int start, int stop) {
            if (stop <= start) return new List<T>();

            List<T> ret = new List<T>(stop - start);
            for (int i = start; i < stop; i++) {
                ret.Add(data[i]);
            }
            return ret;
        }

        internal static List<T> GetSlice<T>(List<T> data, int start, int stop, int step) {
            Debug.Assert(step != 0);

            if (step == 1) {
                return GetSlice(data, start, stop);
            }

            int size = GetSliceSize(start, stop, step);
            if (size <= 0) return new List<T>();

            List<T> res = new List<T>(size);
            for (int i = 0, index = start; i < size; i++, index += step) {
                res.Add(data[index]);
            }
            return res;
        }

        internal static List<T> GetSlice<T>(List<T> data, Traffy.Objects.TrSlice slice) {

            var (start, stop, step) = PythonOps.FixSlice(data.Count, slice.start, slice.stop, slice.step);

            return GetSlice(data, start, stop, step);
        }

        private static int GetSliceSize(int start, int stop, int step) {
            // could cause overflow (?)
            return step > 0 ? (stop - start + step - 1) / step : (stop - start + step + 1) / step;
        }
        #endregion

        public static void DelSlice<T>(List<T> _lst, Traffy.Objects.TrSlice slice)
        {
            lock (_lst) {

                // slice is sealed, indices can't be user code...
                var (start, stop, step) = PythonOps.FixSlice(_lst.Count, slice);

                if (step > 0 && (start >= stop)) return;
                if (step < 0 && (start <= stop)) return;

                if (step == 1) {
                    int i = start;
                    for (int j = stop; j < _lst.Count; j++, i++) {
                        _lst[i] = _lst[j];
                    }
                    _lst.RemoveRange(i, stop - start);
                    return;
                } else if (step == -1) {
                    int i = stop + 1;
                    for (int j = start + 1; j < _lst.Count; j++, i++) {
                        _lst[i] = _lst[j];
                    }
                    _lst.RemoveRange(i, start - stop);
                    return;
                } else if (step < 0) {
                    // find "start" we will skip in the 1,2,3,... order
                    int i = start;
                    while (i > stop) {
                        i += step;
                    }
                    i -= step;

                    // swap start/stop, make step positive
                    stop = start + 1;
                    start = i;
                    step = -step;
                }

                int curr, skip, move;
                // skip: the next position we should skip
                // curr: the next position we should fill in data
                // move: the next position we will check
                curr = skip = move = start;

                while (curr < stop && move < stop) {
                    if (move != skip) {
                        _lst[curr++] = _lst[move];
                    } else
                        skip += step;
                    move++;
                }
                while (stop < _lst.Count) {
                    _lst[curr++] = _lst[stop++];
                }
                _lst.RemoveRange(curr, _lst.Count - curr);
            }
        }
    }
}