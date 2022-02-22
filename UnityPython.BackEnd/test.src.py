# this file is using unitypython, so we have builtins like `ref`
x = 1
x, *z, y = [1, 2, 3]

def g(u):
    return lambda x: x + u

def u(x):
    i = 0
    while i < x:
        i = i + 1
        yield i
print(g(1)(2))

gen = u(5)
print("starting gen")
print(next(gen))
print(next(gen))
print(next(gen))
print(next(gen))
print.__call__(next(gen))

def k(*args, x=(1, 2)):
    print(args)
    return x

print(k(1, 2, 3, 5, 6))

def uq(a, b, *, y = 1):
    return a + b + y

print(uq(1, 2))
print(uq(1, 2, y=3))
print(dict([(1, 2)]))

z = 1
z += 1
print(z)
def test1():
    x = 0
    while x < 10000000:
        x += 1
        yield x
def test2():
    x = 0
    while x < 10000000:
        x += 1
    return x
# a = time()
# xs = test2()
# print("value", xs)
# print("time1", time() - a)

# a = time()
# xs = list(test1())
# print("len", len(xs))
# print("time2", time() - a)
# print(1)

print(1)
print(1)
print(1)


def f():
    print("s2", (yield 5))
print(9)
c = f()

def k():
    yield from c

z = c.send(None, x := ref())

# try:
#     raise Exception("test")
# except NativeError as e:
#     print(e.typename == "DivideByZeroException")

# if z:
#     print("s1", x.value)

# if k().send(None, x):
#     print(x.value)


# co = f()
# print(co.send(None))
# co.send(3)

def __init__(self, x: int, y: int):
    self.x = x
    self.y = y
    self.z = x + y
    self.w = x * y
    # 1-7
    self.u1 = x + y
    self.u2 = x + y
    self.u3 = x + y
    self.u4 = x + y
    self.u5 = x + y
    self.u6 = x + y
    self.u7 = x + y
    # 8 - 20
    self.v1 = x + y
    self.v2 = x + y
    self.v3 = x + y
    self.v4 = x + y
    self.v5 = x + y
    self.v6 = x + y
    self.v7 = x + y
    self.v8 = x + y
    self.v9 = x + y
    # 21 - 35
    
    self.w9 = 10
    self.w1 = x + y
    self.w2 = x + y
    self.w3 = x + y
    self.w4 = x + y
    self.w5 = x + y
    self.w6 = x + y
    self.w7 = x + y
    self.w8 = x + y
    self.z = 0


def __repr__(self):
    return "S" + "(" + str(self.x) + ", " + str(self.y) + ")"



objs = []

S = type("S", (object,), {"__init__": __init__, "__repr__": __repr__, "U": 3})
# objs.append(S(i, i * 2))


def bench(o1, o2, o3, o4):
    k = 0
    os = [o1, o2, o3, o4]
    for i in range(10000000):
        k = os[i % 4].U
    return k

class M:
    U = 5

class N:
    U = 30

class LL:
    U = 30


a = time()
xs = test2()
print("value", xs)
print("time1", time() - a)

a = time()
k = bench(S, M, LL, N)
print("value", k)
print("time1", time() - a)

a = time()
k = bench(S, M, M, M)
print("value", k)
print("time1", time() - a)



# a = time()
# xs = test2()
# print("value", xs)
# print("time1", time() - a)



# print(a)

# MyCls = type("MyCls", (object,), {})
# print(MyCls())


print([1, 2, 3].__str__())
print(str([1, 2, 3]))
print(str([]))
print([1, 2, 3])
print(list().__repr__())
print([1, 2, 3].__repr__())
print(list.__new__)
print(map.__new__)

def acc(i):
    return i + 1;

# print(list(map(acc, [1, 2, 3])))

print(acc(100))

for i in map(acc, [1, 2, 3]):
    print(i)

def add(i, c):
    return i + c;

for _sum in map(add, [1, 2, 3], [4, 5, 6]):
    print(_sum)

print(list(map(acc, [2, 3, 5])))