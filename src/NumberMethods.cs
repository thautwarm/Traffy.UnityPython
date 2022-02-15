using System;
using System.Runtime.CompilerServices;
using int_t = System.Int64;
using uint_t = System.UInt64;

namespace Traffy.Objects
{

#if NUNITY
using static System.MathF;
#else
using static UnityEngine.Mathf;
#endif

    public static class NumberMethods
    {
        static int_t s_intmod(int_t a, int_t b)
        {
            int_t r = a % b;
            return r < 0 ? r + b : r;
        }

        static float s_floatmod(float a, float b)
        {
            float r = a % b;
            return r < 0 ? r + b : r;
        }

        public static Exception unsupported_ops(TrObject lhs, string op, TrObject rhs) =>
            new InvalidOperationException($"'unsupported operation: '{lhs.Class.Name}' {op} '{rhs.Class.Name}'.");

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static TrObject int_t_add(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrInt v: return MK.Int(self.value + v.value);
                case TrFloat v: return MK.Float(self.value + v.value);
                default:
                    throw unsupported_ops(self, "+", other);
            }
        }

        public static TrObject float_add(TrFloat self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return MK.Float(self.value + v.value);
                case TrInt v: return MK.Float(self.value + v.value);
                default:
                    throw unsupported_ops(self, "+", other);
            }
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static TrObject int_t_sub(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return MK.Float(self.value - v.value);
                case TrInt v: return MK.Int(self.value - v.value);
                default:
                    throw unsupported_ops(self, "-", other);
            }
        }

        public static TrObject float_sub(TrFloat self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return MK.Float(self.value - v.value);
                case TrInt v: return MK.Float(self.value - v.value);
                default:
                    throw unsupported_ops(self, "-", other);
            }
        }

        public static TrObject int_t_mul(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return MK.Float(self.value * v.value);
                case TrInt v: return MK.Int(self.value * v.value);
                default:
                    throw unsupported_ops(self, "*", other);
            }
        }

        public static TrObject float_mul(TrFloat self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return MK.Float(self.value * v.value);
                case TrInt v: return MK.Float(self.value * v.value);
                default:
                    throw unsupported_ops(self, "*", other);
            }
        }

        public static TrObject int_t_floordiv(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return MK.Int((int_t) (self.value / v.value));
                case TrInt v: return MK.Int(self.value / v.value);
                default:
                    throw unsupported_ops(self, "/", other);
            }
        }

        public static TrObject float_floordiv(TrFloat self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return MK.Int((int_t) (self.value / v.value));
                case TrInt v: return MK.Int((int_t) (self.value / v.value));
                default:
                    throw unsupported_ops(self, "/", other);
            }
        }

        public static TrObject int_t_truediv(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return MK.Float(self.value / v.value);
                case TrInt v: return MK.Float(((float) self.value) / v.value);
                default:
                    throw unsupported_ops(self, "/", other);
            }
        }

        public static TrObject float_truediv(TrFloat self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return MK.Float(self.value / v.value);
                case TrInt v: return MK.Float(self.value / v.value);
                default:
                    throw unsupported_ops(self, "/", other);
            }
        }


        public static TrObject int_t_mod(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return MK.Float(s_floatmod(((float) self.value), v.value));
                case TrInt v: return MK.Int(s_intmod(self.value, v.value));
                default:
                    throw unsupported_ops(self, "%", other);
            }
        }

        public static TrObject float_mod(TrFloat self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return MK.Float(s_floatmod(self.value, v.value));
                case TrInt v: return MK.Float(s_floatmod(self.value, v.value));
                default:
                    throw unsupported_ops(self, "%", other);
            }
        }


        static int_t s_intpow(int_t b, int_t pow)
        {
            int_t result = 1;
            while (pow > 0)
            {
                if ((pow & 1) != 0)
                {
                    result *= b;
                }

                pow >>= 1;
                b *= b;
            }

            return result;
        }

        static float f_intpow(float b, int_t pow)
        {
            float result = 1;
            while (pow > 0)
            {
                if ((pow & 1) != 0)
                {
                    result *= b;
                }

                pow >>= 1;
                b *= b;
            }

            return result;
        }

        public static TrObject int_t_pow(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return MK.Float(Pow(self.value, v.value));
                case TrInt v:
                    var i = v.value;
                    if (i < 0)
                        return MK.Float(Pow(self.value, v.value));

                    return MK.Int(s_intpow(self.value, v.value));
                default:
                    throw unsupported_ops(self, "**", other);
            }
        }

        public static TrObject float_pow(TrFloat self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return MK.Float(Pow(self.value, v.value));
                case TrInt v:
                    var i = v.value;
                    if (i < 0)
                        return MK.Float(Pow(self.value, v.value));
                    return MK.Float(f_intpow(self.value, v.value));
                default:
                    throw unsupported_ops(self, "**", other);
            }
        }

        public static TrObject int_t_lshift(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: throw unsupported_ops(self, "<<", other);
                case TrInt v:
                    return MK.Int(unchecked((uint_t) self.value << (int) v.value));


                default:
                    throw unsupported_ops(self, "<<", other);
            }
        }

        public static TrObject int_t_rshift(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: throw unsupported_ops(self, ">>", other);
                case TrInt v:
                    return MK.Int(unchecked((uint_t) self.value >> (int) v.value));
                default:
                    throw unsupported_ops(self, ">>", other);
            }
        }

        public static TrObject int_t_bitand(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: throw unsupported_ops(self, "&", other);
                case TrInt v: return MK.Int(self.value & v.value);
                default:
                    throw unsupported_ops(self, "&", other);
            }
        }

        public static TrObject int_t_bitor(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: throw unsupported_ops(self, "|", other);
                case TrInt v: return MK.Int(self.value | v.value);
                default:
                    throw unsupported_ops(self, "|", other);
            }
        }

        public static TrObject int_t_bitxor(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: throw unsupported_ops(self, "^", other);
                case TrInt v: return MK.Int(self.value ^ v.value);
                default:
                    throw unsupported_ops(self, "^", other);
            }
        }


        public static bool int_t_lt(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return (float) self.value < v.value;
                case TrInt v: return self.value < v.value;
                default:
                    throw unsupported_ops(self, "<", other);
            }
        }

        public static bool int_t_le(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return (float) self.value <= v.value;
                case TrInt v: return self.value <= v.value;
                default:
                    throw unsupported_ops(self, "<=", other);
            }
        }

        public static bool int_t_gt(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return (float) self.value > v.value;
                case TrInt v: return self.value > v.value;
                default:
                    throw unsupported_ops(self, ">", other);
            }
        }

        public static bool int_t_ge(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return (float) self.value >= v.value;
                case TrInt v: return self.value >= v.value;
                default:
                    throw unsupported_ops(self, ">=", other);
            }
        }

        public static bool int_t_eq(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return self.value == v.value;
                case TrInt v: return self.value == v.value;
                default:
                    return false;
            }
        }

        public static bool int_t_ne(TrInt self, TrObject other)
        {
            switch (other)
            {
                case TrFloat v: return self.value != v.value;
                case TrInt v: return self.value != v.value;
                default:
                    return true;
            }
        }
    }

    public partial class TrFloat
    {
        public TrObject __add__(TrObject o) => NumberMethods.float_add(this, o);
        public TrObject __sub__(TrObject o) => NumberMethods.float_sub(this, o);

        public TrObject __mul__(TrObject o) => NumberMethods.float_mul(this, o);

        public TrObject __floordiv__(TrObject o) => NumberMethods.float_floordiv(this, o);
        public TrObject __truediv__(TrObject o) => NumberMethods.float_truediv(this, o);

        public TrObject __mod__(TrObject o) => NumberMethods.float_mod(this, o);

        public TrObject __pow__(TrObject o) => NumberMethods.float_pow(this, o);
    }

    public partial class TrInt
    {
        public TrObject __add__(TrObject o) => NumberMethods.int_t_add(this, o);
        public TrObject __sub__(TrObject o) => NumberMethods.int_t_sub(this, o);

        public TrObject __mul__(TrObject o) => NumberMethods.int_t_mul(this, o);

        public TrObject __floordiv__(TrObject o) => NumberMethods.int_t_floordiv(this, o);
        public TrObject __truediv__(TrObject o) => NumberMethods.int_t_truediv(this, o);

        public TrObject __mod__(TrObject o) => NumberMethods.int_t_mod(this, o);

        public TrObject __pow__(TrObject o) => NumberMethods.int_t_pow(this, o);

        public TrObject __lshift__(TrObject o) => NumberMethods.int_t_lshift(this, o);

        public TrObject __rshift__(TrObject o) => NumberMethods.int_t_rshift(this, o);

        public bool __eq__(TrObject o) => NumberMethods.int_t_eq(this, o);

        public bool __ne__(TrObject o) => NumberMethods.int_t_ne(this, o);

        public bool __lt__(TrObject o) => NumberMethods.int_t_lt(this, o);

        public bool __gt__(TrObject o) => NumberMethods.int_t_gt(this, o);

        public bool __le__(TrObject o) => NumberMethods.int_t_le(this, o);

        public bool __ge__(TrObject o) => NumberMethods.int_t_ge(this, o);

        public TrObject __bitand__(TrObject o) => NumberMethods.int_t_bitand(this, o);
        public TrObject __bitor__(TrObject o) => NumberMethods.int_t_bitor(this, o);
        public TrObject __bitxor__(TrObject o) => NumberMethods.int_t_bitxor(this, o);
    }
}