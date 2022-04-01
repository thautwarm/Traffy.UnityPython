from .utils import testsuite
with testsuite("dict"):
    y = dict(x = 1, y = 1.0)
    assert y == {'x': 1, 'y': 1.0}
    def f() -> str:
        return "2"
    k: int = 1
    yy = dict([(1, k), (3, f())])
    assert yy == {1: 1, 3: "2"}
    
    yyy = dict(yy, **y) # type: ignore
    z: dict[int | str, int | str] = {1: 1, 3: "2", 'x': 1, 'y': 1.0} # type: ignore
    assert yyy == z # type: ignore

    try:
        hash({})
        raise RuntimeError
    except TypeError:
        pass

    assert {1: 2, 3: 4, 5: 6} != {1: 2, 3: 4}

    assert not ({1: 2, 3: 4, 5: 6} == {1: 2, 3: 4})

    zzz = dict(x = 3)
    zzz.update({'y': 3})
    assert zzz == {'x': 3, 'y': 3}
