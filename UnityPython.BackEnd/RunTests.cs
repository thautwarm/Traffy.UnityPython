using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Traffy;
using Traffy.Objects;
using static Traffy.Objects.ExtMonoAsyn;
#if UNITY_VERSION
using UnityEngine;
#endif


public abstract class X
{
    public virtual int f()
    {
        return 1;
    }
}

public class Y : X
{
}
public class App
{
    public static TrObject time()
    {
        return MK.Int(System.DateTime.Now.Ticks);
    }
#if TESTS
    public static int Main(string[] argv)
    {
        // return 0;
        Initialization.InitRuntime();
        Initialization.Prelude(TrSharpFunc.FromFunc("time", time));

        ModuleSystem.LoadDirectory(argv.Length == 0 ? "out" : argv[0]);
        var test_modules = ModuleSystem.Modules.Keys.Where(x => x.Split(".").Last().StartsWith("test_")).ToList();
        try
        {
            foreach (var module_name in test_modules)
                ModuleSystem.ImportModule(module_name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            return 1;
        }
        return 0;
    }
#else
    public static int Main()
    {
        foreach(var mi in typeof(Y).GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
        {
            var mf = mi.GetBaseDefinition();
            Console.WriteLine($"{mi.Name} {mf.Name}");
        }
        return 0;
    }
#endif
}