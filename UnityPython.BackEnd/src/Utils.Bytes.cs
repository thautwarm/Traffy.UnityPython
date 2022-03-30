using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Traffy.Objects;

namespace Traffy
{
    public static class BytesUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte CheckByte(TrObject cur)
        {
            if (!(cur is TrInt cur_i))
            {
                throw new TypeError($"{cur.Class.Name} object cannot be interpreted as an integer.");
            }
            return CheckByte(cur_i);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte CheckByte(TrInt cur_i)
        {
            var integer = cur_i.value;
            if (integer > 255 || integer < 0)
            {
                throw new ValueError($"bytes-like integer must be in range(0, 256)");
            }
            return unchecked((byte)integer);
        }

        public static bool CheckByte(TrObject cur, out byte val)
        {
            if (!(cur is TrInt cur_i))
            {
                val = default(byte);
                return false;
            }
            var integer = cur_i.value;
            if (integer > 255 || integer < 0)
            {
                val = default(byte);
                return false;
            }
            val = unchecked((byte)integer);
            return true;
        }

        public static List<byte> ObjectToByteArray(TrObject self)
        {
            var itr = self.__iter__();
            var res = new List<byte>();
            while (itr.MoveNext())
            {
                if (CheckByte(itr.Current, out var val))
                {
                    res.Add(val);
                }
                else
                {
                    throw new TypeError($"requires a sequence of integers in range(0, 255), got an element {itr.Current.__repr__()}.");
                }
            }
            return res;
        }

        public static string Hex<TList>(TList contents, TrObject sep = null, int bytes_per_sep = 0) where TList: IList<byte>
        {
            var s = new StringBuilder();
            if (sep != null)
            {
                if (bytes_per_sep == 0)
                {
                    for (int i = 0; i < contents.Count; i++)
                    {
                        if (i != 0)
                            s.Append(sep.__str__());
                        s.Append(contents[i].ToString("x2"));
                    }

                }
                else if (bytes_per_sep > 0)
                {
                    for (int i = 0; i < contents.Count; i++)
                    {
                        if (((i + contents.Count) % bytes_per_sep == 0) && (i != 0))
                        {
                            s.Append(sep.__str__());
                        }
                        s.Append(contents[i].ToString("x2"));
                    }
                }
                else
                {
                    bytes_per_sep = -bytes_per_sep;
                    for (int i = 0; i < contents.Count; i++)
                    {
                        if ((i % bytes_per_sep == 0) && (i != 0))
                        {
                            s.Append(sep.__str__());
                        }
                        s.Append(contents[i].ToString("x2"));
                    }
                }
            }
            else
            {
                for (int i = 0; i < contents.Count; i++)
                {
                    s.Append(contents[i].ToString("x2"));
                }
            }
            return s.ToString();
        }
    }

}