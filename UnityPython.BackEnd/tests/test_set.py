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
