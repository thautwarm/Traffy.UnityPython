using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using PrettyDoc;
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

    public static Doc GenerateMethod(Doc retype, Doc name, (Doc name, Doc type)[] arguments, Doc[] body)
    {
        var head = "public".Doc() + retype + name * "(".Doc() * arguments.Select(x => x.type + x.name).Join(Comma) * ")".Doc();
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

        Assembly
        .GetAssembly(typeof(CodeGen))
        .GetTypes()
        .Where(t => t.GetCustomAttribute<CodeGen>() != null && t.IsAssignableTo(typeof(HasNamespace)))
        .ForEach(cls =>
        {
            var attr_CodeGen = cls.GetCustomAttribute<CodeGen>();
            var o = (HasNamespace)System.Activator.CreateInstance(cls);
            var path =
                System.IO.Path.Join(
                    CodeGenConfig.RootDir,
                    attr_CodeGen.Path ?? cls.Name.ToLowerInvariant() + ".cs"
                );

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
        });
    }
}