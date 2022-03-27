using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CSAST;
using PrettyDoc;
using Traffy;
using Traffy.Annotations;
using Traffy.InlineCache;
using Traffy.Objects;
using static ExtCodeGen;
using static PrettyDoc.ExtPrettyDoc;
using static Helper;

[CodeGen(Path = "Traffy.Builtins/")]
public class Gen_Builtins : HasNamespace
{
    public static MethodInfo[] magicMethods = CodeGenConfig.MagicMethods;
    public HashSet<string> RequiredNamespace { get; } = new HashSet<string>();
    public Gen_Builtins()
    {
    }
    

    IEnumerable<Doc> GenerateDocument()
    {
        var entry = typeof(Builtins);
        RequiredNamespace.Add(entry.Namespace);
        RequiredNamespace.Add(typeof(Initialization).Namespace);
        RequiredNamespace.Add(typeof(TrSharpFunc).Namespace);

        (this as HasNamespace).AddNamepace("System");
        (this as HasNamespace).AddNamepace("System.Collections.Generic");

        List<Doc> defs = new();

        IEnumerable<Doc> built_bindings()
        {
            foreach(var mi in entry.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (mi.Name.StartsWith('_'))
                    continue;
                if (mi.GetCustomAttribute<PyBuiltin>() == null)
                    continue;
                var methName = mi.Name;
                var retType = mi.ReturnType;
                var hasReturn = retType != typeof(void);
                if (!hasReturn)
                throw new Exception("Method " + mi.Name + " has no return type");
                var methExpr = new EType(entry)[mi.Name];
                var ps = mi.GetParameters().Select(x => (x.ParameterType, x.Name, x.DefaultValue)).ToArray();
                if (retType == typeof(TrObject)
                    && ps.Length == 2
                    && ps[0].ParameterType == typeof(BList<TrObject>)
                    && ps[1].ParameterType == typeof(Dictionary<TrObject, TrObject>))
                {
                    yield return $"{nameof(Initialization)}.Prelude({nameof(TrSharpFunc)}.FromFunc(\"{mi.Name}\", {mi.Name}));".Doc();
                    continue;
                }
                var (nonDefaultArgCount, defaultArgCount) = countPositionalDefault(mi);
                var cases = Enumerable
                    .Range(nonDefaultArgCount, defaultArgCount + 1)
                    .Select(n =>
                        new Case(
                            n,
                            CallFunc(retType, ps, n,
                                (args, kws) => new ECall(methExpr, args, kws)).ToArray()))
                    .ToArray();

                var localBindName = "__bind_" + methName;
                var cm = CSMethod.PyMethod(localBindName, typeof(TrObject),
                    PYARGS["Count"].Switch(
                        cases.Append(new Case(null,
                            new SExpr(
                                new EArgcountError(PYARGS["Count"], methName, nonDefaultArgCount, nonDefaultArgCount + defaultArgCount)).SingletonArray()
                                )).ToArray()
                    ).SingletonArray()
                );
                yield return cm.Doc();
                yield return $"{nameof(Initialization)}.Prelude({nameof(TrSharpFunc)}.FromFunc(\"{mi.Name}\", {localBindName}));".Doc();
            }
        }
        foreach(var use in RequiredNamespace.Select(x => $"using {x};"))
        {
            yield return use.Doc();
        }
        
        CodeGen.Fun_InitRef.Add($"{entry.Namespace}.{entry.Name}.InitBuiltins");
        yield return VSep(
            VSep(
                $"namespace {entry.Namespace}".Doc(),
                "{".Doc(),
                VSep(
                    "public static partial class".Doc() + entry.Name.Doc(),
                    "{".Doc(),
                        VSep(
                            "static void InitBuiltins()".Doc(),
                            "{".Doc(),
                            VSep(built_bindings().ToArray()) >> 4,
                            "}".Doc(),
                            NewLine
                        ).Indent(4),
                    "}".Doc()
                ).Indent(4),
                "}".Doc()));
        
        yield return NewLine;
    }

    public IEnumerable<(string filename, Doc[] docoment)> Generate()
    {
        yield return ("Bindings.cs", GenerateDocument().ToArray());
    }
}