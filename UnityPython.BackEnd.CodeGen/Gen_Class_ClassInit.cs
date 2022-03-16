using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using PrettyDoc;
using Traffy;
using Traffy.Annotations;
using Traffy.InlineCache;
using Traffy.Objects;
using static ExtCodeGen;
using static PrettyDoc.ExtPrettyDoc;

[CodeGen(Path = "Traffy.Objects/Class.ClassInitHelpers.cs")]
public class Gen_Class_ClassInit : HasNamespace
{

    internal static bool IsOwned(MethodInfo mi, Type me)
    {
        if (mi.DeclaringType != mi.GetBaseDefinition().DeclaringType && mi.DeclaringType == me)
        {
            return true;
        }
        return false;
    }

    internal static HashSet<string> magicNames = null;
    internal static HashSet<string> GetInterfaceMethodSource(Type t)
    {
        if (magicNames == null)
            magicNames = magicMethods.Select(x => x.Name).ToHashSet();
        var owned = new HashSet<string>();
        foreach(var mi in t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            if (magicNames.Contains(mi.Name) && IsOwned(mi, t))
            {
                owned.Add(mi.Name);
            }
        }
        return owned;
        // var interface_t = t.GetInterfaceMap(typeof(Traffy.Objects.TrObject));
        // var res = new HashSet<string>();
        // for(int i = 0; i < interface_t.TargetMethods.Length; i++)
        // {
        //     var targetMethod = interface_t.TargetMethods[i];
        //     var interfaceMethod = interface_t.InterfaceMethods[i];
        //     var methName = interfaceMethod.Name;
        //     if (Traffy.MagicNames.ALL.Contains(methName) && interfaceMethod != targetMethod)
        //         res.Add(methName);
        // }
        // return res;
    }
    public static MethodInfo[] magicMethods = CodeGenConfig.MagicMethods;
    public HashSet<string> RequiredNamespace { get; } = new HashSet<string>();
    public Gen_Class_ClassInit()
    {
    }
    void HasNamespace.Generate(Action<string> write)
    {
        var entry = typeof(Traffy.Objects.TrClass);
        RequiredNamespace.Add(typeof(PolyIC).Namespace);

        IEnumerable<Doc> raw_init_ic_fields()
        {
            foreach (var each in magicMethods.Select(x => x.Name.Substring(2, x.Name.Length - 4)))
            {
                yield return $"public {nameof(PolyIC)} ic__{each} = new {nameof(PolyIC)}({nameof(MagicNames)}.i___{each}__);".Doc();
            }
        }

        IEnumerable<Doc> raw_init_generator()
        {
            foreach (var meth in magicMethods)
            {
                var mm = meth.GetCustomAttribute<MagicMethod>();
                if (!mm.Default)
                {
                    continue;
                }
                if (mm.NonInstance)
                {
                    yield return $"cls[{nameof(MagicNames)}.i_{meth.Name}] = {nameof(TrStaticMethod)}.Bind(\"object.{meth.Name}\", {nameof(TrObject)}.{meth.Name});".Doc();
                    continue;
                }
                yield return $"cls[{nameof(MagicNames)}.i_{meth.Name}] = {nameof(TrSharpFunc)}.FromFunc(\"object.{meth.Name}\", {nameof(TrObject)}.{meth.Name});".Doc();
            }
        }

        Type[] builtinPyClasses = typeof(TrObject).Assembly.GetTypes().Where(x =>
            x.GetCustomAttribute<Traffy.Annotations.PyBuiltin>() != null
            && x.IsClass && !x.IsAbstract && x.IsAssignableTo(typeof(TrObject))).ToArray();

        IEnumerable<Doc> builtin_class_init_generator_foreach(Type builtinPyClass)
        {
            yield return $"static void BuiltinClassInit_{builtinPyClass.Name}(TrClass cls)".Doc();
            yield return "{".Doc();
            var owned = GetInterfaceMethodSource(builtinPyClass);
            foreach (var meth in magicMethods)
            {
                if (meth.GetCustomAttribute<MagicMethod>().NonInstance)
                {
                    continue;
                }
                if (!owned.Contains(meth.Name))
                    continue;

                var args = Enumerable.Range(0, meth.GetParameters().Length - 1).Select(x => $"arg{x}".Doc()).ToArray();
                yield return $"cls[MagicNames.i_{meth.Name}] = {nameof(TrSharpFunc)}.FromFunc(cls.Name + \".{meth.Name}\", ({args.Prepend("self".Doc()).Join(Comma)}) => (({builtinPyClass.FullName})self).{meth.Name}({args.Join(Comma)}));".Doc() >> 4;
            }
            yield return "}".Doc();
        }

        IEnumerable<Doc> builtin_class_init_generator()
        {
            foreach(var t in builtinPyClasses)
            {
                yield return $"if (typeof(T) == typeof({t.FullName}))".Doc();
                yield return "{".Doc();
                yield return $"BuiltinClassInit_{t.Name}(cls);".Doc() >> 4;
                yield return $"return;".Doc() >> 4;
                yield return "}".Doc();
            }
            yield return $"throw new System.Exception(\"Unsupported type: \" + typeof(T).FullName);".Doc();
        }

        IEnumerable<Doc> init_ic()
        {
            foreach (var each in magicMethods.Select(x => x.Name.Substring(2, x.Name.Length - 4)))
            {
                yield return $"ic__{each} = new {nameof(PolyIC)}({nameof(MagicNames)}.i___{each}__);".Doc();
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
                            VSep(raw_init_ic_fields().ToArray()),
                            "static void RawClassInit(TrClass cls)".Doc(),
                            "{".Doc(),
                            VSep(raw_init_generator().ToArray()).Indent(4),
                            "}".Doc(),
                            NewLine,
                            VSep(builtinPyClasses.SelectMany(builtin_class_init_generator_foreach).ToArray()),
                            "static void BuiltinClassInit<T>(TrClass cls) where T : TrObject".Doc(),
                            "{".Doc(),
                            VSep(builtin_class_init_generator().ToArray()).Indent(4),
                            "}".Doc(),
                            "public void InitInlineCacheForMagicMethods()".Doc(),
                            "{".Doc(),
                            VSep(init_ic().ToArray()).Indent(4),
                            "}".Doc()
                        ).Indent(4),
                    "}".Doc()
                ).Indent(4),
                "}".Doc()));
        x.Render(write);
        write("\n");
    }
}