from wisepy2 import wise
from unitypython.Transpile import compile_module
from unitypython.TraffyAsm import fast_asdict
from unitypython.JSON import dump_json


def valid_parts(parts: tuple[str, ...]):
    for part in parts:
        if not part.isidentifier():
            n = ".".join(parts)
            raise IOError(f"invalid module name {n}")


def pipeline(filename: str, rootdir: str = ".", includesrc: bool = False):
    """
    :param filename: input python file
    :param includesrc: compile to traffy asm with source code included in the metadata for debugging.
                       this is not recommended for production as everyone can see the source code.
    """
    with open(filename, "r", encoding="utf-8") as file:
        src = file.read()
    from pathlib import Path

    parts = (
        Path(filename)
        .absolute()
        .relative_to(Path(rootdir).absolute())
        .with_suffix("")
        .parts
    )
    valid_parts(parts)
    modulename = ".".join(parts)
    module_spec = compile_module(
        filename=filename, modulename=modulename, src=src, ignore_src=not includesrc
    )
    dict_data = fast_asdict(module_spec)
    with open(f"{filename}.json", "w", encoding="utf-8") as file:
        file.write(dump_json(dict_data))


def main():
    wise(pipeline)()  # type: ignore
