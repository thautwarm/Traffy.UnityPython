
// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using static JsonExt;
using Traffy;
using Traffy.Asm;
using System.Reflection;
using Traffy.Objects;
using System.Diagnostics;
using System.Threading;

public class X
{
    public TrObject o;
    public int i;

    public static int f(int x)
    {
        return 1;
    }
}


public class PointClass
{
    public int X { get; set; }
    public int Y { get; set; }
    public PointClass(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class PointClassFinalized : PointClass
{
    public PointClassFinalized(int x, int y) : base(x, y)
    {
    }
    ~PointClassFinalized()
    {
        // added a finalizer to slow down the GC

    }
}

public struct PointStruct
{
    public int X { get; set; }
    public int Y { get; set; }
    public PointStruct(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Perf
{
    public const int Iterations = 1000000;

    public static void Test<T>(Func<T> f)
    {
        var sw = new Stopwatch();
        sw.Start();
        var xs = f();
        Console.WriteLine(xs.ToString() + " : " + sw.ElapsedMilliseconds + "ms");
    }

    public static PointClassFinalized[] MeasureTestA()
    {
        // access array elements
        var list = new PointClassFinalized[Iterations];
        for (int i = 0; i < Iterations; i++)
        {
            list[i] = new PointClassFinalized(i, i);
        }
        return list;
    }

    public static PointClass[] MeasureTestB()
    {
        // access array elements
        var list = new PointClass[Iterations];
        for (int i = 0; i < Iterations; i++)
        {
            list[i] = new PointClass(i, i);
        }
        return list;
    }

    public static PointStruct[] MeasureTestC()
    {
        // access array elements
        var list = new PointStruct[Iterations];
        for (int i = 0; i < Iterations; i++)
        {
            list[i] = new PointStruct(i, i);
        }
        return list;
    }
}
public class App
{

     public static TrObject time()
    {
        return MK.Int(System.DateTime.Now.Ticks);
    }

    // public static async MonoAsync<int> appendList(List<int> lst)
    // {
    //     for (int i = 0; i < 10000; i++)
    //     {
    //         lst.Add(i);
    //         var k = await MonoAsync.Yield(i);
    //     }
    //     return 1;
    // }

    // public static async MonoAsync<T[]> Gather<T>(MonoAsync<T>[] tasks)
    // {
    //     var res = new T[tasks.Length];
    //     var pool = new Queue<int>();
    //     for(int i = 0; i < tasks.Length; i++)
    //     {
    //         var task = tasks[i];
    //         if (task.IsCompleted)
    //         {
    //             res[i] = task.GetResult();
    //         }
    //         else
    //         {
    //             pool.Enqueue(i);
    //         }
    //     }
    //     while (pool.Count > 0)
    //     {
    //         var i = pool.Dequeue();
    //         var task = tasks[i];
    //         task.MoveNext();
    //         if (task.IsCompleted)
    //         {
    //             res[i] = task.GetResult();
    //         }
    //         else
    //         {
    //             pool.Enqueue(i);
    //         }
    //         await MonoAsync.Yield(0);
    //     }
    //     return res;
    // }

    // public static async MonoAsync<int> TaskPlus1()
    // {
    //     for (int i = 0; i < 10000; i++)
    //     {
    //         await MonoAsync.Yield(i);
    //     }
    //     return 1;
    // }

    //    public static IEnumerable<int> tt()
    // {
    //     for (int i = 0; i < 10000; i++)
    //     {
    //         yield return (i);
    //     }
    // }

    // public static async MonoAsync<int> TaskPlus2()
    // {
    //     await TaskPlus1();
    //     var x = (await TaskPlus1() + await TaskPlus1());
    //     return 1;
    // }

    public static void Main(string[] argv)
    {
        // var lst = new List<int>();
        // var task1 = appendList(lst);
        // Console.WriteLine(lst.Count);
        // Console.WriteLine("len " + lst.Count);
        // task1.MoveNext();
        // Console.WriteLine("len " + lst.Count);
        // Thread.Sleep(100);
        // Console.WriteLine("len " + lst.Count);
        // task1.MoveNext();
        // Console.WriteLine("len " + lst.Count);
        // lst = new List<int>();
        // task1 = appendList(lst);
        // var task2 = appendList(lst);
        // var task3 = appendList(lst);
        // var task = Gather(new MonoAsync<int>[] { task1, task2, task3 });
        // var xs = TaskPlus2().Enum().ToArray();
        // Console.WriteLine(xs.Length);

        // async Async<int[]> testme()
        // {
        //     return await task;
        // }
        // while (!task.IsCompleted)
        // {
        //     task.MoveNext(null);
        // }
        // task.MoveNext(null);
        // Thread.Sleep(20);
        // var tt = testme();
        // Console.WriteLine("len " + lst.Count);
        // var task2 = appendList(lst);
        // var task3 = appendList(lst);
        // foreach(var a in k)
        // {
        //     Console.WriteLine(a);
        // }

        // Perf.Test(Perf.MeasureTestA);
        // Perf.Test(Perf.MeasureTestB);
        // // Perf.Test(Perf.MeasureTestC);

        ModuleInit.InitRuntime();
        var o = System.IO.File.ReadAllText("c.json");
        var x = JsonParse<TrFuncPointer>(o);
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
        ModuleInit.Populate(d);
        x.Exec(d);

    }
}