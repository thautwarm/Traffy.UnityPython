from .utils import testsuite

with testsuite("tuple"):
    assert (1, 2, 3) + (2, 3, 5) == (1, 2, 3, 2, 3, 5)
