using System;
using System.Collections.Generic;
using System.Linq;
using PrettyDoc;
using static PrettyDoc.ExtPrettyDoc;

[CodeGen(Path = "Traffy.Runtime/")]
public class Gen_MagicNames : HasNamespace
{
    public HashSet<string> RequiredNamespace { get; } = new HashSet<string>();

    public IEnumerable<(string, Doc[])> Generate()
    {
        yield return ("MagicNames.cs", GenerateDocument().ToArray());
    }
    IEnumerable<Doc> GenerateDocument()
    {
        var head = "public static class MagicNames".Doc();
        var magicNames = CodeGenConfig.MagicMethods.Select(x => x.Name).ToArray();
        foreach (var x in magicNames)
        {
            if (!x.StartsWith("__") || !x.EndsWith("__"))
            {
                throw new Exception($"Magic method name {x} must start or end with __");
            }
        }
        var s_decls = magicNames.Select(x => $"public static TrStr s_{x.Substring(2, x.Length - 4)} = MK.Str(\"{x}\");".Doc()).ToArray();

        var i_decls = magicNames.Select(x => $"public static InternedString i_{x} = InternedString.FromString(\"{x}\");".Doc()).ToArray();

        var ALL = String.Join(", ", magicNames.Select(x => x.Escape()));

        yield return VSep(
            "using Traffy.Objects;".Doc(),
            "using System.Collections.Generic;".Doc(),
            "namespace Traffy".Doc(),
            "{".Doc(),
                VSep(
                    head,
                    "{".Doc(),
                    VSep(
                        VSep(s_decls),
                        VSep(i_decls),
                        $"public static HashSet<string> ALL = new string[] {{ {ALL} }}.ToHashSet();".Doc()).Indent(4),
                    "}".Doc()
                ).Indent(4),
            "}".Doc()
        );
        yield return NewLine;
    }
}