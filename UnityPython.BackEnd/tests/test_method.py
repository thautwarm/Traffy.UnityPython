from .utils import testsuite
import types

print(types.__dict__)
from types import MethodType

with testsuite("method"):
    lam = lambda x: "das"
    f = MethodType(lam, None)
    assert f() == "das"

    with testsuite("hashable"):
        assert hash(f) == hash(f)
        assert hash(f) != hash(MethodType(lam, None))
