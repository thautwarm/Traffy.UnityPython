/* ================================================================================================
// Modifications Copyright 3/30/2022 thautwarm(Taine Zhao)
// Uses of 'object' are refactored to fit 'Traffy.Objects.TrObject' &
// ================================================================================================
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
// ================================================================================================
*/

using System.Collections.Generic;
using System.Linq;
using Traffy.Compatibility.IronPython;

namespace IronPython.Runtime.Operations {
    public static class ListOfTOps<T> {


        #region Python __ methods

        public static void DeleteItem(List<T> l, int index) {
            l.RemoveAt(PythonOps.FixIndex(index, l.Count));
        }

        public static void DeleteItem(List<T> l, Traffy.Objects.TrSlice slice) {
            if (slice == null) {
                throw PythonOps.TypeError("List<T> indices must be slices or integers");
            }

            // int start, stop, step;
            // slice is sealed, indices can't be user code...
            var (start, stop, step) = slice._indices(l.Count);

            if (step > 0 && (start >= stop)) return;
            if (step < 0 && (start <= stop)) return;

            if (step == 1) {
                int i = start;
                for (int j = stop; j < l.Count; j++, i++) {
                    l[i] = l[j];
                }
                l.RemoveRange(i, stop - start);
                return;
            } else if (step == -1) {
                int i = stop + 1;
                for (int j = start + 1; j < l.Count; j++, i++) {
                    l[i] = l[j];
                }
                l.RemoveRange(i, start - stop);
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
                    l[curr++] = l[move];
                } else
                    skip += step;
                move++;
            }
            while (stop < l.Count) {
                l[curr++] = l[stop++];
            }
            l.RemoveRange(curr, l.Count - curr);
        }


        public static List<T> GetItem(List<T> l, Traffy.Objects.TrSlice slice) {
            if (slice == null) throw PythonOps.TypeError("List<T> indices must be slices or integers");
            
            var (start, stop, step) = slice._indices(l.Count);
            if (step == 1) {
                return stop > start ? l.Skip(start).Take(stop - start).ToList() : new List<T>();
            } else {
                int index = 0;
                List<T> newData;
                if (step > 0) {
                    if (start > stop) return new List<T>();

                    int icnt = (stop - start + step - 1) / step;
                    newData = new List<T>(icnt);
                    for (int i = start; i < stop; i += step) {
                        newData[index++] = l[i];
                    }
                } else {
                    if (start < stop) return new List<T>();

                    int icnt = (stop - start + step + 1) / step;
                    newData = new List<T>(icnt);
                    for (int i = start; i > stop; i += step) {
                        newData[index++] = l[i];
                    }
                }
                return newData;
            }
        }

        #endregion
    }
}