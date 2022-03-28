using System;
using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
namespace Traffy.Interfaces
{
    public partial class AbstractClass
    {
        internal static void generated_BindMethods()
        {
            Traffy.Interfaces.Awaitable.CLASS["__await__"] = TrSharpFunc.FromFunc("Awaitable.__await__", Traffy.Interfaces.Awaitable.__bind___await__);
            Traffy.Interfaces.Callable.CLASS["__call__"] = TrSharpFunc.FromFunc("Callable.__call__", Traffy.Interfaces.Callable.__call__);
            Traffy.Interfaces.Comparable.CLASS["__lt__"] = TrSharpFunc.FromFunc("Comparable.__lt__", Traffy.Interfaces.Comparable.__bind___lt__);
            Traffy.Interfaces.Comparable.CLASS["__ge__"] = TrSharpFunc.FromFunc("Comparable.__ge__", Traffy.Interfaces.Comparable.__bind___ge__);
            Traffy.Interfaces.Comparable.CLASS["__gt__"] = TrSharpFunc.FromFunc("Comparable.__gt__", Traffy.Interfaces.Comparable.__bind___gt__);
            Traffy.Interfaces.Comparable.CLASS["__le__"] = TrSharpFunc.FromFunc("Comparable.__le__", Traffy.Interfaces.Comparable.__bind___le__);
            Traffy.Interfaces.Container.CLASS["__contains__"] = TrSharpFunc.FromFunc("Container.__contains__", Traffy.Interfaces.Container.__bind___contains__);
            Traffy.Interfaces.function.CLASS["__call__"] = TrSharpFunc.FromFunc("function.__call__", Traffy.Interfaces.function.__bind___call__);
            Traffy.Interfaces.Hashable.CLASS["__hash__"] = TrSharpFunc.FromFunc("Hashable.__hash__", Traffy.Interfaces.Hashable.__bind___hash__);
            Traffy.Interfaces.Iterable.CLASS["__iter__"] = TrSharpFunc.FromFunc("Iterable.__iter__", Traffy.Interfaces.Iterable.__bind___iter__);
            Traffy.Interfaces.Reversible.CLASS["__reversed__"] = TrSharpFunc.FromFunc("Reversible.__reversed__", Traffy.Interfaces.Reversible.__bind___reversed__);
            Traffy.Interfaces.Sequence.CLASS["__getitem__"] = TrSharpFunc.FromFunc("Sequence.__getitem__", Traffy.Interfaces.Sequence.__bind___getitem__);
            Traffy.Interfaces.Sequence.CLASS["__iter__"] = TrSharpFunc.FromFunc("Sequence.__iter__", Traffy.Interfaces.Sequence.__bind___iter__);
            Traffy.Interfaces.Sequence.CLASS["__contains__"] = TrSharpFunc.FromFunc("Sequence.__contains__", Traffy.Interfaces.Sequence.__bind___contains__);
            Traffy.Interfaces.Sequence.CLASS["__reversed__"] = TrSharpFunc.FromFunc("Sequence.__reversed__", Traffy.Interfaces.Sequence.__bind___reversed__);
            Traffy.Interfaces.Sequence.CLASS["index"] = TrSharpFunc.FromFunc("Sequence.index", Traffy.Interfaces.Sequence.__bind_index);
            Traffy.Interfaces.Sequence.CLASS["count"] = TrSharpFunc.FromFunc("Sequence.count", Traffy.Interfaces.Sequence.__bind_count);
            Traffy.Interfaces.Sized.CLASS["__len__"] = TrSharpFunc.FromFunc("Sized.__len__", Traffy.Interfaces.Sized.__bind___len__);
        }
    }
    public static partial class Awaitable
    {
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("Awaitable");
        }
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];
        }


        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___await__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Awaitable.__await__" );
        }
    }
    public static partial class Callable
    {
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("Callable");
        }
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];
        }


        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
    }
    public static partial class Collection
    {
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("Collection");
        }
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];
        }


        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
    }
    public static partial class Comparable
    {
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("Comparable");
        }
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];
        }


        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___lt__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Comparable.__lt__" );
        }
        public static  Traffy.Objects.TrObject __bind___ge__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Comparable.__ge__(_0,_1));
                }
                default:
                    throw new ValueError("__ge__() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind___gt__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Comparable.__gt__(_0,_1));
                }
                default:
                    throw new ValueError("__gt__() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind___le__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Comparable.__le__(_0,_1));
                }
                default:
                    throw new ValueError("__le__() requires 2 positional argument(s), got " + __args.Count);
            }
        }
    }
    public static partial class Container
    {
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("Container");
        }
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];
        }


        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___contains__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Container.__contains__" );
        }
    }
    public static partial class function
    {
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("function");
        }
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];
        }


        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___call__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method function.__call__" );
        }
    }
    public static partial class Hashable
    {
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("Hashable");
        }
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];
        }


        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___hash__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Hashable.__hash__" );
        }
    }
    public static partial class Iterable
    {
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("Iterable");
        }
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];
        }


        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___iter__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Iterable.__iter__" );
        }
    }
    public static partial class Reversible
    {
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("Reversible");
        }
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];
        }


        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___reversed__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Reversible.__reversed__" );
        }
    }
    public static partial class Sequence
    {
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("Sequence");
        }
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];
        }


        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___getitem__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Sequence.__getitem__" );
        }
        public static  Traffy.Objects.TrObject __bind___iter__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 1:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    return Box.Apply(Traffy.Interfaces.Sequence.__iter__(_0));
                }
                default:
                    throw new ValueError("__iter__() requires 1 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind___contains__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Sequence.__contains__(_0,_1));
                }
                default:
                    throw new ValueError("__contains__() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind___reversed__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 1:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    return Box.Apply(Traffy.Interfaces.Sequence.__reversed__(_0));
                }
                default:
                    throw new ValueError("__reversed__() requires 1 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_index(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    System.Int32 _2;
                    if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("start"),out var __keyword__2)))
                        _2 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__2);
                    else
                        _2 = 0;
                    System.Int32 _3;
                    if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("stop"),out var __keyword__3)))
                        _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                    else
                        _3 = -1;
                    bool _4;
                    if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("noraise"),out var __keyword__4)))
                        _4 = Unbox.Apply(THint<bool>.Unique,__keyword__4);
                    else
                        _4 = false;
                    return Box.Apply(Traffy.Interfaces.Sequence.index(_0,_1,start : _2,stop : _3,noraise : _4));
                }
                case 3:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                    System.Int32 _3;
                    if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("stop"),out var __keyword__3)))
                        _3 = Unbox.Apply(THint<System.Int32>.Unique,__keyword__3);
                    else
                        _3 = -1;
                    bool _4;
                    if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("noraise"),out var __keyword__4)))
                        _4 = Unbox.Apply(THint<bool>.Unique,__keyword__4);
                    else
                        _4 = false;
                    return Box.Apply(Traffy.Interfaces.Sequence.index(_0,_1,_2,stop : _3,noraise : _4));
                }
                case 4:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    var _2 = Unbox.Apply(THint<System.Int32>.Unique,__args[2]);
                    var _3 = Unbox.Apply(THint<System.Int32>.Unique,__args[3]);
                    bool _4;
                    if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("noraise"),out var __keyword__4)))
                        _4 = Unbox.Apply(THint<bool>.Unique,__keyword__4);
                    else
                        _4 = false;
                    return Box.Apply(Traffy.Interfaces.Sequence.index(_0,_1,_2,_3,noraise : _4));
                }
                default:
                    throw new ValueError("index() requires 2 to 4 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_count(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Sequence.count(_0,_1));
                }
                default:
                    throw new ValueError("count() requires 2 positional argument(s), got " + __args.Count);
            }
        }
    }
    public static partial class Sized
    {
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("Sized");
        }
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];
        }


        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___len__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Sized.__len__" );
        }
    }
}

