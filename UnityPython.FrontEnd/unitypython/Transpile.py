from __future__ import annotations
from ast import *
from dataclasses import replace
import warnings
from unitypython.Collections import OrderedSet, OrderedDict
from . import TraffyAsm as ir
from .JSON import dump_json
from typing import Any
from .Scoper import get_position_string, ConciseSymtable, ScoperStmt, ScoperClassStmt
import typing

name_ClassAnnotations = "__class_annotations__"

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
    if isinstance(v, bool):
        return ir.TrBool(value=v)
    elif isinstance(v, int):
        return ir.TrInt(value=v)
    elif isinstance(v, float):
        return ir.TrFloat(value=v)
    elif isinstance(v, str):
        return ir.TrStr(value=v)
    elif v is None:
        return ir.TrNone()
    elif isinstance(v, frozenset):
        raise TypeError(v)
    elif isinstance(v, tuple):
        return ir.TrTuple(elts=list(map(const_to_variant, v)))
    elif isinstance(v, bytes):
        return ir.TrBytes(contents=''.join(map(chr, v))) # type: ignore
    else:
        raise TypeError(v)


def extract_pos(node: AST) -> ir.Span:
    start = ir.Position(line=node.lineno, col=node.col_offset)
    if node.end_lineno is not None:
        end_line = node.end_lineno
    else:
        end_line = start.line
    if node.end_col_offset is not None:
        end_col_offset = node.end_col_offset
    else:
        end_col_offset = start.col
    end = ir.Position(line=end_line, col=end_col_offset)
    return ir.Span(start, end)


_FunctionAST_Types = (FunctionDef, Lambda, AsyncFunctionDef)
_ComprehensionAST_Types = (ListComp, SetComp, DictComp, GeneratorExp)
_Comprehension_ArgName = ".0"

class Transpiler:
    def __init__(
        self,
        filename: str,
        source_code: str | None,
        init_pos: ir.Span,
        parent_scope: ConciseSymtable | None,
    ):
        self.source_code = source_code
        self.instructions = []  # type: list[ir.TraffyIR]
        self.constants: OrderedDict[tuple[type, object], ir.TrObject] = OrderedDict()
        self.filename = filename
        self.parent_scope = parent_scope
        self.rhs_transpiler = TranspilerRHS(self)
        self.lhs_transpiler = TranspilerLHS(self)
        self.stmt_transpiler = TranspileStmt(self)
        self.class_annotations: OrderedSet[str] = OrderedSet()

        self.flag = 0
        self._cur_pos: ir.Span = init_pos
        self.positions: OrderedSet[ir.Position] = OrderedSet()
        self.spans: OrderedSet[tuple[int, int]] = OrderedSet()
        self._add_span(init_pos)

        self.posargcount = 0  # count of non-variadic positional args
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
    def cur_pos(self, value: ir.Span):
        self._cur_pos = value
        self._add_span(value)

    def _add_span(self, span: ir.Span):
        self.positions.add(span.start)
        self.positions.add(span.end)
        m_key = (self.positions.order(span.start), self.positions.order(span.end))
        self.spans.add(m_key)

    @property
    def pos_ind(self) -> int:
        span = self._cur_pos
        m_key = (self.positions.order(span.start), self.positions.order(span.end))
        return self.spans.order(m_key)

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
            return -i - 1
        else:
            raise NameError(f"{id} is not a local/free variable")

    def load_name_(self, id: str) -> ir.LocalVar | ir.GuessVar | ir.GlobalVar | ir.FreeVar :
        if id in self.scope.unknown_vars:
            return ir.GuessVar(
                name=ir.TrStr(value=id, isInterned=False), position=self.pos_ind
            )
        if id in self.scope.localvars:
            i = self.scope.localvars.order(id)
            return ir.LocalVar(slot=i, position=self.pos_ind)
        elif id in self.scope.freevars:
            i = self.scope.freevars.order(id)
            return ir.FreeVar(slot=i, position=self.pos_ind)
        else:
            return ir.GlobalVar(name=const_to_variant(id), position=self.pos_ind)

    def store_name_(self, id: str) -> ir.TraffyLHS:

        if id in self.scope.localvars:
            i = self.scope.localvars.order(id)
            return ir.StoreLocal(slot=i)
        elif id in self.scope.freevars:
            i = self.scope.freevars.order(id)
            return ir.StoreFree(slot=i)
        else:
            return ir.StoreGlobal(name=const_to_variant(id))

    def delete_name_(self, id: str) -> ir.TraffyIR:
        if id in self.scope.localvars:
            i = self.scope.localvars.order(id)
            return ir.DeleteLocal(slot=i)
        elif id in self.scope.freevars:
            i = self.scope.freevars.order(id)
            return ir.DeleteFree(slot=i)
        else:
            return ir.DeleteGlobal(name=const_to_variant(id))

    def create_fptr_builder(self, code: ir.TraffyIR, name: str) -> ir.TrFuncPointer:
        metadata = ir.Metadata(
            positions=list(self.positions),
            spanPointers=list(self.spans),
            sourceCode=None,
            localnames=list(self.scope.localvars),
            freenames=list(self.scope.freevars),
            codename=name,
            filename=None,
        )
        hasvararg = bool(self.vararg)
        haskwarg = bool(self.kwarg)
        return ir.TrFuncPointer(
            posargcount=self.posargcount,
            allargcount=self.allargcount,
            hasvararg=hasvararg,
            haskwarg=haskwarg,
            kwindices={
                v: const_to_variant(k) for k, v in self.kwindices.asdict().items()
            },
            code=code,
            metadata=metadata,
        )

    def before_visit(self, node: Module | FunctionDef | Lambda | ClassDef | AsyncFunctionDef | ListComp | SetComp | DictComp | GeneratorExp):
        if isinstance(node, Module):
            self.scope = ConciseSymtable.new(self.parent_scope)
        elif isinstance(node, ClassDef):
            scoper = ScoperClassStmt(self.filename, self.parent_scope)
            scoper.symtable_builder.mut_var(name_ClassAnnotations)
            for each in node.body:
                scoper.visit(each)
            self.scope = scoper.solve()
        elif isinstance(node, _FunctionAST_Types):
            scoper = ScoperStmt(self.filename, self.parent_scope)
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
        elif isinstance(node, _ComprehensionAST_Types):
            scoper = ScoperStmt(self.filename, self.parent_scope)
            scoper.symtable_builder.add_arg(_Comprehension_ArgName)
            self.posargcount = 1
            self.allargcount = 1
            rhs_scoper = scoper.rhs_scoper
            lhs_scoper = scoper.lhs_scoper
            for i, each in enumerate(node.generators):
                if i != 0:
                    rhs_scoper.visit(each.iter)
                lhs_scoper.visit(each.target)
            if not isinstance(node, DictComp):
                rhs_scoper.visit(node.elt)
            else:
                rhs_scoper.visit(node.key)
                rhs_scoper.visit(node.value)
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

    def visit_many(self, xs: list[expr]):
        return list(map(self.visit, xs))

    def visit_with_pos(self, node: expr):
        self.root.cur_pos = extract_pos(node)
        pos = self.root.pos_ind
        return (pos, self.visit(node))

    def instr_offset(self):
        return len(self.root.instructions)

    def visit_Constant(self, node: Constant):
        ob = const_to_variant(node.value)
        return ir.Constant(o=ob)

    def visit_BoolOp(self, node: BoolOp):
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        if isinstance(node.op, And):
            if len(node.values) == 2:
                l = self.visit(node.values[0])
                r = self.visit(node.values[1])
                return ir.BoolAnd2(
                    hasCont=l.hasCont or r.hasCont,
                    left=l,
                    right=r,
                    position=position,
                )

            left = self.visit(node.values[0])
            args = list(map(self.visit, node.values[1:]))
            hasCont = left.hasCont or ir.hasCont(*args)
            return ir.BoolAnd(
                hasCont=hasCont, left=left, comparators=args, position=position
            )

        elif isinstance(node.op, Or):
            if len(node.values) == 2:
                l = self.visit(node.values[0])
                r = self.visit(node.values[1])
                return ir.BoolOr2(
                    position=position,
                    hasCont=l.hasCont or r.hasCont,
                    left=l,
                    right=r,
                )

            left = self.visit(node.values[0])
            args = list(map(self.visit, node.values[1:]))
            hasCont = left.hasCont or ir.hasCont(args)
            return ir.BoolOr(
                position=position, hasCont=hasCont, left=left, comparators=args
            )

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
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        op = self._binop_indices[type(node.op)]
        l = self.visit(node.left)
        r = self.visit(node.right)
        return ir.BinOp(
            position=position, hasCont=ir.hasCont(l, r), left=l, right=r, op=op
        )

    def visit_NamedExpr(self, node: NamedExpr) -> Any:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        expr = self.visit(node.value)
        lhs_transpiler = TranspilerLHS(self.root)
        lhs = lhs_transpiler.visit(node.target)
        return ir.NamedExpr(
            position=position,
            hasCont=ir.hasCont(expr, lhs),
            lhs=lhs,
            expr=expr,
        )

    _unaryop_indices: dict[typing.Type[unaryop], ir.OpU] = {
        Invert: ir.OpU.INV,
        Not: ir.OpU.NOT,
        UAdd: ir.OpU.POS,
        USub: ir.OpU.NEG,
    }

    def visit_UnaryOp(self, node: UnaryOp) -> Any:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        operand = self.visit(node.operand)
        op = self._unaryop_indices[type(node.op)]
        return ir.UnaryOp(
            position=position,
            hasCont=ir.hasCont(operand),
            op=op,
            operand=operand,
        )

    def visit_Lambda(self, node: Lambda) -> Any:
        for each in node.args.defaults:
            self.visit(each)
        transpiler = Transpiler(
            self.root.filename,
            self.root.source_code,
            extract_pos(node),
            self.root.scope,
        )
        transpiler.before_visit(node)
        body = transpiler.rhs_transpiler.visit(node.body)
        lambda_body = ir.Return(
            position=transpiler.pos_ind, hasCont=ir.hasCont(body), value=body
        )

        hasCont = False
        default_arg_entries: list[ir.DefaultArgEntry] = []
        for default, i in zip(
            node.args.defaults,
            range(
                transpiler.posargcount - len(node.args.defaults), transpiler.posargcount
            ),
        ):
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
        return ir.Lambda(
            hasCont=hasCont,
            fptr=fptr,
            default_args=default_arg_entries,
            freeslots=freeslots,
        )

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
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        l = self.visit(node.left)
        hasCont = ir.hasCont(l)
        comparators: list[ir.TraffyIR] = []
        for each in node.comparators:
            v = self.visit(each)
            comparators.append(v)
            hasCont = hasCont or ir.hasCont(v)
        op = self._cmp_opcodes[type(node.ops[0])]
        return ir.CmpOp(
            position=position,
            hasCont=hasCont,
            op=op,
            left=l,
            comparators=comparators,
        )

    def visit_Call(self, node: Call) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
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
        return ir.CallEx(
            position=position,
            hasCont=hasCont,
            func=func,
            args=args,
            kwargs=kwargs,
        )

    def visit_Slice(self, node: Slice) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
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
        slice = ir.GlobalVar(position=position, name=const_to_variant("slice"))
        return ir.CallEx(
            position=position,
            hasCont=ir.hasCont(low, high, step),
            func=slice,
            args=[
                ir.SequenceElement(False, low),
                ir.SequenceElement(False, high),
                ir.SequenceElement(False, step),
            ],
            kwargs=[],
        )

    def visit_Attribute(self, node: Attribute) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        value = self.visit(node.value)
        attr = node.attr
        return ir.Attribute(
            position=position,
            hasCont=ir.hasCont(value),
            value=value,
            attr=ir.TrStr(attr),
        )

    def visit_Subscript(self, node: Subscript) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        value = self.visit(node.value)
        item = self.visit(node.slice)
        return ir.Subscript(
            position=position,
            hasCont=ir.hasCont(value, item),
            value=value,
            item=item,
        )

    def visit_Index(self, node: Index) -> ir.TraffyIR:
        return self.visit(getattr(node, "value"))

    def visit_ExtSlice(self, node: ExtSlice) -> ir.TraffyIR:
        raise NotImplementedError

    def visit_Starred(self, node: Starred) -> ir.TraffyIR:
        raise TypeError(
            "Right-hand side '*' expression not handled, please report a bug."
        )

    def visit_Name(self, node: Name) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
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
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        hasCont, sequence = self.visit_sequence(node.elts)
        return ir.List(position=position, hasCont=hasCont, elements=sequence)

    def visit_Set(self, node: Set) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        hasCont, sequence = self.visit_sequence(node.elts)
        return ir.Set(position=position, hasCont=hasCont, elements=sequence)

    def visit_Tuple(self, node: Tuple) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        hasCont, sequence = self.visit_sequence(node.elts)
        return ir.Tuple(position=position, hasCont=hasCont, elements=sequence)

    def visit_Dict(self, node: Dict) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
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
        return ir.Dict(position=position, hasCont=hasCont, entries=args)

    def visit_Yield(self, node: Yield) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        if node.value:
            value = self.visit(node.value)
        else:
            value = ir.Constant(o=const_to_variant(None))
        return ir.Yield(position=position, value=value)

    def visit_YieldFrom(self, node: YieldFrom) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        value = self.visit(node.value)
        return ir.YieldFrom(position=position, value=value)

    def visit_Await(self, node: Await) -> Any:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        value = self.visit(node.value)
        return ir.AwaitValue(position=position, value=value)

    def visit_FormattedValue(self, node: FormattedValue) -> Any:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        value = self.visit(node.value)
        conv = node.conversion
        fv = ir.FormattedValue(position=position, hasCont=value.hasCont, value=value)

        # https://docs.python.org/3/library/ast.html#ast.FormattedValue
        if conv == -1:
            fv.convStr = True
        elif conv == 115:
            fv.convStr = True
        elif conv == 114:
            fv.convRepr = True
        elif conv == 97:
            warn = UserWarning(
                "ascii conversion is not supported, use __str__ for conversion {:a}."
            )
            warnings.warn(warn)
            fv.convStr = True
        else:
            raise ValueError(f"Unknown conversion: {conv}")
        if node.format_spec:
            warn = UserWarning(
                "Format specifier not supported, please report a bug."
            )
            warnings.warn(warn)
        return fv

    def visit_JoinedStr(self, node: JoinedStr) -> Any:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        hasCont = False
        args: list[ir.TraffyIR] = []
        for each in node.values:
            if isinstance(each, FormattedValue):
                arg = self.visit(each)
                args.append(arg)
            else:
                arg = self.visit(each)
                args.append(arg)
            hasCont = hasCont or ir.hasCont(arg)
        return ir.JoinedStr(position=position, hasCont=hasCont, values=args)


    def visit_Comp(self, node: ListComp | GeneratorExp | SetComp | DictComp, create_comp):
        topmost_itr = self.visit(node.generators[0].iter)
        transpiler = Transpiler(
            self.root.filename,
            self.root.source_code,
            extract_pos(node),
            self.root.scope,
        )
        position = transpiler.pos_ind
        transpiler.before_visit(node)
        transpiler.cur_pos = extract_pos(node.generators[0].iter)
        topmost_pos = transpiler.pos_ind
        comprehensions: list[tuple[ir.TraffyLHS, ir.TraffyIR, list[ir.TraffyIR] | None]] = []
        for i, gen in enumerate(node.generators):
            lhs = transpiler.lhs_transpiler.visit(gen.target)
            ifs = transpiler.rhs_transpiler.visit_many(gen.ifs)
            if not ifs:
                ifs = None
            if i == 0:
                load = transpiler.load_name_(_Comprehension_ArgName)
                load.position = topmost_pos
                load.position
                comprehensions.append((lhs, load, ifs))
            else:
                rhs = transpiler.rhs_transpiler.visit(gen.iter)
                comprehensions.append((lhs, rhs, ifs))

        do_yield_from, inner_expr = create_comp(transpiler, position, comprehensions, node)
        fptr = transpiler.create_fptr_builder(inner_expr, "<generator>")
        freeslots = [self.root.load_derefence(id) for id in transpiler.scope.freevars]
        func = ir.Lambda(hasCont=False, fptr=fptr, default_args=[], freeslots=freeslots)
        call = ir.CallEx(
                position,
                hasCont=topmost_itr.hasCont,
                func=func,
                args=[ir.SequenceElement(unpack=False, value=topmost_itr)],
                kwargs=[]
            )
        if not do_yield_from:
            return call
        else:
            return ir.YieldFrom(position=position, value=call)

    @staticmethod
    def create_listcomp_yield(transpiler: Transpiler, position: int, comprehensions: list[tuple[ir.TraffyLHS, ir.TraffyIR, list[ir.TraffyIR] | None]], node: ListComp) -> tuple[bool, ir.TraffyIR]:
        elt = transpiler.rhs_transpiler.visit(node.elt)
        lhs, rhs, ifs = comprehensions[-1]
        last = ir.Comprehension(lhs.hasCont or rhs.hasCont or (bool(ifs) and ir.hasCont(ifs)), lhs, rhs, ifs, None)
        for lhs, rhs, ifs in comprehensions[:-1]:
            last = ir.Comprehension(lhs.hasCont or rhs.hasCont or (bool(ifs) and ir.hasCont(ifs)), lhs, rhs, ifs, last)
        inner_expr = ir.ListComp(position, last.hasCont or elt.hasCont, elt, last)
        return inner_expr.hasCont, ir.Return(position, inner_expr.hasCont, inner_expr)

    @staticmethod
    def create_setcomp_yield(transpiler: Transpiler, position: int, comprehensions: list[tuple[ir.TraffyLHS, ir.TraffyIR, list[ir.TraffyIR] | None]], node: SetComp) -> tuple[bool, ir.TraffyIR]:
        elt = transpiler.rhs_transpiler.visit(node.elt)
        lhs, rhs, ifs = comprehensions[-1]
        last = ir.Comprehension(lhs.hasCont or rhs.hasCont or (bool(ifs) and ir.hasCont(ifs)), lhs, rhs, ifs, None)
        for lhs, rhs, ifs in comprehensions[:-1]:
            last = ir.Comprehension(lhs.hasCont or rhs.hasCont or (bool(ifs) and ir.hasCont(ifs)), lhs, rhs, ifs, last)
        inner_expr = ir.SetComp(position, last.hasCont or elt.hasCont, elt, last)
        return inner_expr.hasCont, ir.Return(position, inner_expr.hasCont, inner_expr)

    @staticmethod
    def create_generator_yield(transpiler: Transpiler, position: int, comprehensions: list[tuple[ir.TraffyLHS, ir.TraffyIR, list[ir.TraffyIR] | None]], node: SetComp) -> tuple[bool, ir.TraffyIR]:
        elt = transpiler.rhs_transpiler.visit(node.elt)
        lhs, rhs, ifs = comprehensions[-1]
        last = ir.Comprehension(lhs.hasCont or rhs.hasCont or (bool(ifs) and ir.hasCont(ifs)), lhs, rhs, ifs, None)
        for lhs, rhs, ifs in comprehensions[:-1]:
            last = ir.Comprehension(lhs.hasCont or rhs.hasCont or (bool(ifs) and ir.hasCont(ifs)), lhs, rhs, ifs, last)
        inner_expr = ir.GeneratorComp(position, elt, last)
        return (last.hasCont or elt.hasCont), ir.Return(position, inner_expr.hasCont, inner_expr)

    @staticmethod
    def create_dictcomp_yield(transpiler: Transpiler, position: int, comprehensions: list[tuple[ir.TraffyLHS, ir.TraffyIR, list[ir.TraffyIR] | None]], node: DictComp) -> tuple[bool, ir.TraffyIR]:
        key = transpiler.rhs_transpiler.visit(node.key)
        value = transpiler.rhs_transpiler.visit(node.value)
        lhs, rhs, ifs = comprehensions[-1]
        last = ir.Comprehension(lhs.hasCont or rhs.hasCont or (bool(ifs) and ir.hasCont(ifs)), lhs, rhs, ifs, None)
        for lhs, rhs, ifs in comprehensions[:-1]:
            last = ir.Comprehension(lhs.hasCont or rhs.hasCont or (bool(ifs) and ir.hasCont(ifs)), lhs, rhs, ifs, last)
        inner_expr = ir.DictComp(position, last.hasCont or ir.hasCont(key, value), key, value, last)
        return inner_expr.hasCont, ir.Return(position, inner_expr.hasCont, inner_expr)

    def visit_ListComp(self, node: ListComp) -> Any:
        return self.visit_Comp(node, self.create_listcomp_yield)

    def visit_SetComp(self, node: SetComp) -> Any:
        return self.visit_Comp(node, self.create_setcomp_yield)

    def visit_DictComp(self, node: DictComp) -> Any:
        return self.visit_Comp(node, self.create_dictcomp_yield)

    def visit_GeneratorExp(self, node: GeneratorExp) -> Any:
        return self.visit_Comp(node, self.create_generator_yield)

class TranspilerLHS(LHSTransformerInlineCache):
    def __init__(self, root: Transpiler):
        self.root = root

    def instr_offset(self):
        return len(self.root.instructions)

    def visit_Name(self, node: Name) -> ir.TraffyLHS:
        return self.root.store_name_(node.id)

    def visit_Attribute(self, node: Attribute) -> ir.TraffyLHS:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        value = self.root.rhs_transpiler.visit(node.value)
        return ir.StoreAttr(
            position=position,
            hasCont=ir.hasCont(value),
            value=value,
            attr=ir.TrStr(node.attr),
        )

    def visit_Starred(self, node: Starred) -> ir.TraffyLHS:
        raise TypeError(
            "Left-hand side '*' expression not handled, please report a bug."
        )

    def visit_Subscript(self, node: Subscript) -> ir.TraffyLHS:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        value = self.root.rhs_transpiler.visit(node.value)
        item = self.root.rhs_transpiler.visit(node.slice)
        return ir.StoreItem(
            position=position, hasCont=ir.hasCont(value, item), value=value, item=item
        )

    def visit_Tuple(self, node: Tuple) -> ir.TraffyLHS:
        return self.visit_List(node)

    def visit_List(self, node: List | Tuple) -> ir.TraffyLHS:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        if not any(isinstance(elt, Starred) for elt in node.elts):
            xs = list(map(self.visit, node.elts))
            return ir.StoreList(position=position, hasCont=ir.hasCont(xs), elts=xs)
        offset = 0
        for i, elt in enumerate(node.elts):
            if isinstance(elt, Starred):
                offset = i
        xs = list(map(self.visit, node.elts[:offset]))
        ys = list(map(self.visit, node.elts[offset + 1 :]))

        starred = node.elts[offset]
        assert isinstance(starred, Starred)
        v = self.visit(starred.value)
        return ir.StoreListEx(
            position=position,
            hasCont=ir.hasCont(*xs, *ys, v),
            before=xs,
            unpack=v,
            after=ys,
        )


class TranspileStmt(IRStmtTransformerInlineCache):
    def __init__(self, root: Transpiler):
        self.root = root

    def instr_offset(self):
        return len(self.root.instructions)

    def visit_Expr(self, node: Expr) -> Any:
        return self.root.rhs_transpiler.visit(node.value)

    def visit_many(self, nodes: list[stmt]):
        xs = list(map(self.visit, nodes))
        if xs:
            return ir.Block(hasCont=ir.hasCont(xs), suite=xs)
        return ir.Constant(o=const_to_variant(None))

    def visit_For(self, node: For) -> Any:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        hasCont = False
        iterable = self.root.rhs_transpiler.visit(node.iter)
        hasCont = hasCont or iterable.hasCont
        target = self.root.lhs_transpiler.visit(node.target)
        hasCont = hasCont or target.hasCont
        body = self.visit_many(node.body)
        hasCont = hasCont or body.hasCont
        if node.orelse:
            orelse = self.visit_many(node.orelse)
            hasCont = hasCont or ir.hasCont(orelse)
        else:
            orelse = None
        return ir.ForIn(
            position=position,
            hasCont=hasCont,
            itr=iterable,
            target=target,
            body=body,
            orelse=orelse,
        )

    def visit_Return(self, node: Return) -> Any:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        if node.value:
            expr = self.root.rhs_transpiler.visit(node.value)
        else:
            expr = ir.Constant(o=const_to_variant(None))
        return ir.Return(position=position, hasCont=ir.hasCont(expr), value=expr)

    def visit_ClassDef(self, node: ClassDef) -> Any:
        span = extract_pos(node)
        if node.bases:
            last_base_span = extract_pos(node.bases[-1])
            span = replace(span, end = last_base_span.end)
        self.root.cur_pos = span
        head_position = self.root.pos_ind
        rhs_transpiler = self.root.rhs_transpiler
        decorator_with_pos_list = list(map(rhs_transpiler.visit_with_pos, node.decorator_list))
        
        bases = list(map(rhs_transpiler.visit, node.bases))
        transpiler = Transpiler(
            self.root.filename,
            self.root.source_code,
            extract_pos(node),
            self.root.scope,
        )
        transpiler.before_visit(node)
        suite = [transpiler.stmt_transpiler.visit(stmt) for stmt in node.body]
        
        suite.reverse()
        ann_rhs = ir.Tuple(head_position, False, [
            ir.SequenceElement(False, ir.Constant(_const_to_variant(each)))
            for each in transpiler.class_annotations
        ])
        ann_lhs = ir.Assign(
            head_position,
            False,
            transpiler.store_name_(name_ClassAnnotations),
            ann_rhs
        )
        suite.append(ann_lhs)
        suite.reverse()

        block = ir.Block(hasCont=ir.hasCont(suite), suite=suite)
        freeslots = [self.root.load_derefence(id) for id in transpiler.scope.freevars]
        fptr = transpiler.create_fptr_builder(block, node.name)
        func_body = ir.Lambda(
            hasCont=False,
            fptr=fptr,
            default_args=[],
            freeslots=freeslots,
        )
        func_body = ir.DefClass(
            position=head_position, hasCont=ir.hasCont(bases), bases=bases, body=func_body
        )

        if decorator_with_pos_list:
            for decorator_pos, each in reversed(decorator_with_pos_list):
                func_body = ir.CallEx(
                    position=decorator_pos,
                    hasCont=ir.hasCont(each, func_body),
                    func=each,
                    args=[ir.SequenceElement(False, func_body)],
                    kwargs=[],
                )
        lhs = self.root.store_name_(node.name)
        return ir.Assign(
            position=head_position, hasCont=func_body.hasCont, lhs=lhs, rhs=func_body
        )

    def visit_AsyncFunctionDef(self, node: AsyncFunctionDef) -> Any:
        return self.visit_FunctionDef(node)

    def visit_FunctionDef(self, node: FunctionDef | AsyncFunctionDef) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        rhs_transpiler = self.root.rhs_transpiler
        decorator_list = list(map(rhs_transpiler.visit, node.decorator_list))
        for each in node.args.defaults:
            rhs_transpiler.visit(each)
        transpiler = Transpiler(
            self.root.filename,
            self.root.source_code,
            extract_pos(node),
            self.root.scope,
        )
        transpiler.before_visit(node)
        position = transpiler.pos_ind
        suite = [transpiler.stmt_transpiler.visit(stmt) for stmt in node.body]
        if isinstance(node, AsyncFunctionDef):
            block = ir.AsyncBlock(suite=suite)
        else:
            block = ir.Block(hasCont=ir.hasCont(suite), suite=suite)

        hasCont = False
        default_arg_entries: list[ir.DefaultArgEntry] = []
        for default, i in zip(
            node.args.defaults,
            range(
                transpiler.posargcount - len(node.args.defaults), transpiler.posargcount
            ),
        ):
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
        func_body = ir.Lambda(
            hasCont=hasCont,
            fptr=fptr,
            default_args=default_arg_entries,
            freeslots=freeslots,
        )
        if decorator_list:
            for each in reversed(decorator_list):
                func_body = ir.CallEx(
                    position=position,
                    hasCont=ir.hasCont(each, func_body),
                    func=each,
                    args=[ir.SequenceElement(False, func_body)],
                    kwargs=[],
                )
        lhs = self.root.store_name_(node.name)
        return ir.Assign(
            position=position,
            hasCont=hasCont or func_body.hasCont,
            lhs=lhs,
            rhs=func_body,
        )

    def visit_Assign(self, node: Assign) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        value = self.root.rhs_transpiler.visit(node.value)
        targets = list(map(self.root.lhs_transpiler.visit, node.targets))
        if len(targets) == 1:
            lhs = targets[0]
        else:
            lhs = ir.MultiAssign(hasCont=ir.hasCont(*targets), targets=targets)
        return ir.Assign(
            position=position, hasCont=ir.hasCont(lhs, value), lhs=lhs, rhs=value
        )

    def visit_AnnAssign(self, node: AnnAssign) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        if self.root.scope.is_class_level:
            if isinstance(node.target, Name):
                self.root.class_annotations.add(node.target.id)
        if not node.value:
            return ir.Constant(o=const_to_variant(None))
        value = self.root.rhs_transpiler.visit(node.value)
        lhs = self.root.lhs_transpiler.visit(node.target)
        return ir.Assign(
            position=position, hasCont=ir.hasCont(lhs, value), lhs=lhs, rhs=value
        )

    def visit_AugAssign(self, node: AugAssign) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        value = self.root.rhs_transpiler.visit(node.value)
        lhs = self.root.lhs_transpiler.visit(node.target)
        op = self.root.rhs_transpiler._binop_indices[type(node.op)]
        return ir.AugAssign(
            position=position, hasCont=ir.hasCont(lhs, value), lhs=lhs, op=op, rhs=value
        )

    def visit_While(self, node: While) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
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

        return ir.While(
            position=position,
            hasCont=hasCont or ir.hasCont(test, body_block),
            test=test,
            body=body_block,
            orelse=orelse_block,
        )

    def visit_Global(self, node: Global) -> ir.TraffyIR:
        return ir.Constant(o=const_to_variant(None))

    def visit_Nonlocal(self, node: Nonlocal) -> ir.TraffyIR:
        return ir.Constant(o=const_to_variant(None))

    @staticmethod
    def _flat(node: If) -> tuple[list[tuple[expr, list[stmt]]], list[stmt]]:
        flatten: list[tuple[expr, list[stmt]]] = []
        while True:
            flatten.append((node.test, node.body))
            if len(node.orelse) == 1 and isinstance(node.orelse[0], If):
                node = node.orelse[0]
            else:
                return (flatten, node.orelse)

    def visit_If(self, node: If) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
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
        return ir.IfThenElse(
            position=position, hasCont=hasCont, clauses=clauses, orelse=v_orelse
        )

    def visit_Pass(self, node: Pass) -> ir.TraffyIR:
        return ir.Constant(o=const_to_variant(None))

    def visit_Break(self, node: Break) -> ir.TraffyIR:
        return ir.Break()

    def visit_Continue(self, node: Continue) -> ir.TraffyIR:
        return ir.Continue()

    def visit_Delete(self, node: Delete) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        block : list[ir.TraffyIR] = []
        for each in node.targets:
            assert not isinstance(each, Attribute)
            if isinstance(each, Name):
                block.append(self.root.delete_name_(each.id))
            elif isinstance(each, Subscript):
                value = self.root.rhs_transpiler.visit(each.value)
                item = self.root.rhs_transpiler.visit(each.slice)
                block.append(ir.DeleteItem(position=position, hasCont=ir.hasCont(value, item), value=value, item=item))
            else:
                e = SyntaxError()
                e.msg = f"Delete target {each} is not supported"
                e.lineno = each.lineno
                e.offset = each.col_offset
                e.filename = self.root.filename
                raise e
        if len(block) == 1:
            return block[0]
        return ir.Block(hasCont=ir.hasCont(*block), suite=block)

    def visit_block(self, suite: list[stmt]) -> ir.Block:
        xs: list[ir.TraffyIR] = []
        for each in suite:
            stmt = self.visit(each)
            xs.append(stmt)
        return ir.Block(hasCont=ir.hasCont(*xs), suite=xs)

    def visit_Try(self, node: Try) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
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
            handlers.append(
                ir.Handler(exc_type=exc_type, exc_bind=exc_bind, body=handler_body)
            )
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
        return ir.Try(
            position=position,
            hasCont=hasCont,
            body=body,
            handlers=handlers,
            orelse=orelse,
            final=final,
        )

    def visit_Raise(self, node: Raise) -> ir.TraffyIR:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        if node.cause:
            raise NotImplementedError(f"Unity Python does not support raise from yet.")
        if node.exc:
            exc = self.root.rhs_transpiler.visit(node.exc)
            return ir.Raise(position=position, hasCont=ir.hasCont(exc), exc=exc)
        return ir.Raise(position=position, hasCont=False, exc=None)

    def visit_With(self, node: With) -> Any:
        with_items: list[ir.WithItem] = []
        for item in node.items:
            self.root.cur_pos = extract_pos(item.context_expr)
            position = self.root.pos_ind
            ctx = self.root.rhs_transpiler.visit(item.context_expr)
            hasCont = ctx.hasCont
            bind = None
            if item.optional_vars:
                bind = self.root.lhs_transpiler.visit(item.optional_vars)
                hasCont = hasCont or bind.hasCont
            with_items.append(ir.WithItem(position, hasCont, ctx, bind))
        body = self.visit_many(node.body)
        hasCont = ir.hasCont(with_items) or ir.hasCont(body)
        return ir.With(hasCont, with_items, body)

    def visit_Import(self, node: Import) -> Any:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        names: list[str] = []
        asnames: list[ir.TraffyLHS] = []
        for each in node.names:
            name = each.name
            asname = each.asname or name
            names.append(name)
            asnames.append(self.root.store_name_(asname))
        rhs = ir.Import(position, 0, None, names)
        lhs = ir.StoreList(position, False, asnames)
        assign = ir.Assign(position, False, lhs, rhs)
        return assign

    def visit_ImportFrom(self, node: ImportFrom) -> Any:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        names: list[str] = []
        asnames: list[ir.TraffyLHS] = []
        import_star = False
        for each in node.names:
            name = each.name
            if name == '*':
                assert not names
                import_star = True
                names.append(name)
                break
            asname = each.asname or name
            names.append(name)
            asnames.append(self.root.store_name_(asname))
        if import_star:
            assert node.module
            return ir.ImportStar(position, node.level, node.module)
        rhs = ir.Import(position, node.level, node.module, names)
        lhs = ir.StoreList(position, False, asnames)
        assign = ir.Assign(position, False, lhs, rhs)
        return assign

    def visit_Assert(self, node: Assert) -> Any:
        self.root.cur_pos = extract_pos(node)
        position = self.root.pos_ind
        hasCont = False
        test = self.root.rhs_transpiler.visit(node.test)
        hasCont = hasCont or test.hasCont
        msg = None
        if node.msg:
            msg = self.root.rhs_transpiler.visit(node.msg)
            hasCont = hasCont or msg.hasCont
        return ir.Assert(position, hasCont, test, msg)

def compile_module(filename: str,  modulename: str, src: str, ignore_src: bool = False) -> ir.ModuleSpec:
    node: Module = parse(src, filename=filename)
    top = Transpiler(filename, None if ignore_src else src, ir.Span.empty(), None)
    top.before_visit(node)
    block = top.stmt_transpiler.visit_block(node.body)
    msc = ir.ModuleSharingContext(
        modulename=modulename, filename=filename, sourceCode=src
    )
    fptr = top.create_fptr_builder(block, "<module>")
    return ir.ModuleSpec(msc, fptr)
