from typing import Iterator
from .utils import testsuite

with testsuite("set"):
    assert set() == set()
    s = set()
    assert s is s
    s.add(5)
    assert s is s
    assert s is not {5}
    assert s == {5}
    hasHash = False
    try:
        hash(s)
        hasHash = True
    except TypeError:
        pass
    if hasHash:
        raise TypeError("set should not have a hash")

    with testsuite("test all methods"):
        s = set[int]()
        s.add(1)
        s.add(2)
        s.add(3)
        s.add(4)
        s.add(5)

        assert len(s) == 5, "add"
        assert s == {1, 2, 3, 4, 5}, "set equal"
        assert s != {56, 7}
        assert s != {1, 2, 3, 4}
        ss = s.copy()
        assert s.pop() in ss
        assert s.pop() in ss
        assert s.pop() in ss
        assert s.pop() in ss
        assert s.pop() in ss
        assert len(s) == 0, "pop"
        assert s == set()
        ok = False
        try:
            s.pop()
        except KeyError:
            ok = True
            pass
        assert ok
        assert s == set(), "pop empty"
        s.add(1)
        s.add(2)
        s.add(3)
        s.add(4)
        s.add(5)
        assert s.remove(1)
        assert s.remove(2)
        assert s.remove(3)
        assert s.remove(4)
        assert s.remove(5)
        assert len(s) == 0, "remove"
        assert s == set()
        s = ss.copy()
        assert s.discard(1)
        assert s.discard(2)
        assert s.discard(3)
        assert s.discard(4)
        assert s.discard(5)
        assert len(s) == 0
        assert s == set()
        s.add(1)
        s.add(2)
        s.add(3)
        s.add(4)
        s.add(5)
        s.clear()
        assert len(s) == 0, "clear"

        with testsuite("+, -"):
            ss = {1, 2, 3, 4, 5}
            s = ss.copy()
            s += {1, 2, 3, 4, 5}
            assert s == ss
            s = ss.copy()
            s -= {1, 2, 3, 4, 5}
            assert s == set()
            extra = {20, "2", (5, 5)}

            s = ss.copy()
            s += extra
            assert len(s) == len(ss) + len(extra)
            for x in ss:
                assert x in s
            for x in extra:
                assert x in s

            s -= extra
            assert s == ss
    with testsuite("isdisjoint"):
        s = set[int]()
        s.add(1)
        s.add(2)
        s.add(3)
        s.add(4)
        s.add(5)
        assert s.isdisjoint({6, 7, 8, 9, 10})
        assert not s.isdisjoint({1, 2, 3, 4, 5})
        assert not s.isdisjoint({1, 2, 3, 4, 5, 6})
        assert not s.isdisjoint({1, 2, 3, 4, 5, 6, 7})
        assert not s.isdisjoint({1, 2, 3, 4, 5, 6, 7, 8})
        assert not s.isdisjoint({1, 2, 3, 4, 5, 6, 7, 8, 9})
        assert not s.isdisjoint({1, 2, 3, 4, 5, 6, 7, 8, 9, 10})
