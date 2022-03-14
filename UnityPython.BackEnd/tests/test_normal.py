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
        self.x: int = x
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

    def __add__(self, x):
        if type(x) is X:
            return X(self.x + x.x)
        raise TypeError("x")

print(sorted([X(1.2), X(1.0), X(2)]))

x = sum([X(2.0), X(3.0)], X(1.0))
print(x)

def fff(x):
    if isinstance(x, int | None):
        return x

print(fff(1.0))
print(fff(None))
print(fff('1'))
print(fff(1))

print(sorted([1, 2, 3], key = lambda x: -x))

k = [1, 2.0]
zz = k[0]
k = zz + 1
x = sum([1.0, 2, 3], 0)


class Pow:
    def __pow__(self, x: str) -> int:
        return 1

pp = Pow()
x = pp ** "asd"

if isinstance(1, int):
    x = 5


x = [1, 2]

x.append(2)

x.extend([3, 4])
x.extend(range(2, 5, 2))

x.insert(0, 100)

print(x)

def f():
    pass

xs = [1, 2, -8, 1]
xs.sort()
print(xs)

