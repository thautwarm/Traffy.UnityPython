using System;
using System.Collections.Generic;
using System.Linq;
using Traffy;
using Traffy.Objects;
using System.Diagnostics;
using System.Threading;
using static Traffy.Objects.ExtMonoAsyn;

public class App
{

     public static TrObject time()
    {
        return MK.Int(System.DateTime.Now.Ticks);
    }

    // public static void Main()
    // {
    //     async MonoAsync<int> myyield0(int n)
    //     {
    //         await Yield(n);
    //         return n;
    //     }
    //     async MonoAsync<int> myyield(int n)
    //     {
    //         await Yield(n);
    //         return await myyield0(n);
    //     }
    //     async MonoAsync<int> simple(int n)
    //     {
    //         for(int i = 0; i < n; i++)
    //         {
    //             await Yield(await(myyield(i)));

    //         }
    //         return 9;
    //     }
    //     async MonoAsync<int> f(int n)
    //     {
    //         await Enumerable.Range(32, 10).YieldFrom();
    //         await simple(5);
    //         // for(int i = 0; i < n; i++)
    //         // {
    //         //     await Yield(i + 10);
    //         // }
    //         return 1;
    //     }

    //     async MonoAsync<int> uu()
    //     {
    //         Console.WriteLine("simple = " + await simple(20));
    //         // Console.WriteLine("f = " + await f(8));
    //         return 50;
    //     }
    //     var k = uu();
    //     int n = 0;
    //     while (k.MoveNext())
    //     {
    //         Console.WriteLine(k.m_Result + " <<" + n);
    //         k.m_Result = 5;
    //         n++;
    //     }
    // }
    public static int Main(string[] argv)
    {
        if (argv.Length != 1)
        {
            Console.WriteLine("Usage: traffy <filepath> (takes only 1 argument as input path)");
            return 1;
        }
        Initialization.InitRuntime();
        // Initialization.Prelude(TrSharpFunc.FromFunc("next", x => x.__next__()));
        Initialization.Prelude(TrSharpFunc.FromFunc("time", time));
        Initialization.Prelude(TrSharpFunc.FromFunc("len", x => x.__len__()));
        ModuleSystem.LoadDirectory("out");

        var cls = (TrObject) RTS.new_class("S", new TrObject[0], null);
        TrUserObjectBase res = (TrUserObjectBase) cls.Call();
        // Console.WriteLine("res " + res.__repr__());

        try
        {
            ModuleSystem.ImportModule(argv[0]);
        }
        catch (Exception e)
        {
            var exc = RTS.exc_frombare(e);
            Console.WriteLine(exc.GetStackTrace());
            return 1;
        }
        return 0;
    }
}