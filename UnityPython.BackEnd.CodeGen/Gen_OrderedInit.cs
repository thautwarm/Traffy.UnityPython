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
using Traffy.Interfaces;
using Traffy.Objects;
using static ExtCodeGen;
using static Helper;
using static PrettyDoc.ExtPrettyDoc;

public class TypeHierarchyComparer : IComparer<Type>
{
    public int Compare(Type x, Type y)
    {

        if (Gen_OrderedInit.GetParents(x).Contains(y))
        {
            return 1;
        }

        if (Gen_OrderedInit.GetParents(y).Contains(x))
        {
            return -1;
        }
        return 0;
    }
}
public class Gen_OrderedInit : HasNamespace
{
    public static MethodInfo[] magicMethods = CodeGenConfig.MagicMethods;
    public HashSet<string> RequiredNamespace { get; } = new HashSet<string>();
    public Gen_OrderedInit()
    {
    }

    static Type[] GetBases(Type t)
    {
        var inherit = t.GetCustomAttribute<PyInherit>();
        if (inherit == null)
            return Array.Empty<Type>();
        return inherit.Parents;
    }

    public static IEnumerable<Type> GetParents(Type t)
    {
        foreach (var each in GetBases(t))
        {
            yield return each;
            foreach (var parent in GetParents(each))
            {
                yield return parent;
            }
        }
    }


    internal Dictionary<Type, HashSet<Type>> SuperClasses = new Dictionary<Type, HashSet<Type>>();

    IEnumerable<Doc> GenerateDocument()
    {
        var entry = typeof(Initialization);

        (this as HasNamespace).AddNamepace("System");
        (this as HasNamespace).AddNamepace("System.Collections.Generic");

        List<Doc> defs = new();

        List<string> Fun_CrateRef = new();
        Dictionary<Type, string> Fun_SetupRef = new();
        List<string> Fun_InitRef = new();

        List<Type> classesToPrepare = new();

        Assembly
            .GetAssembly(typeof(TrObject))
            .GetTypes()
            .Select(t =>
            {
                if (t.GetCustomAttribute<PyBuiltin>() != null || t.GetCustomAttribute<AbstractClass>() != null) ;
                {
                    classesToPrepare.Add(t);
                    SuperClasses[t] = new HashSet<Type>();
                }
                return t;
            })
            .ToArray()
            .By(x =>
            {
                classesToPrepare.Sort(new TypeHierarchyComparer());
                return x;
            })
            .SelectMany(t => t.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).Select(x => (t, x)))
            .ForEach(((Type t, MethodInfo mi) pair) =>
            {
                var attr = pair.mi.GetCustomAttribute<SetupMark>();
                if (attr == null)
                    return;
                var t = pair.t;
                var methodName = $"{t.Namespace}.{t.Name}.{pair.t.Name}";
                switch (attr.Kind)
                {
                    case SetupMarkKind.CreateRef:
                        if (Fun_CrateRef.Contains(methodName))
                            throw new Exception("dup: " + methodName);
                        Fun_CrateRef.Add(methodName);
                        break;
                    case SetupMarkKind.SetupRef:
                        if (!classesToPrepare.Contains(t))
                            throw new Exception($"{t} is not a python class [AbstractClass] or [PyBuiltin].");
                        if (Fun_SetupRef.ContainsKey(t))
                            throw new Exception("dup(class setup): " + t.Namespace + "." + t.Name);
                        Fun_SetupRef[t] = methodName;
                        break;
                    case SetupMarkKind.InitRef:
                        if (Fun_InitRef.Contains(methodName))
                            throw new Exception("dup: " + methodName);
                        Fun_InitRef.Add(methodName);
                        break;
                    default:
                        throw new Exception($"invalid kind {attr.Kind};");
                }
            });

        IEnumerable<Doc> gen_body()
        {

            foreach (var m in Fun_CrateRef)
            {
                yield return $"{m}();".Doc();
            }

            foreach (var t in classesToPrepare)
            {
                yield return $"TrClass.C3Linearized({t}.CLASS);".Doc();
            }

            foreach (var m in Fun_InitRef)
            {
                yield return $"{m}();".Doc();
            }

            foreach (var m in CodeGen.Fun_InitRef)
            {
                yield return $"{m}();".Doc();
            }

            foreach (var t in classesToPrepare)
            {
                var m = Fun_SetupRef[t];
                yield return $"{m}();".Doc();
            }
        }

        foreach (var use in RequiredNamespace.Select(x => $"using {x};"))
        {
            yield return use.Doc();
        }

        yield return VSep(
            VSep(
                $"namespace {entry.Namespace}".Doc(),
                "{".Doc(),
                VSep(
                    "public static partial class".Doc() + entry.Name.Doc(),
                    "{".Doc(),
                        VSep(
                            "public static void InitRuntime()".Doc(),
                            "{".Doc(),
                            VSep(gen_body().ToArray()) >> 4,
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
        yield return ("Initialization.cs", GenerateDocument().ToArray());
    }
}