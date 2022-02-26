using Traffy.Annotations;
using PrettyDoc;
using static PrettyDoc.ExtPrettyDoc;
public class App
{
    public static void Main()
    {
        // See https://aka.ms/new-console-template for more information
        // var target_directory = @"../UnityPython.BackEnd/generated-src/";
        CodeGen.GenerateAll();
    }
}


