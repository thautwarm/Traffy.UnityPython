from .utils import testsuite

# with testsuite("typing module"):
from typing import TypedDict, Generic, TypeVar


class S(TypedDict):
    x: int
    y: int


assert S(x=1, y=2) == {"x": 1, "y": 2}

# _T = TypeVar("_T")

# class K(Generic[_T]):
#     def __init__(self, v: _T):
#         self.v = v
# print(K(1))
