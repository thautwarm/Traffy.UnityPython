using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using PrettyDoc;
using Traffy;
using Traffy.Annotations;
using Traffy.InlineCache;
using Traffy.Objects;
using static ExtCodeGen;
using static PrettyDoc.ExtPrettyDoc;

[CodeGen(Path = "BuiltinBindings.cs")]
public class Gen_Builtins : HasNamespace
{
    public static MethodInfo[] magicMethods = CodeGenConfig.MagicMethods;
    public HashSet<string> RequiredNamespace { get; } = new HashSet<string>();
    public Gen_Builtins()
    {
    }

    void HasNamespace.Generate(Action<string> write)
    {
        var entry = typeof(Builtins);
        RequiredNamespace.Add(entry.Namespace);
        RequiredNamespace.Add(typeof(Initialization).Namespace);
        RequiredNamespace.Add(typeof(TrSharpFunc).Namespace);

        IEnumerable<Doc> built_bindings()
        {
            foreach(var mi in entry.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (mi.Name.StartsWith('_'))
                    continue;
                if (mi.GetCustomAttribute<PyBuiltin>() == null)
                    continue;
                yield return $"{nameof(Initialization)}.Prelude({nameof(TrSharpFunc)}.FromFunc(\"{mi.Name}\", {entry.Name}.{mi.Name}));".Doc();
            }
        }
        RequiredNamespace.Select(x => $"using {x};\n").ForEach(write);
        var x = VSep(
            VSep(
                $"namespace {entry.Namespace}".Doc(),
                "{".Doc(),
                VSep(
                    "public static partial class".Doc() + entry.Name.Doc(),
                    "{".Doc(),
                        VSep(
                            " [Traffy.Annotations.Mark(Initialization.TokenBuiltinInit)]".Doc(),
                            "static void InitBuiltins()".Doc(),
                            "{".Doc(),
                            VSep(built_bindings().ToArray()) >> 4,
                            "}".Doc(),
                            NewLine
                        ).Indent(4),
                    "}".Doc()
                ).Indent(4),
                "}".Doc()));
        x.Render(write);
        write("\n");
    }
}