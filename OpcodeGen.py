from pretty_doc import *
import pathlib

cur = pathlib.Path(__file__)
cases = []
for each in open("instrs").read().splitlines():
    if each.startswith('#'):
        continue
    if not each.strip():
        continue
    cases.append(each.strip())

def gen_py(write_file):
    doc = vsep([
        seg("from enum import IntEnum"),
        seg(""),
        seg(""),
        seg("class Opcode(IntEnum):"),
        vsep([
            seg(f"{each} = {i}")
            for i, each in enumerate(cases)
        ]) >> 4,
        empty
    ])

    doc.render(write_file)

def lines(*args):
    return vsep([*args])
def gen_cs(write_file):
    doc = vsep([
        seg("using System;"),
        seg("namespace Traffy"),
        seg("{"),
        vsep([
            empty,
            seg("[Serializable]"),
            seg("public enum Opcode"),
            seg("{"),
            vsep([
                seg(f"{each} = {i},")
                for i, each in enumerate(cases)
            ]) >> 4,
            seg("}"),
        ]) >> 4,
        seg("}"),
        empty,
    ])
    doc.render(write_file)


cs_file = open('src/Opcode.cs', 'w')
print(gen_cs(cs_file.write))

py_file = open('unitypython/Opcode.py', 'w')
print(gen_py(py_file.write))