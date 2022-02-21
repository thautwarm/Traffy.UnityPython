namespace Test2
{
    public abstract class Abs0
    {
        public abstract int M0();
    }

    public abstract class Abs1: Abs0
    {
        public override int M0()
        {
            return 1;
        }
    }
}