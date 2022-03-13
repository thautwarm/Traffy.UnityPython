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

        (this as HasNamespace).AddNamepace("System");
        (this as HasNamespace).AddNamepace("System.Collections.Generic");

        CSExpr THint(Type t) => new EType((new TId("THint"))[t])["Unique"];
        CSExpr Unbox = (new EId("Unbox"))["Apply"];
        CSExpr Box = (new EId("Box"))["Apply"];

        List<Doc> defs = new List<Doc>();

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
                CSMethod cm;
                var pars = mi.GetParameters();
                if (retType == typeof(TrObject)
                    && pars.Length == 2
                    && pars[0].ParameterType == typeof(BList<TrObject>)
                    && pars[1].ParameterType == typeof(Dictionary<TrObject, TrObject>))
                {
                    yield return $"{nameof(Initialization)}.Prelude({nameof(TrSharpFunc)}.FromFunc(\"{mi.Name}\", {mi.Name}));".Doc();
                    continue;
                }
                var defaultArgCount = mi.GetParameters().Count(x => x.DefaultValue != DBNull.Value);
                var args = new EId(CSExpr.ARGS);
                var arguments = mi.GetParameters().Select((x, i) =>
                    Unbox.Call(THint(x.ParameterType), args[i])).ToArray();
                var cases = Enumerable.Range(arguments.Length - defaultArgCount, defaultArgCount + 1).Select(n =>
                    new Case(n, new EType(entry)[mi.Name].Call(arguments.Take(n).ToArray()))).ToArray();
                var localBindName = "__bind_" + methName;
                cm = CSMethod.PyMethod(localBindName, typeof(TrObject),
                    args["Count"].Switch(
                        cases.Append(new Case(new EId("_"), new EArgcountError(args["Count"], arguments.Length - defaultArgCount, arguments.Length))).ToArray()
                    ).By(x => Box.Call(x))
                );
                yield return cm.Doc();
                yield return $"{nameof(Initialization)}.Prelude({nameof(TrSharpFunc)}.FromFunc(\"{mi.Name}\", {localBindName}));".Doc();
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