x = "-2" <= "-5"
print(type(x), x)

class S:
    x = 1
    def __init__(self, x):
        pass

# print(list(map(lambda x: x // 0, [1, 2, 3])))
print(S(1))
x = "asda"
print([1, 2, 3][2])
x = [1, 2]
x.append(1)
print(x)
x.append(5)
print(x)

print(int.from_bytes(b'\x00\x01\x00\x01\x00\x00\x00\x01', 'little'))

class X:
    def __init__(self, x):
        self.x = x
    def __enter__(self):
        print("__enter__", self.x)
        return self.x
    def __exit__(self, exc_type, exc_val, exc_tb):
        print("__exit__", self.x)

with X(2):
    pass

def f():
    try:
        return 1
    finally:
        print("final 1")
        print("final 2")
    print("after final")

with X(2) as a, X(3) as b:
    print(a == 2, b == 3)

# print(S())