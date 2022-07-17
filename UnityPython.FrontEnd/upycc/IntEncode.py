from __future__ import annotations
from enum import IntEnum
import typing

_T = typing.TypeVar('_T')

class STATE(IntEnum):
    EXTEND1 = 0
    EXTEND2 = 1
    ENDING = 2

class Decoder:
    def __init__(self) -> None:
        self.first = 0
        self.second = 0

BIT_END = 0b_0100_0000

def decode(encoded: list[int], construct: typing.Callable[[int, int], _T]) -> list[_T]:
    result : list[_T] = []
    decoder = Decoder()
    state = STATE.EXTEND1

    def action(sbyte: int, state: STATE, result: list[_T]) -> STATE:
        if state is STATE.EXTEND1:
            if (sbyte & 0x80) == 0:
                decoder.first = (decoder.first << 7) | sbyte
                return state
            else:
                return STATE.EXTEND2
        elif state is STATE.EXTEND2:
            if sbyte & 0x80 == 0:
                decoder.second = (decoder.second << 7) | sbyte
                return state
            else:
                result.append(construct(decoder.first, decoder.second))
                decoder.first = 0
                decoder.second = 0
                flag = sbyte & BIT_END
                if not flag:
                    return STATE.EXTEND1
                return STATE.ENDING
        elif state is STATE.ENDING:
            return state
        else:
            raise Exception("Invalid state")

    for i in encoded:
        state = action(i >> 24, state, result)
        state = action((i & 0x00_ff_00_00) >> 16, state, result)
        state = action((i & 0x00_00_ff_00) >> 8, state, result)
        state = action(i & 0x00_00_00_ff, state, result)
    return result


def encode(data: list[tuple[int, int]]) -> list[int]:
    buffer= bytearray()
    result = bytearray()
    append = bytearray.append
    extend = bytearray.extend
    for datum in data:
        first, second = datum

        val_to_encode = first
        while val_to_encode:
            append(buffer, val_to_encode & 0b01111111)
            val_to_encode >>= 7
        # print(buffer)
        # print(list(map(bin, list(buffer))))
        extend(result, reversed(buffer))
        buffer = bytearray()
        append(result, 0b1000_0000)
        # print(list(map(bin, list(result))))

        val_to_encode = second
        while val_to_encode:
            append(buffer, val_to_encode & 0b01111111)
            val_to_encode >>= 7
        extend(result, reversed(buffer))
        buffer = bytearray()
        append(result, 0b1000_0000)

    result[-1] |= BIT_END
    int_arr: list[int] = []
    left = len(result) % 4
    if left:
        extend(result, [0] * (4 - left))

    for i in range(0, len(result), 4):
        value = result[i] << 8
        value += result[i + 1]
        value <<= 8
        value += result[i + 2]
        value <<= 8
        value += result[i + 3]
        int_arr.append(value)
    return int_arr


# import numpy as np

# xs = [12]
# ys = [2555]


# def m_cons(a, b):
#     return (a, b)

# def m_decons(ab):
#     return ab[0], ab[1]

# sizes: set[int] = set(np.random.randint(700, 1000, size = 500))
# for i, size in enumerate(sizes):
#     xs = np.random.randint(0, 0x23, size = size)
#     ys = np.random.randint(0, 0x23, size = size)
#     data = list(zip(xs, ys))
#     encoded = encode(data, m_decons)
#     decoded = decode(encoded, m_cons)
#     assert data == decoded, (data[:5], decoded[:5])


# data = list(zip(xs, ys))
# encoded = encode(data, m_decons)
# encoded_str = ''.join(map(lambda x: bin(x)[2:].zfill(32), encoded))
# # print binary representation of encoded, 8 bit per line
# print("===== Encoded =====")
# for i in range(0, len(encoded_str), 8):
#     print(encoded_str[i:i+8])
# print("===== End =====")

# print(encoded)
# decoded = decode(encoded, m_cons)
# print('data', data, 'decoded', decoded)
# assert data == decoded

