using Traffy.Objects;
using System.Collections.Generic;
using System;
namespace Traffy.Objects{public partial interface TrObject {
    public TrObject __init__(BList<TrObject> args,Dictionary<TrObject,TrObject> kwargs)
    {
        return TrObject.__init__(this,args,kwargs);
    }
    public String __str__()
    {
        return TrObject.__str__(this);
    }
    public String __repr__()
    {
        return TrObject.__repr__(this);
    }
    public TrObject __next__()
    {
        return TrObject.__next__(this);
    }
    public TrObject __add__(TrObject a)
    {
        return TrObject.__add__(this,a);
    }
    public TrObject __sub__(TrObject a)
    {
        return TrObject.__sub__(this,a);
    }
    public TrObject __mul__(TrObject a)
    {
        return TrObject.__mul__(this,a);
    }
    public TrObject __matmul__(TrObject a)
    {
        return TrObject.__matmul__(this,a);
    }
    public TrObject __floordiv__(TrObject a)
    {
        return TrObject.__floordiv__(this,a);
    }
    public TrObject __truediv__(TrObject a)
    {
        return TrObject.__truediv__(this,a);
    }
    public TrObject __mod__(TrObject a)
    {
        return TrObject.__mod__(this,a);
    }
    public TrObject __pow__(TrObject a)
    {
        return TrObject.__pow__(this,a);
    }
    public TrObject __bitand__(TrObject a)
    {
        return TrObject.__bitand__(this,a);
    }
    public TrObject __bitor__(TrObject a)
    {
        return TrObject.__bitor__(this,a);
    }
    public TrObject __bitxor__(TrObject a)
    {
        return TrObject.__bitxor__(this,a);
    }
    public TrObject __lshift__(TrObject a)
    {
        return TrObject.__lshift__(this,a);
    }
    public TrObject __rshift__(TrObject a)
    {
        return TrObject.__rshift__(this,a);
    }
    public Int32 __hash__()
    {
        return TrObject.__hash__(this);
    }
    public TrObject __call__(BList<TrObject> args,Dictionary<TrObject,TrObject> kwargs)
    {
        return TrObject.__call__(this,args,kwargs);
    }
    public Boolean __contains__(TrObject a)
    {
        return TrObject.__contains__(this,a);
    }
    public Boolean __getitem__(TrObject item,TrRef found)
    {
        return TrObject.__getitem__(this,item,found);
    }
    public void __setitem__(TrObject key,TrObject value)
    {
        TrObject.__setitem__(this,key,value);
    }
    public Boolean __getattr__(TrObject name,TrRef found)
    {
        return TrObject.__getattr__(this,name,found);
    }
    public void __setattr__(TrObject name,TrObject value)
    {
        TrObject.__setattr__(this,name,value);
    }
    public IEnumerator<TrObject> __iter__()
    {
        return TrObject.__iter__(this);
    }
    public TrObject __len__()
    {
        return TrObject.__len__(this);
    }
    public Boolean __eq__(TrObject other)
    {
        return TrObject.__eq__(this,other);
    }
    public Boolean __ne__(TrObject other)
    {
        return TrObject.__ne__(this,other);
    }
    public Boolean __lt__(TrObject other)
    {
        return TrObject.__lt__(this,other);
    }
    public Boolean __le__(TrObject other)
    {
        return TrObject.__le__(this,other);
    }
    public Boolean __gt__(TrObject other)
    {
        return TrObject.__gt__(this,other);
    }
    public Boolean __ge__(TrObject other)
    {
        return TrObject.__ge__(this,other);
    }
    public TrObject __neg__()
    {
        return TrObject.__neg__(this);
    }
    public TrObject __invert__()
    {
        return TrObject.__invert__(this);
    }
    public TrObject __pos__()
    {
        return TrObject.__pos__(this);
    }
    public Boolean __bool__()
    {
        return TrObject.__bool__(this);
    }
    public TrObject __abs__()
    {
        return TrObject.__abs__(this);
    }
    public TrObject __enter__()
    {
        return TrObject.__enter__(this);
    }
    public TrObject __exit__(TrObject exc_type,TrObject exc_value,TrObject traceback)
    {
        return TrObject.__exit__(this,exc_type,exc_value,traceback);
    }
}
}