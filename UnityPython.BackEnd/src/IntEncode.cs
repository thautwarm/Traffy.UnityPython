using System;
using System.Collections.Generic;

namespace Traffy.Utils
{
    public static class IntEncoding
    {
        private const int BIT_END = 0b_0100_0000;

        enum STATE
        {
            EXTEND1, // extending the first 32-bit integer
            EXTEND2, // extending the second 32-bit integer
            ENDING
        }

        class Decoder
        {
            public int first = 0;
            public int second = 0;
        }


        // in an array of 32-bit integers;
        // such an array is can be flatten to a byte array,
        // where each pair of 2 32-bit integers is encoded compactly.
        // 'retrieve' is the inverse operation.

        public static T[] decode<T>(this uint[] encoded, Func<int, int, T> construct)
        {
            var result = new List<T>(1024);
            var decoder = new Decoder();
            var state = STATE.EXTEND1;
            // perform action on each byte
            STATE action(uint x, STATE state, List<T> result)
            {
                switch (state)
                {
                    case STATE.EXTEND1:
                        if ((x & 0x80) == 0)
                        {
                            decoder.first = (decoder.first << 7) | (int) x;
                            return state;
                        }
                        else
                        {
                            return STATE.EXTEND2;
                        }
                    case STATE.EXTEND2:
                        if ((x & 0x80) == 0)
                        {
                            decoder.second = (decoder.second << 7) | (int) x;
                            return state;
                        }
                        else
                        {
                            result.Add(construct(decoder.first, decoder.second));
                            decoder.first = 0;
                            decoder.second = 0;
                            var flag = (int)x & BIT_END;
                            if (flag == 0)
                            {
                                return STATE.EXTEND1;
                            }
                            else
                            {
                                return STATE.ENDING;
                            }
                        }
                    case STATE.ENDING:
                        return state;
                    default:
                        throw new Exception("invalid state");
                }
            }
            for (int i = 0; i < encoded.Length; i++)
            {
                state = action(encoded[i] >> 24, state, result);
                state = action((encoded[i] & 0x00_ff_00_00) >> 16, state, result);
                state = action((encoded[i] & 0x00_00_ff_00) >> 8, state, result);
                state = action(encoded[i] & 0x00_00_00_ff, state, result);
            }
            return result.ToArray();
        }
        // encode a sequence of objects to a sequence of 32-bit integers;
        // the reverse operation is 'retrieve'.
        public static int[] encode<T>(T[] data, Func<T, (int, int)> deconstruct)
        {
            var result = new List<sbyte>();
            var buffer = new List<sbyte>();
            for (int i = 0; i < data.Length; i++)
            {
                var (first, second) = deconstruct(data[i]);
                var valueToEncode = first;

                while (valueToEncode != 0)
                {
                    buffer.Add((sbyte)(valueToEncode & 0b0111_1111));
                    valueToEncode >>= 7;
                }

                for (int j = buffer.Count - 1; j >= 0; j--)
                {
                    result.Add(buffer[i]);
                }
                buffer.Clear();
                result.Add(unchecked((sbyte)0b1000_0000));

                valueToEncode = second;

                while (valueToEncode != 0)
                {
                    buffer.Add((sbyte)(valueToEncode & 0b0111_1111));
                    valueToEncode >>= 7;
                }

                for (int j = buffer.Count - 1; j >= 0; j--)
                {
                    result.Add(buffer[i]);
                }
                buffer.Clear();
                result.Add(unchecked((sbyte)0b1000_0000));
            }
            result[result.Count - 1] |= BIT_END;

            var left = result.Count % 4;
            if (left != 0)
            {
                for (int i = 0; i < 4 - left; i++)
                {
                    result.Add(0);
                }
            }

            var int_array = new int[result.Count / 4];
            for (int i = 0, j = 0; i < result.Count; i += 4)
            {
                var value = 0;
                value |= result[i + 0] << 24;
                value |= result[i + 1] << 16;
                value |= result[i + 2] << 8;
                value |= (int)result[i + 3];
                int_array[j++] = value;
            }
            return int_array;
        }
    }
}