from typing import TypedDict, Generic, TypeVar
from .utils import testsuite

import abc

# with testsuite("sada"):
class S(TypedDict):
    x: int
    y: int

    # assert S(x = 1, y = 2) == {'x': 1, 'y': 2}

print(b"q23".count(ord('q')))


print("should be 2", b"q2323".count(bytearray(b'23')))

def g():
    yield 2
    return 5

task1 = g()

def f():
    yield 1
    yield 2
    yield from task1
    yield 3
    return 100
xs = f()

next(xs)
next(xs)
next(xs)
next(xs, None)
next(xs, None)
print(xs.is_completed)
print(xs.result)
print(task1.result)
