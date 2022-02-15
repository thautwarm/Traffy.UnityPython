namespace Traffy
{
    public static class InitOrder
    {
        public const int InitClassObjects = 0;
        public const int InitIndependentlyClassFields = 1;
        public const int InitIndependentBuiltins = 2;
    }
}