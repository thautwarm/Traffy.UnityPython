#if NETSTANDARD2_0
using System;

public static class _GlobalCompat
{
    public static string[] Split(this string str, string separator, int count, StringSplitOptions opt)
    {
        return str.Split(new[] { separator }, count, opt);
    }

    public static bool Contains(this string str, char value)
    {
        return str.Contains(new string(new char[] { value }));
    }
}
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
    public sealed class AsyncMethodBuilderAttribute : Attribute
    {
        public AsyncMethodBuilderAttribute(Type builderType) =>
            BuilderType = builderType;
        public Type BuilderType { get; }
    }
}

namespace System.Diagnostics.CodeAnalysis
{
    public class AllowNullAttribute : Attribute
    {

    }
    public class DisallowNullAttribute : Attribute
    {

    }
    public class NotNullAttribute : Attribute
    {

    }
}
#endif