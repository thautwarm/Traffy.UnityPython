from typing import TypedDict, Generic, TypeVar
from .utils import testsuite

# with testsuite("sada"):
class S(TypedDict):
    x: int
    y: int

    # assert S(x = 1, y = 2) == {'x': 1, 'y': 2}