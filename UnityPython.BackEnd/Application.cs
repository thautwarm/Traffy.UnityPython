using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Traffy;
using Traffy.Objects;
using static Traffy.Objects.ExtMonoAsyn;
#if !NOT_UNITY
using UnityEngine;
#endif

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
        Initialization.Prelude("__builtin_capsule__", new TrCapsuleObject(typeof(void)));

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
    [Serializable]
    public class Options
    {
        public string[] IncludePythonModuleDirectories;
        public string EntryPoint;
        public string ProjectDir;
    }
    public static int Main(string[] args)
    {
        if (args.Length != 1)
            throw new ArgumentException("UnityPython program: Expected one argument");

        var arg = args[0].Trim();
        if (arg == "-h" || arg == "--help")
        {
            Console.WriteLine("Usage: UnityPython JSON_STRING");
            return 0;
        }
        var opts = SimpleJSON.JSON.Deserialize<Options>(arg);
        ModuleSystem.SetProjectDir(opts.ProjectDir);

        Initialization.InitRuntime();
        Initialization.Prelude("__builtin_capsule__", new TrCapsuleObject(typeof(void)));
        foreach(var includeDir in opts.IncludePythonModuleDirectories)
        {
            ModuleSystem.LoadDirectory(includeDir);
        }
        ModuleSystem.ImportModule(opts.EntryPoint);
        return 0;
    }
#endif
}