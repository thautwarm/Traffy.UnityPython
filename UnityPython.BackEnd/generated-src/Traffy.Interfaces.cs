using System;
using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
namespace Traffy.Interfaces
{
    public partial class AbstractClass
    {
        [Mark(Initialization.TokenBuiltinInit)]
        static void BindMethods()
        {
            Traffy.Interfaces.Awaitable.CLASS["__await__"] = TrSharpFunc.FromFunc("Awaitable.__await__", __bind___await__);
            Traffy.Interfaces.Callable.CLASS["__call__"] = TrSharpFunc.FromFunc("Callable.__call__", __call__);
            Traffy.Interfaces.Comparable.CLASS["__lt__"] = TrSharpFunc.FromFunc("Comparable.__lt__", __bind___lt__);
            Traffy.Interfaces.Comparable.CLASS["__ge__"] = TrSharpFunc.FromFunc("Comparable.__ge__", __bind___ge__);
            Traffy.Interfaces.Comparable.CLASS["__gt__"] = TrSharpFunc.FromFunc("Comparable.__gt__", __bind___gt__);
            Traffy.Interfaces.Comparable.CLASS["__le__"] = TrSharpFunc.FromFunc("Comparable.__le__", __bind___le__);
            Traffy.Interfaces.Container.CLASS["__contains__"] = TrSharpFunc.FromFunc("Container.__contains__", __bind___contains__);
            Traffy.Interfaces.Hashable.CLASS["__hash__"] = TrSharpFunc.FromFunc("Hashable.__hash__", __bind___hash__);
            Traffy.Interfaces.Iterable.CLASS["__iter__"] = TrSharpFunc.FromFunc("Iterable.__iter__", __bind___iter__);
            Traffy.Interfaces.Reversible.CLASS["__reversed__"] = TrSharpFunc.FromFunc("Reversible.__reversed__", __bind___reversed__);
            Traffy.Interfaces.Sequence.CLASS["__getitem__"] = TrSharpFunc.FromFunc("Sequence.__getitem__", __bind___getitem__);
            Traffy.Interfaces.Sequence.CLASS["__iter__"] = TrSharpFunc.FromFunc("Sequence.__iter__", __bind___iter__);
            Traffy.Interfaces.Sequence.CLASS["__contains__"] = TrSharpFunc.FromFunc("Sequence.__contains__", __bind___contains__);
            Traffy.Interfaces.Sequence.CLASS["__reversed__"] = TrSharpFunc.FromFunc("Sequence.__reversed__", __bind___reversed__);
            Traffy.Interfaces.Sequence.CLASS["index"] = TrSharpFunc.FromFunc("Sequence.index", __bind_index);
            Traffy.Interfaces.Sequence.CLASS["count"] = TrSharpFunc.FromFunc("Sequence.count", __bind_count);
            Traffy.Interfaces.Sized.CLASS["__len__"] = TrSharpFunc.FromFunc("Sized.__len__", __bind___len__);
        }
    }
    public static partial class Awaitable
    {
        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("Awaitable", Traffy.Objects.TrABC.CLASS);
            TrClass.TypeDict[typeof(Awaitable)] = CLASS;
        }


        [Traffy.Annotations.Mark(typeof(Awaitable))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        static  Traffy.Objects.TrObject __bind___await__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 1:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    return Box.Apply(Traffy.Interfaces.Awaitable.__await__(_0));
                }
                default:
                    throw new ValueError("__await__() requires 1 positional argument(s), got " + __args.Count);
            }
        }
    }
    public static partial class Callable
    {
        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("Callable", Traffy.Objects.TrABC.CLASS);
            TrClass.TypeDict[typeof(Callable)] = CLASS;
        }


        [Traffy.Annotations.Mark(typeof(Callable))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
    }
    public static partial class Collection
    {
        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("Collection", Traffy.Objects.TrABC.CLASS,Traffy.Interfaces.Sized.CLASS,Traffy.Interfaces.Iterable.CLASS,Traffy.Interfaces.Container.CLASS);
            TrClass.TypeDict[typeof(Collection)] = CLASS;
        }


        [Traffy.Annotations.Mark(typeof(Collection))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
    }
    public static partial class Comparable
    {
        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("Comparable", Traffy.Objects.TrABC.CLASS);
            TrClass.TypeDict[typeof(Comparable)] = CLASS;
        }


        [Traffy.Annotations.Mark(typeof(Comparable))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        static  Traffy.Objects.TrObject __bind___lt__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Comparable.__lt__(_0,_1));
                }
                default:
                    throw new ValueError("__lt__() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        static  Traffy.Objects.TrObject __bind___ge__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Comparable.__ge__" );
        }
        static  Traffy.Objects.TrObject __bind___gt__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Comparable.__gt__" );
        }
        static  Traffy.Objects.TrObject __bind___le__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Comparable.__le__" );
        }
    }
    public static partial class Container
    {
        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("Container", Traffy.Objects.TrABC.CLASS);
            TrClass.TypeDict[typeof(Container)] = CLASS;
        }


        [Traffy.Annotations.Mark(typeof(Container))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        static  Traffy.Objects.TrObject __bind___contains__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Container.__contains__(_0,_1));
                }
                default:
                    throw new ValueError("__contains__() requires 2 positional argument(s), got " + __args.Count);
            }
        }
    }
    public static partial class Hashable
    {
        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("Hashable", Traffy.Objects.TrABC.CLASS);
            TrClass.TypeDict[typeof(Hashable)] = CLASS;
        }


        [Traffy.Annotations.Mark(typeof(Hashable))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        static  Traffy.Objects.TrObject __bind___hash__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 1:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    return Box.Apply(Traffy.Interfaces.Hashable.__hash__(_0));
                }
                default:
                    throw new ValueError("__hash__() requires 1 positional argument(s), got " + __args.Count);
            }
        }
    }
    public static partial class Iterable
    {
        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("Iterable", Traffy.Objects.TrABC.CLASS);
            TrClass.TypeDict[typeof(Iterable)] = CLASS;
        }


        [Traffy.Annotations.Mark(typeof(Iterable))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        static  Traffy.Objects.TrObject __bind___iter__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 1:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    return Box.Apply(Traffy.Interfaces.Iterable.__iter__(_0));
                }
                default:
                    throw new ValueError("__iter__() requires 1 positional argument(s), got " + __args.Count);
            }
        }
    }
    public static partial class Reversible
    {
        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("Reversible", Traffy.Objects.TrABC.CLASS,Traffy.Interfaces.Iterable.CLASS);
            TrClass.TypeDict[typeof(Reversible)] = CLASS;
        }


        [Traffy.Annotations.Mark(typeof(Reversible))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        static  Traffy.Objects.TrObject __bind___reversed__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 1:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    return Box.Apply(Traffy.Interfaces.Reversible.__reversed__(_0));
                }
                default:
                    throw new ValueError("__reversed__() requires 1 positional argument(s), got " + __args.Count);
            }
        }
    }
    public static partial class Sequence
    {
        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("Sequence", Traffy.Objects.TrABC.CLASS,Traffy.Interfaces.Reversible.CLASS,Traffy.Interfaces.Collection.CLASS);
            TrClass.TypeDict[typeof(Sequence)] = CLASS;
        }


        [Traffy.Annotations.Mark(typeof(Sequence))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        static  Traffy.Objects.TrObject __bind___getitem__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Sequence.__getitem__(_0,_1));
                }
                default:
                    throw new ValueError("__getitem__() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        static  Traffy.Objects.TrObject __bind___iter__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Sequence.__iter__" );
        }
        static  Traffy.Objects.TrObject __bind___contains__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Sequence.__contains__" );
        }
        static  Traffy.Objects.TrObject __bind___reversed__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Sequence.__reversed__" );
        }
        static  Traffy.Objects.TrObject __bind_index(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Sequence.index" );
        }
        static  Traffy.Objects.TrObject __bind_count(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Sequence.count" );
        }
    }
    public static partial class Sized
    {
        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("Sized", Traffy.Objects.TrABC.CLASS);
            TrClass.TypeDict[typeof(Sized)] = CLASS;
        }


        [Traffy.Annotations.Mark(typeof(Sized))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        static  Traffy.Objects.TrObject __bind___len__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 1:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    return Box.Apply(Traffy.Interfaces.Sized.__len__(_0));
                }
                default:
                    throw new ValueError("__len__() requires 1 positional argument(s), got " + __args.Count);
            }
        }
    }
}
