using System.Collections.Generic;
using Traffy;
using Traffy.Annotations;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [AbstractClass(typeof(Reversible), typeof(Collection))]
    public static partial class Sequence
    {

        [AbsMember]
        public static TrObject __getitem__(TrObject self, TrObject item)
        {
            return TrObject.__getitem__(self, item);
        }
    
        [MixinMember]
        public static IEnumerator<TrObject> __iter__(TrObject self)
        {
            var n = self.__len__().AsInt();
            for(int i = 0; i < n; i++)
            {
                yield return self.__getitem__(MK.Int(i));
            }
        }

        [MixinMember]
        public static bool __contains__(TrObject self, TrObject element)
        {
            var itr = self.__iter__();
            while (itr.MoveNext())
            {
                if(itr.Current.__eq__(element))
                    return true;
            }
            return false;
        }

        [MixinMember]
        public static IEnumerator<TrObject> __reversed__(TrObject self)
        {
            var n = self.__len__().AsInt();
            for(int i = n - 1; i >= 0; i--)
            {
                yield return self.__getitem__(MK.Int(i));
            }
        }

        [MixinMember]
        public static int index(
            TrObject self,
            TrObject element,
            [PyBind.Keyword] int start = 0,
            [PyBind.Keyword] int stop = -1,
            [PyBind.Keyword(Only = true)] bool noraise=false)
        {
            if (stop == -1)
                stop = self.__len__().AsInt();
            for(int i = start; i < stop; i++)
            {
                var curr = self.__getitem__(MK.Int(i));
                if(curr.__eq__(element))
                    return i;
            }
            if (noraise)
                return -1;
            throw new IndexError($"index(x): {element.__repr__()} not in list");
        }

        [MixinMember]
        public static int count(TrObject self, TrObject element)
        {
            var n = 0;
            var len = self.__len__().AsInt();
            for(int i = 0; i < len; i++)
            {
                var curr = self.__getitem__(MK.Int(i));
                if(curr.__eq__(element))
                    n++;
            }
            return n;
        }

        

    }
}
