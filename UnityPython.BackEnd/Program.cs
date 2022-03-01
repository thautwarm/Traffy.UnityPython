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
        var o = System.IO.File.ReadAllText(argv[0]);
        var x =  ModuleSpec.Parse(o);
        var d = RTS.baredict_create();
        d[MK.Str("print")] = TrSharpFunc.FromFunc("print", (BList<TrObject> xs, Dictionary<TrObject, TrObject> kwargs) => {
            var itr = xs.GetEnumerator();
            if (itr.MoveNext())
            {
                Console.Write(itr.Current.__str__());
                while (itr.MoveNext())
                {
                    Console.Write(" ");
                    Console.Write(itr.Current.__str__());
                }
            }
            Console.WriteLine();
            return MK.None();
        });

        d[MK.Str("next")] = TrSharpFunc.FromFunc("next", x => x.__next__());
        d[MK.Str("time")] = TrSharpFunc.FromFunc("time", time);
        d[MK.Str("len")] = TrSharpFunc.FromFunc("len", x => x.__len__());
        Initialization.Populate(d);

        var cls = (TrObject) RTS.new_class("S", new TrObject[0], null);
        TrUserObjectBase res = (TrUserObjectBase) cls.Call();
        // Console.WriteLine("res " + res.__repr__());

        try
        {
            x.Exec(d);
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