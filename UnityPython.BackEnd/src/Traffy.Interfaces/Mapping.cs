// Iterator = iter | Generator
using System.Collections.Generic;
using Traffy;
using Traffy.Annotations;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [AbstractClass]
    [PyInherit(typeof(Collection))]
    public static partial class Mapping
    {
        [AbsMember]
        public static IEnumerator<TrObject> __iter__(TrObject self)
        {
            return TrObject.__iter__(self);
        }

        [AbsMember]
        public static bool __finditem__(TrObject self, TrObject key, TrRef refval)
        {
            return TrObject.__finditem__(self, key, refval);
        }

        [MixinMember]
        public static TrObject __getitem__(TrObject self, TrObject key)
        {
            var refval = MK.Ref();
            if (self.__finditem__(key, refval))
            {
                return refval.value;
            }
            throw new KeyError(key);
        }

        [MixinMember]
        public static bool __contains__(TrObject self, TrObject key)
        {
            var refval = MK.Ref();
            return self.__finditem__(key, refval);
        }

        [MixinMember]
        public static IEnumerator<TrObject> keys(TrObject self)
        {
            return self.__iter__();
        }

        [MixinMember]
        public static IEnumerator<TrObject> values(TrObject self)
        {
            var itr = self.__iter__();
            var refval = MK.Ref();
            while (itr.MoveNext())
            {
                if (self.__finditem__(itr.Current, refval))
                {
                    yield return refval.value;
                }
                else
                {
                    throw new KeyError(itr.Current);
                }
            }
        }

        [MixinMember]
        public static IEnumerator<TrObject> items(TrObject self)
        {
            var itr = self.__iter__();
            var refval = MK.Ref();
            while (itr.MoveNext())
            {
                if (self.__finditem__(itr.Current, refval))
                {
                    yield return MK.NTuple(itr.Current, refval.value);
                }
                else
                {
                    throw new KeyError(itr.Current);
                }
            }
        }

        [MixinMember]
        public static TrObject get(TrObject self, TrObject key, TrObject defaultVal = null)
        {
            var refval = MK.Ref();
            if (self.__finditem__(key, refval))
            {
                return refval.value;
            }
            if (defaultVal == null)
            {
                return MK.None();
            }
            return defaultVal;
        }

        
        public static bool EqualImpl(TrObject self, TrObject other)
        {
            if (object.ReferenceEquals(self, other))
            {
                return true;
            }
            var len1 = self.__len__().AsInt();
            var len2 = other.__len__().AsInt();
            if (len1 != len2) return false;
            var itr = self.__iter__();
            var refval = MK.Ref();
            while (itr.MoveNext())
            {
                var key = itr.Current;
                if (!self.__finditem__(key, refval))
                {
                    throw new KeyError(key);
                }
                var myval = refval.value;
                if (!other.__finditem__(key, refval))
                {
                    return false;
                }
                if (!myval.__eq__(refval.value))
                {
                    return false;
                }
            }
            return true;
        }

        [MixinMember]
        public static bool __ne__(TrObject self, TrObject other)
        {
            return !EqualImpl(self, other);
        }
        
        [MixinMember]
        public static bool __eq__(TrObject self, TrObject other)
        {
            return EqualImpl(self, other);
        }
    }
}
