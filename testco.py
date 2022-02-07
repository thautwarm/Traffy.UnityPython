
from dataclasses import dataclass
import typing
@dataclass
class Coro:
    co: typing.Generator
    result: object

def Yield(v):
    def mk():
        yield v
    return Coro(mk(), None)

def YieldFrom(v: Coro):
    def mk():
        next(v.co)
