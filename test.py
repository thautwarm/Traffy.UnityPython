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
""", "a.py")


tr = compile_module("a.py", node)

import json

with open("c.json", 'w', encoding='utf-8') as file:
    file.write(json.dumps(tr))

