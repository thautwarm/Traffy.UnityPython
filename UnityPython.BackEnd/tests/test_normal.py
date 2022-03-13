x = 1
x, *z, y = [1, 2, 3]
assert z == [2], "list packing"

x, *z, y = (1, 2, 3)
assert z == [2], "list packing from tuple"

def g(u):
    return lambda x: x + u

assert g(1)(2) == 3, "high order lambda"

_x = 1
class MyP:
    @property
    def f(self):
        return _x
    @f.setter
    def f(self, v):
        global _x
        _x = 1

m = MyP()
print(m.f)
m.f = 10
print(m.f)

xs = [1, 2, 3]

print(xs[::-1])

print(list(reversed(xs[::1])))

# print(reversed({}))