from .utils import testsuite

with testsuite("conversion"):
    assert int(1) == 1
    assert int("1") == 1
    assert int(1.0) == 1
    assert int(False) == 0

    assert str(1) == "1"
    assert str("1") == "1"
    assert str(1.02) == "1.02"
    assert str(False) == "False"

    assert bool(1) is True
    assert bool("1") is True
    assert bool(0.02) is True
    assert bool(False) is False
    assert bool(0) is False
    assert bool(0.0) is False
    assert bool("") is False

    assert float(1) == 1.0
    assert float("1") == 1
    assert float(1.02) == 1.02
    assert float(False) == 0.0

    assert bool() is False
    assert float() == 0.0
    assert int() == 0
    assert str() == ""
