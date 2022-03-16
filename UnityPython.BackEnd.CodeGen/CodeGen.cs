using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using PrettyDoc;
using Traffy.Objects;
using static PrettyDoc.ExtPrettyDoc;
public static class ExtCodeGen
{
    public static Doc RefGen(this Type t, HasNamespace ctx)
    {
        if (t == typeof(void))
        {
            return "void".Doc();
        }
        t.Namespace?.By(ctx.AddNamepace);
        var eltype = t.GetElementType();
        if (eltype != null)
        {
            return eltype.RefGen(ctx) * "[]".Doc();
        }
        if (t.IsGenericType)
        {
            t.GetGenericArguments().ForEach(a => a.Namespace?.By(ctx.AddNamepace));

            var typeArgs =
                t.GenericTypeArguments
                .Select(x => x.RefGen(ctx))
                .Join(Comma);
            return t.Name.Substring(0, t.Name.IndexOf('`')).Doc() * typeArgs.SurroundedBy(Angle);
        }
        return t.Name.Doc();
    }

    public static string DefaultValueToStr(this object t)
    {
        if (t is string s)
        {
            return s.Escape();
        }
        if (t is char c)
            throw new NotImplementedException("char default value is not implemented yet");
        if (t is bool b)
            return b ? "true" : "false";
        return t.ToString();
    }
    public static Doc RefGen(this MethodInfo t, HasNamespace ctx)
    {
        var dt = t.DeclaringType;
        if (dt == null)
        {
            throw new Exception("MethodInfo must have a declaring type");
        }
        dt.Namespace?.By(ctx.AddNamepace);
        return $"{dt.Name}.{t.Name}".Doc();
    }

    public static Doc GenerateMethod(Doc retype, Doc name, (Doc name, Doc type)[] arguments, Doc[] body, bool Public = false, bool Static = false, bool Override = false)
    {
        var head_str = "";
        if (Public)
            head_str = "public " + head_str;
        if (Static)
            head_str = "static " + head_str;
        else if (Override)
            head_str = "override " + head_str;
        var head = (Public ? "public ".Doc() : Empty) * retype + name * "(".Doc() * arguments.Select(x => x.type + x.name).Join(Comma) * ")".Doc();
        return head * NewLine * VSep("{".Doc(),
            VSep(body).Indent(4),
        "}".Doc());
    }
}
public interface HasNamespace
{
    HashSet<string> RequiredNamespace { get; }
    public void AddNamepace(string ns)
    {
        RequiredNamespace.Add(ns);
    }
    void Generate(Action<string> write);
}

public class CodeGen : Attribute
{
    [AllowNull] public string Path;

    public static void GenerateAll()
    {

        var py_classes =
            Assembly
            .GetAssembly(typeof(TrObject))
            .GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && x.IsAssignableTo(typeof(TrObject)))
            .ToArray();

        Assembly
        .GetAssembly(typeof(CodeGen))
        .GetTypes()
        .Where(t => t.GetCustomAttribute<CodeGen>() != null && t.IsAssignableTo(typeof(HasNamespace)))
        .ForEach(cls =>
        {
            var attr_CodeGen = cls.GetCustomAttribute<CodeGen>();
            var ctors = cls.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            ConstructorInfo ctor;
            List<Func<(string Path, HasNamespace CodeGenerator)>> makers = new List<Func<(string, HasNamespace)>>();
            for (int i = 0; i < ctors.Length; i++)
            {
                ctor = ctors[i];
                if (ctor.GetParameters().Length == 0)
                {
                    var path = System.IO.Path.Join(
                        CodeGenConfig.RootDir,
                        attr_CodeGen.Path ?? cls.Name.ToLowerInvariant() + ".cs"
                    );
                    makers.Add(() => (path, (HasNamespace)ctor.Invoke(null)));
                    goto codegen;
                }
                if (ctor.GetParameters().Length == 1 && ctor.GetParameters()[0].ParameterType == typeof(Type))
                {
                    foreach (var t in py_classes)
                    {
                        var t_ = t;
                        var path = System.IO.Path.Join(
                            CodeGenConfig.RootDir,
                            attr_CodeGen.Path ?? "Parametric",
                            t.Name + ".cs"
                        );
                        makers.Add(() => (path, (HasNamespace)ctor.Invoke(new object[] { t_ })));
                    }
                    goto codegen;
                }
            }
            return;
        codegen:
            foreach (var maker in makers)
            {
                var (path, o) = maker();
                var dirPath = System.IO.Path.GetDirectoryName(path); // c# getdirectoryname is actually getting a path
                if (!System.IO.Directory.Exists(dirPath))
                {
                    System.IO.Directory.CreateDirectory(dirPath);
                }
                using (var file = System.IO.File.Open(path, System.IO.FileMode.Create))
                {
                    using (var writer = new System.IO.StreamWriter(file))
                    {
                        o.Generate(writer.Write);
                    }
                }
            }
        });
    }
}