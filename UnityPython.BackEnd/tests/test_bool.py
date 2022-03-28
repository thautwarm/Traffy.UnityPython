from .utils import testsuite

with testsuite("bool"):
    with testsuite("equality"):
        x1 = None
        x2 = None
        assert x1 == x2
        assert x1 is x2

        def f():
            return None
        
        assert x2 is f()
        assert x2 == f()