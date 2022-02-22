from __future__ import annotations
from ast import (
    AST,
    For,
    Index,
    stmt,
    expr,
    NodeVisitor,

    ClassDef,
    Return,
    FunctionDef,
    Assign,
    AnnAssign,
    AugAssign,
    While,
    Global,
    Nonlocal,
    Expr,
    If,
    Pass,
    Break,
    Continue,
    Delete,

    Try,
    Raise,
    Yield,
    YieldFrom,

    BoolOp,
    NamedExpr,
    BinOp,
    UnaryOp,
    Lambda,
    # IfExp,
    Compare,
    Call,
    Constant,
    Slice,
    Dict,
    Set,
    Attribute,
    Subscript,
    Starred,
    Name,
    List,
    Tuple,
)
import typing
from .Symtable import Symtable, ConciseSymtable
from typing import Any
from .JSON import dump_json


def get_position_string(x: AST, filename: str):
    return f"file {filename}, line {x.lineno}, column  {x.col_offset}"


SupportedStmts = [
    ClassDef,
    Return,
    FunctionDef,
    Assign,
    AnnAssign,
    AugAssign,
    While,
    Global,
    Nonlocal,
    Expr,
    If,
    Pass,
    Break,
    Continue,
    Delete,

    Try,
    Raise,
    Yield,
    YieldFrom,
]

SupportedExprs = [
    BoolOp,
    NamedExpr,
    BinOp,
    UnaryOp,
    Lambda,
    # IfExp,
    Compare,
    Call,
    Constant,
    Slice,
    Dict,
    Set,
    Attribute,
    Subscript,
    Starred,
    Name,
    List,
    Tuple,
]


def make_syntax_error(filename: str, node: AST, msg):
    e = SyntaxError()
    e.lineno = node.lineno
    e.filename = filename
    e.msg = msg
    e.offset = node.col_offset
    return e


class StmtNodeVisitorInlineCache(NodeVisitor):
    _dispatch_tables: dict[type, typing.Callable]

    def __init_subclass__(cls) -> None:
        cls._dispatch_tables = {}

    def visit(self, node: stmt):
        if method := self._dispatch_tables.get(node.__class__):
            return method(self, node)
        method = getattr(self.__class__, "visit_" + node.__class__.__name__)
        self._dispatch_tables[node.__class__] = method
        return method(self, node)


class ExprNodeVisitorInlineCache(NodeVisitor):
    _dispatch_tables: dict[type, typing.Callable]

    def __init_subclass__(cls) -> None:
        cls._dispatch_tables = {}

    def visit(self, node: expr):
        if method := self._dispatch_tables.get(node.__class__):
            return method(self, node)
        method = getattr(self.__class__, "visit_" + node.__class__.__name__)
        self._dispatch_tables[node.__class__] = method
        return method(self, node)


class ScoperStmt(StmtNodeVisitorInlineCache):
    def __init__(self, filename: str, parent: ConciseSymtable | None):
        self.filename = filename
        self.symtable_builder = Symtable.new(parent)
        self.rhs_scoper = ScoperRHS(self)
        self.lhs_scoper = ScoperLHS(self)

    def solve(self):
        solved = self.symtable_builder.solve()
        return solved

    def visit_For(self, node: For) -> Any:
        self.rhs_scoper.visit(node.iter)
        self.lhs_scoper.visit(node.target)
        for each in node.body:
            self.visit(each)
        for each in node.orelse:
            self.visit(each)

    def visit_Return(self, node: Return) -> Any:
        if node.value:
            self.rhs_scoper.visit(node.value)

    def visit_FunctionDef(self, node: FunctionDef) -> Any:
        self.symtable_builder.mut_var(node.name)
        for each in node.args.defaults:
            self.rhs_scoper.visit(each)
        for each in node.args.kw_defaults:
            if each:
                self.rhs_scoper.visit(each)

    def visit_Assign(self, node: Assign) -> Any:
        for each in node.targets:
            self.lhs_scoper.visit(each)
        self.rhs_scoper.visit(node.value)

    def visit_AugAssign(self, node: AugAssign) -> Any:
        self.lhs_scoper.visit(node.target)
        self.rhs_scoper.visit(node.value)

    def visit_AnnAssign(self, node: AnnAssign) -> Any:
        self.lhs_scoper.visit(node.target)
        if node.value:
            self.rhs_scoper.visit(node.value)

    def visit_While(self, node: While) -> Any:
        self.rhs_scoper.visit(node.test)
        for each in node.body:
            self.visit(each)
        for each in node.orelse:
            self.visit(each)

    def visit_Global(self, node: Global) -> Any:
        for name in node.names:
            self.symtable_builder.add_global(name)

    def visit_Nonlocal(self, node: Nonlocal) -> Any:
        for name in node.names:
            self.symtable_builder.add_nonlocalvar(name)

    def visit_Expr(self, node: Expr) -> Any:
        self.rhs_scoper.visit(node.value)

    def visit_If(self, node: If) -> Any:
        self.rhs_scoper.visit(node.test)
        for each in node.body:
            self.visit(each)
        for each in node.orelse:
            self.visit(each)

    def visit_Delete(self, node: Delete) -> Any:
        for each in node.targets:
            self.lhs_scoper.visit(each)

    def visit_Pass(self, node: Pass) -> Any:
        pass

    def visit_Break(self, node: Break) -> Any:
        pass

    def visit_Continue(self, node: Continue) -> Any:
        pass

    def visit_Try(self, node: Try) -> Any:
        for each in node.body:
            self.visit(each)
        for handler in node.handlers:
            if handler.type:
                self.rhs_scoper.visit(handler.type)
            if handler.name:
                self.symtable_builder.mut_var(handler.name)
            for each in handler.body:
                self.visit(each)
        for each in node.orelse:
            self.visit(each)
        for each in node.finalbody:
            self.visit(each)

    def visit_Raise(self, node: Raise) -> Any:
        if node.exc:
            self.rhs_scoper.visit(node.exc)
        if node.cause:
            self.rhs_scoper.visit(node.cause)

    def visit_ClassDef(self, node: ClassDef) -> Any:
        if node.keywords:
            raise make_syntax_error(self.filename, node, msg="class keywords not supported in UnityPython")
        for each in node.decorator_list:
            self.rhs_scoper.visit(each)
        for each in node.bases:
            self.rhs_scoper.visit(each)


class ScoperRHS(ExprNodeVisitorInlineCache):
    def __init__(self, scoper: ScoperStmt):
        self.scoper = scoper

    def visit_Index(self, node: Index) -> Any:
        return self.visit(getattr(node, "value"))

    def visit_BoolOp(self, node: BoolOp) -> Any:
        for each in node.values:
            self.visit(each)

    def visit_NamedExpr(self, node: NamedExpr) -> Any:
        self.scoper.lhs_scoper.visit(node.target)
        self.visit(node.value)

    def visit_BinOp(self, node: BinOp) -> Any:
        self.visit(node.left)
        self.visit(node.right)

    def visit_UnaryOp(self, node: UnaryOp) -> Any:
        self.visit(node.operand)

    def visit_Lambda(self, node: Lambda) -> Any:
        for each in node.args.defaults:
            self.visit(each)
        for each in node.args.kw_defaults:
            if each:
                self.visit(each)

    def visit_Compare(self, node: Compare) -> Any:
        self.visit(node.left)
        for each in node.comparators:
            self.visit(each)

    def visit_Call(self, node: Call) -> Any:
        self.visit(node.func)
        for each in node.args:
            self.visit(each)

        for each in node.keywords:
            self.visit(each.value)

    def visit_Constant(self, node: Constant) -> Any:
        pass

    def visit_Dict(self, node: Dict) -> Any:
        for each in node.keys:
            if each:
                self.visit(each)

        for each in node.values:
            self.visit(each)

    def visit_Set(self, node: Set) -> Any:
        for each in node.elts:
            self.visit(each)

    def visit_Attribute(self, node: Attribute) -> Any:
        self.visit(node.value)

    def visit_Subscript(self, node: Subscript) -> Any:
        self.visit(node.value)
        self.visit(node.slice)

    def visit_Slice(self, node: Slice) -> Any:
        if node.lower:
            self.visit(node.lower)
        if node.upper:
            self.visit(node.upper)
        if node.step:
            self.visit(node.step)

    def visit_Starred(self, node: Starred) -> Any:
        self.visit(node.value)

    def visit_Name(self, node: Name) -> Any:
        self.scoper.symtable_builder.read_var(node.id)

    def visit_List(self, node: List) -> Any:
        for each in node.elts:
            self.visit(each)

    def visit_Tuple(self, node: Tuple) -> Any:
        for each in node.elts:
            self.visit(each)

    def visit_Yield(self, node: Yield) -> Any:
        if node.value:
            self.visit(node.value)

    def visit_YieldFrom(self, node: YieldFrom) -> Any:
        self.visit(node.value)


class ScoperLHS(ExprNodeVisitorInlineCache):
    def __init__(self, scoper: ScoperStmt):
        self.scoper = scoper

    def visit_Attribute(self, node: Attribute) -> Any:
        self.scoper.rhs_scoper.visit(node.value)

    def visit_Subscript(self, node: Subscript) -> Any:
        self.scoper.rhs_scoper.visit(node.value)
        self.scoper.rhs_scoper.visit(node.slice)

    def visit_Starred(self, node: Starred) -> Any:
        self.visit(node.value)

    def visit_Name(self, node: Name) -> Any:
        self.scoper.symtable_builder.mut_var(node.id)

    def visit_List(self, node: List) -> Any:
        for each in node.elts:
            self.visit(each)

    def visit_Tuple(self, node: Tuple) -> Any:
        for each in node.elts:
            self.visit(each)


class ScoperClassLHS(ScoperLHS):
    def __init__(self, scoper: ScoperClassStmt):
        self.scoper = scoper

    def visit_Name(self, node: Name) -> Any:
        self.scoper.symtable_builder.mut_unknown(node.id)


class ScoperClassRHS(ScoperRHS):
    def __init__(self, scoper: ScoperClassStmt):
        self.scoper = scoper

    def visit_Name(self, node: Name) -> Any:
        self.scoper.symtable_builder.read_unknown(node.id)


class ScoperClassStmt(ScoperStmt):
    def __init__(self, filename: str, parent: ConciseSymtable | None):
        self.filename = filename
        self.symtable_builder = Symtable.new(parent)
        self.rhs_scoper = ScoperClassRHS(self)
        self.lhs_scoper = ScoperClassLHS(self)
