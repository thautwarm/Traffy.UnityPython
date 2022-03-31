from .utils import testsuite
with testsuite("str"):
    xs = "abdasdadasdakjfjvjskjcnsajcnacn"
    with testsuite("slice"):
        print(xs[1:27:3])
        assert xs[1:27:3] == 'bsddjvknj'
        assert xs[-1:-27:-3] == 'nnacsjksa'
        assert xs[3:16:5] == 'aaj'

    with testsuite("split"):
        assert xs.split("a") == ['', 'bd', 'sd', 'd', 'sd', 'kjfjvjskjcns', 'jcn', 'cn']
        assert xs.split("a", 2) == ['', 'bd', 'sdadasdakjfjvjskjcnsajcnacn']
        assert xs.split("a", 1) ==  ['', 'bdasdadasdakjfjvjskjcnsajcnacn']
        assert  xs.split('a', -1) == ['', 'bd', 'sd', 'd', 'sd', 'kjfjvjskjcns', 'jcn', 'cn']

    xs.format_map({'a': 'b'})