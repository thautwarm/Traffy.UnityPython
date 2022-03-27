// using Traffy.Annotations;
// using PrettyDoc;
// using static PrettyDoc.ExtPrettyDoc;
// using System.Linq.Expressions;
using Traffy;
using Traffy.Objects;
// using System;
using System;
public class App
{
    public static T Throw<T> (string msg)
    {
        throw new Exception(msg);
    }
    public static void Main()
    {
        // See https://aka.ms/new-console-template for more information
        // var target_directory = @"../UnityPython.BackEnd/generated-src/";
        CodeGen.GenerateAll();
        
    }
}


