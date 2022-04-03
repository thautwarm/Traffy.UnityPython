def _test1():
    x = 0
    for i in range(10000000):
        x += 1
        yield x

def test1():
    x = list(_test1())
    print("test1 elt[50]", x[50])
    return len(x)

def test2():
    x = 0
    while x < 10000000:
        x += 1
    return x

# class XX:
#     xx =5
#     def __init__(self, x):
#         self.x = x


def testfunc(f):
    a = time()
    res = f()
    t = time() - a
    print("test result of", f, ":", t)


testfunc(test1)
testfunc(test2)
testfunc(test1)
testfunc(test2)
testfunc(test1)
testfunc(test2)

# class K(TypedDict):
#     x: int

# print(K(x=1))