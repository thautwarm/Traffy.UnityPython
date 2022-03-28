using System.Linq;
using System.Runtime.CompilerServices;
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
    }

}