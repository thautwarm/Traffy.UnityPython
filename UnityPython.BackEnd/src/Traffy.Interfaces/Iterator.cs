using System.Collections.Generic;
using Traffy;
using Traffy.Annotations;
using Traffy.Objects;

namespace Traffy.Interfaces
{
    [AbstractClass]
    [PyInherit(typeof(Iterable))]
    public static partial class Iterator
    {
        [AbsMember]
        public static bool __trynext__(TrObject self, TrRef refval)
        {
            return TrObject.__trynext__(self, refval);
        }

        [MixinMember]
        public static TrObject __next__(TrObject self)
        {
            var refval = MK.Ref();
            return TrObject.__trynext__(self, refval) ? refval.value : throw new StopIteration();
        }

        [MixinMember]
        public static IEnumerator<TrObject> map(TrObject self, TrObject func)
        {
            var itr = self.__iter__();
            while(itr.MoveNext())
            {
                yield return func.Call(itr.Current);
            }
        }

        [MixinMember]
        public static IEnumerator<TrObject> mapi(TrObject self, TrObject func)
        {
            var itr = self.__iter__();
            var i = 0;
            while(itr.MoveNext())
            {
                yield return func.Call(MK.Int(i), itr.Current);
            }
        }

        [MixinMember]
        public static IEnumerator<TrObject> filter(TrObject self, TrObject func)
        {
            var itr = self.__iter__();
            while(itr.MoveNext())
            {
                if(func.Call(itr.Current).__bool__())
                    yield return itr.Current;
            }
        }

        [MixinMember]
        public static IEnumerator<TrObject> filteri(TrObject self, TrObject func)
        {
            var itr = self.__iter__();
            var i = 0;
            while(itr.MoveNext())
            {
                if(func.Call(MK.Int(i), itr.Current).__bool__())
                    yield return itr.Current;
            }
        }

        [MixinMember]
        public static IEnumerator<TrObject> skip(TrObject self, TrObject objInt)
        {
            var itr = self.__iter__();
            var n = objInt.AsInt();
            while(n-- > 0 && itr.MoveNext())
            {
            }
            while (itr.MoveNext())
                yield return itr.Current;
        }

        [MixinMember]
        public static IEnumerator<TrObject> append(TrObject self, TrObject e)
        {
            var itr = self.__iter__();
            while (itr.MoveNext())
                yield return itr.Current;
            yield return e;
        }

        
        [MixinMember]
        public static IEnumerator<TrObject> prepend(TrObject self, TrObject e)
        {
            yield return e;
            var itr = self.__iter__();
            while (itr.MoveNext())
                yield return itr.Current;   
        }

        [MixinMember]
        public static void @foreach(TrObject self, TrObject action)
        {
            var itr = self.__iter__();
            while (itr.MoveNext())
                action.Call(itr.Current);
        }

        [MixinMember]
        public static void @foreachi(TrObject self, TrObject action)
        {
            var itr = self.__iter__();
            int i = 0;
            while (itr.MoveNext())
            {
                action.Call(MK.Int(i), itr.Current);
            }
        }

        [MixinMember]
        public static IEnumerator<TrObject> concat(TrObject self, TrObject other)
        {
            var itr = self.__iter__();
            while (itr.MoveNext())
                yield return itr.Current;
            itr = other.__iter__();
            while (itr.MoveNext())
                yield return itr.Current;
        }

        [MixinMember]
        public static TrObject sum(TrObject self, TrObject __init = null)
        {
            var itr = self.__iter__();
            TrObject s = __init ?? MK.IntZero;
            while (itr.MoveNext())
                s = RTS.object_add(s, itr.Current);
            return s;
        }

        [MixinMember]
        public static List<TrObject> tolist(TrObject self)
        {
            var list = new List<TrObject>();
            var itr = self.__iter__();
            while (itr.MoveNext())
                list.Add(itr.Current);
            return list;
        }
    }
}
