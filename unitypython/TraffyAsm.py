from __future__ import annotations

from enum import IntEnum
from logging import handlers
import typing


if typing.TYPE_CHECKING:
    from typing import TypedDict
else:

    class TypedDict:

        def __init_subclass__(cls: type) -> type:
            anns = getattr(cls, '__annotations__', {})
            _defaults = {}
            for k, v in cls.__dict__.items():
                if k.startswith('__'):
                    continue
                if k in anns:
                    _defaults[k] = v
            setattr(cls, '_defaults', _defaults)
            return cls

        def __new__(cls, **kwargs):
            kwargs = {"$type": cls.__name__, **kwargs}
            kwargs.update(cls._defaults)
            return kwargs


class OpU(IntEnum):
    INV = 0
    NOT = 1
    NEG = 2
    POS = 3  # positive


def hasCont(*xs: TraffyLHS | TraffyIR):
    return any(x.get('hasCont', False) for x in xs)

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


class TrInt(TypedDict):
    value: int


class TrFloat(TypedDict):
    value: float


class TrStr(TypedDict):
    value: str


class TrNone(TypedDict):
    pass


class TrTuple(TypedDict):
    elts: list[TrObject]


class TrBool(TypedDict):
    value: bool


if typing.TYPE_CHECKING:
    TrObject = typing.Union[TrInt, TrFloat, TrStr, TrTuple, TrNone, TrBool]
else:
    TrObject = (TrInt, TrFloat, TrStr, TrTuple, TrNone)


class Block(TypedDict):
    hasCont: bool
    suite: list[TraffyIR]


class AugAssign(TypedDict):
    hasCont: bool
    op: OpBin
    lhs: TraffyLHS
    rhs: TraffyIR
    position: int


class Assign(TypedDict):
    hasCont: bool
    lhs: TraffyLHS
    rhs: TraffyIR


class Return(TypedDict):
    hasCont: bool
    value: TraffyIR


class While(TypedDict):
    hasCont: bool
    test: TraffyIR
    body: TraffyIR
    orelse: TraffyIR | None


class ForIn(TypedDict):
    hasCont: bool
    target: TraffyLHS
    itr: TraffyIR
    body: TraffyIR
    orelse: TraffyIR | None
    position: int


class IfClause(TypedDict):
    cond: TraffyIR
    body: TraffyIR


class IfThenElse(TypedDict):
    hasCont: bool
    clauses: list[IfClause]
    orelse: TraffyIR | None


class Continue(TypedDict):
    pass


class Break(TypedDict):
    pass


class Handler(TypedDict):
    exc_type: TraffyIR | None
    exc_bind: TraffyLHS | None
    body: TraffyIR


class Try(TypedDict):
    hasCont: bool
    body: TraffyIR
    handlers: list[Handler]
    orelse: TraffyIR | None
    final: TraffyIR | None
    position: int


class Raise(TypedDict):
    hasCont: bool
    exc: TraffyIR | None


class BoolAnd2(TypedDict):
    hasCont: bool
    left: TraffyIR
    right: TraffyIR
    position: int


class BoolOr2(TypedDict):
    hasCont: bool
    left: TraffyIR
    right: TraffyIR
    position: int


class BoolAnd(TypedDict):
    hasCont: bool
    left: TraffyIR
    comparators: list[TraffyIR]
    position: int


class BoolOr(TypedDict):
    hasCont: bool
    left: TraffyIR
    comparators: list[TraffyIR]
    position: int


class NamedExpr(TypedDict):
    hasCont: bool
    lhs: TraffyLHS
    expr: TraffyIR


class BinOp(TypedDict):
    hasCont: bool
    left: TraffyIR
    right: TraffyIR
    op: OpBin
    position: int

class CmpOp(TypedDict):
    hasCont: bool
    op: OpBin
    left: TraffyIR
    comparators: list[TraffyIR]
    position: int


class UnaryOp(TypedDict):
    hasCont: bool
    op: OpU
    operand: TraffyIR
    position: int


class DefaultArgEntry(TypedDict):
    slot: int
    value: TraffyIR


class Lambda(TypedDict):
    hasCont: bool
    fptr: TrFuncPointer
    default_args: list[DefaultArgEntry]
    freeslots: list[int]


class CallEx(TypedDict):
    hasCont: bool
    func: TraffyIR
    args: list[SequenceElement]
    kwargs: list[tuple[TrObject | None, TraffyIR]]
    position: int

class Constant(TypedDict):
    o: TrObject


class DictEntry(TypedDict):
    key: TraffyIR | None
    value: TraffyIR


class Dict(TypedDict):
    hasCont: bool
    entries: list[DictEntry]
    position: int


class SequenceElement(TypedDict):
    unpack: bool
    value: TraffyIR


class List(TypedDict):
    hasCont: bool
    elements: list[SequenceElement]
    position: int


class Tuple(TypedDict):
    hasCont: bool
    elements: list[SequenceElement]
    position: int


class Set(TypedDict):
    hasCont: bool
    elements: list[SequenceElement]
    position: int


class Attribute(TypedDict):
    hasCont: bool
    value: TraffyIR
    attr: TrObject
    position: int


class Subscript(TypedDict):
    hasCont: bool
    value: TraffyIR
    item: TraffyIR
    position: int


class Yield(TypedDict):
    value: TraffyIR
    hasCont: bool


class YieldFrom(TypedDict):
    value: TraffyIR
    hasCont: bool
    position: int


class LocalVar(TypedDict):
    slot: int
    position: int


class GlobalVar(TypedDict):
    name: TrObject
    position: int


class StoreListEx(TypedDict):
    hasCont: bool
    position: int
    before: list[TraffyLHS]
    unpack: TraffyLHS
    after: list[TraffyLHS]


class StoreList(TypedDict):
    hasCont: bool
    position: int
    elts: list[TraffyLHS]


class StoreLocal(TypedDict):
    slot: int


class StoreGlobal(TypedDict):
    name: TrObject


class StoreItem(TypedDict):
    hasCont: bool
    position: int
    value: TraffyIR
    item: TraffyIR


class StoreAttr(TypedDict):
    hasCont: bool
    position: int
    attr: TrObject
    value: TraffyIR

class MultiAssign(TypedDict):
    hasCont: bool
    targets: list[TraffyLHS]


class TrFuncPointer(TypedDict):
    posargcount: int
    allargcount: int
    hasvararg: bool
    haskwarg: bool
    kwindices: dict[int, TrObject]
    code: TraffyIR
    metadata: Metadata


class Position(TypedDict):
    line: int
    col: int

class Span(TypedDict):
    start: Position
    end: Position


class Metadata(TypedDict):
    positions: list[Span]
    localnames: list[str]
    freenames: list[str]
    codename: str
    filename: str


if typing.TYPE_CHECKING:
    TraffyIR = typing.Union[
        Block,
        AugAssign,
        Return,
        Assign,
        While,
        ForIn,
        Dict,
        List,
        Tuple,
        Set,
        Attribute,
        Subscript,
        Constant,
        Yield,
        YieldFrom,
        LocalVar,
        GlobalVar,
        CmpOp,

        Try,
        Raise,
        Continue,
        Break
    ]
    TraffyLHS = typing.Union[
        StoreListEx, StoreList, StoreLocal, StoreGlobal, StoreItem, StoreAttr, MultiAssign
    ]
else:
    TraffyIR = (
        Block,
        AugAssign,
        Return,
        Assign,
        While,
        ForIn,
        Dict,
        List,
        Tuple,
        Set,
        Attribute,
        Subscript,
        Constant,
        Yield,
        YieldFrom,
        LocalVar,
        GlobalVar,
        CmpOp,

        Try,
        Raise,
        Continue,
        Break,
    )
    TraffyLHS = (StoreListEx, StoreList, StoreLocal, StoreGlobal, StoreItem, StoreAttr, MultiAssign)
