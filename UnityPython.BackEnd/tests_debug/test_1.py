from __future__ import annotations
from typing import TypedDict

from enum import Enum, auto

class S(Enum):
    a: S
    b: S
    c: S = auto()

assert S.a.value == 0
assert S.a.name == "a"

print(S.a)
print(S.c)

# class K(TypedDict):
#     x: int

# print(K(x=1))