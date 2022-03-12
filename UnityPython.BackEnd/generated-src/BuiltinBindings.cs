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
            Initialization.Prelude(TrSharpFunc.FromFunc("print", Builtins.print));
            Initialization.Prelude(TrSharpFunc.FromFunc("stacktrace", Builtins.stacktrace));
            Initialization.Prelude(TrSharpFunc.FromFunc("abs", Builtins.abs));
            Initialization.Prelude(TrSharpFunc.FromFunc("bin", Builtins.bin));
            Initialization.Prelude(TrSharpFunc.FromFunc("chr", Builtins.chr));
        }

    
    }
}
