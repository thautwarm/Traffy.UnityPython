from typing import Any, TypedDict, Generic, TypeVar
from .utils import testsuite
from typing import Mapping

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


print(list({1: 5}.items()), 'get')

class M(Mapping[int, int]):
    def __finditem__(self, __k: int, __v_ref: ref[Any]):
        if __k == 1:
            __v_ref.value = 5
            return True
        return False
    def __len__(self) -> int:
        return 1
    def __iter__(self):
        yield 1

xs = M()

ys = M()

print(xs == ys)


print(type((i for i in range(10))).mro())


print(range(10).sum())
print(range(10).map(lambda x: "a" * x).map(len).tolist())

from . import utils

print(utils.__dict__)
print(utils.__name__)
