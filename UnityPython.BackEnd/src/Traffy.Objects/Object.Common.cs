using System.Collections.Generic;

namespace Traffy.Objects
{
    public partial interface TrObject
    {
        public static int ObjectSequenceHash<TList>(TList xs, int seed, int primSeed) where TList : IList<TrObject>
        {
            unchecked
            {
                int hash = seed;
                for(int i = 0; i < xs.Count; i++)
                {
                    hash = hash * primSeed + xs[i].__hash__();
                }
                return hash;
            }
        }


        public static bool __instancecheck__(TrObject obj, TrObject classes)
        {
            if (classes is TrClass cls)
            {
                return cls.__subclasscheck__(obj.Class);
            }
            else if (classes is TrUnionType union)
            {
                return __instancecheck__(obj, union.left) || __instancecheck__(obj, union.right);
            }
            else if (classes is TrTuple tup)
            {
                foreach (var cls_ in tup.elts)
                {
                    if (__instancecheck__(obj, cls_))
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

        public bool __instancecheck__(TrObject classes) => __instancecheck__(this, classes);
    }
}