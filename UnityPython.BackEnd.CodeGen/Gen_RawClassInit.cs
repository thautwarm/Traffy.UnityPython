using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using PrettyDoc;
using Traffy;
using Traffy.Annotations;
using Traffy.Objects;
using static ExtCodeGen;
using static PrettyDoc.ExtPrettyDoc;

[CodeGen(Path = "Traffy.Objects/Class.RawClassInit.cs")]
public class Gen_Class_RawClassInit : HasNamespace
{
    public static MethodInfo[] magicMethods = CodeGenConfig.MagicMethods;
    public HashSet<string> RequiredNamespace { get; } = new HashSet<string>();
    public Gen_Class_RawClassInit()
    {
    }

    void HasNamespace.Generate(Action<string> write)
    {
        var entry = typeof(Traffy.Objects.TrClass);
        List<Doc> defs = new List<Doc>();
        foreach (var meth in magicMethods)
        {
            if (meth.GetCustomAttribute<MagicMethod>().NonInstance)
            {
                defs.Add($"cls[{nameof(MagicNames)}.i_{meth.Name}] = {nameof(TrStaticMethod)}.Bind(\"object.{meth.Name}\", {nameof(TrObject)}.{meth.Name});".Doc());
                continue;
            }
            defs.Add($"cls[{nameof(MagicNames)}.i_{meth.Name}] = {nameof(TrSharpFunc)}.FromFunc(\"object.{meth.Name}\", {nameof(TrObject)}.{meth.Name});".Doc());
        }
        RequiredNamespace.Remove(entry.Namespace);
        RequiredNamespace.Select(x => $"using {x};\n").ForEach(write);
        var x = VSep(
            VSep(
                $"namespace {entry.Namespace}".Doc(),
                "{".Doc(),
                VSep(
                    "public partial class".Doc() + entry.Name.Doc(),
                    "{".Doc(),
                        VSep(
                            "static void RawClassInit(TrClass cls)".Doc(),
                            "{".Doc(),
                            VSep(defs.ToArray()).Indent(4),
                            "}".Doc()
                        ).Indent(4),
                    "}".Doc()
                ).Indent(4),
                "}".Doc()));
        x.Render(write);
        write("\n");
    }
}