using Traffy.Objects;

namespace Traffy.Unity2D
{
    public static class Conversion
    {
        public static int NumCastInt(this TrObject self)
        {
            if (self is TrInt integer)
                return (int) integer.value;
            throw new TypeError($"Cannot cast {self.Class.Name} to int");
        }

        public static float NumToFloat(this TrObject self)
        {
            if (self is TrInt integer)
                return integer.value;
            if (self is TrFloat floating)
                return floating.value;
            throw new TypeError($"Cannot cast {self.Class.Name} to float");
        }
    }
}