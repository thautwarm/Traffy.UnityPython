# x = 1
from typing import Any, Callable, Generator, ParamSpec, TypeVar, Awaitable, Concatenate, Protocol, TypedDict

# TP = ParamSpec("TP")
# TA = TypeVar("TA")
# TB = TypeVar("TB")
# TC = TypeVar("TC")

P = ParamSpec("P")
# R = TypeVar("R")

# class K:
#     pass

class S(Protocol[P]):
    f : Callable[P, int]



class K(TypedDict):
    x : int

class A(Protocol):
    x : int

def k(x: A):
    pass



# box









# def add_logging(f: Callable[Concatenate[K, P], R]) -> Callable[P, R]:
#   def inner(*args: P.args, **kwargs: P.kwargs) -> R:
#     # await log_to_database()
#     return f(K(), *args, **kwargs)
#   return inner




# def wrap(f: Callable[TP, Generator[TA, Any, Any]]) -> TA:
#     def g(*args: TP.args, **kwargs):
#         return next(f(*args, **kwargs))
#     return g