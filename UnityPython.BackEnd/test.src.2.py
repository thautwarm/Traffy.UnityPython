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
    def __enter__(self):
        print("__enter__")
    def __exit__(self, exc_type, exc_val, exc_tb):
        print("__exit__")

with X():
    pass

def f():
    try:
        return 1
    finally:
        print("final 1")
        print("final 2")
    print("after final")

print(f())
# print(S())