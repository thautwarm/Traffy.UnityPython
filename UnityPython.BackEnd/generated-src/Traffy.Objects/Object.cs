using System.Collections.Generic;
using System;
    namespace Traffy.Objects
    {
    public partial interface TrObject
    {
        TrObject __init__(BList<TrObject> args,Dictionary<TrObject,TrObject> kwargs)
        {
            return TrObject.__init__(this,args,kwargs);
        }
        String __str__()
        {
            return TrObject.__str__(this);
        }
        String __repr__()
        {
            return TrObject.__repr__(this);
        }
        Boolean __next__(TrRef refval)
        {
            return TrObject.__next__(this,refval);
        }
        TrObject __add__(TrObject a)
        {
            return TrObject.__add__(this,a);
        }
        TrObject __sub__(TrObject a)
        {
            return TrObject.__sub__(this,a);
        }
        TrObject __mul__(TrObject a)
        {
            return TrObject.__mul__(this,a);
        }
        TrObject __matmul__(TrObject a)
        {
            return TrObject.__matmul__(this,a);
        }
        TrObject __floordiv__(TrObject a)
        {
            return TrObject.__floordiv__(this,a);
        }
        TrObject __truediv__(TrObject a)
        {
            return TrObject.__truediv__(this,a);
        }
        TrObject __mod__(TrObject a)
        {
            return TrObject.__mod__(this,a);
        }
        TrObject __pow__(TrObject a)
        {
            return TrObject.__pow__(this,a);
        }
        TrObject __bitand__(TrObject a)
        {
            return TrObject.__bitand__(this,a);
        }
        TrObject __bitor__(TrObject a)
        {
            return TrObject.__bitor__(this,a);
        }
        TrObject __bitxor__(TrObject a)
        {
            return TrObject.__bitxor__(this,a);
        }
        TrObject __lshift__(TrObject a)
        {
            return TrObject.__lshift__(this,a);
        }
        TrObject __rshift__(TrObject a)
        {
            return TrObject.__rshift__(this,a);
        }
        Int32 __hash__()
        {
            return TrObject.__hash__(this);
        }
        TrObject __call__(BList<TrObject> args,Dictionary<TrObject,TrObject> kwargs)
        {
            return TrObject.__call__(this,args,kwargs);
        }
        Boolean __contains__(TrObject a)
        {
            return TrObject.__contains__(this,a);
        }
        TrObject __round__(TrObject ndigits)
        {
            return TrObject.__round__(this,ndigits);
        }
        TrObject __reversed__()
        {
            return TrObject.__reversed__(this);
        }
        TrObject __getitem__(TrObject item)
        {
            return TrObject.__getitem__(this,item);
        }
        void __delitem__(TrObject item)
        {
            TrObject.__delitem__(this,item);
        }
        void __setitem__(TrObject key,TrObject value)
        {
            TrObject.__setitem__(this,key,value);
        }
        IEnumerator<TrObject> __iter__()
        {
            return TrObject.__iter__(this);
        }
        Awaitable<TrObject> __await__()
        {
            return TrObject.__await__(this);
        }
        TrObject __len__()
        {
            return TrObject.__len__(this);
        }
        Boolean __eq__(TrObject other)
        {
            return TrObject.__eq__(this,other);
        }
        Boolean __ne__(TrObject other)
        {
            return TrObject.__ne__(this,other);
        }
        Boolean __lt__(TrObject other)
        {
            return TrObject.__lt__(this,other);
        }
        Boolean __le__(TrObject other)
        {
            return TrObject.__le__(this,other);
        }
        Boolean __gt__(TrObject other)
        {
            return TrObject.__gt__(this,other);
        }
        Boolean __ge__(TrObject other)
        {
            return TrObject.__ge__(this,other);
        }
        TrObject __neg__()
        {
            return TrObject.__neg__(this);
        }
        TrObject __invert__()
        {
            return TrObject.__invert__(this);
        }
        TrObject __pos__()
        {
            return TrObject.__pos__(this);
        }
        Boolean __bool__()
        {
            return TrObject.__bool__(this);
        }
        TrObject __abs__()
        {
            return TrObject.__abs__(this);
        }
        TrObject __enter__()
        {
            return TrObject.__enter__(this);
        }
        TrObject __exit__(TrObject exc_type,TrObject exc_value,TrObject traceback)
        {
            return TrObject.__exit__(this,exc_type,exc_value,traceback);
        }
    }
}
