using System.Runtime.CompilerServices;

public static class MethodImplOptionsCompat
{
    public const MethodImplOptions AggressiveInlining = MethodImplOptions.AggressiveInlining;
#if UNITY_VERSION
    public const  MethodImplOptions Best = MethodImplOptions.AggressiveInlining;
#else
    public const MethodImplOptions Best = MethodImplOptions.AggressiveInlining;
#endif
}
