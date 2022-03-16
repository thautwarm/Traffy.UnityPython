using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using PrettyDoc;
using Traffy.Annotations;
using Traffy.Objects;
using static ExtCodeGen;
using static PrettyDoc.ExtPrettyDoc;

[CodeGen(Path = "Traffy.Objects/Object.cs")]
public class Gen_ObjectDefault : HasNamespace
{
    public static MethodInfo[] magicMethods = CodeGenConfig.MagicMethods;
    public HashSet<string> RequiredNamespace { get; } = new HashSet<string>();
    public Gen_ObjectDefault()
    {
    }

    void HasNamespace.Generate(Action<string> write)
    {
        var entry = typeof(Traffy.Objects.TrObject);

        List<Doc> defs = new List<Doc>();
        foreach (var meth in magicMethods)
        {
            var o = meth.GetParameters().First();
            if (o.ParameterType != typeof(TrObject))
            {
                if (o.ParameterType == typeof(TrClass) && meth.GetCustomAttribute<MagicMethod>().NonInstance)
                {
                    continue;
                }
                throw new Exception($"Magic method {meth.Name} either takes a TrObject as first parameter, but got {o.ParameterType}; otherwise, it takes a TrClass as first parameter, and must be marked with [MagicMethod(NonInstance = true)]..");
            }
            var args = meth.GetParameters().Skip(1);

            (Doc name, Doc type)[] sig_Args = args.Select((x, i) =>
                {
                    i = i + 1;
                    var parName = x.Name ?? $"__arg{i}";
                    return (parName.Doc(), x.ParameterType.RefGen(this));
                }).ToArray();

            var body = new Doc[]
            {
                (meth.ReturnType == typeof(void) ? Empty : "return ".Doc()) *
                    meth.RefGen(this)
                        * sig_Args.Select(x =>  x.name)
                                  .Prepend("this".Doc())
                                  .Join(Comma)
                                  .SurroundedBy(Parens)
                        * ";".Doc(),
            };
            defs.Add(GenerateMethod(meth.ReturnType.RefGen(this), meth.Name.Doc(), sig_Args, body));
        }

        RequiredNamespace.Remove(entry.Namespace);
        RequiredNamespace.Select(x => $"using {x};\n").ForEach(write);
        var x = VSep(
            $"namespace {entry.Namespace}".Doc(),
            "{".Doc(),
            VSep(
                "public abstract partial class".Doc() + entry.Name.Doc(),
                "{".Doc(),
                defs.Join(NewLine).Indent(4),
                "}".Doc()
            ).Indent(4),
            "}".Doc()
        );
        x.Render(write);
        write("\n");
    }
}