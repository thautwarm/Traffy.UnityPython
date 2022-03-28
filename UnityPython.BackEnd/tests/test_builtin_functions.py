from .utils import testsuite
from types import BuiltinFunctionType
with testsuite("builtin function"):
    
    with testsuite("classing"):
        assert isinstance(ord, BuiltinFunctionType)
    
    import types
    print(types)

    assert ord.__call__
    assert isinstance(ord.__name__, str)
