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

[CodeGen(Path = "Traffy.Objects/Class.ClassInitHelpers.cs")]
public class Gen_Class_ClassInit : HasNamespace
{
    public static MethodInfo[] magicMethods = CodeGenConfig.MagicMethods;
    public HashSet<string> RequiredNamespace { get; } = new HashSet<string>();
    public Gen_Class_ClassInit()
    {
    }

    void HasNamespace.Generate(Action<string> write)
    {
        var entry = typeof(Traffy.Objects.TrClass);
        IEnumerable<Doc> raw_init_generator()
        {
            foreach (var meth in magicMethods)
            {
                if (meth.GetCustomAttribute<MagicMethod>().NonInstance)
                {
                    yield return $"cls[{nameof(MagicNames)}.i_{meth.Name}] = {nameof(TrStaticMethod)}.Bind(\"object.{meth.Name}\", {nameof(TrObject)}.{meth.Name});".Doc();
                    continue;
                }
                yield return $"cls[{nameof(MagicNames)}.i_{meth.Name}] = {nameof(TrSharpFunc)}.FromFunc(\"object.{meth.Name}\", {nameof(TrObject)}.{meth.Name});".Doc();
            }
        }

        IEnumerable<Doc> builtin_class_init_generator()
        {
            foreach (var meth in magicMethods)
            {
                if (meth.GetCustomAttribute<MagicMethod>().NonInstance)
                {
                    continue;
                }
                var args = Enumerable.Range(0, meth.GetParameters().Length - 1).Select(x => $"arg{x}".Doc()).ToArray();
                yield return $"cls[MagicNames.i_{meth.Name}] = {nameof(TrSharpFunc)}.FromFunc(cls.Name + \".{meth.Name}\", ({args.Prepend("self".Doc()).Join(Comma)}) => ((T)self).{meth.Name}({args.Join(Comma)}));".Doc();
            }
        }

        RequiredNamespace.Remove(entry.Namespace);
        RequiredNamespace.Select(x => $"using {x};\n").ForEach(write);
        var x = VSep(
            VSep(
                $"namespace {entry.Namespace}".Doc(),
                "{".Doc(),
                VSep(
                    "public partial class".Doc() + entry.Name.Doc(),
                    "{".Doc(),
                        VSep(
                            "static void RawClassInit(TrClass cls)".Doc(),
                            "{".Doc(),
                            VSep(raw_init_generator().ToArray()).Indent(4),
                            "}".Doc(),
                            NewLine,
                            "static void BuiltinClassInit<T>(TrClass cls) where T : TrObject".Doc(),
                            "{".Doc(),
                            VSep(builtin_class_init_generator().ToArray()).Indent(4),
                            "}".Doc()
                        ).Indent(4),
                    "}".Doc()
                ).Indent(4),
                "}".Doc()));
        x.Render(write);
        write("\n");
    }
}