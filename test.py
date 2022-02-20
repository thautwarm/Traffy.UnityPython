import cProfile, pstats, io
from pstats import SortKey
pr = cProfile.Profile()
pr.enable()

from unitypython.Transpile import compile_module
from unitypython.TraffyAsm import fast_asdict
from unitypython.JSON import dump_json


src = open("test.src.py").read()


def pipeline(src):
    tr = compile_module("a.py", src)
    dict_data = fast_asdict(tr)
    with open("c.json", 'w', encoding='utf-8') as file:
        file.write(dump_json(dict_data))

pipeline(src)
pipeline(src)
pipeline(src)

pr.disable()


s = io.StringIO()
sortby = SortKey.CUMULATIVE
ps = pstats.Stats(pr, stream=s).sort_stats(sortby)
ps.print_stats()
with open("report.txt", 'w', encoding='utf-8') as file:
    file.write(s.getvalue())