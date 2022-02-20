from __future__ import annotations
from typing import MutableSet, Iterable
from sys import version_info
import typing

if version_info >= (3, 7):
    OrderedDict = dict
else:
    from collections import OrderedDict

_T = typing.TypeVar('_T')

_unset = object()

class OrderedSet(MutableSet[_T]):
    def __init__(self, iter: Iterable[_T] | None = None):
        self._data = OrderedDict() # type: dict[_T, int]
        if iter:
            for each in iter:
                self.add(each)

    def __contains__(self, x: _T) -> bool:
        return x in self._data

    def __iter__(self):
        yield from self._data.keys()

    def __len__(self):
        return len(self._data)

    def add(self, v: _T):
        if self._data.get(v, _unset) is _unset:
            self._data[v] = len(self._data)

    def discard(self, v: _T):
        self._data.pop(v, None)

    def order(self, v: _T):
        return self._data[v]

    def asdict(self):
        return self._data.copy()

    def __repr__(self) -> str:
        return '{' + ', '.join(map(repr, self)) + '}'
