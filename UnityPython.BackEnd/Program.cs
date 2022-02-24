using System;
using System.Collections.Generic;
using System.Linq;
using static JsonExt;
using Traffy;
using Traffy.Objects;
using System.Diagnostics;
using System.Threading;

public class App
{

     public static TrObject time()
    {
        return MK.Int(System.DateTime.Now.Ticks);
    }

    public static void Main(string[] argv)
    {
        if (argv.Length != 1)
        {
            Console.WriteLine("Usage: traffy <filepath> (takes only 1 argument as input path)");
            return;
        }
        Initialization.InitRuntime();
        var o = System.IO.File.ReadAllText(argv[0]);
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
        Initialization.Populate(d);
        try
        {
            x.Exec(d);
        }
        catch (Exception e)
        {
            var exc = RTS.exc_frombare(e);
            Console.WriteLine(exc.GetStackTrace());
            throw e;
        }

    }
}