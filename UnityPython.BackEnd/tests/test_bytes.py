from .utils import testsuite

with testsuite("bytes"):
    x = b'1231'.decode('utf-8')
    assert x == '1231'
