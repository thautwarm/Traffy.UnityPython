from unitypython.Transpile import compile_module
import ast


node = ast.parse("""
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
a = time()
xs = test2()
print("value", xs)
print("time1", time() - a)

a = time()
xs = list(test1())
print("len", len(xs))
print("time2", time() - a)
""", "a.py")


tr = compile_module("a.py", node)

import json

with open("c.json", 'w', encoding='utf-8') as file:
    file.write(json.dumps(tr))

