/* ================================================================================================
// Modifications Copyright 3/30/2022 thautwarm(Taine Zhao)
// 1. Extensible<string> is removed.
// 2. Uses of 'object' are refactored to fit 'Traffy.Objects.TrObject' &
//    Some runtime typechecks become redundant and get removed.
// 3. encoding-related methods and classes are removed  (PythonEncoderFallbackBuffer)
// 4. 'string GetItem(string s, Slice slice)' is renamed to 'GetSlice'
// 5. Annotations like [BytesConversion] are removed.
// ================================================================================================
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
// ================================================================================================
*/


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Traffy.Compatibility.IronPython;

namespace IronPython.Runtime.Operations {
    /// <summary>
    /// StringOps is the static class that contains the methods defined on strings, i.e. 'abc'
    /// 
    /// Here we define all of the methods that a Python user would see when doing dir('abc').
    /// If the user is running in a CLS aware context they will also see all of the methods
    /// defined in the CLS System.String type.
    /// </summary>
    public static class StringOps {
        internal const int LowestUnicodeValue = 0x7f;

        internal static Traffy.Objects.TrStr FastNew(Traffy.Objects.TrObject x) {
            if (x is Traffy.Objects.TrStr xstr) {
                return xstr;
            }
            
            string value = x.__str__();
            return PythonOps.BoxString(value);
        }

        #region Python __ methods

        public static bool __contains__(string s, string item) {
            return s.Contains(item);
        }

        public static bool __contains__(string s, char item) {
            return s.IndexOf(item) != -1;
        }

        public static int __len__(string s) {
            return s.Length;
        }
        
        public static string GetSlice(string s, Traffy.Objects.TrSlice slice) {
            if (slice == null) throw PythonOps.TypeError("string indices must be slices or integers");
            int start, stop, step;
            (start, stop, step) = slice._indices(s.Length);
            if (step == 1) {
                return stop > start ? s.Substring(start, stop - start) : String.Empty;
            } else {
                int index = 0;
                char[] newData;
                if (step > 0) {
                    if (start > stop) return String.Empty;

                    int icnt = (stop - start + step - 1) / step;
                    newData = new char[icnt];
                    for (int i = start; i < stop; i += step) {
                        newData[index++] = s[i];
                    }
                } else {
                    if (start < stop) return String.Empty;

                    int icnt = (stop - start + step + 1) / step;
                    newData = new char[icnt];
                    for (int i = start; i > stop; i += step) {
                        newData[index++] = s[i];
                    }
                }
                return new string(newData);
            }
        }


        #endregion

        #region Public Python methods

        /// <summary>
        /// Returns a copy of this string converted to uppercase
        /// </summary>
        public static string capitalize(this string self) {
            if (self.Length == 0) return self;
            return Char.ToUpperInvariant(self[0]) + self.Substring(1).ToLowerInvariant();
        }

        //  default fillchar (padding char) is a space
        public static string center(this string self, int width) {
            return center(self, width, ' ');
        }

        public static string center(this string self, int width, char fillchar) {
            int spaces = width - self.Length;
            if (spaces <= 0) return self;

            StringBuilder ret = new StringBuilder(width);
            ret.Append(fillchar, spaces / 2);
            ret.Append(self);
            ret.Append(fillchar, (spaces + 1) / 2);
            return ret.ToString();
        }

        public static int count(this string self, string sub) {
            return count(self, sub, 0, self.Length);
        }

        public static int count(this string self, string sub, int start) {
            return count(self, sub, start, self.Length);
        }

        public static int count(this string self, string ssub, int start, int end) {
            if (ssub == null) throw PythonOps.TypeError("expected string for 'sub' argument, got NoneType");

            if (start > self.Length) {
                return 0;
            }

            start = PythonOps.FixSliceIndex(start, self.Length);
            end = PythonOps.FixSliceIndex(end, self.Length);

            if (ssub.Length == 0) {
                return Math.Max((end - start) + 1, 0);
            }

            int count = 0;
            CompareInfo c = CultureInfo.InvariantCulture.CompareInfo;
            while (true) {
                if (end <= start) break;
                int index = c.IndexOf(self, ssub, start, end - start, CompareOptions.Ordinal);
                if (index == -1) break;
                count++;
                start = index + ssub.Length;
            }
            return count;
        }

        public static bool endswith(this string self, Traffy.Objects.TrObject suffix) {
            TryStringOrTuple(suffix);
            if (suffix is Traffy.Objects.TrTuple tuple)
                return endswith(self, (PythonTuple) tuple);
            else
                return endswith(self, GetString(suffix));
        }

        public static bool endswith(this string self, Traffy.Objects.TrObject suffix, int start) {
            TryStringOrTuple(suffix);
            if (suffix is Traffy.Objects.TrTuple tuple)
                return endswith(self, (PythonTuple) tuple, start);
            else
                return endswith(self, suffix, start);
        }

        public static bool endswith(this string self, Traffy.Objects.TrObject suffix, int start, int end) {
            TryStringOrTuple(suffix);
            if (suffix is Traffy.Objects.TrTuple tuple)
                return endswith(self, (PythonTuple)tuple, start, end);
            else
                return endswith(self, GetString(suffix), start, end);
        }

        public static string expandtabs(string self) {
            return expandtabs(self, 8);
        }

        public static string expandtabs(this string self, int tabsize) {
            StringBuilder ret = new StringBuilder(self.Length * 2);
            string v = self;
            int col = 0;
            for (int i = 0; i < v.Length; i++) {
                char ch = v[i];
                switch (ch) {
                    case '\n':
                    case '\r': col = 0; ret.Append(ch); break;
                    case '\t':
                        if (tabsize > 0) {
                            int tabs = tabsize - (col % tabsize);
                            int existingSize = ret.Capacity;
                            ret.Capacity = checked(existingSize + tabs);
                            ret.Append(' ', tabs);
                            col = 0;
                        }
                        break;
                    default:
                        col++;
                        ret.Append(ch);
                        break;
                }
            }
            return ret.ToString();
        }

        public static int find(this string self, string sub) {
            if (sub == null) throw PythonOps.TypeError("expected string, got NoneType");
            if (sub.Length == 1) return self.IndexOf(sub[0]);

            CompareInfo c = CultureInfo.InvariantCulture.CompareInfo;
            return c.IndexOf(self, sub, CompareOptions.Ordinal);
        }

        public static int find(this string self, string sub, int start) {
            if (sub == null) throw PythonOps.TypeError("expected string, got NoneType");
            if (start > self.Length) return -1;
            start = PythonOps.FixSliceIndex(start, self.Length);

            CompareInfo c = CultureInfo.InvariantCulture.CompareInfo;
            return c.IndexOf(self, sub, start, CompareOptions.Ordinal);
        }

        public static int find(this string self, string sub, BigInteger start) {
            if (sub == null) throw PythonOps.TypeError("expected string, got NoneType");
            if (start.Unbox.value > self.Length) return -1;
            return find(self, sub, start.Unbox.ToIntChecked());
        }

        public static int find(this string self, string sub, int start, int end) {
            if (sub == null) throw PythonOps.TypeError("expected string, got NoneType");
            if (start > self.Length) return -1;
            start = PythonOps.FixSliceIndex(start, self.Length);
            end = PythonOps.FixSliceIndex(end, self.Length);
            if (end < start) return -1;

            CompareInfo c = CultureInfo.InvariantCulture.CompareInfo;
            return c.IndexOf(self, sub, start, end - start, CompareOptions.Ordinal);
        }

        public static int find(this string self, string sub, BigInteger start, BigInteger end) {
            if (sub == null) throw PythonOps.TypeError("expected string, got NoneType");
            if (start.Unbox.value > self.Length) return -1;
            return find(self, sub, start.Unbox.ToIntChecked(), end.Unbox.ToIntChecked());
        }

        public static int index(this string self, string sub) {
            if (sub == null) throw PythonOps.TypeError("expected string, got NoneType");
            return index(self, sub, 0, self.Length);
        }

        public static int index(this string self, string sub, int start) {
            if (sub == null) throw PythonOps.TypeError("expected string, got NoneType");
            return index(self, sub, start, self.Length);
        }

        public static int index(this string self, string sub, int start, int end) {
            if (sub == null) throw PythonOps.TypeError("expected string, got NoneType");
            int ret = find(self, sub, start, end);
            if (ret == -1) throw PythonOps.ValueError("substring {0} not found in {1}", sub, self);
            return ret;
        }

        public static bool isalnum(this string self) {
            if (self.Length == 0) return false;
            string v = self;
            for (int i = v.Length - 1; i >= 0; i--) {
                if (!Char.IsLetterOrDigit(v, i)) return false;
            }
            return true;
        }

        public static bool isalpha(this string self) {
            if (self.Length == 0) return false;
            string v = self;
            for (int i = v.Length - 1; i >= 0; i--) {
                if (!Char.IsLetter(v, i)) return false;
            }
            return true;
        }

        public static bool isdigit(this string self) {
            if (self.Length == 0) return false;
            string v = self;
            for (int i = v.Length - 1; i >= 0; i--) {
                // CPython considers the circled digits to be digits
                if (!Char.IsDigit(v, i) && (v[i] < '\u2460' || v[i] > '\u2468')) return false;
            }
            return true;
        }

        public static bool isspace(this string self) {
            if (self.Length == 0) return false;
            string v = self;
            for (int i = v.Length - 1; i >= 0; i--) {
                if (!Char.IsWhiteSpace(v, i)) return false;
            }
            return true;
        }

        public static bool isdecimal(this string self) {
            return isnumeric(self);
        }

        public static bool isnumeric(this string self) {
            if (String.IsNullOrEmpty(self)) return false;

            foreach (char c in self) {
                if (!Char.IsDigit(c)) return false;
            }
            return true;
        }

        public static bool islower(this string self) {
            if (self.Length == 0) return false;
            string v = self;
            bool hasLower = false;
            for (int i = v.Length - 1; i >= 0; i--) {
                if (!hasLower && Char.IsLower(v, i)) hasLower = true;
                if (Char.IsUpper(v, i)) return false;
            }
            return hasLower;
        }

        public static bool isupper(this string self) {
            if (self.Length == 0) return false;
            string v = self;
            bool hasUpper = false;
            for (int i = v.Length - 1; i >= 0; i--) {
                if (!hasUpper && Char.IsUpper(v, i)) hasUpper = true;
                if (Char.IsLower(v, i)) return false;
            }
            return hasUpper;
        }

        /// <summary>
        /// return true if self is a titlecased string and there is at least one
        /// character in self; also, uppercase characters may only follow uncased
        /// characters (e.g. whitespace) and lowercase characters only cased ones.
        /// return false otherwise.
        /// </summary>
        public static bool istitle(this string self) {
            if (self == null || self.Length == 0) return false;

            string v = self;
            bool prevCharCased = false, currCharCased = false, containsUpper = false;
            for (int i = 0; i < v.Length; i++) {
                if (Char.IsUpper(v, i) || CharUnicodeInfo.GetUnicodeCategory(v, i) == UnicodeCategory.TitlecaseLetter) {
                    containsUpper = true;
                    if (prevCharCased)
                        return false;
                    else
                        currCharCased = true;
                } else if (Char.IsLower(v, i))
                    if (!prevCharCased)
                        return false;
                    else
                        currCharCased = true;
                else
                    currCharCased = false;
                prevCharCased = currCharCased;
            }

            //  if we've gone through the whole string and haven't encountered any rule 
            //  violations but also haven't seen an Uppercased char, then this is not a 
            //  title e.g. '\n', all whitespace etc.
            return containsUpper;
        }

        public static bool isunicode(this string self) {
            return self.Any(c => c >= LowestUnicodeValue);
        }

        /// <summary>
        /// Return a string which is the concatenation of the strings 
        /// in the sequence seq. The separator between elements is the 
        /// string providing this method
        /// </summary>
        public static string join(this string self, Traffy.Objects.TrObject sequence) {
            IEnumerator<Traffy.Objects.TrObject> seq = sequence.__iter__();
            if (!seq.MoveNext()) return "";

            // check if we have just a sequence of just one value - if so just
            // return that value.
            Traffy.Objects.TrObject curVal = seq.Current;
            if (!seq.MoveNext()) return GetString(curVal);

            StringBuilder ret = new StringBuilder();
            AppendJoin(curVal, 0, ret);

            int index = 1;
            do {
                ret.Append(self);

                AppendJoin(seq.Current, index, ret);

                index++;
            } while (seq.MoveNext());

            return ret.ToString();
        }

        public static string join(this string/*!*/ self, Traffy.Objects.TrList/*!*/ sequence) {
            if (sequence.container.Count == 0) return String.Empty;

            lock (sequence) {
                if (sequence.container.Count == 1) {
                    return GetString(sequence.container[0]);
                }

                StringBuilder ret = new StringBuilder();

                AppendJoin(sequence.container[0], 0, ret);
                for (int i = 1; i < sequence.container.Count; i++) {
                    if (!String.IsNullOrEmpty(self)) {
                        ret.Append(self);
                    }
                    AppendJoin(sequence.container[i], i, ret);
                }

                return ret.ToString();
            }
        }

        public static string ljust(this string self, int width) {
            return ljust(self, width, ' ');
        }

        public static string ljust(this string self, int width, char fillchar) {
            if (width < 0) return self;
            int spaces = width - self.Length;
            if (spaces <= 0) return self;

            StringBuilder ret = new StringBuilder(width);
            ret.Append(self);
            ret.Append(fillchar, spaces);
            return ret.ToString();
        }

        // required for better match with cpython upper/lower
        private static CultureInfo CasingCultureInfo = new CultureInfo("en");

        public static string lower(this string self) {
            return CasingCultureInfo.TextInfo.ToLower(self);
        }

        internal static string ToLowerAsciiTriggered(this string self) {
            for (int i = 0; i < self.Length; i++) {
                if (self[i] >= 'A' && self[i] <= 'Z') {
                    return self.ToLowerInvariant();
                }
            }
            return self;
        }

        public static string lstrip(this string self) {
            return self.TrimStart();
        }

        public static string lstrip(this string self, string chars) {
            if (chars == null) return lstrip(self);
            return self.TrimStart(chars.ToCharArray());
        }

        public static (string, string, string) partition(this string self, string sep) {
            if (sep == null)
                throw PythonOps.TypeError("expected string, got NoneType");
            if (sep.Length == 0)
                throw PythonOps.ValueError("empty separator");

            (string, string, string) obj = ("", "", "");

            if (self.Length != 0) {
                int index = find(self, sep);
                if (index == -1) {
                    obj.Item1 = self;
                } else {
                    obj.Item1 = self.Substring(0, index);
                    obj.Item2 = sep;
                    obj.Item3 = self.Substring(index + sep.Length, self.Length - index - sep.Length);
                }
            }
            return obj;
        }

        public static string replace(this string self, string old, string @new, int count = -1) {

            if (old == null) {
                throw PythonOps.TypeError("expected a character buffer object"); // cpython message
            }
            if (old.Length == 0) return ReplaceEmpty(self, @new, count);

            string v = self;
            int replacements = StringOps.count(v, old);
            replacements = (count < 0 || count > replacements) ? replacements : count;
            int newLength = v.Length;
            newLength -= replacements * old.Length;
            newLength = checked(newLength + replacements * @new.Length);
            StringBuilder ret = new StringBuilder(newLength);

            int index;
            int start = 0;
            while (count != 0 && (index = v.IndexOf(old, start, StringComparison.Ordinal)) != -1) {
                ret.Append(v, start, index - start);
                ret.Append(@new);
                start = index + old.Length;
                count--;
            }
            ret.Append(v.Substring(start));

            return ret.ToString();
        }

        public static int rfind(this string self, string sub) {
            if (sub == null) throw PythonOps.TypeError("expected string, got NoneType");
            return rfind(self, sub, 0, self.Length);
        }

        public static int rfind(this string self, string sub, int start) {
            if (sub == null) throw PythonOps.TypeError("expected string, got NoneType");
            if (start > self.Length) return -1;
            return rfind(self, sub, start, self.Length);
        }


        public static int rfind(this string self, string sub, int start, int end) {
            if (sub == null) throw PythonOps.TypeError("expected string, got NoneType");
            if (start > self.Length) return -1;

            start = PythonOps.FixSliceIndex(start, self.Length);
            end = PythonOps.FixSliceIndex(end, self.Length);

            if (start > end) return -1;     // can't possibly match anything, not even an empty string
            if (sub.Length == 0) return end;    // match at the end
            if (end == 0) return -1;    // can't possibly find anything

            CompareInfo c = CultureInfo.InvariantCulture.CompareInfo;
            return c.LastIndexOf(self, sub, end - 1, end - start, CompareOptions.Ordinal);
        }

        public static int rindex(this string self, string sub) {
            return rindex(self, sub, 0, self.Length);
        }

        public static int rindex(this string self, string sub, int start) {
            return rindex(self, sub, start, self.Length);
        }

        public static int rindex(this string self, string sub, int start, int end) {
            int ret = rfind(self, sub, start, end);
            if (ret == -1) throw PythonOps.ValueError("substring {0} not found in {1}", sub, self);
            return ret;
        }

        public static string rjust(this string self, int width) {
            return rjust(self, width, ' ');
        }

        public static string rjust(this string self, int width, char fillchar) {
            int spaces = width - self.Length;
            if (spaces <= 0) return self;

            StringBuilder ret = new StringBuilder(width);
            ret.Append(fillchar, spaces);
            ret.Append(self);
            return ret.ToString();
        }

        public static (string, string, string) rpartition(this string self, string sep) {
            if (sep == null)
                throw PythonOps.TypeError("expected string, got NoneType");
            if (sep.Length == 0)
                throw PythonOps.ValueError("empty separator");

            (string, string, string) obj = ("", "", "");
            if (self.Length != 0) {
                int index = rfind(self, sep);
                if (index == -1) {
                    obj.Item2 = self;
                } else {
                    obj.Item1 = self.Substring(0, index);
                    obj.Item2 = sep;
                    obj.Item3 = self.Substring(index + sep.Length, self.Length - index - sep.Length);
                }
            }
            return obj;
        }

        //  when no maxsplit arg is given then just use split
        public static Traffy.Objects.TrList rsplit(this string self) {
            return SplitInternal(self, (char[])null, -1);
        }

        public static Traffy.Objects.TrList rsplit(this string self, string sep) {
            return rsplit(self, sep, -1);
        }

        public static Traffy.Objects.TrList rsplit(this string self, string sep, int maxsplit) {
            //  rsplit works like split but needs to split from the right;
            //  reverse the original string (and the sep), split, reverse 
            //  the split list and finally reverse each element of the list
            string reversed = Reverse(self);
            if (sep != null) sep = Reverse(sep);
            Traffy.Objects.TrList temp = null, ret = null;
            temp = split(reversed, sep, maxsplit);
            temp.reverse();
            int resultlen = temp.container.Count;
            if (resultlen != 0) {
                ret = PythonOps.MakeEmptyList(resultlen);
                foreach (Traffy.Objects.TrObject s in temp.container)
                    ret.container.Add(PythonOps.BoxString(Reverse(GetString(s))));
            } else {
                ret = temp;
            }
            return ret;
        }

        public static string rstrip(this string self) {
            return self.TrimEnd();
        }

        public static string rstrip(this string self, string chars) {
            if (chars == null) return rstrip(self);
            return self.TrimEnd(chars.ToCharArray());
        }

        public static Traffy.Objects.TrList split(this string self) {
            return SplitInternal(self, (char[])null, -1);
        }

        public static Traffy.Objects.TrList split(this string self, string sep) {
            return split(self, sep, -1);
        }

        public static Traffy.Objects.TrList split(this string self, string sep, int maxsplit) {
            if (sep == null) {
                if (maxsplit == 0) {
                    // Corner case for CPython compatibility
                    Traffy.Objects.TrList result = PythonOps.MakeEmptyList(1);
                    result.container.Add(PythonOps.BoxString(self.TrimStart()));
                    return result;

                } else {
                    return SplitInternal(self, (char[])null, maxsplit);
                }
            }

            if (sep.Length == 0) {
                throw PythonOps.ValueError("empty separator");
            } else if (sep.Length == 1) {
                return SplitInternal(self, new char[] { sep[0] }, maxsplit);
            } else {
                return SplitInternal(self, sep, maxsplit);
            }
        }

        public static Traffy.Objects.TrList splitlines(this string self) {
            return splitlines(self, false);
        }

        public static Traffy.Objects.TrList splitlines(this string self, bool keepends) {
            Traffy.Objects.TrList ret = PythonOps.MakeEmptyList(0b11111);
            int i, linestart;
            for (i = 0, linestart = 0; i < self.Length; i++) {
                if (self[i] == '\n' || self[i] == '\r' || self[i] == '\x2028') {
                    //  special case of "\r\n" as end of line marker
                    if (i < self.Length - 1 && self[i] == '\r' && self[i + 1] == '\n') {
                        if (keepends)
                            ret.container.Add(PythonOps.BoxString(self.Substring(linestart, i - linestart + 2)));
                        else
                            ret.container.Add(PythonOps.BoxString(self.Substring(linestart, i - linestart)));
                        linestart = i + 2;
                        i++;
                    } else { //'\r', '\n', or unicode new line as end of line marker
                        if (keepends)
                            ret.container.Add(PythonOps.BoxString(self.Substring(linestart, i - linestart + 1)));
                        else
                            ret.container.Add(PythonOps.BoxString(self.Substring(linestart, i - linestart)));
                        linestart = i + 1;
                    }
                }
            }
            //  the last line needs to be accounted for if it is not empty
            if (i - linestart != 0)
                ret.container.Add(PythonOps.BoxString(self.Substring(linestart, i - linestart)));
            return ret;
        }
        public static bool startswith(this string self, Traffy.Objects.TrObject prefix) {
            TryStringOrTuple(prefix);
            if (prefix is Traffy.Objects.TrTuple tuple)
                return startswith(self, (PythonTuple)tuple);
            else
                return startswith(self, GetString(prefix));
        }

        public static bool startswith(this string self, Traffy.Objects.TrObject prefix, int start) {
            TryStringOrTuple(prefix);
            if (prefix is Traffy.Objects.TrTuple tuple)
                return startswith(self, (PythonTuple)tuple, start);
            else
                return startswith(self, GetString(prefix), start);
        }

        public static bool startswith(this string self, Traffy.Objects.TrObject prefix, int start, int end) {
            TryStringOrTuple(prefix);
            if (prefix is Traffy.Objects.TrTuple tuple)
                return startswith(self, (PythonTuple)tuple, start, end);
            else
                return startswith(self, GetString(prefix), start, end);
        }

        public static string strip(this string self) {
            return self.Trim();
        }

        public static string strip(this string self, string chars) {
            if (chars == null) return strip(self);
            return self.Trim(chars.ToCharArray());
        }

        public static string swapcase(this string self) {
            StringBuilder ret = new StringBuilder(self);
            for (int i = 0; i < ret.Length; i++) {
                char ch = ret[i];
                if (Char.IsUpper(ch)) ret[i] = Char.ToLowerInvariant(ch);
                else if (Char.IsLower(ch)) ret[i] = Char.ToUpperInvariant(ch);
            }
            return ret.ToString();
        }

        public static string title(this string self) {
            if (self == null || self.Length == 0) return self;

            char[] retchars = self.ToCharArray();
            bool prevCharCased = false;
            bool currCharCased = false;
            int i = 0;
            do {
                if (Char.IsUpper(retchars[i]) || Char.IsLower(retchars[i])) {
                    if (!prevCharCased)
                        retchars[i] = Char.ToUpperInvariant(retchars[i]);
                    else
                        retchars[i] = Char.ToLowerInvariant(retchars[i]);
                    currCharCased = true;
                } else {
                    currCharCased = false;
                }
                i++;
                prevCharCased = currCharCased;
            }
            while (i < retchars.Length);
            return new string(retchars);
        }

        //translate on a unicode string differs from that on an ascii
        //for unicode, the table argument is actually a dictionary with
        //character ordinals as keys and the replacement strings as values
        public static string translate(this string self, Dictionary<Traffy.Objects.TrObject, Traffy.Objects.TrObject> table) {
            if (table == null || self.Length == 0) {
                return self;
            }

            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < self.Length; i++) {
                var idx = PythonOps.BoxInt((int)self[i]);
                if (table.TryGetValue(idx, out var mapped)) {
                    if (IronPythonCompatExtras.IsNone(mapped)) {
                        continue;
                    }
                    if (mapped is Traffy.Objects.TrInt mapped_as_int) {
                        var mappedInt = mapped_as_int.value;
                        if (mappedInt > 0xFFFF) {
                            throw PythonOps.TypeError("character mapping must be in range(0x%lx)");
                        }
                        ret.Append((char)mappedInt);
                    } else if (mapped is Traffy.Objects.TrStr mapped_as_str) {
                        ret.Append(mapped_as_str.value);
                    } else {
                        throw PythonOps.TypeError("character mapping must return integer, None or unicode");
                    }
                } else {
                    ret.Append(self[i]);
                }
            }
            return ret.ToString();
        }

        public static string translate(this string self, string table) {
            return translate(self, table, (string)null);
        }

        public static string translate(this string self, string table, string deletechars) {
            if (table != null && table.Length != 256) {
                throw PythonOps.ValueError("translation table must be 256 characters long");
            } else if (self.Length == 0) {
                return self;
            }

            // List<char> is about 2/3rds as expensive as StringBuilder appending individual 
            // char's so we use that instead of a StringBuilder
            List<char> res = new List<char>();
            for (int i = 0; i < self.Length; i++) {
                if (deletechars == null || !deletechars.Contains(Char.ToString(self[i]))) {
                    if (table != null) {
                        int idx = (int)self[i];
                        if (idx >= 0 && idx < 256) {
                            res.Add(table[idx]);
                        }
                    } else {
                        res.Add(self[i]);
                    }
                }
            }
            return new String(res.ToArray());
        }

        public static string upper(this string self) {
            return CasingCultureInfo.TextInfo.ToUpper(self);
        }

        public static string zfill(this string self, int width) {
            int spaces = width - self.Length;
            if (spaces <= 0) return self;

            StringBuilder ret = new StringBuilder(width);
            if (self.Length > 0 && IsSign(self[0])) {
                ret.Append(self[0]);
                ret.Append('0', spaces);
                ret.Append(self.Substring(1));
            } else {
                ret.Append('0', spaces);
                ret.Append(self);
            }
            return ret.ToString();
        }

        #endregion

        #region operators
    
        public static string Add(string self, string other) {
            return self + other;
        }

    
        public static string Add(string self, char other) {
            return self + other;
        }


        public static string Add(char self, string other) {
            return self + other;
        }

        public static string Multiply(string s, int count) {
            if (count <= 0) return String.Empty;
            if (count == 1) return s;

            long size = (long)s.Length * (long)count;
            if (size > Int32.MaxValue) throw PythonOps.OverflowError("repeated string is too long");

            int sz = s.Length;
            if (sz == 1) return new string(s[0], count);

            StringBuilder ret = new StringBuilder(sz * count);
            ret.Insert(0, s, count);
            // the above code is MUCH faster than the simple loop
            //for (int i=0; i < count; i++) ret.Append(s);
            return ret.ToString();
        }
        
        public static string Multiply(int other, string self) {
            return Multiply(self, other);
        }

        public static bool GreaterThan(string x, string y) {
            return string.CompareOrdinal(x, y) > 0;
        }

        public static bool LessThan(string x, string y) {
            return string.CompareOrdinal(x, y) < 0;
        }

        public static bool LessThanOrEqual(string x, string y) {
            return string.CompareOrdinal(x, y) <= 0;
        }

        public static bool GreaterThanOrEqual(string x, string y) {
            return string.CompareOrdinal(x, y) >= 0;
        }

        public static bool Equals(string x, string y) {
            return string.Equals(x, y);
        }

        public static bool NotEquals(string x, string y) {
            return !string.Equals(x, y);
        }

        #endregion

        public static char ConvertToChar(string s) {
            if (s.Length == 1) return s[0];
            throw PythonOps.TypeErrorForTypeMismatch("char", s);
        }

        internal static int Compare(string self, string obj) {
            int ret = string.CompareOrdinal(self, obj);
            return ret == 0 ? 0 : (ret < 0 ? -1 : +1);
        }


        public static string __str__(string self) {
            return self;
        }

        #region Internal implementation details
        internal static string Quote(string s) {

            bool isUnicode = false;
            StringBuilder b = new StringBuilder(s.Length + 5);
            char quote = '\'';
            if (s.IndexOf('\'') != -1 && s.IndexOf('\"') == -1) {
                quote = '\"';
            }
            b.Append(quote);
            b.Append(ReprEncode(s, quote, ref isUnicode));
            b.Append(quote);
            if (isUnicode) return "u" + b.ToString();
            return b.ToString();
        }

        internal static string ReprEncode(string s, ref bool isUnicode) {
            return ReprEncode(s, (char)0, ref isUnicode);
        }

        internal static string RawUnicodeEscapeEncode(string s) {
            // in the common case we don't need to encode anything, so we
            // lazily create the StringBuilder only if necessary.
            StringBuilder b = null;
            for (int i = 0; i < s.Length; i++) {
                char ch = s[i];
                if (ch > 0xff) {
                    ReprInit(ref b, s, i);
                    b.AppendFormat("\\u{0:x4}", (int)ch);
                } else if (b != null) {
                    b.Append(ch);
                }
            }

            if (b == null) return s;
            return b.ToString();
        }


        #endregion

        #region Private implementation details

        private static void AppendJoin(Traffy.Objects.TrObject value, int index, StringBuilder sb) {
            string strVal;

            if ((strVal = value.Native as string) != null) {
                sb.Append(strVal);
            } else {
                throw PythonOps.TypeError("sequence item {0}: expected string, {1} found", index.ToString(), PythonOps.GetPythonTypeName(value));
            }
        }

        private static string ReplaceEmpty(string self, string @new, int count) {
            string v = self;

            if (count == 0) return v;
            else if (count < 0) count = v.Length + 1;
            else if (count > v.Length + 1) count = checked(v.Length + 1);

            int newLength = checked(v.Length + @new.Length * count);
            int max = Math.Min(v.Length, count);
            StringBuilder ret = new StringBuilder(newLength);
            for (int i = 0; i < max; i++) {
                ret.Append(@new);
                ret.Append(v[i]);
            }
            if (count > max) {
                ret.Append(@new);
            } else {
                ret.Append(v, max, v.Length - max);
            }

            return ret.ToString();
        }

        private static string Reverse(string s) {
            if (s.Length == 0 || s.Length == 1) return s;
            char[] rchars = new char[s.Length];
            for (int i = s.Length - 1, j = 0; i >= 0; i--, j++) {
                rchars[j] = s[i];
            }
            return new string(rchars);
        }

        internal static string ReprEncode(string s, char quote, ref bool isUnicode) {
            // in the common case we don't need to encode anything, so we
            // lazily create the StringBuilder only if necessary.
            StringBuilder b = null;
            for (int i = 0; i < s.Length; i++) {
                char ch = s[i];

                if (ch >= LowestUnicodeValue) isUnicode = true;
                switch (ch) {
                    case '\\': ReprInit(ref b, s, i); b.Append("\\\\"); break;
                    case '\t': ReprInit(ref b, s, i); b.Append("\\t"); break;
                    case '\n': ReprInit(ref b, s, i); b.Append("\\n"); break;
                    case '\r': ReprInit(ref b, s, i); b.Append("\\r"); break;
                    default:
                        if (quote != 0 && ch == quote) {
                            ReprInit(ref b, s, i);
                            b.Append('\\'); b.Append(ch);
                        } else if (ch < ' ' || (ch >= 0x7f && ch <= 0xff)) {
                            ReprInit(ref b, s, i);
                            b.AppendFormat("\\x{0:x2}", (int)ch);
                        } else if (ch > 0xff) {
                            ReprInit(ref b, s, i);
                            b.AppendFormat("\\u{0:x4}", (int)ch);
                        } else if (b != null) {
                            b.Append(ch);
                        }
                        break;
                }
            }

            if (b == null) return s;
            return b.ToString();
        }

        private static void ReprInit(ref StringBuilder sb, string s, int c) {
            if (sb != null) return;

            sb = new StringBuilder(s, 0, c, s.Length);
        }

        private static bool IsSign(char ch) {
            return ch == '+' || ch == '-';
        }

        internal static string GetEncodingName(Encoding encoding) {
#if FEATURE_ENCODING
            string name = null;

            // if we have a valid code page try and get a reasonable name.  The
            // web names / mail displays match tend to CPython's terse names
            if (encoding.CodePage != 0) {
#if !NETCOREAPP && !NETSTANDARD
                if (encoding.IsBrowserDisplay) {
                    name = encoding.WebName;
                }

                if (name == null && encoding.IsMailNewsDisplay) {
                    name = encoding.HeaderName;
                }
#endif

                // otherwise use a code page number which also matches CPython               
                if (name == null) {
                    name = "cp" + encoding.CodePage;
                }
            }

            if (name == null) {
                // otherwise just finally fall back to the human readable name
                name = encoding.EncodingName;
            }
#else
            // only has web names
            string name = encoding.WebName;
#endif

            return NormalizeEncodingName(name);
        }

        internal static string NormalizeEncodingName(string name) {
            if (name == null) {
                return null;
            }
            return name.ToLowerInvariant().Replace('-', '_').Replace(' ', '_');
        }

        /// <summary>
        /// Gets the starting offset checking to see if the incoming bytes already include a preamble.
        /// </summary>
        private static int GetStartingOffset(Encoding e, byte[] bytes) {
            byte[] preamble = e.GetPreamble();

            if (preamble.Length != 0 && bytes.Length >= preamble.Length) {
                for (int i = 0; i < preamble.Length; i++) {
                    if (bytes[i] != preamble[i]) {
                        return 0;
                    }
                }

                return preamble.Length;
            }

            return 0;
        }

        private static Traffy.Objects.TrList SplitEmptyString(bool separators) {
            Traffy.Objects.TrList ret = PythonOps.MakeEmptyList(1);
            if (separators) {
                ret.container.Add(PythonOps.BoxString(String.Empty));
            }
            return ret;
        }

        private static Traffy.Objects.TrList SplitInternal(string self, char[] seps, int maxsplit) {
            if (String.IsNullOrEmpty(self)) {
                return SplitEmptyString(seps != null);
            } else {
                string[] r;
                //  If the optional second argument sep is absent or None, the words are separated 
                //  by arbitrary strings of whitespace characters (space, tab, newline, return, formfeed);

                r = self.Split(seps, (maxsplit < 0) ? Int32.MaxValue : maxsplit + 1,
                    (seps == null) ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);

                Traffy.Objects.TrList ret = PythonOps.MakeEmptyList(r.Length);
                foreach (string s in r) ret.container.Add(PythonOps.BoxString(s));
                return ret;
            }
        }

        private static Traffy.Objects.TrList SplitInternal(string self, string separator, int maxsplit) {
            if (String.IsNullOrEmpty(self)) {
                return SplitEmptyString(separator != null);
            } else {
                string[] r = self.Split(separator, (maxsplit < 0) ? Int32.MaxValue : maxsplit + 1, StringSplitOptions.None);

                Traffy.Objects.TrList ret = PythonOps.MakeEmptyList(r.Length);
                foreach (string s in r) ret.container.Add(PythonOps.BoxString(s));
                return ret;
            }
        }

        private static void TryStringOrTuple(Traffy.Objects.TrObject prefix) {
            if (Traffy.Objects.ExtTrObject.IsNone(prefix)) {
                throw PythonOps.TypeError("expected string or Tuple, got NoneType");
            }
            if (!(prefix is Traffy.Objects.TrStr) && !(prefix is Traffy.Objects.TrTuple)) {
                throw PythonOps.TypeError("expected string or Tuple, got {0} Type", prefix.GetType());
            }
        }

        private static string GetString(Traffy.Objects.TrObject obj) {
            return Traffy.Objects.TrObjectFromString.AsStr(obj);
        }

        public static bool endswith(string self, string suffix) {
            return self.EndsWith(suffix, StringComparison.Ordinal);
        }

        //  Indexing is 0-based. Need to deal with negative indices
        //  (which mean count backwards from end of sequence)
        //  +---+---+---+---+---+
        //  | a | b | c | d | e |
        //  +---+---+---+---+---+
        //    0   1   2   3   4
        //   -5  -4  -3  -2  -1

        public static bool endswith(string self, string suffix, int start) {
            int len = self.Length;
            if (start > len) return false;
            // map the negative indice to its positive counterpart
            if (start < 0) {
                start += len;
                if (start < 0) start = 0;
            }
            return self.Substring(start).EndsWith(suffix, StringComparison.Ordinal);
        }

        //  With optional start, test beginning at that position (the char at that index is
        //  included in the test). With optional end, stop comparing at that position (the
        //  char at that index is not included in the test)
        public static bool endswith(string self, string suffix, int start, int end) {
            int len = self.Length;
            if (start > len) return false;
            // map the negative indices to their positive counterparts
            else if (start < 0) {
                start += len;
                if (start < 0) start = 0;
            }
            if (end >= len) return self.Substring(start).EndsWith(suffix, StringComparison.Ordinal);
            else if (end < 0) {
                end += len;
                if (end < 0) return false;
            }
            if (end < start) return false;
            return self.Substring(start, end - start).EndsWith(suffix, StringComparison.Ordinal);
        }

        private static bool endswith(string self, PythonTuple suffix) {
            foreach (Traffy.Objects.TrObject obj in suffix) {
                if (self.EndsWith(GetString(obj), StringComparison.Ordinal)) {
                    return true;
                }
            }
            return false;
        }

        private static bool endswith(string self, PythonTuple suffix, int start) {
            foreach (Traffy.Objects.TrObject obj in suffix) {
                if (endswith(self, GetString(obj), start)) {
                    return true;
                }
            }
            return false;
        }

        private static bool endswith(string self, PythonTuple suffix, int start, int end) {
            foreach (Traffy.Objects.TrObject obj in suffix) {
                if (endswith(self, GetString(obj), start, end)) {
                    return true;
                }
            }
            return false;
        }

        public static bool startswith(string self, string prefix) {
            return self.StartsWith(prefix, StringComparison.Ordinal);
        }

        public static bool startswith(string self, string prefix, int start) {
            int len = self.Length;
            if (start > len) return false;
            if (start < 0) {
                start += len;
                if (start < 0) start = 0;
            }
            return self.Substring(start).StartsWith(prefix, StringComparison.Ordinal);
        }

        public static bool startswith(string self, string prefix, int start, int end) {
            int len = self.Length;
            if (start > len) return false;
            // map the negative indices to their positive counterparts
            else if (start < 0) {
                start += len;
                if (start < 0) start = 0;
            }
            if (end >= len) return self.Substring(start).StartsWith(prefix, StringComparison.Ordinal);
            else if (end < 0) {
                end += len;
                if (end < 0) return false;
            }
            if (end < start) return false;
            return self.Substring(start, end - start).StartsWith(prefix, StringComparison.Ordinal);
        }

        private static bool startswith(string self, PythonTuple prefix) {
            foreach (Traffy.Objects.TrObject obj in prefix) {
                if (self.StartsWith(GetString(obj), StringComparison.Ordinal)) {
                    return true;
                }
            }
            return false;
        }

        private static bool startswith(string self, PythonTuple prefix, int start) {
            foreach (Traffy.Objects.TrObject obj in prefix) {
                if (startswith(self, GetString(obj), start)) {
                    return true;
                }
            }
            return false;
        }

        private static bool startswith(string self, PythonTuple prefix, int start, int end) {
            foreach (Traffy.Objects.TrObject obj in prefix) {
                if (startswith(self, GetString(obj), start, end)) {
                    return true;
                }
            }
            return false;
        }
        #endregion

        public static string/*!*/ __repr__(string/*!*/ self) {
            return StringOps.Quote(self);
        }
    }
}