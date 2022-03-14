using Traffy;
using Traffy.Objects;
using System;
using System.Collections.Generic;
namespace Traffy
{
    public static partial class Builtins
    {
         [Traffy.Annotations.Mark(Initialization.TokenBuiltinInit)]
        static void InitBuiltins()
        {
            Initialization.Prelude(TrSharpFunc.FromFunc("map", map));
            Initialization.Prelude(TrSharpFunc.FromFunc("filter", filter));
            Initialization.Prelude(TrSharpFunc.FromFunc("range", range));
            Initialization.Prelude(TrSharpFunc.FromFunc("all", all));
            Initialization.Prelude(TrSharpFunc.FromFunc("any", any));
            Initialization.Prelude(TrSharpFunc.FromFunc("zip", zip));
            Initialization.Prelude(TrSharpFunc.FromFunc("enumerate", enumerate));
            Traffy.Objects.TrObject __bind_reversed(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    1 => Traffy.Builtins.reversed(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0])),
                    _ => throw new ValueError("requires 1 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("reversed", __bind_reversed));
            Initialization.Prelude(TrSharpFunc.FromFunc("sorted", sorted));
            Traffy.Objects.TrObject __bind_sum(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    1 => Traffy.Builtins.sum(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0])),
                    2 => Traffy.Builtins.sum(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]),Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1])),
                    _ => throw new ValueError("requires 1 to 2 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("sum", __bind_sum));
            Initialization.Prelude(TrSharpFunc.FromFunc("print", print));
            Traffy.Objects.TrObject __bind_stacktrace(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    1 => Traffy.Builtins.stacktrace(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0])),
                    _ => throw new ValueError("requires 1 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("stacktrace", __bind_stacktrace));
            Traffy.Objects.TrObject __bind_abs(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    1 => Traffy.Builtins.abs(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0])),
                    _ => throw new ValueError("requires 1 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("abs", __bind_abs));
            Traffy.Objects.TrObject __bind_bin(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    1 => Traffy.Builtins.bin(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0])),
                    _ => throw new ValueError("requires 1 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("bin", __bind_bin));
            Traffy.Objects.TrObject __bind_chr(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    1 => Traffy.Builtins.chr(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0])),
                    _ => throw new ValueError("requires 1 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("chr", __bind_chr));
            Traffy.Objects.TrObject __bind_ord(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    1 => Traffy.Builtins.ord(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0])),
                    _ => throw new ValueError("requires 1 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("ord", __bind_ord));
            Traffy.Objects.TrObject __bind_oct(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    1 => Traffy.Builtins.oct(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0])),
                    _ => throw new ValueError("requires 1 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("oct", __bind_oct));
            Traffy.Objects.TrObject __bind_hex(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    1 => Traffy.Builtins.hex(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0])),
                    _ => throw new ValueError("requires 1 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("hex", __bind_hex));
            Traffy.Objects.TrObject __bind_hasattr(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    2 => Traffy.Builtins.hasattr(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]),Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1])),
                    _ => throw new ValueError("requires 2 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("hasattr", __bind_hasattr));
            Initialization.Prelude(TrSharpFunc.FromFunc("getattr", getattr));
            Traffy.Objects.TrObject __bind_setattr(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    3 => Traffy.Builtins.setattr(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]),Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1]),Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[2])),
                    _ => throw new ValueError("requires 3 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("setattr", __bind_setattr));
            Traffy.Objects.TrObject __bind_len(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    1 => Traffy.Builtins.len(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0])),
                    _ => throw new ValueError("requires 1 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("len", __bind_len));
            Traffy.Objects.TrObject __bind_hash(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    1 => Traffy.Builtins.hash(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0])),
                    _ => throw new ValueError("requires 1 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("hash", __bind_hash));
            Traffy.Objects.TrObject __bind_pow(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    2 => Traffy.Builtins.pow(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]),Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1])),
                    _ => throw new ValueError("requires 2 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("pow", __bind_pow));
            Traffy.Objects.TrObject __bind_repr(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    1 => Traffy.Builtins.repr(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0])),
                    _ => throw new ValueError("requires 1 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("repr", __bind_repr));
            Traffy.Objects.TrObject __bind_round(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    1 => Traffy.Builtins.round(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0])),
                    2 => Traffy.Builtins.round(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]),Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1])),
                    _ => throw new ValueError("requires 1 to 2 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("round", __bind_round));
            Traffy.Objects.TrObject __bind_isinstance(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    2 => Traffy.Builtins.isinstance(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]),Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1])),
                    _ => throw new ValueError("requires 2 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("isinstance", __bind_isinstance));
            Traffy.Objects.TrObject __bind_issubclass(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                return Box.Apply(__args.Count switch
                {
                    2 => Traffy.Builtins.issubclass(Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[0]),Unbox.Apply(THint<Traffy.Objects.TrObject>.Unique,__args[1])),
                    _ => throw new ValueError("requires 2 argument(s), got " + __args.Count)
                }) ;
            }
            Initialization.Prelude(TrSharpFunc.FromFunc("issubclass", __bind_issubclass));
        }

    
    }
}
