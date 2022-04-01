using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
namespace Traffy.Modules
{
    public sealed partial class TrModule_typing
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind_runtime_checkable(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Modules.TrModule_typing.runtime_checkable(_0));
                    }
                    default:
                        throw new ValueError("runtime_checkable() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["runtime_checkable"] = TrStaticMethod.Bind(CLASS.Name + "." + "runtime_checkable", __bind_runtime_checkable);
            static  Traffy.Objects.TrObject __bind_overload(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Modules.TrModule_typing.overload(_0));
                    }
                    default:
                        throw new ValueError("overload() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["overload"] = TrStaticMethod.Bind(CLASS.Name + "." + "overload", __bind_overload);
            static  Traffy.Objects.TrObject __bind_cast(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Modules.TrModule_typing.cast(_0));
                    }
                    default:
                        throw new ValueError("cast() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["cast"] = TrStaticMethod.Bind(CLASS.Name + "." + "cast", __bind_cast);
            static  Traffy.Objects.TrObject __bind_reveal_type(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Modules.TrModule_typing.reveal_type(_0));
                    }
                    default:
                        throw new ValueError("reveal_type() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["reveal_type"] = TrStaticMethod.Bind(CLASS.Name + "." + "reveal_type", __bind_reveal_type);
            static  Traffy.Objects.TrObject __bind_assert_never(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Modules.TrModule_typing.assert_never(_0));
                    }
                    default:
                        throw new ValueError("assert_never() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["assert_never"] = TrStaticMethod.Bind(CLASS.Name + "." + "assert_never", __bind_assert_never);
            static  Traffy.Objects.TrObject __bind_type_check_only(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Modules.TrModule_typing.type_check_only(_0));
                    }
                    default:
                        throw new ValueError("type_check_only() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["type_check_only"] = TrStaticMethod.Bind(CLASS.Name + "." + "type_check_only", __bind_type_check_only);
            static  Traffy.Objects.TrObject __bind_no_type_check(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Modules.TrModule_typing.no_type_check(_0));
                    }
                    default:
                        throw new ValueError("no_type_check() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["no_type_check"] = TrStaticMethod.Bind(CLASS.Name + "." + "no_type_check", __bind_no_type_check);
            static  Traffy.Objects.TrObject __bind_no_type_check_decorator(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Modules.TrModule_typing.no_type_check_decorator(_0));
                    }
                    default:
                        throw new ValueError("no_type_check_decorator() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["no_type_check_decorator"] = TrStaticMethod.Bind(CLASS.Name + "." + "no_type_check_decorator", __bind_no_type_check_decorator);
            static  Traffy.Objects.TrObject __bind_NewType(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 2:
                    {
                        var _0 = Unbox.Apply(THint<string>.Unique,__args[0]);
                        var _1 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]);
                        return Box.Apply(Traffy.Modules.TrModule_typing.NewType(_0,_1));
                    }
                    default:
                        throw new ValueError("NewType() requires 2 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["NewType"] = TrStaticMethod.Bind(CLASS.Name + "." + "NewType", __bind_NewType);
            CLASS["TypeVar"] = TrStaticMethod.Bind(CLASS.Name + "." + "TypeVar", TypeVar);
            static  Traffy.Objects.TrObject __bind_final(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]);
                        return Box.Apply(Traffy.Modules.TrModule_typing.final(_0));
                    }
                    default:
                        throw new ValueError("final() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["final"] = TrStaticMethod.Bind(CLASS.Name + "." + "final", __bind_final);
            CLASS["TypeGuard"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.TypeGuard);
            CLASS["TypeAlias"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.TypeAlias);
            CLASS["ForwardRef"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.ForwardRef);
            CLASS["TYPE_CHECKING"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.TYPE_CHECKING);
            CLASS["Type"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Type);
            CLASS["Any"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Any);
            CLASS["Never"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Never);
            CLASS["Self"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Self);
            CLASS["Final"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Final);
            CLASS["Literal"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Literal);
            CLASS["AnyStr"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.AnyStr);
            CLASS["Annotated"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Annotated);
            CLASS["Generic"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Generic);
            CLASS["NoReturn"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.NoReturn);
            CLASS["Protocol"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Protocol);
            CLASS["TypedDict"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.TypedDict);
            CLASS["Awaitable"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Awaitable);
            CLASS["Callable"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Callable);
            CLASS["Collection"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Collection);
            CLASS["Comparable"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Comparable);
            CLASS["Container"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Container);
            CLASS["Hashable"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Hashable);
            CLASS["Iterable"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Iterable);
            CLASS["Iterator"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Iterator);
            CLASS["Reversible"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Reversible);
            CLASS["Sequence"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Sequence);
            CLASS["Sized"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Sized);
            CLASS["Mapping"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Mapping);
            CLASS["ContextManager"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.ContextManager);
            CLASS["Coroutine"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.Coroutine);
            CLASS["AsyncGenerator"] = Traffy.Box.Apply(Traffy.Modules.TrModule_typing.AsyncGenerator);
        }
    }
}

