public static class Utils
{
    public static T[] SingletonArray<T>(this T item)
    {
        return new T[] { item };
    }
}