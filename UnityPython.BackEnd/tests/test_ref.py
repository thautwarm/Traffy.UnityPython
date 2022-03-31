from .utils import testsuite

with testsuite("ref"):
    assert ref() != ref()
    r = ref()
    assert r is r
    r.value = 5
    assert r is r
    assert r is not ref(5)
    assert r != ref(5)
    print(hash(r))
    print(hash(r))
