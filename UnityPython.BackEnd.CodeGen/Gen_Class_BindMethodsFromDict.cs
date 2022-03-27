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

[CodeGen(Path = "Traffy.Objects.Setup/")]
public class Gen_Class_BindMethodsFromDict : HasNamespace
{
    public static MethodInfo[] magicMethods = CodeGenConfig.MagicMethods;
    public HashSet<string> RequiredNamespace { get; } = new HashSet<string>();
    public Gen_Class_BindMethodsFromDict()
    {
    }

    public IEnumerable<(string, Doc[])> Generate()
    {
        yield return ("Class.BindMethodsFromDict.cs", GenerateDocument().ToArray());
    }

    IEnumerable<Doc> GenerateDocument()
    {
        var entry = typeof(Traffy.Objects.TrClass);
        List<Doc> defs = new List<Doc>();
        (this as HasNamespace).AddNamepace(typeof(Dictionary<TrObject, TrObject>).Namespace);
        (this as HasNamespace).AddNamepace(typeof(TrObject).Namespace);
        (this as HasNamespace).AddNamepace(typeof(MagicNames).Namespace);
        foreach (var meth in magicMethods)
        {
            var methdNameStripUnderscore = meth.Name.Substring(2, meth.Name.Length-4);
            defs.Add(
                $"if (cp_kwargs.TryPop({nameof(MagicNames)}.s_{methdNameStripUnderscore}, out var o_{methdNameStripUnderscore}))".Doc());
            defs.Add($"    this[{nameof(MagicNames)}.i_{meth.Name}] = o_{methdNameStripUnderscore};".Doc());
        }
        RequiredNamespace.Remove(entry.Namespace);
        foreach(var use in RequiredNamespace.Select(x => $"using {x};"))
        {
            yield return use.Doc();
        }
        yield return VSep(
            VSep(
                $"namespace {entry.Namespace}".Doc(),
                "{".Doc(),
                VSep(
                    "public partial class".Doc() + entry.Name.Doc(),
                    "{".Doc(),
                        VSep(
                            "void BindMethodsFromDict(Dictionary<TrObject, TrObject> cp_kwargs)".Doc(),
                            "{".Doc(),
                            VSep(defs.ToArray()).Indent(4),
                            "}".Doc()
                        ).Indent(4),
                    "}".Doc()
                ).Indent(4),
                "}".Doc()));
        
        yield return NewLine;
    }
}