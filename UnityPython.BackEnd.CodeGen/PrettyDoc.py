from dataclasses import dataclass
import typing
import io

_T = typing.TypeVar("_T")

if typing.TYPE_CHECKING:
    Doc = typing.Union[
        'Doc_Concat',
        'Doc_VSep',
        'Doc_Align',
        'Doc_Indent',
        'Doc_LineSeg',
        'Doc_Breakable',
        'Doc_Empty',
    ]
else:
    Doc = object

if typing.TYPE_CHECKING:
    PDoc = typing.Union[
        'PDoc_Breakable',
        'PDoc_LineSeg',
        'PDoc_PopIndent',
        'PDoc_PushCurrentIndent',
        'PDoc_PushIndent',
    ]
else:
    PDoc = object


class DocRt:
    def __mul__(self: Doc, b: Doc) -> Doc:  # type: ignore
        return Doc_Concat(self, b)

    def __add__(self: Doc, b: Doc) -> Doc:  # type: ignore
        return self * Doc_LineSeg(" ") * b

    def __rshift__(self: Doc, i: int):  # type: ignore
        return Doc_Indent(i, self)

    def show(self: Doc, render_options: 'RenderOptions | None' = None):  # type: ignore
        m_str_io = io.StringIO()
        write = typing.cast(typing.Callable[[str], None], m_str_io.write)
        self.render(write, render_options)
        return m_str_io.getvalue()

    def render(
        self: Doc,  # type: ignore
        write: typing.Callable[[str], None],
        render_options: 'RenderOptions | None' = None,
    ):
        render_options = render_options or default_render_options
        render(render_options, compile_to_prims(self), write)


@dataclass
class Doc_Empty(DocRt):
    pass


@dataclass
class Doc_Concat(DocRt):
    l: Doc
    r: Doc


@dataclass
class Doc_VSep(DocRt):
    elements: 'list[Doc]'


@dataclass
class Doc_Align(DocRt):
    it: Doc


@dataclass
class Doc_Indent(DocRt):
    indent: int
    inner: Doc


@dataclass
class Doc_LineSeg(DocRt):
    it: str


@dataclass
class Doc_Breakable(DocRt):
    it: Doc


@dataclass
class PDoc_PopIndent:
    def cast(self) -> PDoc:
        return self


@dataclass
class PDoc_PushCurrentIndent:
    def cast(self) -> PDoc:
        return self


@dataclass
class PDoc_PushIndent:
    indent: int

    def cast(self) -> PDoc:
        return self


@dataclass
class PDoc_LineSeg:
    it: str

    def cast(self) -> PDoc:
        return self


@dataclass
class PDoc_Breakable:
    def cast(self) -> PDoc:
        return self


def compile_to_prims(doc: Doc) -> 'list[list[PDoc]]':

    if isinstance(doc, Doc_LineSeg):
        return [[PDoc_LineSeg(doc.it)]]

    elif isinstance(doc, Doc_Breakable):
        it = compile_to_prims(doc.it)
        if not it or len(it) == 1 and not it[0]:
            return [[PDoc_Breakable()]]
        else:
            it[0] = [PDoc_Breakable().cast()] + it[0]
            return it
    elif isinstance(doc, Doc_Concat):
        l = compile_to_prims(doc.l)
        r = compile_to_prims(doc.r)
        if not l:
            return r
        elif not r:
            return l

        return l[:-1] + [l[-1] + r[0]] + r[1:]

    elif isinstance(doc, Doc_Align):
        it = compile_to_prims(doc.it)
        if not it:
            return it

        it[0] = [PDoc_PushCurrentIndent().cast()] + it[0]
        it[-1] = it[-1] + [PDoc_PopIndent().cast()]

        return it

    elif isinstance(doc, Doc_Indent):
        prefix = [PDoc_PushIndent(doc.indent).cast()]
        it = compile_to_prims(doc.inner)
        if not it:
            return it

        it[0] = prefix + it[0]
        it[-1] = it[-1] + [PDoc_PopIndent().cast()]
        return it
    elif isinstance(doc, Doc_Empty):
        return []
    else:  # isinstance(doc, Doc_VSep):

        return [v for elt in doc.elements for v in compile_to_prims(elt)]


@dataclass
class RenderOptions:
    expect_line_width: int


default_render_options = RenderOptions(40)


@dataclass
class Level:
    level: int
    breaked: bool


def render(
    opts: RenderOptions,
    sentences: 'list[list[PDoc]]',
    write: typing.Callable[[str], None],
) -> None:
    levels = [Level(0, False)]
    if not sentences:
        return
    for segments in sentences:
        col = 0
        initialized = False

        def line_init():
            nonlocal col
            nonlocal initialized
            if not initialized:
                col = levels[-1].level
                write(" " * col)
                initialized = True

        for seg in segments:
            if isinstance(seg, PDoc_Breakable):
                if col > opts.expect_line_width:
                    initialized = False
                    levels[-1].breaked = True
                    write("\n")
                    col = 0
            elif isinstance(seg, PDoc_LineSeg):
                line_init()
                write(seg.it)
                col += len(seg.it)

            elif isinstance(seg, PDoc_PushCurrentIndent):
                line_init()
                levels.append(Level(col, False))

            elif isinstance(seg, PDoc_PopIndent):
                levels.pop()

            else:
                levels.append(Level(levels[-1].level + seg.indent, False))

        write("\n")

    return


def pretty(s: object) -> Doc:
    return Doc_LineSeg(repr(s))


def seg(s: str) -> Doc:
    return Doc_LineSeg(s)


def vsep(sections: 'list[Doc]') -> Doc:
    return Doc_VSep(sections)


def align(inner: Doc) -> Doc:
    return Doc_Align(inner)


def indent(i: int, inner: Doc) -> Doc:
    return Doc_Indent(i, inner)


empty: Doc = Doc_Empty()


def parens(a: Doc) -> Doc:
    return seg("(") * a * seg(")")


def bracket(a: Doc) -> Doc:
    return seg("[") * a * seg("]")


def brace(a: Doc) -> Doc:
    return seg("{") * a * seg("}")


def angle(a: Doc) -> Doc:
    return seg("<") * a * seg(">")


space: Doc = seg(" ")


comma = seg(",")


def listof(elements: typing.Iterable[Doc]):
    itr = iter(elements)
    res = next(itr, None)
    if res is None:
        return empty

    for each in itr:
        res *= each

    return res


def seplistof(sep: Doc, elements: typing.Iterable[Doc]):
    itr = iter(elements)
    res = next(itr, None)
    if res is None:
        return empty

    for each in itr:
        res *= sep * each

    return res


def breakable(x: Doc):
    return Doc_Breakable(x)