from wisepy2 import wise
from unitypython.Transpile import compile_module
from unitypython.TraffyAsm import fast_asdict
from unitypython.JSON import dump_json
from pathlib import Path


def valid_parts(parts: tuple[str, ...]):
    for part in parts:
        if not part.isidentifier():
            n = ".".join(parts)
            raise IOError(f"invalid module name {n}")


def pipeline(filename: Path, rootdir: Path = Path("."), outdir: Path = Path('out'), includesrc: bool = False, recursive: bool = False):
    """
    :param filename: input python file or the directory containing python files
    :param includesrc: compile to traffy asm with source code included in the metadata for debugging.
                       this is not recommended for production as everyone can see the source code.
    """
    
    if filename.is_dir():
        for each in filename.iterdir():
            if each.suffix == '.py':
                pipeline(each, rootdir, outdir, includesrc, recursive)
            elif recursive and each.is_dir():
                pipeline(each, rootdir, outdir, includesrc, recursive)
        return

    with filename.open("r", encoding="utf-8") as file:
        src = file.read()

    parts = (
        filename
        .absolute()
        .relative_to(rootdir.absolute())
        .with_suffix("")
        .parts
    )
    valid_parts(parts)
    modulename = ".".join(parts)
    module_spec = compile_module(
        filename=str(filename), modulename=modulename, src=src, ignore_src=not includesrc
    )
    dict_data = fast_asdict(module_spec)
    if not outdir.exists():
        outdir.mkdir(parents=True, exist_ok=True, mode=0o755)
    
    with outdir.joinpath(modulename + ".py.json").open('w', encoding="utf-8") as file:
        file.write(dump_json(dict_data))

def main():
    wise(pipeline)()  # type: ignore
