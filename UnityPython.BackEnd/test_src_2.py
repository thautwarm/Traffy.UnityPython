x = "-2" <= "-5"
print(type(x), x)

class S:
    x = 1
    def __init__(self, x):
        pass

# print(list(map(lambda x: x // 0, [1, 2, 3])))
print(S(1))
x = "asda"
print([1, 2, 3][2])
x = [1, 2]
x.append(1)
print(x)
x.append(5)
print(x)

print(int.from_bytes(b'\x00\x01\x00\x01\x00\x00\x00\x01', 'little'))

class X:
    u = 5
    def __init__(self, x):
        self.x = x
        self.y = x + 1
    def __enter__(self):
        print("__enter__", self.x)
        return self.x
    def __exit__(self, exc_type, exc_val, exc_tb):
        print("__exit__", self.x)

with X(2):
    pass

def f():
    try:
        return 1
    finally:
        print("final 1")
        print("final 2")
    print("after final")

with X(2) as a, X(3) as b:
    print(a == 2, b == 3)


# try:
#     1//0
# except:
#     print("except")
#     raise
# print(S())

a = 1
print((f"{a} {'c'}").__repr__())

print((f"{a} {'c'!r}").__repr__())

# x = X(1)

# def test1():
#     for i in range(1000000):
#         z = x.y


# def test2():
#     for i in range(1000000):
#         z = x

def testfunc(f):
    a = time()
    print("res", f())
    print(time() - a)

# testfunc(test1)
# testfunc(test2)

def _test1():
    x = 0
    for i in range(10000000):
        x += 1
        yield x

def test1():
    x = list(_test1())
    print(x[50])
    return len(x)

def test2():
    x = 0
    while x < 10000000:
        x += 1
    return x

class XX:
    xx =5
    def __init__(self, x):
        self.x = x



# testfunc(test1)
# testfunc(test2)

# testfunc(test1)
# testfunc(test2)

import TestModules as mm
print(mm.add1(5))

def a0():
    yield 1
    yield 2
    return 8

async def a1():
    await a0()
    print("x", (yield 3))
    return 1

def a2():
    x = yield from a1()
    print(x)


print(list(a2()))

x = 5
print([x for x in range(10)])
print(x)

print(list(i for i in range(15) if i % 2 == 0 if i > 4))

print({a: b for a in range(7) if a > 3 for b in [a + 1]})

def f(x: int):
    yield 1 + x
    yield 2 + x
    return 3 + x

async def make():
    x = {await f(i * 10)  for i in range(10)}
    print("xx", x)
    return x

def k():
    (yield from make())

for e in k():
    print(e)

print(a)

from TestModules.TestImportStar import *
from TestModules.TestImportStar__all__ import *
print(a)
print(b)

x = {1: 2, 3: 4}
z = {**x}
del z[1], z[2], z[3]
print(z)

xs = iter([1, 2, 3])


if xs.__next__(xr := ref()):
    a = xr.value
    print(a)

print(int("0b111"))
print(chr(49))


def test3():
    o = XX(1)
    i = 0
    s = 0
    while i < 10000000:
        i += 1
        s += o.x
    return s

# testfunc(test3)

class X:
    k = 5
    pass

print(XX.xx)

# assert X.k == 8, "asda"


