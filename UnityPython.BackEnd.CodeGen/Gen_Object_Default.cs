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
    MethodInfo[] magicMethods;
    public HashSet<string> RequiredNamespace {get; } = new HashSet<string>();
    public Gen_ObjectDefault()
    {
        magicMethods =
            typeof(Traffy.Objects.TrObject)
            .GetMethods(BindingFlags.Static | BindingFlags.Public)
            .Where(m => m.GetCustomAttribute<MagicMethod>() != null)
            .ToArray();
    }

    void HasNamespace.Generate(Action<string> write)
    {
        var entry = typeof(Traffy.Objects.TrObject);
        entry.Namespace?.By(((HasNamespace )this).AddNamepace);

        List<Doc> defs = new List<Doc>();
        foreach (var meth in magicMethods)
        {
            var o = meth.GetParameters().First();
            if (o.ParameterType != typeof(TrObject))
            {
                throw new Exception($"Magic method {meth.Name} must take a TrObject as first parameter, got {o.ParameterType}");
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
                "return".Doc() +
                    meth.RefGen(this)
                        * sig_Args.Select(x =>  x.name)
                                  .Prepend("this".Doc())
                                  .Join(Comma)
                                  .SurroundedBy(Parens)
                        * ";".Doc(),
            };
            defs.Add(GenerateMethod(meth.Name.Doc(), sig_Args, body));
        }

        RequiredNamespace.Select(x => $"using {x};\n").ForEach(write);
        write($"namespace {entry.Namespace}");
        write("{");
        var x = "public partial interface".Doc() + entry.Name.Doc() + VSep(
            "{".Doc(),
            defs.Join(NewLine).Indent(4),
            "}".Doc()
        );
        x.Render(write);
        write("}");
    }
}