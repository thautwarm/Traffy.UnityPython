from .utils import testsuite

with testsuite("bytearray"):
    pass
    xs = bytearray([1, 2, 3])

    assert xs == bytearray(b'\1\2\3')

    print(bytearray("xs", "utf-8"))

    assert xs * 0 == bytearray()
    assert xs * 3 == bytearray([1, 2, 3, 1, 2, 3, 1, 2, 3])

    assert xs.pop() == 3
    xs.extend(b'123')
    assert xs == bytearray([1, 2, 49, 50, 51])

    assert bytearray([1, 2]) + b'123'  == bytearray([1, 2, 49, 50, 51])

    del xs[::]
    assert xs == bytearray()

    xs.extend((i for i in [0, 1, 2, 3, 4, 5]))
    assert xs[1:5:2] == bytearray([1, 3])
    assert xs is not xs.copy()
    assert xs == xs.copy()

    assert bytearray(b'abc').islower()
    assert not bytearray(b'ABc').islower()
    assert bytearray(b'ABC').isupper()
    assert not bytearray(b'AbC').isupper()

    xs = bytearray(b'abc')
    xs.reverse()
    assert xs == bytearray(b'cba')

    assert bytearray(b'1231').center(10, b'0') == bytearray(b'0001231000')
    assert bytearray(b'fad ksad Il').capitalize() == bytearray(b'Fad ksad il')
    
    value = bytearray([0xb9, 0x01, 0xef])
    assert value.hex() == 'b901ef'
    assert value.hex(":") == 'b9:01:ef'
    assert value.hex(":", 2) == 'b9:01ef'
    assert value.hex(':', -2) == 'b901:ef'

    assert bytearray(b'cc').join(
        [
            b"A",
            b"B",
            b"b",
            b"a"
        ]
    ) == bytearray(b'AccBccbcca')

    xs = bytearray(b"xs")
    assert xs.pop() == ord('s')
