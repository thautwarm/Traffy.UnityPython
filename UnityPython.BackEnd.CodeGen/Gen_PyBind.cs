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
using CSAST;

[CodeGen(Path = "MethodBindings/")]
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
        CSExpr THint(Type t) => new EType((new TId("THint"))[t])["Unique"];
        CSExpr Unbox = (new EId("Unbox"))["Apply"];
        CSExpr Box = (new EId("Box"))["Apply"];

        List<Doc> defs = new List<Doc>();
        bool s_GenerateAny = false;

        (typeof(Mark)).RefGen(this);

        foreach (var meth in entry.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
        {
            var attr = meth.GetCustomAttribute<PyBind>();
            if (attr == null)
                continue;
            s_GenerateAny = true;
            var methName = attr.Name ?? meth.Name;
            var retType = meth.ReturnType;
            var hasReturn = retType != typeof(void);
            if (!hasReturn)
                throw new Exception("Method " + meth.Name + " has no return type");
            var defaultArgCount = meth.GetParameters().Count(x => x.DefaultValue != DBNull.Value);
            var args = new EId(CSExpr.ARGS);
            var arguments = meth.GetParameters().Select((x, i) =>
                Unbox.Call(THint(x.ParameterType), args[i])).ToArray();
            var methExpr = new EId(meth.Name);
            var cases = Enumerable.Range(arguments.Length - defaultArgCount, defaultArgCount + 1).Select(n =>
                    new Case(n, new EType(entry)[meth.Name].Call(arguments.Take(n).ToArray()))).ToArray();
            var localBindName = "__bind_" + methName;
            var cm = CSMethod.PyMethod(localBindName, retType,
                    args["Count"].Switch(
                        cases.Append(new Case(new EId("_"), new EArgcountError(args["Count"], arguments.Length - defaultArgCount, arguments.Length))).ToArray()
                    ).By(x => Box.Call(x))
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
            var hasReturn = retType != typeof(void);
            if (!hasReturn)
                throw new Exception("Method " + meth.Name + " has no return type");
            var defaultArgCount = meth.GetParameters().Count(x => x.DefaultValue != DBNull.Value);
            var args = new EId(CSExpr.ARGS);
            var arguments = meth.GetParameters().Select((x, i) => Unbox.Call(THint(x.ParameterType), args[i + 1])).ToArray();
            var self = args[0].Cast(entry);
            Case[] cases;
            var methNameSplit = meth.Name.Split('.');
            if (methNameSplit.Length != 1)
            {
                var realMethName = methNameSplit.Last();
                var declTypeName = methNameSplit.Take(methNameSplit.Length - 1).By(x => String.Join(".", x));
                var dcl = new TId(declTypeName);
                cases = Enumerable.Range(arguments.Length - defaultArgCount, defaultArgCount + 1).Select(n =>
                    new Case(n + 1, new ECast(self, dcl)[realMethName].Call(arguments.Take(n).ToArray()))).ToArray();
            }
            else
            {
                cases = Enumerable.Range(arguments.Length - defaultArgCount, defaultArgCount + 1).Select(n =>
                    new Case(n + 1, self[meth.Name].Call(arguments.Take(n).ToArray()))).ToArray();
            }
            var localBindName = "__bind_" + methName;
            var cm = CSMethod.PyMethod(localBindName, typeof(TrObject),
                    args["Count"].Switch(
                        cases.Append(new Case(new EId("_"), new EArgcountError(args["Count"], arguments.Length - defaultArgCount, arguments.Length))).ToArray()
                    ).By(x => Box.Call(x))
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
                throw new Exception("Method " + meth.Name + " has no return type");
            var arg = new EId("_arg").Cast(entry);
            var value = Unbox.Call(THint(retType), new EId("_value"));
            var prop_Reader = "__read_" + methName;
            var prop_Writer = "__write_" + methName;
            if (!meth.CanRead)
            {
                defs.Add($"Func<TrObject, TrObject> {prop_Reader} = null;".Doc());
            }
            else
            {
                var cm = new CSMethod(prop_Reader, retType, new[] { ("_arg", (CSType)typeof(TrObject)) }, arg[meth.Name].By(x => Box.Call(x)));
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
                    new[] { ("_arg", typeof(TrObject)), ("_value", (CSType)typeof(TrObject)) }, arg[meth.Name].Assign(value));
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