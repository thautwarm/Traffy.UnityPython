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
def __repr__(self):
    return "S" + "(" + str(self.x) + ", " + str(self.y) + ")"



objs = []

S = type("S", (object,), {"__init__": __init__, "__repr__": __repr__})
# objs.append(S(i, i * 2))


def bench(o):
    i = 0
    k = 0
    while i < 10000000:
        k = o.x
        k = o.y
        k = o.x
        k = o.y
        i += 1
    return k

a = time()
xs = test2()
print("value", xs)
print("time1", time() - a)

a = time()
k = bench(S(1, 2))
print("value", k)
print("time1", time() - a)

a = time()
k = bench(S(1, 2))
print("value", k)
print("time1", time() - a)


# a = time()
# xs = test2()
# print("value", xs)
# print("time1", time() - a)



# print(a)

# MyCls = type("MyCls", (object,), {})
# print(MyCls())


