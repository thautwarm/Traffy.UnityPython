using System.Linq;
using System.Reflection;
using Traffy.Annotations;

public static class CodeGenConfig
{
    public const string RootDir =  @"../UnityPython.BackEnd/generated-src/";
    public static MethodInfo[] MagicMethods = typeof(Traffy.Objects.TrObject)
            .GetMethods(BindingFlags.Static | BindingFlags.Public)
            .Where(m => m.GetCustomAttribute<MagicMethod>() != null)
            .ToArray();
    
}