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

print(round(1.555, 2))
print(round(5))

# print(reversed({}))
print(sorted([1, 2, 3, 1.5, -1]))

class X:
    def __init__(self, x):
        self.x = x
    def __gt__(self, x):
        if type(x) is X:
            return self.x > x.x
        raise TypeError("x")
    def __lt__(self, x):
        if type(x) is X:
            return self.x < x.x
        raise TypeError("x")
    def __eq__(self, x):
        if type(x) is X:
            return self.x == x.x
        return False
    def __ge__(self, x):
        if type(x) is X:
            return self.x >= x.x
        raise TypeError("x")
    def __le__(self, x):
        if type(x) is X:
            return self.x <= x.x
        raise TypeError("x")
    def __repr__(self) -> str:
        return  f"<X {self.x}>"

print(sorted([X(1.2), X(1.0), X(2)]))
