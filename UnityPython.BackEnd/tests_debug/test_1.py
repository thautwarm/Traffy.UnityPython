from typing import TypedDict, Generic, TypeVar
from .utils import testsuite

# with testsuite("sada"):
class S(TypedDict):
    x: int
    y: int

    # assert S(x = 1, y = 2) == {'x': 1, 'y': 2}

print(b"q23".count(ord('q')))

print("should be 2", b"q2323".count(bytearray(b'23')))
