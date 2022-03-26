using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CSAST;
using PrettyDoc;
using Traffy;
using Traffy.Annotations;
using Traffy.Objects;
using Traffy.Interfaces;
using static ExtCodeGen;
using static PrettyDoc.ExtPrettyDoc;
using static Helper;

[CodeGen(Path = "Traffy.Interfaces.cs")]
public class Gen_InterfaceClasses : HasNamespace
{
    public HashSet<string> RequiredNamespace { get; } = new HashSet<string>();
    public Gen_InterfaceClasses()
    {
        RequiredNamespace.Clear();
    }


    void HasNamespace.Generate(Action<string> write)
    {
        var asm = typeof(AbstractClass).Assembly;
        List<Doc> defs = new();
        List<Doc> binding_defs = new();
        (this as HasNamespace).AddNamepace("System");
        (this as HasNamespace).AddNamepace("System.Collections.Generic");

        typeof(Mark).RefGen(this);
        typeof(TrObject).RefGen(this);

        foreach(var each in asm.GetTypes())
        {
            // test static
            // https://stackoverflow.com/questions/4145072/how-to-tell-if-a-type-is-a-static-class
            if (each.IsAbstract && each.IsSealed)
            {
                var abc = each.GetCustomAttribute<AbstractClass>();
                if (abc == null)
                    continue;
                var inheritances = abc.Parents;
                defs.AddRange(GenerateClass(each, inheritances, binding_defs));
            }
        }

        RequiredNamespace.Remove(typeof(AbstractClass).Namespace);
        RequiredNamespace.Select(x => $"using {x};\n").ForEach(write);

        var x = VSep(
            VSep(
                $"namespace {typeof(AbstractClass).Namespace}".Doc(),
                "{".Doc(),
                VSep(
                    $"public partial class {nameof(AbstractClass)}".Doc(),
                    "{".Doc(),
                        VSep(
                            $"[{nameof(Mark)}(Initialization.TokenBuiltinInit)]".Doc(),
                            "static void BindMethods()".Doc(),
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
        x.Render(write);
        write("\n");

    }

    IEnumerable<Doc> GenerateClass(Type t, Type[] bases, List<Doc> binding_defs)
    {

        
        yield return $"public static partial class {t.Name}".Doc();
        yield return "{".Doc();

        yield return "[Traffy.Annotations.Mark(Initialization.TokenClassInit)]".Doc().Indent(4);
        yield return "static void _Init()".Doc().Indent(4);
        yield return "{".Doc().Indent(4);
        var base_args = String.Join(",", bases.Select(x => x.Namespace + "." + x.Name + ".CLASS").Prepend(typeof(TrABC) + ".CLASS"));
        yield return $"    CLASS = TrClass.CreateClass({t.Name.Escape()}, {base_args});".Doc().Indent(4);
        yield return $"    TrClass.TypeDict[typeof({t.Name})] = CLASS;".Doc().Indent(4);
        yield return "}".Doc().Indent(4);

        yield return NewLine;

        yield return $"[Traffy.Annotations.Mark(typeof({t.Name}))]".Doc().Indent(4);
        yield return $"static void _SetupClasses()".Doc().Indent(4);
        yield return "{".Doc().Indent(4);
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
                binding_defs.Add($"{t.Namespace}.{t.Name}.CLASS[{methName.Escape()}] = {nameof(TrSharpFunc)}.FromFunc(\"{t.Name}.{methName}\", {methName});".Doc());
                continue;
            }
            var (nonDefaultArgCount, defaultArgCount) = countPositionalDefault(meth);
            var methExpr = new EType(t)[meth.Name];
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
            if (isAbstract)
            {
                cm = CSMethod.PyMethod(localBindName, typeof(TrObject),
                    PYARGS["Count"].Switch(
                        cases.Append(new Case(null,
                            new SExpr(
                                new EArgcountError(PYARGS["Count"], methName, nonDefaultArgCount, nonDefaultArgCount + defaultArgCount)).SingletonArray()
                                )).ToArray()
                    ).SingletonArray()
                );
            }
            else
            {
                cm = CSMethod.PyMethod(localBindName, typeof(TrObject),
                    new SError(new EStr("cannot call abstract method " + t.Name + "." + meth.Name)).SingletonArray()
                );
            }
            
            yield return cm.Doc().Indent(4);

            binding_defs.Add($"{t.Namespace}.{t.Name}.CLASS[{methName.Escape()}] = TrSharpFunc.FromFunc({(t.Name + "." + methName).Escape()}, {localBindName});".Doc());
        }        
        yield return "}".Doc();
    }
}