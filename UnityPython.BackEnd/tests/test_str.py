from .utils import testsuite
with testsuite("str"):
    xs = "abdasdadasdakjfjvjskjcnsajcnacn"
    with testsuite("slice"):
        print(xs[1:27:3])
        assert xs[1:27:3] == 'bsddjvknj'
        assert xs[-1:-27:-3] == 'nnacsjksa'
        assert xs[3:16:5] == 'aaj'
