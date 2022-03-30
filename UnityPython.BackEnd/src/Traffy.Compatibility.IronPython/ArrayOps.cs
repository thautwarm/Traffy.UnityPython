/* ===================================================================
Modification Copyright 2022
Adapted for use with the Traffy.UnityPython project by:
  thautwarm(Taine Zhao)
  - The original source can be found at:
    github.com/IronLanguages/ironpython2/master/Src/IronPython/Runtime/Operations/ArrayOps.cs
// ================================================================
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Traffy.Compatibility.IronPython;

namespace IronPython.Runtime.Operations {
    public static class ArrayOps {
        #region Python APIs

        
        public static T[] Add<T>(T[] data1, T[] data2) {
            T[] ret = new T[data1.Length + data2.Length];
            Array.Copy(data1, 0, ret, 0, data1.Length);
            Array.Copy(data2, 0, ret, data1.Length, data2.Length);
            return ret;
        }

        
        /// <summary>
        /// Multiply two object[] arrays - slow version, we need to get the type, etc...
        /// </summary>
        public static T[] Multiply<T>(T[] data, int count) {
            if (count <= 0) return ArrayUtils.EmptyObjects<T>();

            int newCount = data.Length * count;

            T[] ret = new T[newCount];
            Array.Copy(data, 0, ret, 0, data.Length);

            // this should be extremely fast for large count as it uses the same algoithim as efficient integer powers
            // ??? need to test to see how large count and n need to be for this to be fastest approach
            int block = data.Length;
            int pos = data.Length;
            while (pos < newCount) {
                Array.Copy(ret, 0, ret, pos, Math.Min(block, newCount - pos));
                pos += block;
                block *= 2;
            }
            return ret;
        }

        
        public static T[] GetItem<T>(T[] data, Traffy.Objects.TrSlice slice) {
            return GetSlice(data, data.Length, slice);
        }
        
        public static void SetItem<T, TList>(T[] a, Traffy.Objects.TrSlice slice, TList lst)
        where T: IEquatable<T>
        where TList: IList<T>
        {
            PythonOps.DoSliceAssign<InlineHelper.FArray<T>, TList, T>(
                a, 
                slice,
                lst
            );
        }

        #endregion

        #region Internal APIs


        /// <summary>
        /// Multiply two object[] arrays - internal version used for objects backed by arrays
        /// </summary>
        internal static T[] Multiply<T>(T[] data, int size, int count) {
            int newCount = checked(size * count);

            T[] ret = ArrayOps.CopyArray(data, newCount);
            if (count > 0) {
                // this should be extremely fast for large count as it uses the same algoithim as efficient integer powers
                // ??? need to test to see how large count and n need to be for this to be fastest approach
                int block = size;
                int pos = size;
                while (pos < newCount) {
                    Array.Copy(ret, 0, ret, pos, Math.Min(block, newCount - pos));
                    pos += block;
                    block *= 2;
                }
            }
            return ret;
        }

        /// <summary>
        /// Add two arrays - internal versions for objects backed by arrays
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="size1"></param>
        /// <param name="data2"></param>
        /// <param name="size2"></param>
        /// <returns></returns>
        internal static T[] Add<T>(T[] data1, int size1, T[] data2, int size2) {
            T[] ret = ArrayOps.CopyArray(data1, size1 + size2);
            Array.Copy(data2, 0, ret, size1, size2);
            return ret;
        }

        internal static T[] GetSlice<T>(T[] data, int start, int stop) {
            if (stop <= start) return ArrayUtils.EmptyObjects<T>();

            T[] ret = new T[stop - start];
            int index = 0;
            for (int i = start; i < stop; i++) {
                ret[index++] = data[i];
            }
            return ret;
        }

        internal static T[] GetSlice<T>(T[] data, int start, int stop, int step) {
            Debug.Assert(step != 0);

            if (step == 1) {
                return GetSlice(data, start, stop);
            }

            int size = GetSliceSize(start, stop, step);
            if (size <= 0) return ArrayUtils.EmptyObjects<T>();

            T[] res = new T[size];
            for (int i = 0, index = start; i < res.Length; i++, index += step) {
                res[i] = data[index];
            }
            return res;
        }

        internal static T[] GetSlice<T>(T[] data, Traffy.Objects.TrSlice slice) {

            var (start, stop, step) = PythonOps.FixSlice(data.Length, slice.start, slice.stop, slice.step);

            return GetSlice(data, start, stop, step);
        }

        internal static T[] GetSlice<T>(T[] data, int size, Traffy.Objects.TrSlice slice) {
            var (start, stop, step) = PythonOps.FixSlice(size, slice.start, slice.stop, slice.step);

            if ((step > 0 && start >= stop) || (step < 0 && start <= stop)) {
                return ArrayUtils.EmptyObjects<T>();
            }

            if (step == 1) {
                int n = stop - start;
                T[] ret = new T[n];
                Array.Copy(data, start, ret, 0, n);
                return ret;
            } else {
                int n = GetSliceSize(start, stop, step);
                T[] ret = new T[n];
                int ri = 0;
                for (int i = 0, index = start; i < n; i++, index += step) {
                    ret[ri++] = data[index];
                }
                return ret;
            }
        }

        private static int GetSliceSize(int start, int stop, int step) {
            // could cause overflow (?)
            return step > 0 ? (stop - start + step - 1) / step : (stop - start + step + 1) / step;
        }        

        internal static T[] CopyArray<T>(T[] data, int newSize) {
            if (newSize == 0) return ArrayUtils.EmptyObjects<T>();

            T[] newData = new T[newSize];
            if (data.Length < 20) {
                for (int i = 0; i < data.Length && i < newSize; i++) {
                    newData[i] = data[i];
                }
            } else {
                Array.Copy(data, newData, Math.Min(newSize, data.Length));
            }

            return newData;
        }

        #endregion
    }
}