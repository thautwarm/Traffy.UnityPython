using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Traffy;
using Traffy.Objects;
using static Traffy.Objects.ExtMonoAsyn;

public class App
{
    public static TrObject time()
    {
        return MK.Int(System.DateTime.Now.Ticks);
    }
#if TESTS
    public static int Main(string[] argv)
    {
        Initialization.InitRuntime();
        Initialization.Prelude(TrSharpFunc.FromFunc("time", time));
        ModuleSystem.LoadDirectory(argv[0]);
        var test_modules = ModuleSystem.Modules.Keys.Where(x => x.Split(".").Last().StartsWith("test_")).ToList();
        try
        {
            foreach (var module_name in test_modules)
                ModuleSystem.ImportModule(module_name);
        }
        catch (Exception e)
        {
            var exc = RTS.exc_frombare(e);
            Console.WriteLine(exc.GetStackTrace());
            return 1;
        }
        return 0;
    }
#endif
}