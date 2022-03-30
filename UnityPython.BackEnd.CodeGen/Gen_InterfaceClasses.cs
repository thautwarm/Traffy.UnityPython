using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CSAST;
using PrettyDoc;
using Traffy;
using Traffy.Annotations;
using Traffy.Interfaces;
using Traffy.Objects;
using static ExtCodeGen;
using static Helper;
using static PrettyDoc.ExtPrettyDoc;

[CodeGen(Path = "Traffy.Interfaces/")]
public class Gen_InterfaceClasses : HasNamespace
{
    public HashSet<string> RequiredNamespace { get; } = new HashSet<string>();
    public Gen_InterfaceClasses()
    {
        RequiredNamespace.Clear();
    }

    public IEnumerable<(string, Doc[])> Generate()
    {
        yield return ("AbstractClasses.cs", GenerateDocument().ToArray());
    }
    IEnumerable<Doc> GenerateDocument()
    {
        var asm = typeof(AbstractClass).Assembly;
        List<Doc> defs = new();
        List<Doc> binding_defs = new();
        (this as HasNamespace).AddNamepace("System");
        (this as HasNamespace).AddNamepace("System.Collections.Generic");

        typeof(SetupMark).RefGen(this);
        typeof(TrObject).RefGen(this);
        var cls_ABC = typeof(TrABC);

        foreach (var each in asm.GetTypes())
        {
            // test static
            // https://stackoverflow.com/questions/4145072/how-to-tell-if-a-type-is-a-static-class
            if (each.IsAbstract && each.IsSealed)
            {
                var abc = each.GetCustomAttribute<AbstractClass>();
                if (abc == null)
                    continue;

                defs.AddRange(GenerateClass(each, binding_defs));
            }
        }

        RequiredNamespace.Remove(typeof(AbstractClass).Namespace);
        
        foreach(var use in RequiredNamespace.Select(x => $"using {x};"))
        {
            yield return use.Doc();
        }

        CodeGen.Fun_InitRef.Add($"{typeof(AbstractClass).Namespace}.{typeof(AbstractClass).Name}.generated_BindMethods");

        yield return VSep(
            VSep(
                $"namespace {typeof(AbstractClass).Namespace}".Doc(),
                "{".Doc(),
                VSep(
                    $"public partial class {nameof(AbstractClass)}".Doc(),
                    "{".Doc(),
                        VSep(
                            "internal static void generated_BindMethods()".Doc(),
                            "{".Doc(),
                                VSep(binding_defs.ToArray()).Indent(4),
                            "}".Doc()
                        ).Indent(4),
                    "}".Doc()
                ).Indent(4),
                VSep(
                    defs.ToArray()
                ).Indent(4),
                "}".Doc()));
        yield return NewLine;

    }

    IEnumerable<Doc> GenerateClass(Type t, List<Doc> binding_defs)
    {


        yield return $"public static partial class {t.Name}".Doc();
        yield return "{".Doc();

        CodeGen.Func_ClassBasedCrateRef[t] = $"{t.Namespace}.{t.Name}._Create";
        yield return "internal static void _Create()".Doc().Indent(4);
        yield return "{".Doc().Indent(4);
        yield return $"    CLASS = TrClass.CreateClass({t.Name.Escape()});".Doc().Indent(4);
        yield return "}".Doc().Indent(4);

        CodeGen.Fun_InitRef.Add($"{t.Namespace}.{t.Name}._Init");
        yield return "internal static void _Init()".Doc().Indent(4);
        yield return "{".Doc().Indent(4);
        yield return $"    CLASS[CLASS.ic__new] = TrABC.CLASS[TrABC.CLASS.ic__new];".Doc().Indent(4);
        yield return "}".Doc().Indent(4);
        yield return NewLine;

        
        CodeGen.Fun_SetupRef[t] = $"{t.Namespace}.{t.Name}._SetupClasses";
        yield return $"internal static void _SetupClasses()".Doc().Indent(4);
        yield return "{".Doc().Indent(4);
        // var base_args = String.Join(",", bases.Select(x => x.Namespace + "." + x.Name + ".CLASS").Prepend(typeof(TrABC) + ".CLASS"));
        // yield return $"    CLASS.__base = new TrClass[] {{ {base_args} }};".Doc().Indent(4);
        yield return $"    CLASS.SetupClass();".Doc().Indent(4);
        yield return $"    CLASS.IsFixed = true;".Doc().Indent(4);
        yield return "}".Doc().Indent(4);

        yield return $"public static TrClass CLASS;".Doc().Indent(4);
        foreach (var meth in t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
        {
            var isAbstract = meth.GetCustomAttribute<AbsMember>() != null;
            var isMixin = meth.GetCustomAttribute<MixinMember>() != null;
            if (isAbstract && isMixin)
            {
                throw new Exception($"{t.Name}.{meth.Name} cannot be both abstract and mixin.");
            }
            if (!isAbstract && !isMixin)
                continue;
            var methName = meth.Name;
            var retType = meth.ReturnType;
            var ps = meth.GetParameters().Select(x => (x.ParameterType, x.Name, x.DefaultValue)).ToArray();
            if (retType == typeof(TrObject)
                && ps.Length == 3
                && ps[0].ParameterType == typeof(TrObject)
                && ps[1].ParameterType == typeof(BList<TrObject>)
                && ps[2].ParameterType == typeof(Dictionary<TrObject, TrObject>))
            {
                binding_defs.Add($"{t.Namespace}.{t.Name}.CLASS[{methName.Escape()}] = {nameof(TrSharpFunc)}.FromFunc(\"{t.Name}.{methName}\", {t.Namespace}.{t.Name}.{methName.ValidName()});".Doc());
                continue;
            }
            var (nonDefaultArgCount, defaultArgCount) = countPositionalDefault(meth);
            var methExpr = new EType(t)[meth.Name.ValidName()];
            var cases = Enumerable
                    .Range(nonDefaultArgCount, defaultArgCount + 1)
                    .Select(n =>
                        new Case(
                            n,
                            CallFunc(retType, ps, n,
                                (args, kws) => new ECall(methExpr, args, kws)).ToArray()))
                    .ToArray();
            var localBindName = "__bind_" + methName;
            CSMethod cm;
            if (!isAbstract)
            {
                cm = CSMethod.PyMethod(localBindName, typeof(TrObject),
                    PYARGS["Count"].Switch(
                        cases.Append(new Case(null,
                            new SExpr(
                                new EArgcountError(PYARGS["Count"], methName, nonDefaultArgCount, nonDefaultArgCount + defaultArgCount)).SingletonArray()
                                )).ToArray()
                    ).SingletonArray(),
                    Public: true
                );
            }
            else
            {
                cm = CSMethod.PyMethod(localBindName, typeof(TrObject),
                    new SError(new EStr("cannot call abstract method " + t.Name + "." + meth.Name)).SingletonArray(),
                    Public: true
                );
            }

            yield return cm.Doc().Indent(4);

            binding_defs.Add($"{t.Namespace}.{t.Name}.CLASS[{methName.Escape()}] = TrSharpFunc.FromFunc({(t.Name + "." + methName).Escape()}, {t.Namespace}.{t.Name}.{localBindName});".Doc());
        }
        yield return "}".Doc();
    }
}