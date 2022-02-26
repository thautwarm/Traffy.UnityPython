using System;
using System.Collections.Generic;
using System.Linq;
using PrettyDoc;
using static PrettyDoc.ExtPrettyDoc;

[CodeGen(Path = "Traffy.Runtime/MagicNames.cs")]
public class Gen_MagicNames : HasNamespace
{
    public HashSet<string> RequiredNamespace { get; } = new HashSet<string>();
    public void Generate(Action<string> write)
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

        VSep(
            "using Traffy.Objects;".Doc(),
            "namespace Traffy".Doc(),
            "{".Doc(),
                VSep(
                    head,
                    "{".Doc(),
                    VSep(VSep(s_decls), VSep(i_decls)).Indent(4),
                    "}".Doc()
                ).Indent(4),
            "}".Doc()
        ).Render(write);
        write("\n");
    }
}