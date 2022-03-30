# def myfunc():
#     print("hello lfk")
#     pass

# myfunc()


print(b"q23".count(ord('q')))
print("should be 2", b"q2323".count(bytearray(b'23')))

x = "2232211222"
print(x.count('22'))

def test():
    x.count("22")
    x.count("22")

def testfunc(f):
    a = time()
    for i in range(10000000):
        f()
    print(time() - a)


# testfunc(test)
# testfunc(test)
# testfunc(test)
