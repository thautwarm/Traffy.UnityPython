from __future__ import annotations
from enum import Enum, auto


class S(Enum):
    a: S
    b: S
    c: S = auto()
    d: S = "231"  # type: ignore


assert S.a.value == 0
assert S.a.name == "a"

print(S.a.value == 0)
print(S.c.value == "231")
