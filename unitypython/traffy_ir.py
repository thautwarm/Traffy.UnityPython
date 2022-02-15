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


class Raise(TypedDict):
    hasCont: bool
    exc: TraffyIR | None


class BoolAnd2(TypedDict):
    hasCont: bool
    left: TraffyIR
    right: TraffyIR


class BoolOr2(TypedDict):
    hasCont: bool
    left: TraffyIR
    right: TraffyIR


class BoolAnd(TypedDict):
    hasCont: bool
    left: TraffyIR
    comparators: list[TraffyIR]


class BoolOr(TypedDict):
    hasCont: bool
    left: TraffyIR
    comparators: list[TraffyIR]


class NamedExpr(TypedDict):
    hasCont: bool
    lhs: TraffyLHS
    expr: TraffyIR


class BinOp(TypedDict):
    hasCont: bool
    left: TraffyIR
    right: TraffyIR
    op: OpBin

class CmpOp(TypedDict):
    hasCont: bool
    op: OpBin
    left: TraffyIR
    comparators: list[TraffyIR]


class UnaryOp(TypedDict):
    hasCont: bool
    op: OpU
    operand: TraffyIR


class DefaultArgEntry(TypedDict):
    slot: int
    value: TraffyIR


class Lambda(TypedDict):
    hasCont: bool
    fptr: TrFuncPointer
    default_args: list[DefaultArgEntry]
    freeslots: list[int]


class Call(TypedDict):
    hasCont: bool
    func: TraffyIR
    args: list[TraffyIR]

class CallEx(TypedDict):
    hasCont: bool
    func: TraffyIR
    args: list[SequenceElement]
    kwargs: list[tuple[TrObject | None, TraffyIR]]

class Constant(TypedDict):
    o: TrObject


class DictEntry(TypedDict):
    key: TraffyIR | None
    value: TraffyIR


class Dict(TypedDict):
    hasCont: bool
    entries: list[DictEntry]


class SequenceElement(TypedDict):
    unpack: bool
    value: TraffyIR


class List(TypedDict):
    hasCont: bool
    elements: list[SequenceElement]


class Tuple(TypedDict):
    hasCont: bool
    elements: list[SequenceElement]


class Set(TypedDict):
    hasCont: bool
    elements: list[SequenceElement]


class Attribute(TypedDict):
    hasCont: bool
    value: TraffyIR
    attr: str


class Subscript(TypedDict):
    hasCont: bool
    value: TraffyIR
    item: TraffyIR


class Yield(TypedDict):
    value: TraffyIR
    hasCont: bool


class YieldFrom(TypedDict):
    value: TraffyIR
    hasCont: bool


class LocalVar(TypedDict):
    slot: int


class GlobalVar(TypedDict):
    name: TrObject


class StoreListEx(TypedDict):
    hasCont: bool
    before: list[TraffyLHS]
    unpack: TraffyLHS
    after: list[TraffyLHS]


class StoreList(TypedDict):
    hasCont: bool
    elts: list[TraffyLHS]


class StoreLocal(TypedDict):
    slot: int


class StoreGlobal(TypedDict):
    name: TrObject


class StoreItem(TypedDict):
    hasCont: bool
    value: TraffyIR
    item: TraffyIR


class StoreAttr(TypedDict):
    value: TraffyIR
    attr: str
    hasCont: bool

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


class Metadata(TypedDict):
    positions: list[Position]
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
