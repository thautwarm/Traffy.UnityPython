using System;
using System.Reflection;
using Traffy.Annotations;

public static class Utils
{
    public static T[] SingletonArray<T>(this T item)
    {
        return new T[] { item };
    }

    public static bool IsUnitySpecific(this Type t)
    {
        return t.GetCustomAttribute<UnitySpecific>() != null;
    }
}