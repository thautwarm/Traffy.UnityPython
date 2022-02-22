from __future__ import annotations
from enum import IntEnum
from turtle import position
from .IntEncode import encode as encode_int, decode as decode_int
from dataclasses import InitVar, dataclass
import dataclasses
import typing


if typing.TYPE_CHECKING:
    import typing_extensions

    class TraffyIR(typing_extensions.Protocol):
        hasCont: bool

    class TraffyLHS(typing_extensions.Protocol):
        hasCont: bool
else:
    TraffyIR = object
    TraffyLHS = object

# else:
#     TraffyIR = (
#         Block,
#         AugAssign,
#         Return,
#         Assign,
#         While,
#         ForIn,
#         Dict,
#         List,
#         Tuple,
#         Set,
#         Attribute,
#         Subscript,
#         Constant,
#         Yield,
#         YieldFrom,
#         LocalVar,
#         GlobalVar,
#         CmpOp,
#         Try,
#         Raise,
#         Continue,
#         Break,
#     )
#     TraffyLHS = (
#         StoreListEx,
#         StoreList,
#         StoreLocal,
#         StoreGlobal,
#         StoreItem,
#         StoreAttr,
#         MultiAssign,
#     )


class OpU(IntEnum):
    INV = 0
    NOT = 1
    NEG = 2
    POS = 3  # positive


def hasCont(head: TraffyLHS | TraffyIR | list[TraffyLHS | TraffyIR], *xs: TraffyLHS | TraffyIR) -> bool:
    if isinstance(head, list):
        test_head = bool(head) and any(x.hasCont for x in head)
        if test_head:
            return True
    else:
        if head.hasCont:
            return True
    return bool(xs) and any(x.hasCont for x in xs)


class OpBin(IntEnum):
    ADD = 0
    SUB = 1
    MUL = 2
    TRUEDIV = 3
    FLOORDIV = 4
    POW = 5
    LSHIFT = 6
    RSHIFT = 7
    BITAND = 8
    BITOR = 9
    BITXOR = 10
    MATMUL = 11
    MOD = 12

    EQ = 13
    NE = 14
    LT = 15
    LE = 16
    GT = 17
    GE = 18
    IS = 19
    ISNOT = 20
    IN = 21
    NOTIN = 22


@dataclass
class TrInt(object):
    value: int


@dataclass
class TrFloat(object):
    value: float


@dataclass
class TrStr(object):
    value: str
    isInterned: bool = False


@dataclass
class TrNone(object):
    pass


@dataclass
class TrTuple(object):
    elts: list[TrObject]


@dataclass
class TrBool(object):
    value: bool


@dataclass
class TrBytes(object):
    value: str


if typing.TYPE_CHECKING:
    TrObject = typing.Union[TrInt, TrFloat,
                            TrStr, TrTuple, TrNone, TrBool, TrBytes]
else:
    TrObject = (TrInt, TrFloat, TrStr, TrTuple, TrNone)


@dataclass
class Block(TraffyIR):
    hasCont: bool
    suite: list[TraffyIR]


@dataclass
class AugAssign(TraffyIR):
    position: int
    op: OpBin
    lhs: TraffyLHS
    rhs: TraffyIR
    hasCont: InitVar[bool]  # type: ignore

    def __post_init__(self, hasCont):
        op = self.op
        if hasCont:
            op = -op
        self.op = op  # type: ignore

    @property
    def hasCont(self) -> bool:
        return self.op < 0


@dataclass
class Assign(TraffyIR):
    position: int
    hasCont: bool
    lhs: TraffyLHS
    rhs: TraffyIR


@dataclass
class Return(TraffyIR):
    position: int
    hasCont: bool
    value: TraffyIR


@dataclass
class While(TraffyIR):
    position: int
    hasCont: bool
    test: TraffyIR
    body: TraffyIR
    orelse: TraffyIR | None


@dataclass
class ForIn(TraffyIR):
    position: int
    hasCont: bool
    target: TraffyLHS
    itr: TraffyIR
    body: TraffyIR
    orelse: TraffyIR | None


@dataclass
class IfClause(object):
    cond: TraffyIR
    body: TraffyIR


@dataclass
class DefClass(object):
    position: int
    hasCont: bool
    bases: list[TraffyIR]
    body: Lambda


@dataclass
class IfThenElse(TraffyIR):
    position: int
    hasCont: bool
    clauses: list[IfClause]
    orelse: TraffyIR | None


@dataclass
class Continue(TraffyIR):
    hasCont = False


@dataclass
class Break(TraffyIR):
    hasCont = False


@dataclass
class Handler(object):
    exc_type: TraffyIR | None
    exc_bind: TraffyLHS | None
    body: TraffyIR


@dataclass
class Try(TraffyIR):
    position: int
    hasCont: bool
    body: TraffyIR
    handlers: list[Handler]
    orelse: TraffyIR | None
    final: TraffyIR | None


@dataclass
class Raise(TraffyIR):
    position: int
    hasCont: bool
    exc: TraffyIR | None


@dataclass
class BoolAnd2(TraffyIR):
    position: int
    hasCont: bool
    left: TraffyIR
    right: TraffyIR


@dataclass
class BoolOr2(TraffyIR):
    position: int
    hasCont: bool
    left: TraffyIR
    right: TraffyIR


@dataclass
class BoolAnd(TraffyIR):
    position: int
    hasCont: bool
    left: TraffyIR
    comparators: list[TraffyIR]
    position: int


@dataclass
class BoolOr(TraffyIR):
    position: int
    hasCont: bool
    left: TraffyIR
    comparators: list[TraffyIR]
    position: int


@dataclass
class NamedExpr(TraffyIR):
    position: int
    hasCont: bool
    lhs: TraffyLHS
    expr: TraffyIR


@dataclass
class BinOp(TraffyIR):
    position: int
    left: TraffyIR
    right: TraffyIR
    op: OpBin
    hasCont: InitVar[bool]  # type: ignore

    def __post_init__(self, hasCont):
        op = self.op
        if hasCont:
            op = -op
        self.op = op  # type: ignore

    @property
    def hasCont(self) -> bool:
        return self.op < 0


@dataclass
class CmpOp(TraffyIR):
    position: int
    op: OpBin
    left: TraffyIR
    comparators: list[TraffyIR]
    hasCont: InitVar[bool]  # type: ignore

    def __post_init__(self, hasCont):
        op = self.op
        if hasCont:
            op = -op
        self.op = op  # type: ignore

    @property
    def hasCont(self) -> bool:
        return self.op < 0


@dataclass
class UnaryOp(TraffyIR):
    position: int
    op: OpU
    operand: TraffyIR
    hasCont: InitVar[bool]  # type: ignore

    def __post_init__(self, hasCont):
        op = self.op
        if hasCont:
            op = -op
        self.op = op  # type: ignore

    @property
    def hasCont(self) -> bool:
        return self.op < 0


@dataclass
class DefaultArgEntry(object):
    slot: int
    value: TraffyIR


@dataclass
class Lambda(TraffyIR):
    hasCont: bool
    fptr: TrFuncPointer
    default_args: list[DefaultArgEntry]
    freeslots: list[int]


@dataclass
class CallEx(TraffyIR):
    position: int
    hasCont: bool
    func: TraffyIR
    args: list[SequenceElement]
    kwargs: list[tuple[TrObject | None, TraffyIR]]


@dataclass
class Constant(TraffyIR):
    o: TrObject
    hasCont = False


@dataclass
class DictEntry(object):
    key: TraffyIR | None
    value: TraffyIR


@dataclass
class Dict(TraffyIR):
    position: int
    hasCont: bool
    entries: list[DictEntry]


@dataclass
class SequenceElement(object):
    unpack: bool
    value: TraffyIR


@dataclass
class List(TraffyIR):
    position: int
    hasCont: bool
    elements: list[SequenceElement]


@dataclass
class Tuple(TraffyIR):
    position: int
    hasCont: bool
    elements: list[SequenceElement]


@dataclass
class Set(TraffyIR):
    position: int
    hasCont: bool
    elements: list[SequenceElement]


@dataclass
class Attribute(TraffyIR):
    position: int
    hasCont: bool
    value: TraffyIR
    attr: TrStr


@dataclass
class Subscript(TraffyIR):
    position: int
    hasCont: bool
    value: TraffyIR
    item: TraffyIR


@dataclass
class Yield(TraffyIR):
    position: int
    value: TraffyIR
    hasCont = True


@dataclass
class YieldFrom(TraffyIR):
    position: int
    value: TraffyIR
    position: int
    hasCont = True


@dataclass
class GuessVar(TraffyIR):
    position: int
    name: TrStr
    hasCont = False


@dataclass
class LocalVar(TraffyIR):
    position: int
    slot: int
    hasCont = False


@dataclass
class FreeVar(TraffyIR):
    position: int
    slot: int
    hasCont = False


@dataclass
class GlobalVar(TraffyIR):
    position: int
    name: TrObject
    hasCont = False


@dataclass
class StoreListEx(TraffyLHS):
    position: int
    hasCont: bool
    before: list[TraffyLHS]
    unpack: TraffyLHS
    after: list[TraffyLHS]


@dataclass
class StoreList(TraffyLHS):
    position: int
    hasCont: bool
    elts: list[TraffyLHS]


@dataclass
class StoreLocal(TraffyLHS):
    slot: int
    hasCont = False


@dataclass
class StoreFree(TraffyLHS):
    slot: int
    hasCont = False


@dataclass
class StoreGlobal(TraffyLHS):
    name: TrObject
    hasCont = False


@dataclass
class StoreItem(TraffyLHS):
    position: int
    hasCont: bool
    value: TraffyIR
    item: TraffyIR


@dataclass
class StoreAttr(TraffyLHS):
    position: int
    hasCont: bool
    attr: TrStr
    value: TraffyIR


@dataclass
class MultiAssign(TraffyLHS):
    hasCont: bool
    targets: list[TraffyLHS]


@dataclass
class TrFuncPointer(object):
    posargcount: int
    allargcount: int
    hasvararg: bool
    haskwarg: bool
    kwindices: dict[int, TrObject]
    code: TraffyIR
    metadata: Metadata


class Position(typing.NamedTuple):
    line: int
    col: int


@dataclass(frozen=True)
class Span(object):
    start: Position
    end: Position
    filename: str | None = None

    @staticmethod
    def empty() -> Span:
        return Span(Position(1, 0), Position(1, 0))


@dataclass
class Metadata(object):
    localnames: list[str]
    freenames: list[str]
    codename: str
    filename: str
    sourceCode: str | None
    compressedSpanPointers: list[int] = dataclasses.field(default_factory=list)
    compressedPositions: list[int] = dataclasses.field(default_factory=list)
    positions: InitVar[list[tuple[int, int]]] = []
    spanPointers: InitVar[list[tuple[int, int]]] = []
    def __post_init__(self, positions: list[tuple[int, int]], span_pointers: list
                      [tuple[int, int]]):
        # print(list(map(tuple, positions)))
        self.compressedPositions = encode_int(positions)
        # print(self.compressedPositions)
        # assert decode_int(self.compressedPositions, lambda x, y: (x, y)) == positions
        self.compressedSpanPointers = encode_int(span_pointers)
        # assert decode_int(self.compressedSpanPointers, lambda x, y: (x, y)) == span_pointers


__cache_type_dict = {}
__prim_types = (int, float, str, bool, type(None))
__seq_types = (list, tuple, set, frozenset)


def fast_asdict(o):
    if isinstance(o, __seq_types):
        return [fast_asdict(x) for x in o]
    if isinstance(o, dict):
        return {k: fast_asdict(v) for k, v in o.items()}
    if isinstance(o, __prim_types):
        return o
    if lookuptype := __cache_type_dict.get(o.__class__):
        pass
    else:
        cls = o.__class__
        assert dataclasses.is_dataclass(
            cls), f"cannot serialize {cls} object: not a dataclass."
        lookuptype = cls.__name__, [
            field.name for field in dataclasses.fields(cls)]
        __cache_type_dict[cls] = lookuptype
    tname, fields = lookuptype
    res = {"$type": tname}
    for k in fields:
        res[k] = fast_asdict(getattr(o, k))
    return res
