using System.Collections.Generic;
using System;
namespace Traffy.Objects
{
    public abstract partial class TrObject
    {
        public virtual  TrObject __init__(BList<TrObject> args,Dictionary<TrObject,TrObject> kwargs)
        {
            return TrObject.__init__(this,args,kwargs);
        }
        public virtual  String __str__()
        {
            return TrObject.__str__(this);
        }
        public virtual  String __repr__()
        {
            return TrObject.__repr__(this);
        }
        public virtual  Boolean __next__(TrRef refval)
        {
            return TrObject.__next__(this,refval);
        }
        public virtual  TrObject __add__(TrObject a)
        {
            return TrObject.__add__(this,a);
        }
        public virtual  TrObject __sub__(TrObject a)
        {
            return TrObject.__sub__(this,a);
        }
        public virtual  TrObject __mul__(TrObject a)
        {
            return TrObject.__mul__(this,a);
        }
        public virtual  TrObject __matmul__(TrObject a)
        {
            return TrObject.__matmul__(this,a);
        }
        public virtual  TrObject __floordiv__(TrObject a)
        {
            return TrObject.__floordiv__(this,a);
        }
        public virtual  TrObject __truediv__(TrObject a)
        {
            return TrObject.__truediv__(this,a);
        }
        public virtual  TrObject __mod__(TrObject a)
        {
            return TrObject.__mod__(this,a);
        }
        public virtual  TrObject __pow__(TrObject a)
        {
            return TrObject.__pow__(this,a);
        }
        public virtual  TrObject __bitand__(TrObject a)
        {
            return TrObject.__bitand__(this,a);
        }
        public virtual  TrObject __bitor__(TrObject a)
        {
            return TrObject.__bitor__(this,a);
        }
        public virtual  TrObject __bitxor__(TrObject a)
        {
            return TrObject.__bitxor__(this,a);
        }
        public virtual  TrObject __lshift__(TrObject a)
        {
            return TrObject.__lshift__(this,a);
        }
        public virtual  TrObject __rshift__(TrObject a)
        {
            return TrObject.__rshift__(this,a);
        }
        public virtual  Int32 __hash__()
        {
            return TrObject.__hash__(this);
        }
        public virtual  TrObject __call__(BList<TrObject> args,Dictionary<TrObject,TrObject> kwargs)
        {
            return TrObject.__call__(this,args,kwargs);
        }
        public virtual  Boolean __contains__(TrObject a)
        {
            return TrObject.__contains__(this,a);
        }
        public virtual  TrObject __round__(TrObject ndigits)
        {
            return TrObject.__round__(this,ndigits);
        }
        public virtual  TrObject __reversed__()
        {
            return TrObject.__reversed__(this);
        }
        public virtual  TrObject __getitem__(TrObject item)
        {
            return TrObject.__getitem__(this,item);
        }
        public virtual  void __delitem__(TrObject item)
        {
            TrObject.__delitem__(this,item);
        }
        public virtual  void __setitem__(TrObject key,TrObject value)
        {
            TrObject.__setitem__(this,key,value);
        }
        public virtual  Boolean __finditem__(TrObject key,TrRef refval)
        {
            return TrObject.__finditem__(this,key,refval);
        }
        public virtual  IEnumerator<TrObject> __iter__()
        {
            return TrObject.__iter__(this);
        }
        public virtual  Awaitable<TrObject> __await__()
        {
            return TrObject.__await__(this);
        }
        public virtual  TrObject __len__()
        {
            return TrObject.__len__(this);
        }
        public virtual  Boolean __eq__(TrObject other)
        {
            return TrObject.__eq__(this,other);
        }
        public virtual  Boolean __ne__(TrObject other)
        {
            return TrObject.__ne__(this,other);
        }
        public virtual  Boolean __lt__(TrObject other)
        {
            return TrObject.__lt__(this,other);
        }
        public virtual  Boolean __le__(TrObject other)
        {
            return TrObject.__le__(this,other);
        }
        public virtual  Boolean __gt__(TrObject other)
        {
            return TrObject.__gt__(this,other);
        }
        public virtual  Boolean __ge__(TrObject other)
        {
            return TrObject.__ge__(this,other);
        }
        public virtual  TrObject __neg__()
        {
            return TrObject.__neg__(this);
        }
        public virtual  TrObject __invert__()
        {
            return TrObject.__invert__(this);
        }
        public virtual  TrObject __pos__()
        {
            return TrObject.__pos__(this);
        }
        public virtual  Boolean __bool__()
        {
            return TrObject.__bool__(this);
        }
        public virtual  TrObject __abs__()
        {
            return TrObject.__abs__(this);
        }
        public virtual  TrObject __enter__()
        {
            return TrObject.__enter__(this);
        }
        public virtual  TrObject __exit__(TrObject exc_type,TrObject exc_value,TrObject traceback)
        {
            return TrObject.__exit__(this,exc_type,exc_value,traceback);
        }
    }
}

