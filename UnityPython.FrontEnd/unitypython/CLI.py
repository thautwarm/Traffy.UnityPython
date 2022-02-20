from wisepy2 import wise
from unitypython.Transpile import compile_module
from unitypython.TraffyAsm import fast_asdict
from unitypython.JSON import dump_json


def pipeline(filename: str, includesrc: bool = False):
    """
    :param filename: input python file
    :param includesrc: compile to traffy asm with source code included in the metadata for debugging.
                       this is not recommended for production as everyone can see the source code.
    """
    with open(filename, 'r', encoding='utf-8') as file:
        src = file.read()
    tr = compile_module(filename, src, ignore_src=not includesrc)
    dict_data = fast_asdict(tr)
    with open(f"{filename}.json", 'w', encoding='utf-8') as file:
        file.write(dump_json(dict_data))


def main():
    wise(pipeline)()  # type: ignore
