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

f = lambda x, y, z: None

def testcall1():
    x = 1    
    while x < 10000000:
        f(1, 2, 3)
        x += 1

# testfunc(testcall1)
# testfunc(testcall1)
# testfunc(testcall1)
# testfunc(testcall1)
# testfunc(testcall1)

class S:
    def f(self):
        return 1
    
class K(S):
    pass

class D(S):
    pass

print(D().f())

S.f = lambda self: 2

print(D().f())
# testfunc(test1)
# testfunc(test2)
# testfunc(test1)
# testfunc(test2)
# testfunc(test1)
# testfunc(test2)

# class K(TypedDict):
#     x: int

# print(K(x=1))