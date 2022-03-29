// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using Traffy.Compatibility.IronPython;
using Traffy.Objects;

namespace IronPython.Runtime.Operations {
    public static partial class ByteOps {
        internal static byte ToByteChecked(this int item) {
            try {
                return checked((byte)item);
            } catch (OverflowException) {
                throw PythonOps.ValueError("byte must be in range(0, 256)");
            }
        }

        internal static byte ToByteChecked(this long item) {
            try {
                return checked((byte)item);
            } catch (OverflowException) {
                throw PythonOps.ValueError("byte must be in range(0, 256)");
            }
        }

        internal static byte ToByteChecked(this BigInteger item) {
            int val;
            if (item.AsInt32(out val)) {
                return ToByteChecked(val);
            }
            throw PythonOps.ValueError("byte must be in range(0, 256)");
        }

        internal static byte ToByteChecked(this double item) {
            try {
                return checked((byte)item);
            } catch (OverflowException) {
                throw PythonOps.ValueError("byte must be in range(0, 256)");
            }
        }

        internal static bool IsSign(this byte ch) {
            return ch == '+' || ch == '-';
        }

        internal static byte ToUpper(this byte p) {
            if (p >= 'a' && p <= 'z') {
                p -= ('a' - 'A');
            }
            return p;
        }

        internal static byte ToLower(this byte p) {
            if (p >= 'A' && p <= 'Z') {
                p += ('a' - 'A');
            }
            return p;
        }

        internal static bool IsLower(this byte p) {
            return p >= 'a' && p <= 'z';
        }

        internal static bool IsUpper(this byte p) {
            return p >= 'A' && p <= 'Z';
        }

        internal static bool IsDigit(this byte b) {
            return b >= '0' && b <= '9';
        }

        internal static bool IsLetter(this byte b) {
            return IsLower(b) || IsUpper(b);
        }

        internal static bool IsWhiteSpace(this byte b) {
            return b == ' ' ||
                    b == '\t' ||
                    b == '\n' ||
                    b == '\r' ||
                    b == '\f' ||
                    b == 11;
        }

        internal static void AppendJoin(TrObject value, int index, List<byte> byteList) {
            IList<byte> bytesValue;
            if ((bytesValue = value.Native as IList<byte>) != null) {
                byteList.AddRange(bytesValue);
            }
            else {
                throw PythonOps.TypeError("sequence item {0}: expected bytes or byte array, {1} found", index.ToString(), PythonOps.GetPythonTypeName(value));
            }
        }

        internal static IList<byte> CoerceBytes(TrObject obj) {
            if (!(obj.Native is IList<byte> ret)) {
                throw PythonOps.TypeError("expected string, got {0} Type", PythonTypeOps.GetName(obj));
            }
            return ret;
        }
    }
}