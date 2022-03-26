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
using static ExtCodeGen;
using static PrettyDoc.ExtPrettyDoc;
using static Helper;

[CodeGen(Path = "Traffy.MethodBindings/")]
public class Gen_PyBind : HasNamespace
{
    public HashSet<string> RequiredNamespace { get; } = new HashSet<string>();

    public Type entry;
    public Gen_PyBind(Type t)
    {
        entry = t;
        RequiredNamespace.Clear();
    }


    void HasNamespace.Generate(Action<string> write)
    {
        if (!entry.IsAssignableTo(typeof(TrObject)))
        {
            return;
        }
        (this as HasNamespace).AddNamepace("System");
        (this as HasNamespace).AddNamepace("System.Collections.Generic");

        List<Doc> defs = new();
        bool s_GenerateAny = false;

        typeof(Mark).RefGen(this);

        foreach (var meth in entry.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
        {
            var attr = meth.GetCustomAttribute<PyBind>();
            if (attr == null)
                continue;
            s_GenerateAny = true;
            var methName = attr.Name ?? meth.Name;
            var retType = meth.ReturnType;
            var ps = meth.GetParameters().Select(x => (x.ParameterType, x.Name, x.DefaultValue)).ToArray();
            var (nonDefaultArgCount, defaultArgCount) = countPositionalDefault(meth);
            var methExpr = new EType(entry)[meth.Name];
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
            defs.Add(cm.Doc());
            defs.Add($"CLASS[{methName.Escape()}] = TrStaticMethod.Bind(CLASS.Name + \".\" + {methName.Escape()}, {localBindName});".Doc());
        }


        foreach (var meth in entry.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            var attr = meth.GetCustomAttribute<PyBind>();
            if (attr == null)
                continue;
            s_GenerateAny = true;
            var methName = attr.Name ?? meth.Name;
            var retType = meth.ReturnType;
            var (nonDefaultArgCount, defaultArgCount) = countPositionalDefault(meth);
            var args = new EId(CSExpr.ARGS);
            var ps = meth.GetParameters()
                .Select(x => (x.ParameterType, x.Name, x.DefaultValue))
                .Prepend((entry, "__self", DBNull.Value))
                .ToArray();

            var methNameSplit = meth.Name.Split('.');
            Func<CSExpr, CSExpr> getMeth = self => self[meth.Name];
            if (methNameSplit.Length != 1)
            {
                var realMethName = methNameSplit.Last();
                var declTypeName = methNameSplit.Take(methNameSplit.Length - 1).By(x => String.Join(".", x));
                var dcl = new TId(declTypeName);
                getMeth = self => new ECast(self, dcl)[realMethName];
            }

            var cases = Enumerable
                    .Range(nonDefaultArgCount + 1, defaultArgCount + 1)
                    .Select(n =>
                        new Case(
                            n,
                            CallFunc(retType, ps, n,
                                (args, kws) => new ECall(getMeth(args[0]), args.Skip(1).ToArray(), kws)).ToArray()))
                    .ToArray();

            var localBindName = "__bind_" + methName;

            var cm = CSMethod.PyMethod(localBindName, typeof(TrObject),
                    PYARGS["Count"].Switch(
                        cases.Append(new Case(null,
                            new SExpr(
                                new EArgcountError(PYARGS["Count"], methName, nonDefaultArgCount + 1, nonDefaultArgCount + 1 + defaultArgCount)).SingletonArray()
                                )).ToArray()
                    ).SingletonArray()
                );
            defs.Add(cm.Doc());
            defs.Add($"CLASS[{methName.Escape()}] = TrSharpFunc.FromFunc({methName.Escape()}, {localBindName});".Doc());
        }

        foreach (var meth in entry.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            var attr = meth.GetCustomAttribute<PyBind>();
            if (attr == null)
                continue;
            s_GenerateAny = true;
            var methName = attr.Name ?? meth.Name;
            var retType = meth.PropertyType;
            var hasReturn = retType != typeof(void);
            if (!hasReturn)
                throw new Exception("property " + meth.Name + " has no return type");
            var arg = new EId("_arg").Cast(entry);
            var value = Helper.Unbox.Call(THint(retType), new EId("_value"));
            var prop_Reader = "__read_" + methName;
            var prop_Writer = "__write_" + methName;
            if (!meth.CanRead)
            {
                defs.Add($"Func<TrObject, TrObject> {prop_Reader} = null;".Doc());
            }
            else
            {
                var cm = new CSMethod(prop_Reader, retType,
                    new[] { ("_arg", (CSType)typeof(TrObject)) },
                    new SReturn(arg[meth.Name].By(x => Helper.Box.Call(x))).SingletonArray()
                );
                defs.Add(cm.Doc());
            }
            if (!meth.CanWrite)
            {
                defs.Add($"Action<TrObject, TrObject> {prop_Writer} = null;".Doc());
            }
            else
            {
                var cm = new CSMethod(
                    prop_Writer,
                    typeof(void),
                    new[] { ("_arg", typeof(TrObject)), ("_value", (CSType)typeof(TrObject)) },
                    new SExpr (arg[meth.Name].Assign(value)).SingletonArray()
                );
                defs.Add(cm.Doc());
            }
            defs.Add($"CLASS[{methName.Escape()}] = TrProperty.Create(CLASS.Name + \".{methName}\", {prop_Reader}, {prop_Writer});".Doc());
        }
        if (!s_GenerateAny)
            return;

        RequiredNamespace.Remove(entry.Namespace);
        RequiredNamespace.Select(x => $"using {x};\n").ForEach(write);
        var x = VSep(
            VSep(
                $"namespace {entry.Namespace}".Doc(),
                "{".Doc(),
                VSep(
                    "public sealed partial class".Doc() + entry.Name.Doc(),
                    "{".Doc(),
                        VSep(
                            $"[{nameof(Mark)}(Initialization.TokenBuiltinInit)]".Doc(),
                            "static void BindMethods()".Doc(),
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