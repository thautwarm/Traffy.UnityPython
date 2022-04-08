from .utils import testsuite
from . import utils

with testsuite("module"):
    assert isinstance(utils.__dict__, dict)
    assert utils.__name__ == "tests.utils"
