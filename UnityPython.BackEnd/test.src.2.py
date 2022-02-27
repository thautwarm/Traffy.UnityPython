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
# print(S())