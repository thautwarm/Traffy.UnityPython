from __future__ import annotations
from dataclasses import dataclass
from .Collections import OrderedDict, OrderedSet

class ConflictNonlocal(Exception):
    def __init__(self, name: str):
        self.name = name
        super().__init__()

@dataclass
class ConciseSymtable:
    localvars: OrderedSet[str]
    freevars: OrderedSet[str]

    parent: ConciseSymtable | None

    @classmethod
    def new(cls, parent: ConciseSymtable | None):
        return cls(OrderedSet(), OrderedSet(), parent)

    def find_local_or_free(self, x: str):
        if x in self.localvars or x in self.freevars:
            return True
        if self.parent:
            if self.parent.find_local_or_free(x):
                self.freevars.add(x)
                return True
        return False


_Write = bool

@dataclass
class Symtable:
    localvars: OrderedSet[str]
    freevars: OrderedSet[str]

    enteredvars: OrderedDict[str, _Write]
    explicit_globalvars: set[str]
    parent: ConciseSymtable | None
    @classmethod
    def new(cls, parent: ConciseSymtable | None):
        return cls(OrderedSet(), OrderedSet(), OrderedDict(), set(), parent)

    def find_local_or_free(self, x: str):
        if x in self.localvars or x in self.freevars:
            return True
        if self.parent:
            if self.parent.find_local_or_free(x):
                self.freevars.add(x)
                return True
        return False

    def add_arg(self, x: str):
        if x in self.freevars or x in self.explicit_globalvars:
            raise ConflictNonlocal(x)
        self.localvars.add(x)

    def add_nonlocalvar(self, x: str):
        if x in self.localvars or x in self.explicit_globalvars:
            raise ConflictNonlocal(x)
        self.freevars.add(x)

    def add_global(self, x: str):
        if x in self.freevars or x in self.localvars:
            raise ConflictNonlocal(x)
        self.explicit_globalvars.add(x)

    def mut_var(self, x: str):
        self.enteredvars[x] = True

    def read_var(self, x: str):
        self.enteredvars.setdefault(x, False)

    def solve(self):
        for enter, do_write in self.enteredvars.items():
            if enter in self.explicit_globalvars:
                continue

            if not self.find_local_or_free(enter):
                if do_write:
                    self.localvars.add(enter)
        return ConciseSymtable(self.localvars, self.freevars, self.parent)

