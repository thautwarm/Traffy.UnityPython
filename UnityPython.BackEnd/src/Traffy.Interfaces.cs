using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Traffy.Annotations;
using Traffy.Objects;

namespace Traffy
{
    public static class Interfaces_
    {
        public enum AbsMemberKind
        {
            ClassMethod,
            Method,
            StaticMethod,
            Property
        }

        [AttributeUsage(AttributeTargets.Field)]
        public class AbsMember: Attribute
        {
            public AbsMemberKind Kind;
            public string[] Names;
            public AbsMember(AbsMemberKind kind, params string[] names)
            {
                Kind = kind;
                Names = names;
            }
        }

        [AbsMember(AbsMemberKind.Method, nameof(TrObject.__lt__))]
        public static TrClass Comparable;

        [AbsMember(AbsMemberKind.Method, nameof(TrObject.__contains__))]
        public static TrClass Container;

        [AbsMember(AbsMemberKind.Method, nameof(TrObject.__hash__))]
        public static TrClass Hashable;

        [AbsMember(AbsMemberKind.Method, nameof(TrObject.__iter__))]
        public static TrClass Iterable;

        [AbsMember(AbsMemberKind.Method, nameof(TrObject.__next__))]
        public static TrClass Iterator;

        [AbsMember(AbsMemberKind.Method, nameof(TrObject.__reversed__))]
        public static TrClass Reversible;

        [AbsMember(AbsMemberKind.Method, nameof(TrObject.__len__))]
        public static TrClass Sized;
        [AbsMember(AbsMemberKind.Method, nameof(TrObject.__call__))]
        public static TrClass Callable;

        [AbsMember(AbsMemberKind.Method)]
        public static TrClass Collection;

        [AbsMember(AbsMemberKind.Method, nameof(TrObject.__getitem__))]
        public static TrClass Sequence;
        public static TrClass MutableSequence;
        public static TrClass ByteString;
        public static TrClass Set;
        public static TrClass MutableSet;
        public static TrClass Mapping;
        public static TrClass MutableMapping;
        public static TrClass Awaitable;

        public static SetupSortPair[] GetInterfaceClasses()
        {
            throw new NotImplementedException();
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrClass abs_class = (TrClass) args[0];
            throw new TypeError($"cannot instantiate abstract class {abs_class.Name}");
        }

        [Mark(Initialization.TokenClassInit)]
        static void InitInterfaceClasses()
        {
            Comparable = TrClass.CreateClass(nameof(Comparable));
            Container = TrClass.CreateClass(nameof(Container));
            Hashable = TrClass.CreateClass(nameof(Hashable));
            Iterable = TrClass.CreateClass(nameof(Iterable));
            Iterator = TrClass.CreateClass(nameof(Iterator), Iterable);
            Reversible = TrClass.CreateClass(nameof(Reversible), Iterable);
            Sized = TrClass.CreateClass(nameof(Sized));
            Callable = TrClass.CreateClass(nameof(Callable));
            Collection = TrClass.CreateClass(nameof(Collection), Sized, Iterable, Container);
            Sequence = TrClass.CreateClass(nameof(Sequence), Collection, Reversible);
            MutableSequence = TrClass.CreateClass(nameof(MutableSequence), Sequence);
            Set = TrClass.CreateClass(nameof(Set), Collection);
            MutableSet = TrClass.CreateClass(nameof(MutableSet), Set);
            Mapping = TrClass.CreateClass(nameof(Mapping), Collection);
            MutableMapping = TrClass.CreateClass(nameof(MutableMapping), Mapping);
            ByteString = TrClass.CreateClass(nameof(ByteString), Sequence);
            Awaitable = TrClass.CreateClass(nameof(Awaitable));
        }
    }


}