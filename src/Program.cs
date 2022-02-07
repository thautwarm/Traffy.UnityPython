
// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using static Traffy.JsonExt;
using Traffy;
using Traffy.IR;
using System.Reflection;

public class X
{
    public TrObject o;
    public int i;

    public static int f(int x)
    {
        return 1;
    }
}


public class App
{

     public static TrObject time()
    {
        return MK.Int(System.DateTime.Now.Ticks);
    }
    public static void Main(string[] argv)
    {
        InitSetup.ApplyInitialization();
        var o = System.IO.File.ReadAllText("c.json");
        var x = JsonParse<TrFuncPointer>(o);
        var d = RTS.baredict_create();
        d[MK.Str("print")] = TrSharpFunc.FromFunc((BList<TrObject> xs, Dictionary<TrObject, TrObject> kwargs) => {
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

        d[MK.Str("next")] = TrSharpFunc.FromFunc(x => x.__next__());
        d[MK.Str("dict")] = TrClass.DictClass;
        d[MK.Str("time")] = TrSharpFunc.FromFunc(time);
        d[MK.Str("list")] = TrClass.ListClass;
        d[MK.Str("len")] = TrSharpFunc.FromFunc(x => x.__len__());
        x.Exec(d);
        // Console.WriteLine(x);

        // BList<int> xs = new BList<int> { 1, 2, 3};
        // xs.AddLeft(0);
        // xs.AddLeft(-1);
        // xs.Insert(3, 99);
        // Console.WriteLine(xs);
        // foreach(var e in xs)
        // {
        //     Console.WriteLine(e + " " + xs.IndexOf(e));
        // }

        // var t = typeof(int);
        // var u = JsonParse<Traffy.TrObject>("{\"value\": \"x\", \"$type\": \"TrStr\"}");
    }
}