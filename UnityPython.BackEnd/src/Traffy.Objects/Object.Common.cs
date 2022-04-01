using System.Collections.Generic;

namespace Traffy.Objects
{
    public abstract partial class TrObject
    {
        public static int ObjectSequenceHash<TList>(TList xs, int seed, int primSeed) where TList : IList<TrObject>
        {
            unchecked
            {
                int hash = seed;
                for (int i = 0; i < xs.Count; i++)
                {
                    hash = hash * primSeed ^ xs[i].__hash__();
                }
                return hash;
            }
        }

        public static bool isinstanceof(TrObject obj, TrObject classes)
        {
            if (classes is TrClass cls)
            {
                return cls.__subclasscheck__(obj.Class);
            }
            else if (classes is TrUnionType union)
            {
                return isinstanceof(obj, union.left) || isinstanceof(obj, union.right);
            }
            else if (classes is TrTuple tup)
            {
                foreach (var cls_ in tup.elts)
                {
                    if (isinstanceof(obj, cls_))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                throw new TypeError($"{classes.__repr__()} is not a class or tuple of classes");
            }
        }

        public static bool issubclassof(TrObject x, TrObject type)
        {
            if (type is TrTuple tup)
            {
                foreach (var elt in tup.elts)
                {
                    if (issubclassof(x, elt))
                        return true;
                }
                return false;
            }
            else if (type is TrClass cls)
            {
                return cls.__subclasscheck__((TrClass)x);
            }
            else if (type is TrUnionType union)
            {
                return issubclassof(x, union.left) || issubclassof(x, union.right);
            }
            else
            {
                throw new TypeError($"issubclass() arg 2 must be a class, type, or tuple of classes, or a uniontype, not {type}");
            }
        }
        public bool __instancecheck__(TrObject classes) => isinstanceof(this, classes);
    }
}