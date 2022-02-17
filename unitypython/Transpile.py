from __future__ import annotations
from ast import *
from asyncio import constants
from operator import le
from re import A
from unitypython.Collections import OrderedSet, OrderedDict
from . import Structures as S
from .Symtable import Symtable, ConciseSymtable
from . import TraffyAsm as ir
from typing import Any
from .Scoper import *
import typing


class IRStmtTransformerInlineCache(NodeTransformer):
    _dispatch_tables: dict[type, typing.Callable]
    def __init_subclass__(cls) -> None:
        cls._dispatch_tables = {}

    def visit(self, node: stmt) -> ir.TraffyIR:
        if method := self._dispatch_tables.get(node.__class__):
            return method(self, node)
        method = getattr(self.__class__, "visit_" + node.__class__.__name__)
        self._dispatch_tables[node.__class__] = method
        return method(self, node)

class IRExprTransformerInlineCache(NodeTransformer):
    _dispatch_tables: dict[type, typing.Callable]
    def __init_subclass__(cls) -> None:
        cls._dispatch_tables = {}

    def visit(self, node: expr) -> ir.TraffyIR:
        if method := self._dispatch_tables.get(node.__class__):
            return method(self, node)
        method = getattr(self.__class__, "visit_" + node.__class__.__name__)
        self._dispatch_tables[node.__class__] = method
        return method(self, node)


class LHSTransformerInlineCache(NodeTransformer):
    _dispatch_tables: dict[type, typing.Callable]
    def __init_subclass__(cls) -> None:
        cls._dispatch_tables = {}

    def visit(self, node: expr) -> ir.TraffyLHS:
        if method := self._dispatch_tables.get(node.__class__):
            return method(self, node)
        method = getattr(self.__class__, "visit_" + node.__class__.__name__)
        self._dispatch_tables[node.__class__] = method
        return method(self, node)


_constants = {}
def const_to_variant(v: object) -> ir.TrObject:
    key = type(v), v
    n = _constants.get(key)
    if n is not None:
        return n
    n = _constants[key] = _const_to_variant(v)
    return n


def _const_to_variant(v):
    if isinstance(v, int):
        return ir.TrInt(value=v)
    elif isinstance(v, float):
        return ir.TrFloat(value=v)
    elif isinstance(v, str):
        return ir.TrStr(value=v)
    elif v is None:
        return ir.TrNone()
    elif isinstance(v, bool):
        return ir.TrBool(value=v)
    elif isinstance(v, frozenset):
        raise TypeError(v)
    elif isinstance(v, tuple):
        return ir.TrTuple(elts = list(map(const_to_variant, v)))
    else:
        raise TypeError(v)


def json_str(x: str):
    return json.dumps(x, ensure_ascii=False)


class AutoIndex(OrderedDict):
    def __missing__(self, k):
        v = self[k] = len(self)
        return v

def extract_pos(node: AST) -> ir.Position:
    return ir.Position(line=node.lineno, col=node.col_offset)


class Transpiler:
    def __init__(self, filename: str, init_pos: ir.Position, parent_scope: ConciseSymtable | None):
        self.instructions = []  # type: list[ir.TraffyIR]
        self.constants: OrderedDict[tuple[type, object], ir.TrObject] = OrderedDict()
        self.filename = filename
        self.parent_scope = parent_scope
        self.rhs_transpiler = TranspilerRHS(self)
        self.lhs_transpiler = TranspilerLHS(self)
        self.stmt_transpiler = TranspileStmt(self)

        self.flag = 0
        self._cur_pos : ir.Position = init_pos
        self.positions: OrderedSet[ir.Position] = OrderedSet()
        self.positions.add(init_pos)

        self.posargcount = 0 # count of non-variadic positional args
        self.allargcount = 0
        self.vararg: str | None = None
        self.kwarg: str | None = None
        self.kwindices: OrderedSet[str] = OrderedSet()

    def __lshift__(self, o: ir.TraffyIR):
        self.instructions.append(o)

    @property
    def cur_pos(self):
        return self._cur_pos

    @cur_pos.setter
    def cur_pos(self, value: ir.Position):
        self._cur_pos = value
        self.positions.add(value)

    @property
    def pos_ind(self) -> int:
        return self.positions.order(self._cur_pos)


    def localname_slot(self, id: str):
        if id in self.scope.localvars:
            i = self.scope.localvars.order(id)
            return i
        elif id in self.scope.freevars:
            i = self.scope.freevars.order(id)
            return i
        else:
            raise NameError(f"unknown local variable {id}")

    def load_derefence(self, id: str) -> int:
        if id in self.scope.localvars:
            i = self.scope.localvars.order(id)
            return i
        elif id in self.scope.freevars:
            i = self.scope.freevars.order(id)
            return -i -1
        else:
            raise NameError(f"{id} is not a local/free variable")

    def load_name_(self, id: str) -> ir.TraffyIR:
        if id in self.scope.localvars:
            i = self.scope.localvars.order(id)
            return ir.LocalVar(slot=i, position=self.pos_ind)
        elif id in self.scope.freevars:
            i = self.scope.freevars.order(id)
            return ir.LocalVar(slot=-i -1, position=self.pos_ind)
        else:
            return ir.GlobalVar(name=const_to_variant(id), position=self.pos_ind)

    def store_name_(self, id: str) -> ir.TraffyLHS:

        if id in self.scope.localvars:
            i = self.scope.localvars.order(id)
            return ir.StoreLocal(slot=i)
        elif id in self.scope.freevars:
            i = self.scope.freevars.order(id)
            return ir.StoreLocal(slot=-i -1)
        else:
            return ir.StoreGlobal(name=const_to_variant(id))


    def delete_name_(self, id: str):
        raise NotImplementedError

    def create_fptr_builder(self, code: ir.TraffyIR, name: str) -> ir.TrFuncPointer:
        metadata = ir.Metadata(
            positions=list(self.positions),
            localnames=list(self.scope.localvars),
            freenames=list(self.scope.freevars),
            codename=name,
            filename=self.filename,
        )
        hasvararg = bool(self.vararg)
        haskwarg = bool(self.kwarg)
        return ir.TrFuncPointer(
            posargcount=self.posargcount,
            allargcount=self.allargcount,
            hasvararg=hasvararg,
            haskwarg=haskwarg,
            kwindices={v: const_to_variant(k) for k, v in self.kwindices.asdict().items()},
            code=code,
            metadata=metadata,
        )

    def before_visit(self, node: Module | FunctionDef | Lambda):
        if isinstance(node, Module):
            self.scope = ConciseSymtable.new(self.parent_scope)
        elif isinstance(node, (FunctionDef, Lambda)):
            scoper = ScoperStmt(self.parent_scope)
            allargcount = 0
            for arg in node.args.args:
                scoper.symtable_builder.add_arg(arg.arg)
                self.kwindices.add(arg.arg)

            self.posargcount = len(node.args.args)
            allargcount = self.posargcount

            vararg = node.args.vararg
            if vararg is not None:
                scoper.symtable_builder.add_arg(vararg.arg)
                self.vararg = vararg.arg
                self.kwindices.add(vararg.arg)
                allargcount += 1

            for arg in node.args.kwonlyargs:
                scoper.symtable_builder.add_arg(arg.arg)
                self.kwindices.add(arg.arg)
                allargcount += 1

            if node.args.kwarg is not None:
                kwarg = node.args.kwarg.arg
                scoper.symtable_builder.add_arg(kwarg)
                self.kwarg = kwarg
                self.kwindices.add(kwarg)
                allargcount += 1

            self.allargcount = allargcount

            if isinstance(node.body, list):
                for each in node.body:
                    scoper.visit(each)
            else:
                scoper.rhs_scoper.visit(node.body)
            scope = scoper.solve()
            self.scope = scope
        else:
            raise TypeError(node)

    def instr_offset(self):
        return len(self.instructions)

class TranspilerRHS(IRExprTransformerInlineCache):
    def __init__(self, root: Transpiler):
        self.root = root

    def __lshift__(self, o: ir.TraffyIR):
        self.root.instructions.append(o)

    def instr_offset(self):
        return len(self.root.instructions)

    def visit_Constant(self, node: Constant):
        ob = const_to_variant(node.value)
        return ir.Constant(o=ob)

    def visit_BoolOp(self, node: BoolOp):
        self.root.cur_pos = extract_pos(node)
        if isinstance(node.op, And):
            if len(node.values) == 2:
                l = self.visit(node.values[0])
                r = self.visit(node.values[1])
                return ir.BoolAnd2(
                    hasCont=l.get('hasCont', False) or r.get('hasCont', False),
                    left=l,
                    right=r,
                    position=self.root.pos_ind,
                )

            left = self.visit(node.values[0])
            args = list(map(self.visit, node.values[1:]))
            hasCont = (left.get('hasCont', False) or
                        any(each.get('hasCont', False) for each in args))
            return ir.BoolAnd(
                hasCont=hasCont,
                left=left,
                comparators=args,
                position=self.root.pos_ind
            )

        elif isinstance(node.op, Or):
            if len(node.values) == 2:
                l = self.visit(node.values[0])
                r = self.visit(node.values[1])
                return ir.BoolOr2(hasCont=l.get('hasCont', False) or r.get('hasCont', False), left=l, right=r)

            left = self.visit(node.values[0])
            args = list(map(self.visit, node.values[1:]))
            hasCont = (left.get('hasCont', False) or
                        any(each.get('hasCont', False) for each in args))
            return ir.BoolOr(hasCont=hasCont, left=left, comparators=args)

        else:
            raise TypeError(
                f"unknown boolop {node.op} {get_position_string(node, self.root.filename)}"
            )

    _binop_indices: dict[typing.Type[operator], ir.OpBin] = {
        Add: ir.OpBin.ADD,
        Sub: ir.OpBin.SUB,
        Mult: ir.OpBin.MUL,
        Div: ir.OpBin.TRUEDIV,
        FloorDiv: ir.OpBin.FLOORDIV,
        Pow: ir.OpBin.POW,
        LShift: ir.OpBin.LSHIFT,
        RShift: ir.OpBin.RSHIFT,
        BitAnd: ir.OpBin.BITAND,
        BitOr: ir.OpBin.BITOR,
        BitXor: ir.OpBin.BITXOR,
        MatMult: ir.OpBin.MATMUL,
        Mod: ir.OpBin.MOD,
    }

    def visit_BinOp(self, node: BinOp) -> Any:
        op = self._binop_indices[type(node.op)]
        l = self.visit(node.left)
        r = self.visit(node.right)
        return ir.BinOp(hasCont=ir.hasCont(l, r), left=l, right=r, op=op)

    def visit_NamedExpr(self, node: NamedExpr) -> Any:
        expr = self.visit(node.value)
        lhs_transpiler = TranspilerLHS(self.root)
        lhs = lhs_transpiler.visit(node.target)
        return ir.NamedExpr(hasCont=ir.hasCont(expr, lhs), lhs=lhs, expr=expr)

    _unaryop_indices: dict[typing.Type[unaryop], ir.OpU] = {
        Invert: ir.OpU.INV,
        Not: ir.OpU.NOT,
        UAdd: ir.OpU.POS,
        USub: ir.OpU.NEG,
    }

    def visit_UnaryOp(self, node: UnaryOp) -> Any:
        operand = self.visit(node.operand)
        op = self._unaryop_indices[type(node.op)]
        return ir.UnaryOp(hasCont=ir.hasCont(operand), op=op, operand=operand)

    def visit_Lambda(self, node: Lambda) -> Any:
        for each in node.args.defaults:
            self.visit(each)
        transpiler = Transpiler(self.root.filename, self.root.scope)
        transpiler.before_visit(node)
        body = transpiler.rhs_transpiler.visit(node.body)
        lambda_body = ir.Return(hasCont=ir.hasCont(body), value=body)

        hasCont = False
        default_arg_entries: list[ir.DefaultArgEntry] = []
        for default, i in zip(node.args.defaults, range(transpiler.posargcount-len(node.args.defaults), transpiler.posargcount)):
            v_arg = self.visit(default)
            default_arg_entries.append(ir.DefaultArgEntry(slot=i, value=v_arg))
            hasCont = hasCont or ir.hasCont(v_arg)

        for arg, default in zip(node.args.kwonlyargs, node.args.kw_defaults):
            if default:
                i = transpiler.kwindices.order(arg.arg)
                v_arg = self.visit(default)
                default_arg_entries.append(ir.DefaultArgEntry(slot=i, value=v_arg))
                hasCont = hasCont or ir.hasCont(v_arg)


        fptr = transpiler.create_fptr_builder(lambda_body, "<lambda>")
        freeslots = [self.root.load_derefence(id) for id in transpiler.scope.freevars]
        return ir.Lambda(hasCont=hasCont, fptr=fptr, default_args=default_arg_entries, freeslots=freeslots)

    _cmp_opcodes: dict[typing.Type[cmpop], ir.OpBin] = {
        Eq: ir.OpBin.EQ,
        NotEq: ir.OpBin.NE,
        Lt: ir.OpBin.LT,
        LtE: ir.OpBin.LE,
        Gt: ir.OpBin.GT,
        GtE: ir.OpBin.GE,
        Is: ir.OpBin.IS,
        IsNot: ir.OpBin.ISNOT,
        In: ir.OpBin.IN,
        NotIn: ir.OpBin.NOTIN,
    }

    def visit_Compare(self, node: Compare) -> ir.TraffyIR:
        l = self.visit(node.left)
        hasCont = ir.hasCont(l)
        comparators: list[ir.TraffyIR] = []
        for each in node.comparators:
            v = self.visit(each)
            comparators.append(v)
            hasCont = hasCont or ir.hasCont(v)
        op = self._cmp_opcodes[type(node.ops[0])]
        return ir.CmpOp(hasCont=hasCont, op=op, left=l, comparators=comparators)

    def visit_Call(self, node: Call) -> ir.TraffyIR:
        func = self.visit(node.func)
        hasCont = ir.hasCont(func)
        args: list[ir.SequenceElement] = []
        kwargs: list[tuple[ir.TrObject | None, ir.TraffyIR]] = []
        for each in node.args:
            if isinstance(each, Starred):
                arg = self.visit(each.value)
                args.append(ir.SequenceElement(unpack=True, value=arg))
            else:
                arg = self.visit(each)
                args.append(ir.SequenceElement(unpack=False, value=arg))
            hasCont = hasCont or ir.hasCont(arg)

        for each in node.keywords:
            if each.arg is None:
                arg = self.visit(each.value)
                kwargs.append((None, arg))
            else:
                arg = self.visit(each.value)
                kwargs.append((const_to_variant(each.arg), arg))
            hasCont = hasCont or ir.hasCont(arg)
        return ir.CallEx(hasCont=hasCont, func=func, args=args, kwargs=kwargs)

    def visit_Slice(self, node: Slice) -> ir.TraffyIR:

        if node.lower:
            low = self.visit(node.lower)
        else:
            low = ir.Constant(o=const_to_variant(None))

        if node.upper:
            high = self.visit(node.upper)
        else:
            high = ir.Constant(o=const_to_variant(None))

        if node.step:
            step = self.visit(node.step)
        else:
            step = ir.Constant(o=const_to_variant(None))
        slice = ir.GlobalVar(name = const_to_variant("slice"))
        return ir.Call(hasCont=ir.hasCont(low, high, step), func=slice, args=[low, high, step])

    def visit_Attribute(self, node: Attribute) -> ir.TraffyIR:
        value = self.visit(node.value)
        attr = node.attr
        return ir.Attribute(hasCont=ir.hasCont(value), value=value, attr=attr)

    def visit_Subscript(self, node: Subscript) -> ir.TraffyIR:
        value = self.visit(node.value)
        item = self.visit(node.slice)
        return ir.Subscript(hasCont=ir.hasCont(value, item), value=value, item=item)

    def visit_Index(self, node: Index) -> ir.TraffyIR:
        return self.visit(getattr(node, "value"))

    def visit_ExtSlice(self, node: ExtSlice) -> ir.TraffyIR:
        raise NotImplementedError

    def visit_Starred(self, node: Starred) -> ir.TraffyIR:
        raise TypeError(
            "Right-hand side '*' expression not handled, please report a bug."
        )

    def visit_Name(self, node: Name) -> ir.TraffyIR:
        return self.root.load_name_(node.id)

    def visit_sequence(self, xs: list[expr]) -> tuple[bool, list[ir.SequenceElement]]:
        hasCont = False
        args: list[ir.SequenceElement] = []
        for each in xs:
            if isinstance(each, Starred):
                arg = self.visit(each.value)
                args.append(ir.SequenceElement(unpack=True, value=arg))
            else:
                arg = self.visit(each)
                args.append(ir.SequenceElement(unpack=False, value=arg))
            hasCont = hasCont or ir.hasCont(arg)
        return hasCont, args

    def visit_List(self, node: List) -> ir.TraffyIR:
        hasCont, sequence = self.visit_sequence(node.elts)
        return ir.List(hasCont=hasCont, elements=sequence)

    def visit_Set(self, node: Set) -> ir.TraffyIR:
        hasCont, sequence = self.visit_sequence(node.elts)
        return ir.Set(hasCont=hasCont, elements=sequence)

    def visit_Tuple(self, node: Tuple) -> ir.TraffyIR:
        hasCont, sequence = self.visit_sequence(node.elts)
        return ir.Tuple(hasCont=hasCont, elements=sequence)

    def visit_Dict(self, node: Dict) -> ir.TraffyIR:
        hasCont = False
        args: list[ir.DictEntry] = []
        for key, value in zip(node.keys, node.values):
            if key is None:
                arg = self.visit(value)
                args.append(ir.DictEntry(key=key, value=arg))
                hasCont = hasCont or ir.hasCont(arg)
            else:
                arg = self.visit(value)
                key = self.visit(key)
                args.append(ir.DictEntry(key=key, value=arg))
                hasCont = hasCont or ir.hasCont(arg, key)
        return ir.Dict(hasCont=hasCont, entries=args)

    def visit_Yield(self, node: Yield) -> ir.TraffyIR:
        if node.value:
            value = self.visit(node.value)
        else:
            value = ir.Constant(o=const_to_variant(None))
        return ir.Yield(value=value, hasCont=True)

    def visit_YieldFrom(self, node: YieldFrom) -> ir.TraffyIR:
        value = self.visit(node.value)
        return ir.YieldFrom(value=value, hasCont=True)

class TranspilerLHS(LHSTransformerInlineCache):
    def __init__(self, root: Transpiler):
        self.root = root

    def instr_offset(self):
        return len(self.root.instructions)

    def visit_Name(self, node: Name) -> ir.TraffyLHS:
        return self.root.store_name_(node.id)

    def visit_Attribute(self, node: Attribute) -> ir.TraffyLHS:
        value = self.root.rhs_transpiler.visit(node.value)
        return ir.StoreAttr(hasCont=ir.hasCont(value), value=value, attr=node.attr)

    def visit_Starred(self, node: Starred) -> ir.TraffyLHS:
        raise TypeError(
            "Left-hand side '*' expression not handled, please report a bug."
        )

    def visit_Subscript(self, node: Subscript) -> ir.TraffyLHS:
        value = self.root.rhs_transpiler.visit(node.value)
        item = self.root.rhs_transpiler.visit(node.slice)
        return ir.StoreItem(hasCont=ir.hasCont(value, item), value=value, item=item)

    def visit_Tuple(self, node: Tuple) -> ir.TraffyLHS:
        return self.visit_List(node)

    def visit_List(self, node: List | Tuple) -> ir.TraffyLHS:
        if not any(isinstance(elt, Starred) for elt in node.elts):
            xs = list(map(self.visit, node.elts))
            return ir.StoreList(hasCont=ir.hasCont(*xs), elts=xs)
        offset = 0
        for i, elt in enumerate(node.elts):
            if isinstance(elt, Starred):
                offset = i
        xs = list(map(self.visit, node.elts[:offset]))
        ys = list(map(self.visit, node.elts[offset+1:]))

        starred = node.elts[offset]
        assert isinstance(starred, Starred)
        v = self.visit(starred.value)
        return ir.StoreListEx(hasCont=ir.hasCont(*xs, *ys, v), before=xs, unpack=v, after=ys)




class TranspileStmt(IRStmtTransformerInlineCache):
    def __init__(self, root: Transpiler):
        self.root = root

    def instr_offset(self):
        return len(self.root.instructions)

    def visit_Expr(self, node: Expr) -> Any:
        return self.root.rhs_transpiler.visit(node.value)

    def visit_Return(self, node: Return) -> Any:
        if node.value:
            expr = self.root.rhs_transpiler.visit(node.value)
        else:
            expr = ir.Constant(o=const_to_variant(None))
        return ir.Return(hasCont=ir.hasCont(expr), value=expr)

    def visit_FunctionDef(self, node: FunctionDef) -> ir.TraffyIR:
        rhs_transpiler = self.root.rhs_transpiler
        for each in node.args.defaults:
            rhs_transpiler.visit(each)
        transpiler = Transpiler(self.root.filename, self.root.scope)
        transpiler.before_visit(node)

        suite = [transpiler.stmt_transpiler.visit(stmt) for stmt in node.body]
        block = ir.Block(hasCont=ir.hasCont(*suite), suite=suite)

        hasCont = False
        default_arg_entries: list[ir.DefaultArgEntry] = []
        for default, i in zip(node.args.defaults, range(transpiler.posargcount-len(node.args.defaults), transpiler.posargcount)):
            v_arg = self.root.rhs_transpiler.visit(default)
            default_arg_entries.append(ir.DefaultArgEntry(slot=i, value=v_arg))
            hasCont = hasCont or ir.hasCont(v_arg)

        for arg, default in zip(node.args.kwonlyargs, node.args.kw_defaults):
            if default:
                i = transpiler.kwindices.order(arg.arg)
                v_arg = self.root.rhs_transpiler.visit(default)
                default_arg_entries.append(ir.DefaultArgEntry(slot=i, value=v_arg))
                hasCont = hasCont or ir.hasCont(v_arg)


        fptr = transpiler.create_fptr_builder(block, node.name)
        freeslots = [self.root.load_derefence(id) for id in transpiler.scope.freevars]
        func_body = ir.Lambda(hasCont=hasCont, fptr=fptr, default_args=default_arg_entries, freeslots=freeslots)
        lhs = self.root.store_name_(node.name)
        return ir.Assign(hasCont=hasCont, lhs=lhs, rhs=func_body)

    def visit_Assign(self, node: Assign) -> ir.TraffyIR:
        value = self.root.rhs_transpiler.visit(node.value)
        targets = list(map(self.root.lhs_transpiler.visit, node.targets))
        if len(targets) == 1:
            lhs = targets[0]
        else:
            lhs =ir.MultiAssign(hasCont=ir.hasCont(*targets), targets=targets)
        return ir.Assign(hasCont=ir.hasCont(lhs, value), lhs=lhs, rhs=value)

    def visit_AnnAssign(self, node: AnnAssign) -> ir.TraffyIR:
        if not node.value:
            return ir.Constant(o=const_to_variant(None))
        value = self.root.rhs_transpiler.visit(node.value)
        lhs = self.root.lhs_transpiler.visit(node.target)
        return ir.Assign(hasCont=ir.hasCont(lhs, value), lhs=lhs, rhs=value)

    def visit_AugAssign(self, node: AugAssign) -> ir.TraffyIR:
        value = self.root.rhs_transpiler.visit(node.value)
        lhs = self.root.lhs_transpiler.visit(node.target)
        op = self.root.rhs_transpiler._binop_indices[type(node.op)]
        return ir.AugAssign(hasCont=ir.hasCont(lhs, value), lhs=lhs, op=op, rhs=value)

    def visit_While(self, node: While) -> ir.TraffyIR:
        test = self.root.rhs_transpiler.visit(node.test)
        body = [self.visit(each) for each in node.body]
        body_block = ir.Block(hasCont=ir.hasCont(*body), suite=body)
        if node.orelse:
            orelse = [self.visit(each) for each in node.orelse]
            orelse_block = ir.Block(hasCont=ir.hasCont(*orelse), suite=orelse)
            hasCont = ir.hasCont(orelse_block)
        else:
            orelse_block = None
            hasCont = False

        return ir.While(hasCont = hasCont or ir.hasCont(test, body_block), test=test, body=body_block, orelse=orelse_block)

    def visit_Global(self, node: Global) -> ir.TraffyIR:
        return ir.Constant(o=const_to_variant(None))

    def visit_Nonlocal(self, node: Nonlocal) -> ir.TraffyIR:
        return ir.Constant(o=const_to_variant(None))

    @staticmethod
    def _flat(node: If) -> tuple[list[tuple[expr, list[stmt]]], list[stmt]]:
        flatten: list[tuple[expr, list[stmt]]] = []
        while True:
            if len(node.orelse) == 1 and isinstance(node.orelse[0], If):
                flatten.append((node.test, node.body))
                node = node.orelse[0]
            else:
                return (flatten, node.orelse)

    def visit_If(self, node: If) -> ir.TraffyIR:
        flatten, orelse = self._flat(node)
        hasCont = False
        clauses: list[ir.IfClause] = []
        for expr, suite in flatten:
            v_expr = self.root.rhs_transpiler.visit(expr)
            v_suite = [self.visit(stmt) for stmt in suite]
            v_block = ir.Block(hasCont=ir.hasCont(*v_suite), suite=v_suite)
            hasCont = hasCont or ir.hasCont(v_expr, v_block)
            clauses.append(ir.IfClause(cond=v_expr, body=v_block))
        if not orelse:
            v_orelse = None
        else:
            v_orelse = [self.visit(stmt) for stmt in orelse]
            v_orelse = ir.Block(hasCont=ir.hasCont(*v_orelse), suite=v_orelse)
            hasCont = hasCont or ir.hasCont(v_orelse)
        return ir.IfThenElse(hasCont=hasCont, clauses=clauses, orelse=v_orelse)

    def visit_Pass(self, node: Pass) -> ir.TraffyIR:
        return ir.Constant(o=const_to_variant(None))

    def visit_Break(self, node: Break) -> ir.TraffyIR:
        return ir.Break()

    def visit_Continue(self, node: Continue) -> ir.TraffyIR:
        return ir.Continue()

    def visit_Delete(self, node: Delete) -> ir.TraffyIR:
        raise NotImplementedError

    def visit_block(self, suite: list[stmt]) -> ir.Block:
        xs: list[ir.TraffyIR] = []
        for each in suite:
            stmt = self.visit(each)
            xs.append(stmt)
        return ir.Block(hasCont=ir.hasCont(*xs), suite=xs)

    def visit_Try(self, node: Try) -> ir.TraffyIR:
        body = self.visit_block(node.body)
        handlers: list[ir.Handler] = []
        hasCont = ir.hasCont(body)
        for h in node.handlers:
            hasCont_ = False
            if not h.type:
                exc_type = None
            else:
                exc_type = self.root.rhs_transpiler.visit(h.type)
                hasCont_ = hasCont_ or ir.hasCont(exc_type)
            if not h.name:
                exc_bind = None
            else:
                exc_bind = self.root.store_name_(h.name)
                hasCont_ = hasCont_ or ir.hasCont(exc_bind)
            handler_body = self.visit_block(h.body)
            hasCont_ = hasCont_ or ir.hasCont(handler_body)
            handlers.append(ir.Handler(exc_type=exc_type, exc_bind=exc_bind, body=handler_body))
            hasCont = hasCont or hasCont_
        if node.orelse:
            orelse = self.visit_block(node.orelse)
            hasCont = hasCont or ir.hasCont(orelse)
        else:
            orelse = None
        if node.finalbody:
            final = self.visit_block(node.finalbody)
            hasCont = hasCont or ir.hasCont(final)
        else:
            final = None
        return ir.Try(hasCont=hasCont, body=body, handlers=handlers, orelse=None, final=None)

    def visit_Raise(self, node: Raise) -> ir.TraffyIR:
        if node.cause:
            raise NotImplementedError(f"Unity Python does not support raise from yet.")
        if node.exc:
            exc = self.root.rhs_transpiler.visit(node.exc)
            return ir.Raise(hasCont=ir.hasCont(exc), exc=exc)
        return ir.Raise(hasCont=False, exc=None)

def compile_module(filename: str, node: Module):
    top = Transpiler(filename, None)
    top.before_visit(node)
    block = top.stmt_transpiler.visit_block(node.body)
    return top.create_fptr_builder(block, "<module>")
