from .utils import testsuite

with testsuite("slice"):
    with testsuite("fields"):
        x1 = slice(1)
        assert x1.start == None and x1.stop == 1 and x1.step is None
        x2 = slice(1, 2)
        assert x2.start == 1 and x2.stop == 2 and x2.step is None
        x3 = slice(1, 6, 2)
        assert x3.start == 1 and x3.stop == 6 and x3.step == 2
    with testsuite("indices"):
        assert x1.indices(5) == (0, 1, 1)
        assert x2.indices(5) == (1, 2, 1)
        assert x3.indices(5) == (1, 5, 2)

    with testsuite("unhashable"):
        try:
            hash(slice(1))
        except TypeError as e:
            assert "unhashable" in e.args[0]

        assert slice.__hash__ == None, "method __hash__ is None"
