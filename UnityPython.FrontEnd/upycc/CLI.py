from wisepy2 import wise
from upycc.Transpile import compile_module
from upycc.TraffyAsm import fast_asdict
from upycc.JSON import dump_json
from pathlib import Path
import os
import subprocess

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
            if each.suffix == '.py' and each.is_file():
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

def run_upy(main: str, *include: str, projectdir: str="."):
    directory = os.path.abspath(os.getcwd())
    arg = dump_json(dict(
        EntryPoint=main,
        IncludePythonModuleDirectories=[directory, *include],
        ProjectDir=os.path.abspath(projectdir),
    ))
    subprocess.call(['RunUnityPython', arg])


def compile_and_run(filename: Path, rootdir: Path = Path("."), outdir: Path = Path('out'), includesrc: bool = False, recursive: bool = False, nocompile: bool = False, norun: bool = False):
    if not nocompile:
        pipeline(filename, rootdir, outdir, includesrc, recursive)
    if not norun:
        run_upy("main", *[str(outdir.absolute())], projectdir=str(rootdir.absolute()))

def cli_compile():
    wise(pipeline)()  # type: ignore

def cli_run():
    wise(run_upy)()  # type: ignore

def cli_compile_and_run():
    wise(compile_and_run)()  # type: ignore
