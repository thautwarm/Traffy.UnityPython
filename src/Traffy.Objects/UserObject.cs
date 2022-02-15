using System;
using System.Collections.Generic;

namespace Traffy.Objects
{
    public class TrUserObject : TrObject
    {
        public Dictionary<TrObject, TrObject> innerDict = RTS.baredict_create();
        public object Native => this;
        public TrClass Class { get; }
        public Dictionary<TrObject, TrObject> __dict__ => innerDict;
        public string __str__() => Class.__str(this);
        public string __repr__() => Class.__repr(this);
        public TrObject __next__() => Class.__next(this);
        // Arithmetic ops
        public TrObject __add__(TrObject a) => Class.__add(this, a);
        public TrObject __sub__(TrObject a) => Class.__sub(this, a);

        public TrObject __mul__(TrObject a) => Class.__mul(this, a);
        public TrObject __floordiv__(TrObject a) => Class.__floordiv(this, a);
        public TrObject __truediv__(TrObject a) => Class.__truediv(this, a);

        public TrObject __mod__(TrObject a) => Class.__mod(this, a);

        public TrObject __pow__(TrObject a) => Class.__pow(this, a);
        // Bitwise logic operations

        public TrObject __bitand__(TrObject a) => Class.__bitand(this, a);

        public TrObject __bitor__(TrObject a) => Class.__bitor(this, a);


        public TrObject __bitxor__(TrObject a) => Class.__bitxor(this, a);


        // bit shift
        public TrObject __lshift__(TrObject a) => Class.__lshift(this, a);

        public TrObject __rshift__(TrObject a) => Class.__rshift(this, a);


        // Object protocol

        public int __hash__() => Class.__hash(this);

        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs) =>
            Class.__call(this, args, kwargs);

        public bool __contains__(TrObject a) => Class.__contains(this, a);

        public IEnumerator<TrObject> __iter__() => Class.__iter(this);
        public TrObject __len__() => Class.__len(this);
        // Comparators
        public bool __eq__(TrObject o) => Class.__eq(this, o);

        public bool __lt__(TrObject o) => Class.__lt(this, o);
        // Unary ops
        public TrObject __neg__() => Class.__neg(this);

        public TrObject __inv__() => Class.__inv(this);
        public TrObject __pos__() => Class.__pos(this);
        public bool __bool__() => Class.__bool(this);
    }



}
