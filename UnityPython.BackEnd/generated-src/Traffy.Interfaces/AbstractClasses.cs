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
            Traffy.Interfaces.ContextManager.CLASS["__enter__"] = TrSharpFunc.FromFunc("ContextManager.__enter__", Traffy.Interfaces.ContextManager.__bind___enter__);
            Traffy.Interfaces.ContextManager.CLASS["__exit__"] = TrSharpFunc.FromFunc("ContextManager.__exit__", Traffy.Interfaces.ContextManager.__bind___exit__);
            Traffy.Interfaces.Hashable.CLASS["__hash__"] = TrSharpFunc.FromFunc("Hashable.__hash__", Traffy.Interfaces.Hashable.__bind___hash__);
            Traffy.Interfaces.Iterable.CLASS["__iter__"] = TrSharpFunc.FromFunc("Iterable.__iter__", Traffy.Interfaces.Iterable.__bind___iter__);
            Traffy.Interfaces.Iterator.CLASS["__trynext__"] = TrSharpFunc.FromFunc("Iterator.__trynext__", Traffy.Interfaces.Iterator.__bind___trynext__);
            Traffy.Interfaces.Iterator.CLASS["__next__"] = TrSharpFunc.FromFunc("Iterator.__next__", Traffy.Interfaces.Iterator.__bind___next__);
            Traffy.Interfaces.Iterator.CLASS["map"] = TrSharpFunc.FromFunc("Iterator.map", Traffy.Interfaces.Iterator.__bind_map);
            Traffy.Interfaces.Iterator.CLASS["mapi"] = TrSharpFunc.FromFunc("Iterator.mapi", Traffy.Interfaces.Iterator.__bind_mapi);
            Traffy.Interfaces.Iterator.CLASS["filter"] = TrSharpFunc.FromFunc("Iterator.filter", Traffy.Interfaces.Iterator.__bind_filter);
            Traffy.Interfaces.Iterator.CLASS["filteri"] = TrSharpFunc.FromFunc("Iterator.filteri", Traffy.Interfaces.Iterator.__bind_filteri);
            Traffy.Interfaces.Iterator.CLASS["skip"] = TrSharpFunc.FromFunc("Iterator.skip", Traffy.Interfaces.Iterator.__bind_skip);
            Traffy.Interfaces.Iterator.CLASS["append"] = TrSharpFunc.FromFunc("Iterator.append", Traffy.Interfaces.Iterator.__bind_append);
            Traffy.Interfaces.Iterator.CLASS["prepend"] = TrSharpFunc.FromFunc("Iterator.prepend", Traffy.Interfaces.Iterator.__bind_prepend);
            Traffy.Interfaces.Iterator.CLASS["foreach"] = TrSharpFunc.FromFunc("Iterator.foreach", Traffy.Interfaces.Iterator.__bind_foreach);
            Traffy.Interfaces.Iterator.CLASS["foreachi"] = TrSharpFunc.FromFunc("Iterator.foreachi", Traffy.Interfaces.Iterator.__bind_foreachi);
            Traffy.Interfaces.Iterator.CLASS["concat"] = TrSharpFunc.FromFunc("Iterator.concat", Traffy.Interfaces.Iterator.__bind_concat);
            Traffy.Interfaces.Iterator.CLASS["sum"] = TrSharpFunc.FromFunc("Iterator.sum", Traffy.Interfaces.Iterator.__bind_sum);
            Traffy.Interfaces.Iterator.CLASS["tolist"] = TrSharpFunc.FromFunc("Iterator.tolist", Traffy.Interfaces.Iterator.__bind_tolist);
            Traffy.Interfaces.Mapping.CLASS["__iter__"] = TrSharpFunc.FromFunc("Mapping.__iter__", Traffy.Interfaces.Mapping.__bind___iter__);
            Traffy.Interfaces.Mapping.CLASS["__finditem__"] = TrSharpFunc.FromFunc("Mapping.__finditem__", Traffy.Interfaces.Mapping.__bind___finditem__);
            Traffy.Interfaces.Mapping.CLASS["__getitem__"] = TrSharpFunc.FromFunc("Mapping.__getitem__", Traffy.Interfaces.Mapping.__bind___getitem__);
            Traffy.Interfaces.Mapping.CLASS["__contains__"] = TrSharpFunc.FromFunc("Mapping.__contains__", Traffy.Interfaces.Mapping.__bind___contains__);
            Traffy.Interfaces.Mapping.CLASS["keys"] = TrSharpFunc.FromFunc("Mapping.keys", Traffy.Interfaces.Mapping.__bind_keys);
            Traffy.Interfaces.Mapping.CLASS["values"] = TrSharpFunc.FromFunc("Mapping.values", Traffy.Interfaces.Mapping.__bind_values);
            Traffy.Interfaces.Mapping.CLASS["items"] = TrSharpFunc.FromFunc("Mapping.items", Traffy.Interfaces.Mapping.__bind_items);
            Traffy.Interfaces.Mapping.CLASS["get"] = TrSharpFunc.FromFunc("Mapping.get", Traffy.Interfaces.Mapping.__bind_get);
            Traffy.Interfaces.Mapping.CLASS["__ne__"] = TrSharpFunc.FromFunc("Mapping.__ne__", Traffy.Interfaces.Mapping.__bind___ne__);
            Traffy.Interfaces.Mapping.CLASS["__eq__"] = TrSharpFunc.FromFunc("Mapping.__eq__", Traffy.Interfaces.Mapping.__bind___eq__);
            Traffy.Interfaces.Reversible.CLASS["__reversed__"] = TrSharpFunc.FromFunc("Reversible.__reversed__", Traffy.Interfaces.Reversible.__bind___reversed__);
            Traffy.Interfaces.Sequence.CLASS["__getitem__"] = TrSharpFunc.FromFunc("Sequence.__getitem__", Traffy.Interfaces.Sequence.__bind___getitem__);
            Traffy.Interfaces.Sequence.CLASS["__iter__"] = TrSharpFunc.FromFunc("Sequence.__iter__", Traffy.Interfaces.Sequence.__bind___iter__);
            Traffy.Interfaces.Sequence.CLASS["__contains__"] = TrSharpFunc.FromFunc("Sequence.__contains__", Traffy.Interfaces.Sequence.__bind___contains__);
            Traffy.Interfaces.Sequence.CLASS["__reversed__"] = TrSharpFunc.FromFunc("Sequence.__reversed__", Traffy.Interfaces.Sequence.__bind___reversed__);
            Traffy.Interfaces.Sequence.CLASS["index"] = TrSharpFunc.FromFunc("Sequence.index", Traffy.Interfaces.Sequence.__bind_index);
            Traffy.Interfaces.Sequence.CLASS["count"] = TrSharpFunc.FromFunc("Sequence.count", Traffy.Interfaces.Sequence.__bind_count);
            Traffy.Interfaces.Sized.CLASS["__len__"] = TrSharpFunc.FromFunc("Sized.__len__", Traffy.Interfaces.Sized.__bind___len__);
            Traffy.Interfaces.Sized.CLASS["__bool__"] = TrSharpFunc.FromFunc("Sized.__bool__", Traffy.Interfaces.Sized.__bind___bool__);
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
            CLASS.IsClassFixed = true;
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
            CLASS.IsClassFixed = true;
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
            CLASS.IsClassFixed = true;
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
            CLASS.IsClassFixed = true;
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
            CLASS.IsClassFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___contains__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Container.__contains__" );
        }
    }
    public static partial class ContextManager
    {
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("ContextManager");
        }
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];
        }


        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsClassFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___enter__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method ContextManager.__enter__" );
        }
        public static  Traffy.Objects.TrObject __bind___exit__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method ContextManager.__exit__" );
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
            CLASS.IsClassFixed = true;
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
            CLASS.IsClassFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___iter__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Iterable.__iter__" );
        }
    }
    public static partial class Iterator
    {
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("Iterator");
        }
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];
        }


        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsClassFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___trynext__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Iterator.__trynext__" );
        }
        public static  Traffy.Objects.TrObject __bind___next__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 1:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    return Box.Apply(Traffy.Interfaces.Iterator.__next__(_0));
                }
                default:
                    throw new ValueError("__next__() requires 1 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_map(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Iterator.map(_0,_1));
                }
                default:
                    throw new ValueError("map() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_mapi(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Iterator.mapi(_0,_1));
                }
                default:
                    throw new ValueError("mapi() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_filter(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Iterator.filter(_0,_1));
                }
                default:
                    throw new ValueError("filter() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_filteri(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Iterator.filteri(_0,_1));
                }
                default:
                    throw new ValueError("filteri() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_skip(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Iterator.skip(_0,_1));
                }
                default:
                    throw new ValueError("skip() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_append(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Iterator.append(_0,_1));
                }
                default:
                    throw new ValueError("append() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_prepend(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Iterator.prepend(_0,_1));
                }
                default:
                    throw new ValueError("prepend() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_foreach(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    Traffy.Interfaces.Iterator.@foreach(_0,_1);
                    return Traffy.MK.None();
                }
                default:
                    throw new ValueError("foreach() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_foreachi(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    Traffy.Interfaces.Iterator.foreachi(_0,_1);
                    return Traffy.MK.None();
                }
                default:
                    throw new ValueError("foreachi() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_concat(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Iterator.concat(_0,_1));
                }
                default:
                    throw new ValueError("concat() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_sum(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 1:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    Traffy.Objects.TrObject _1;
                    if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("__init"),out var __keyword__1)))
                        _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__keyword__1);
                    else
                        _1 = null;
                    return Box.Apply(Traffy.Interfaces.Iterator.sum(_0,__init : _1));
                }
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Iterator.sum(_0,_1));
                }
                default:
                    throw new ValueError("sum() requires 1 to 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_tolist(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 1:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    return Box.Apply(Traffy.Interfaces.Iterator.tolist(_0));
                }
                default:
                    throw new ValueError("tolist() requires 1 positional argument(s), got " + __args.Count);
            }
        }
    }
    public static partial class Mapping
    {
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("Mapping");
        }
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];
        }


        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsClassFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___iter__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Mapping.__iter__" );
        }
        public static  Traffy.Objects.TrObject __bind___finditem__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Mapping.__finditem__" );
        }
        public static  Traffy.Objects.TrObject __bind___getitem__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Mapping.__getitem__(_0,_1));
                }
                default:
                    throw new ValueError("__getitem__() requires 2 positional argument(s), got " + __args.Count);
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
                    return Box.Apply(Traffy.Interfaces.Mapping.__contains__(_0,_1));
                }
                default:
                    throw new ValueError("__contains__() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_keys(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 1:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    return Box.Apply(Traffy.Interfaces.Mapping.keys(_0));
                }
                default:
                    throw new ValueError("keys() requires 1 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_values(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 1:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    return Box.Apply(Traffy.Interfaces.Mapping.values(_0));
                }
                default:
                    throw new ValueError("values() requires 1 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_items(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 1:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    return Box.Apply(Traffy.Interfaces.Mapping.items(_0));
                }
                default:
                    throw new ValueError("items() requires 1 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind_get(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    Traffy.Objects.TrObject _2;
                    if (((__kwargs != null) && __kwargs.TryGetValue(MK.Str("defaultVal"),out var __keyword__2)))
                        _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__keyword__2);
                    else
                        _2 = null;
                    return Box.Apply(Traffy.Interfaces.Mapping.get(_0,_1,defaultVal : _2));
                }
                case 3:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    var _2 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[2]);
                    return Box.Apply(Traffy.Interfaces.Mapping.get(_0,_1,_2));
                }
                default:
                    throw new ValueError("get() requires 2 to 3 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind___ne__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Mapping.__ne__(_0,_1));
                }
                default:
                    throw new ValueError("__ne__() requires 2 positional argument(s), got " + __args.Count);
            }
        }
        public static  Traffy.Objects.TrObject __bind___eq__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 2:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                    return Box.Apply(Traffy.Interfaces.Mapping.__eq__(_0,_1));
                }
                default:
                    throw new ValueError("__eq__() requires 2 positional argument(s), got " + __args.Count);
            }
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
            CLASS.IsClassFixed = true;
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
            CLASS.IsClassFixed = true;
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
            CLASS.IsClassFixed = true;
        }
        public static TrClass CLASS;
        public static  Traffy.Objects.TrObject __bind___len__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            throw new ValueError( "cannot call abstract method Sized.__len__" );
        }
        public static  Traffy.Objects.TrObject __bind___bool__(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
        {
            switch(__args.Count)
            {
                case 1:
                {
                    var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                    return Box.Apply(Traffy.Interfaces.Sized.__bool__(_0));
                }
                default:
                    throw new ValueError("__bool__() requires 1 positional argument(s), got " + __args.Count);
            }
        }
    }
}

