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
    public static readonly TypeHierarchyComparer Inst = new TypeHierarchyComparer();
    public int Compare(Type x, Type y)
    {

        if (Gen_OrderedInit.GetMro(x).Contains(y))
        {
            return 1;
        }

        if (Gen_OrderedInit.GetMro(y).Contains(x))
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


    public static void TotalSort_Types(IList<Type> array)
    {
        int length = array.Count;
        if (length == 0)
            return;
        

        for (int i = 0; i < length; i++)
        {
            for (int j = i + 1; j < length; j++)
            {
                if (TypeHierarchyComparer.Inst.Compare(array[i], array[j]) > 0)
                {
                    var temp = array[i];

                    array[i] = array[j];

                    array[j] = temp;
                }
            }
        }
    }

    static Type[] _GetBases(Type t)
    {
        var inherit = t.GetCustomAttribute<PyInherit>();
        if (inherit == null)
            return Array.Empty<Type>();
        return inherit.Parents;
    }

    static IEnumerable<Type> GetBases(Type t)
    {
        if (t.GetCustomAttribute<AbstractClass>() != null)
            return _GetBases(t).Prepend(typeof(TrABC));
        return _GetBases(t);
    }

    public static IEnumerable<Type> GetMro(Type t)
    {
        return _GetParents(t).Append(typeof(TrRawObject));
    }
    static IEnumerable<Type> _GetParents(Type t)
    {
        yield return t;
        foreach (var each in _GetBases(t))
        {
            foreach (var parent in _GetParents(each))
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
        (this as HasNamespace).AddNamepace("Traffy.Objects");

        List<Doc> defs = new();

        List<(bool, string)> Fun_CrateRef = CodeGen.Func_CrateRef;
        var Func_ClassBasedCrateRef = CodeGen.Func_ClassBasedCrateRef;
        List<(bool, string)> Fun_InitRef = CodeGen.Fun_InitRef;
        var Fun_SetupRef = CodeGen.Fun_SetupRef;
        List<Type> classesToPrepare = new();

        Assembly
            .GetAssembly(typeof(TrObject))
            .GetTypes()
            .Select(t =>
            {
                if (t.GetCustomAttribute<PyBuiltin>() != null || t.GetCustomAttribute<AbstractClass>() != null)
                {
                    classesToPrepare.Add(t);
                    SuperClasses[t] = new HashSet<Type>();
                }
                return t;
            })
            .ToArray()
            .By(x =>
            {
                TotalSort_Types(classesToPrepare);
                return x;
            })
            .SelectMany(t => t.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).Distinct().Select(x => (t, x)))
            .ForEach(((Type t, MethodInfo mi) pair) =>
            {
                var attr = pair.mi.GetCustomAttribute<SetupMark>();
                if (attr == null)
                    return;
                var t = pair.t;
                var methodName = $"{t.Namespace}.{t.Name}.{pair.mi.Name}";
                switch (attr.Kind)
                {
                    case SetupMarkKind.CreateRef:
                        if (!classesToPrepare.Contains(t))
                        {
                            if (Fun_CrateRef.Contains((t.IsUnitySpecific(), methodName)))
                                throw new Exception("dup: " + methodName);
                            Fun_CrateRef.Add((t.IsUnitySpecific(), methodName));
                        }
                        else
                        {
                            if (Func_ClassBasedCrateRef.ContainsKey(t))
                                throw new Exception("dup (create): " + t);
                            Func_ClassBasedCrateRef[t] = methodName;
                        }
                        break;
                    case SetupMarkKind.SetupRef:
                        if (!classesToPrepare.Contains(t))
                            throw new Exception($"{t} is not a python class [AbstractClass] or [PyBuiltin].");
                        if (CodeGen.Fun_SetupRef.ContainsKey(t))
                            throw new Exception("dup(class setup): " + t.Namespace + "." + t.Name);
                        CodeGen.Fun_SetupRef[t] = methodName;
                        break;
                    case SetupMarkKind.InitRef:
                        if (Fun_InitRef.Contains((t.IsUnitySpecific(), methodName)))
                            throw new Exception("dup: " + methodName);
                        Fun_InitRef.Add((t.IsUnitySpecific(), methodName));
                        break;
                    default:
                        throw new Exception($"invalid kind {attr.Kind};");
                }
            });

        IEnumerable<Doc> gen_body()
        {

            yield return $"Traffy.Objects.TrClass.BeforeReInitRuntime();".Doc();
            foreach (var t in classesToPrepare)
            {
                if (Func_ClassBasedCrateRef.TryGetValue(t, out var m))
                {
                    if (t.IsUnitySpecific())
                    {
                        yield return "#if !NOT_UNITY".Doc();
                    }
                    yield return $"{m}();".Doc();
                    if (t.IsUnitySpecific())
                    {
                        yield return "#endif".Doc();
                    }
                }
            }

            foreach (var (IsUnitySpecific, m) in Fun_CrateRef)
            {
                if (!Func_ClassBasedCrateRef.ContainsValue(m))
                {
                    if (IsUnitySpecific)
                    {
                        yield return "#if !NOT_UNITY".Doc();
                    }
                    yield return $"{m}();".Doc();
                    if (IsUnitySpecific)
                    {
                        yield return "#endif".Doc();
                    }
                }
                
            }

            // set bases
            foreach (var t in classesToPrepare)
            {
                var bases = String.Join(",", GetBases(t).Select(x => $"{x.Namespace}.{x.Name}.CLASS"));
                if (t.IsUnitySpecific())
                    yield return "#if !NOT_UNITY".Doc();
                yield return $"{t.Namespace}.{t.Name}.CLASS.__base = new TrClass[] {{ {bases} }};".Doc();
                if (t.IsUnitySpecific())
                    yield return "#endif".Doc();
            }

            
            // set mro
            foreach (var t in classesToPrepare)
            {
                if (t.IsUnitySpecific())
                    yield return "#if !NOT_UNITY".Doc();
                yield return $"{t}.CLASS.__mro = TrClass.C3Linearized({t}.CLASS);".Doc();
                if (t.IsUnitySpecific())
                    yield return "#endif".Doc();
            }

            foreach (var (IsUnitySpecific, m) in Fun_InitRef)
            {
                if (IsUnitySpecific)
                    yield return "#if !NOT_UNITY".Doc();
                yield return $"{m}();".Doc();
                if (IsUnitySpecific)
                    yield return "#endif".Doc();
            }

            foreach (var t in classesToPrepare)
            {
                if (CodeGen.Fun_SetupRef.TryGetValue(t, out var m))
                {
                    if (t.IsUnitySpecific())
                        yield return "#if !NOT_UNITY".Doc();
                    yield return $"{m}();".Doc();
                    if (t.IsUnitySpecific())
                        yield return "#endif".Doc();
                }
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