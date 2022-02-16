namespace Traffy
{
    public static class InitOrder
    {
        public const int InitMeta = 0;
        public const int InitClassObjects = 1;
        public const int SetupClassObjects = 2;
        public const int InitIndependentlyClassFields = 3;
        public const int InitIndependentBuiltins = 4;
    }
}