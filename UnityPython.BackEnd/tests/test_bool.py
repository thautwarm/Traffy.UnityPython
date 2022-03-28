from .utils import testsuite

with testsuite("bool"):
    with testsuite("equality"):
        x1 = True
        x2 = True
        assert x1 == x2



        assert x1 is x2
        
        def f1():
            return True
        
        assert x2 is f1()
        assert x2 == f1()
        
        x1 = False
        x2 = False
        assert x1 == x2
        assert x1 is x2
        
        def f2():
            return list is tuple
        print("list is tuple", list is tuple)
        assert x2 is f2()
        assert x2 == f2()
        
        x1 = True
        x2 = False
        assert tuple != list
        nimamadi = True
        print(nimamadi != False)
        assert x1 != x2
        assert x1 is not x2
        
        def f3():
            return True
        
        assert x2 is not f3()
        assert x2 != f3()
        
        x1 = False
        x2 = True
        assert x1 != x2
        assert x1 is not x2
        
        def f4():
            return False
        
        assert x2 is not f4()
        assert x2 != f4()
    with testsuite("hashable"):
        xs = {}
        xs[True] = 1
        xs[False] = 2
        assert xs[True] == 1
        assert xs[False] == 2
        xs[(True, False)] = 1231
        assert xs[(True, False)] == 1231
