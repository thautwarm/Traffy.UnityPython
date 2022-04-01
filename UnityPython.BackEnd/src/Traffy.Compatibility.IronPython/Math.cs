// // Licensed to the .NET Foundation under one or more agreements.
// // The .NET Foundation licenses this file to you under the Apache 2.0 License.
// // See the LICENSE file in the project root for more information.

// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Numerics;
// using Traffy.Compatibility.IronPython;

// namespace IronPython.Modules {
//     public static partial class PythonMath {
//         public const string __doc__ = "Provides common mathematical functions.";

//         public const float pi = MathF.PI;
//         public const float e = MathF.E;

//         private const float degreesToRadians = MathF.PI / 180.0f;
//         private const int Bias = 0x3FE;

//         public static float degrees(float radians) {
//             return Check(radians, radians / degreesToRadians);
//         }

//         public static float radians(float degrees) {
//             return Check(degrees, degrees * degreesToRadians);
//         }

//         public static float fmod(float v, float w) {
//             return Check(v, w, v % w);
//         }

//         private static float sum(List<float> partials) {
//             // sum the partials the same was as CPython does
//             var n = partials.Count;
//             var hi = 0.0f;

//             if (n == 0) return hi;

//             var lo = 0.0f;

//             // sum exact
//             while (n > 0) {
//                 var x = hi;
//                 var y = partials[--n];
//                 hi = x + y;
//                 lo = y - (hi - x);
//                 if (lo != 0.0f)
//                     break;
//             }

//             if (n == 0) return hi;

//             // half-even rounding
//             if (lo < 0.0f && partials[n - 1] < 0.0f || lo > 0.0f && partials[n - 1] > 0.0f) {
//                 var y = lo * 2.0f;
//                 var x = hi + y;
//                 var yr = x - hi;
//                 if (y == yr)
//                     hi = x;
//             }
//             return hi;
//         }

//         public static float fsum(IEnumerator<Traffy.Objects.TrObject> e) {
//             // msum from https://code.activestate.com/recipes/393090/
//             var partials = new List<float>();
//             while (e.MoveNext())
//             {
//                 var v = e.Current.__float__().AsFloat();
//                 var x = v;
//                 var i = 0;
//                 for (var j = 0; j < partials.Count; j++) {
//                     var y = partials[j];
//                     if (MathF.Abs(x) < MathF.Abs(y)) {
//                         var t = x;
//                         x = y;
//                         y = t;
//                     }
//                     var hi = x + y;
//                     var lo = y - (hi - x);
//                     if (lo != 0) {
//                         partials[i++] = lo;
//                     }
//                     x = hi;
//                 }
//                 partials.RemoveRange(i, partials.Count - i);
//                 partials.Add(x);
//             }

//             return sum(partials);
//         }

//         public static Traffy.Objects.TrTuple modf(float v) {
//             if (float.IsInfinity(v)) {
//                 return PythonOps.MakeNTuple(Traffy.Box.Apply(0.0f), Traffy.Box.Apply(v));
//             }
//             float w = v % 1.0f;
//             v -= w;
//             return PythonOps.MakeNTuple(Traffy.Box.Apply(w), Traffy.Box.Apply(v));
//         }

//         public static float ldexp(float v, int w) {
//             if (v == 0.0f || float.IsInfinity(v)) {
//                 return v;
//             }
//             return Check(v, v * MathF.Pow(2.0f, (float) w));
//         }

//         public static float hypot(float v, float w) {
//             if (float.IsInfinity(v) || float.IsInfinity(w)) {
//                 return float.PositiveInfinity;
//             }
//             return Check(v, w, MathUtils.Hypot(v, w));
//         }

//         public static float pow(float v, float exp) {
//             if (v == 1.0f || exp == 0.0f) {
//                 return 1.0f;
//             } else if (float.IsNaN(v) || float.IsNaN(exp)) {
//                 return float.NaN;
//             } else if (v == 0.0f) {
//                 if (exp > 0.0f) {
//                     return 0.0f;
//                 }
//                 throw PythonOps.ValueError("math domain error");
//             } else if (float.IsPositiveInfinity(exp)) {
//                 if (v > 1.0 || v < -1.0) {
//                     return float.PositiveInfinity;
//                 } else if (v == -1.0) {
//                     return 1.0f;
//                 } else {
//                     return 0.0f;
//                 }
//             } else if (float.IsNegativeInfinity(exp)) {
//                 if (v > 1.0f || v < -1.0f) {
//                     return 0.0f;
//                 } else if (v == -1.0f) {
//                     return 1.0f;
//                 } else {
//                     return float.PositiveInfinity;
//                 }
//             }
//             return Check(v, exp, MathF.Pow(v, exp));
//         }

//         public static float log(float v0) {
//             if (v0 <= 0.0f) {
//                 throw PythonOps.ValueError("math domain error");
//             }
//             return Check(v0, MathF.Log(v0));
//         }

//         public static float log(float v0, float v1) {
//             if (v0 <= 0.0f || v1 == 0.0f) {
//                 throw PythonOps.ValueError("math domain error");
//             } else if (v1 == 1.0f) {
//                 throw PythonOps.ZeroDivisionError("float division");
//             } else if (v1 == Double.PositiveInfinity) {
//                 return 0.0f;
//             }
//             return Check(MathF.Log(v0, v1));
//         }

//         public static float log(int value) {
            
//             return log((float) value);
//         }

//         public static float log(int value, float newBase) {
//             if (newBase <= 0.0f || value <= 0f) {
//                 throw PythonOps.ValueError("math domain error");
//             } else if (newBase == 1.0f) {
//                 throw PythonOps.ZeroDivisionError("float division");
//             } else if (newBase == Double.PositiveInfinity) {
//                 return 0.0f;
//             }
//             return Check(MathF.Log(value, newBase));
//         }

//         public static float log2(float x) {
//             if (x <= 0) throw PythonOps.ValueError("math domain error");
//             if (float.IsPositiveInfinity(x) || float.IsNaN(x)) return x;
//             return MathF.Log(x, 2);
//         }

//         public static float log2(int x) {
//             if (x <= 0) throw PythonOps.ValueError("math domain error");
//             var d = (float)x;
//             return log2(d);
//         }

//         public static float log10(float v0) {
//             if (v0 <= 0.0) {
//                 throw PythonOps.ValueError("math domain error");
//             }
//             return Check(v0, MathF.Log10(v0));
//         }

//         public static float log10(int value) {
//             if (value <= 0) {
//                 throw PythonOps.ValueError("math domain error");
//             }
//             return MathF.Log10(value);
//         }

//         public static float log1p(float v0) {
//             // Calculate log(1.0 + v0) using William Kahan's algorithm for numerical precision

//             if (float.IsPositiveInfinity(v0)) {
//                 return float.PositiveInfinity;
//             }

//             float v1 = v0 + 1.0f;

//             // Linear approximation for very small v0
//             if (v1 == 1.0) {
//                 return v0;
//             }

//             // Apply correction factor
//             return log(v1) * (v0 / (v1 - 1.0f));
//         }

//         public static float log1p(int value) {
//             return log(value + 1);
//         }


//         public static float expm1(float v0) {
//             return Check(v0, MathF.Tanh(v0 / 2.0f) * (MathF.Exp(v0) + 1.0f));
//         }

//         public static float asinh(float v0) {
//             if (v0 == 0.0 || float.IsInfinity(v0)) {
//                 return v0;
//             }
//             // rewrote ln(v0 + sqrt(v0**2 + 1)) for precision
//             if (MathF.Abs(v0) > 1.0) {
//                 return MathF.Sign(v0) * (MathF.Log(MathF.Abs(v0)) + MathF.Log(1.0 + MathUtils.Hypot(1.0f, 1.0f / v0)));
//             } else {
//                 return MathF.Log(v0 + MathUtils.Hypot(1.0f, v0));
//             }
//         }

//         public static float acosh(float v0) {
//             if (v0 < 1.0) {
//                 throw PythonOps.ValueError("math domain error");
//             } else if (float.IsPositiveInfinity(v0)) {
//                 return float.PositiveInfinity;
//             }
//             // rewrote ln(v0 + sqrt(v0**2 - 1)) for precision
//             float c = MathF.Sqrt(v0 + 1.0f);
//             return MathF.Log(c) + MathF.Log(v0 / c + MathF.Sqrt(v0 - 1.0f));
//         }


//         public static float atanh(float v0) {
//             if (v0 >= 1.0 || v0 <= -1.0) {
//                 throw PythonOps.ValueError("math domain error");
//             } else if (v0 == 0.0) {
//                 // preserve +/-0.0
//                 return v0;
//             }

//             return MathF.Log((1.0f + v0) / (1.0f - v0)) * 0.5f;
//         }

//         public static float atanh(int value) {
//             if (value == 0) {
//                 return 0;
//             } else {
//                 throw PythonOps.ValueError("math domain error");
//             }
//         }

//         public static float atan2(float v0, float v1) {
//             if (float.IsNaN(v0) || float.IsNaN(v1)) {
//                 return float.NaN;
//             } else if (float.IsInfinity(v0)) {
//                 if (float.IsPositiveInfinity(v1)) {
//                     return pi * 0.25f * MathF.Sign(v0);
//                 } else if (float.IsNegativeInfinity(v1)) {
//                     return pi * 0.75f * MathF.Sign(v0);
//                 } else {
//                     return pi * 0.5f * MathF.Sign(v0);
//                 }
//             } else if (float.IsInfinity(v1)) {
//                 return v1 > 0.0f ? 0.0f : pi * MathF.Sign(v0);
//             }
//             return MathF.Atan2(v0, v1);
//         }

//         public static object ceil(CodeContext context, object x) {
//             object val;
//             if (PythonTypeOps.TryInvokeUnaryOperator(context, x, "__ceil__", out val)) {
//                 return val;
//             }

//             throw PythonOps.TypeError("a float is required");
//         }

//         public static object ceil(float v0) {
//             if (float.IsInfinity(v0)) throw PythonOps.OverflowError("cannot convert float infinity to integer");
//             if (float.IsNaN(v0)) throw PythonOps.ValueError("cannot convert float NaN to integer");

//             var res = MathF.Ceiling(v0);
//             if (res < int.MinValue || res > int.MaxValue) {
//                 return (BigInteger)res;
//             }
//             return (int)res;
//         }

//         /// <summary>
//         /// Error function on real values
//         /// </summary>
//         public static float erf(float v0) {
//             return MathUtils.Erf(v0);
//         }

//         /// <summary>
//         /// Complementary error function on real values: erfc(x) =  1 - erf(x)
//         /// </summary>
//         public static float erfc(float v0) {
//             return MathUtils.ErfComplement(v0);
//         }

//         public static object factorial(float v0) {
//             if (v0 % 1.0 != 0.0) {
//                 throw PythonOps.ValueError("factorial() only accepts integral values");
//             }
//             return factorial((BigInteger)v0);
//         }

//         public static object factorial(BigInteger value) {
//             if (value < 0) {
//                 throw PythonOps.ValueError("factorial() not defined for negative values");
//             }
//             if (value > SysModule.maxsize) {
//                 throw PythonOps.OverflowError("factorial() argument should not exceed {0}", SysModule.maxsize);
//             }

//             BigInteger val = 1;
//             for (BigInteger mul = value; mul > BigInteger.One; mul -= BigInteger.One) {
//                 val *= mul;
//             }

//             if (val > int.MaxValue) {
//                 return val;
//             }
//             return (int)val;
//         }

//         public static object factorial(object value) {
//             // CPython tries float first, then float, so we need
//             // an explicit overload which properly matches the order here
//             float val;
//             if (Converter.TryConvertToDouble(value, out val)) {
//                 return factorial(val);
//             } else {
//                 return factorial(Converter.ConvertToBigInteger(value));
//             }
//         }

//         public static object floor(CodeContext context, object x) {
//             object val;
//             if (PythonTypeOps.TryInvokeUnaryOperator(context, x, "__floor__", out val)) {
//                 return val;
//             }

//             throw PythonOps.TypeError("a float is required");
//         }

//         public static object floor(float v0) {
//             if (float.IsInfinity(v0)) throw PythonOps.OverflowError("cannot convert float infinity to integer");
//             if (float.IsNaN(v0)) throw PythonOps.ValueError("cannot convert float NaN to integer");

//             var res = MathF.Floor(v0);
//             if (res < int.MinValue || res > int.MaxValue) {
//                 return (BigInteger)res;
//             }
//             return (int)res;
//         }

//         /// <summary>
//         /// Gamma function on real values
//         /// </summary>
//         public static float gamma(float v0) {
//             return Check(v0, MathUtils.Gamma(v0));
//         }

//         /// <summary>
//         /// Natural log of absolute value of Gamma function
//         /// </summary>
//         public static float lgamma(float v0) {
//             return Check(v0, MathUtils.LogGamma(v0));
//         }

//         public static object trunc(CodeContext/*!*/ context, object value) {
//             object func;
//             if (PythonOps.TryGetBoundAttr(value, "__trunc__", out func)) {
//                 return PythonOps.CallWithContext(context, func);
//             } else {
//                 throw PythonOps.TypeError("type {0} doesn't define __trunc__ method", PythonOps.GetPythonTypeName(value));
//             }
//         }

//         public static bool isfinite(float x) {
//             return !float.IsInfinity(x) && !float.IsNaN(x);
//         }

//         public static bool isinf(float v0) {
//             return float.IsInfinity(v0);
//         }

//         public static bool isinf(BigInteger value) {
//             return false;
//         }

//         public static bool isinf(object value) {
//             // CPython tries float first, then float, so we need
//             // an explicit overload which properly matches the order here
//             float val;
//             if (Converter.TryConvertToDouble(value, out val)) {
//                 return isinf(val);
//             }
//             return false;
//         }

//         public static bool isnan(float v0) {
//             return float.IsNaN(v0);
//         }

//         public static bool isnan(BigInteger value) {
//             return false;
//         }

//         public static bool isnan(object value) {
//             // CPython tries float first, then float, so we need
//             // an explicit overload which properly matches the order here
//             float val;
//             if (Converter.TryConvertToDouble(value, out val)) {
//                 return isnan(val);
//             }
//             return false;
//         }

//         public static float copysign(float x, float y) {
//             return DoubleOps.CopySign(x, y);
//         }

//         public static float copysign(object x, object y) {
//             float val, sign;
//             if (!Converter.TryConvertToDouble(x, out val) ||
//                 !Converter.TryConvertToDouble(y, out sign)) {
//                 throw PythonOps.TypeError("TypeError: a float is required");
//             }
//             return DoubleOps.CopySign(val, sign);
//         }

//         // new in CPython 3.5
//         public static object gcd(BigInteger x, BigInteger y) {
//             var res = BigInteger.GreatestCommonDivisor(x, y);
//             if (res.AsInt32(out var i))
//                 return i;
//             return res;
//         }

//         public static object gcd(object x, object y) {
//             return gcd(ObjectToBigInteger(x), ObjectToBigInteger(y));

//             static BigInteger ObjectToBigInteger(object x) {
//                 BigInteger a;
//                 switch (PythonOps.Index(x)) {
//                     case int i:
//                         a = i;
//                         break;
//                     case BigInteger bi:
//                         a = bi;
//                         break;
//                     default:
//                         throw new InvalidOperationException();
//                 }
//                 return a;
//             }
//         }

//         public static readonly float nan = float.NaN; // new in CPython 3.5

//         public static readonly float inf = float.PositiveInfinity; // new in CPython 3.5

//         // new in CPython 3.5
//         public static bool isclose(float a, float b, float rel_tol = 1e-09, float abs_tol = 0.0) {
//             if (rel_tol < 0 || abs_tol < 0) throw PythonOps.ValueError("tolerances must be non-negative");
//             //if (float.IsNaN(a) || float.IsNaN(b)) return false;
//             if (a == b) return true;
//             if (float.IsInfinity(a) || float.IsInfinity(b)) return false;
//             return MathF.Abs(a - b) <= MathF.Max(rel_tol * MathF.Max(MathF.Abs(a), MathF.Abs(b)), abs_tol);
//         }

//         #region Private Implementation Details

//         private static void SetExponentLe(byte[] v, int exp) {
//             exp += Bias;
//             ushort oldExp = LdExponentLe(v);
//             ushort newExp = (ushort)(oldExp & 0x800f | (exp << 4));
//             StExponentLe(v, newExp);
//         }

//         private static int IntExponentLe(byte[] v) {
//             ushort exp = LdExponentLe(v);
//             return ((int)((exp & 0x7FF0) >> 4) - Bias);
//         }

//         private static ushort LdExponentLe(byte[] v) {
//             return (ushort)(v[6] | ((ushort)v[7] << 8));
//         }

//         private static long LdMantissaLe(byte[] v) {
//             int i1 = (v[0] | (v[1] << 8) | (v[2] << 16) | (v[3] << 24));
//             int i2 = (v[4] | (v[5] << 8) | ((v[6] & 0xF) << 16));

//             return i1 | (i2 << 32);
//         }

//         private static void StExponentLe(byte[] v, ushort e) {
//             v[6] = (byte)e;
//             v[7] = (byte)(e >> 8);
//         }

//         private static bool IsDenormalizedLe(byte[] v) {
//             ushort exp = LdExponentLe(v);
//             long man = LdMantissaLe(v);

//             return ((exp & 0x7FF0) == 0 && (man != 0));
//         }

//         private static void DecomposeLe(byte[] v, out float m, out int e) {
//             if (IsDenormalizedLe(v)) {
//                 m = BitConverter.ToSingle(v, 0);
//                 m *= MathF.Pow(2.0f, 1022);
//                 v = BitConverter.GetBytes(m);
//                 e = IntExponentLe(v) - 1022;
//             } else {
//                 e = IntExponentLe(v);
//             }

//             SetExponentLe(v, 0);
//             m = BitConverter.ToDouble(v, 0);
//         }

//         private static float Check(float v) {
//             return PythonOps.CheckMath(v);
//         }

//         private static float Check(float input, float output) {
//             return PythonOps.CheckMath(input, output);
//         }

//         private static float Check(float in0, float in1, float output) {
//             return PythonOps.CheckMath(in0, in1, output);
//         }

//         #endregion
//     }
// }