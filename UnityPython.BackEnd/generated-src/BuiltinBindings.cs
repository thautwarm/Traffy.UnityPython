using Traffy;
using Traffy.Objects;
namespace Traffy
{
    public static partial class Builtins
    {
         [Traffy.Annotations.Mark(Initialization.TokenBuiltinInit)]
        static void InitBuiltins()
        {
            Initialization.Prelude(TrSharpFunc.FromFunc("map", Builtins.map));
            Initialization.Prelude(TrSharpFunc.FromFunc("filter", Builtins.filter));
            Initialization.Prelude(TrSharpFunc.FromFunc("range", Builtins.range));
            Initialization.Prelude(TrSharpFunc.FromFunc("all", Builtins.all));
            Initialization.Prelude(TrSharpFunc.FromFunc("any", Builtins.any));
            Initialization.Prelude(TrSharpFunc.FromFunc("zip", Builtins.zip));
            Initialization.Prelude(TrSharpFunc.FromFunc("enumerate", Builtins.enumerate));
            Initialization.Prelude(TrSharpFunc.FromFunc("reversed", Builtins.reversed));
            Initialization.Prelude(TrSharpFunc.FromFunc("print", Builtins.print));
            Initialization.Prelude(TrSharpFunc.FromFunc("stacktrace", Builtins.stacktrace));
            Initialization.Prelude(TrSharpFunc.FromFunc("abs", Builtins.abs));
            Initialization.Prelude(TrSharpFunc.FromFunc("bin", Builtins.bin));
            Initialization.Prelude(TrSharpFunc.FromFunc("chr", Builtins.chr));
            Initialization.Prelude(TrSharpFunc.FromFunc("hex", Builtins.hex));
            Initialization.Prelude(TrSharpFunc.FromFunc("getattr", Builtins.getattr));
            Initialization.Prelude(TrSharpFunc.FromFunc("setattr", Builtins.setattr));
            Initialization.Prelude(TrSharpFunc.FromFunc("len", Builtins.len));
            Initialization.Prelude(TrSharpFunc.FromFunc("hash", Builtins.hash));
        }

    
    }
}
