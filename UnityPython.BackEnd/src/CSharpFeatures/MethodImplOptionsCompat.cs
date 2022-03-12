using System.Runtime.CompilerServices;

public static class MethodImplOptionsCompat
{
    public const MethodImplOptions AggressiveInlining = MethodImplOptions.AggressiveInlining;
#if NUNITY
    public const MethodImplOptions Best = MethodImplOptions.AggressiveInlining;
#else
    public const  MethodImplOptions Best = MethodImplOptions.AggressiveInlining;
#endif
}