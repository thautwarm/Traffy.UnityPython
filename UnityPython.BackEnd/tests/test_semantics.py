from .utils import testsuite

with testsuite("list packing"):
    x = 1
    x, *z, y = [1, 2, 3]
    assert z == [2], "list packing"

    x, *z, y = (1, 2, 3)
    assert z == [2], "list packing from tuple"

    xs = [3, 6, 6, 1, 1, 2, 0, 0, 1, 1, 2, 3]
    assert [3, *[*[6, 6], 1, *[1, 2]], *[*[0, 0], 1, *[1, 2]], 3]\
        == xs, "list unpack"

with testsuite("first-class functions"):
    def fcf_0(u):
        return lambda x: x + u

    assert fcf_0(1)(2) == 3, "high order lambda"

    def fcf_1(f, x):
        return f(x)
    
    assert fcf_1(lambda x: x // 6, 122) == 122 // 6, "first-class lambda"

with testsuite("function parameters"):
    def scope_func_params():
        with testsuite("default parameters"):
            def f1(x, y, z=1):
                return x + y + z

            assert f1(1, 2) == 4 == f1(1, 2, 1), "default parameter"
        with testsuite("variable parameters"):
            def f2(*args):
                return sum(args)

            assert f2(1, 2, 3) == 6, "variable parameter"
    
        with testsuite("keyword parameters"):
            def f3(**kwargs):
                return (sorted(kwargs.keys()), sum(kwargs.values()))

            assert f3(x=1, y=2, z=3) == (['x', 'y', 'z'], 6), "keyword parameter"
    
        with testsuite("variable and keyword parameters"):
            def f4(*args, **kwargs):
                return (args, kwargs)

            assert f4(1, 2, 3, x=1, y=2, z=3) == ((1, 2, 3), {'x': 1, 'y': 2, 'z': 3}), "variable and keyword parameter"
        
        with testsuite("variable and keyword parameters with default values"):
            def f5(x, y, z='z', **kwargs):
                return (x, y, z, kwargs)

            assert f5(1, 2, a=4, b=5) == (1, 2, 'z', {'a': 4, 'b': 5}), "variable and keyword parameter with default values"
        
        with testsuite("not enough default arguments"):
            def f6(x, y, z=1, a = 2, d = 3):
                return 0
            try:
                f6(1) # type: ignore
                assert False, "not enough default, arguments"
            except TypeError:
                pass
        
        with testsuite("keyword only"):
            def f(x, y, *, z):
                return x * y ** z

            try:    
                f(1, 2, 3) # type: ignore
                assert False, "keyword only 1"
            except TypeError:
                pass

            assert f(2, 3, z=4) == 2 * 3 ** 4 == 162, "keyword only 2"

    scope_func_params()

with testsuite("async + generators"):
    def f0(x):
        yield x
        yield x

    assert list(f0(0)) == [0, 0], "generator1"


    def f02(x):
        yield from f0(x+x)
        yield 1
        yield from [1, 2]

    def f01(x):
        yield from f02(x)
        yield from f02(0)

    async def f00(x):
        yield x
        await f01(x)
        yield x
        
    assert list(f00(3)) == xs, "generator1"

    async def f000(x):
        yield x
        yield x

    assert list(f000(1)) == [1, 1], "async is generator in Unity Python"

with testsuite("classdef"):

    def test_class():
        class Cxw:
            def __init__(self, x):
                self.x = x

            def f(self):
                return self.x

        assert Cxw(125).f() == 125, "classdef"

        with testsuite("class freevars"):
            def f(x):
                class CC:
                    def k(self):
                        return x
                assert CC().k() == x, "class freevars"
            f(1)
            f("q123")
        
        with testsuite("classmethods"):
                class C0:
                    @classmethod
                    def f(cls):
                        return cls

                assert C0.f() == C0, "classmethods no arg"

                class C1:
                    @classmethod
                    def f(cls, *args):
                        return (cls, *args)

                assert C1.f(1, 2, 3) == (C1, 1, 2, 3), "classmethods narg"
    test_class()


with testsuite("raise"):
    
    ok = False
    try:
        raise 1
    except TypeError as e:
        ok =  "exception value must be an instance of BaseException, not" in e.args[0]
    assert ok

    ok = False
    try:
        raise ValueError
    except ValueError:
        ok = True
    assert ok

    
