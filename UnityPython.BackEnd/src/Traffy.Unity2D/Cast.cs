using Traffy.Objects;

namespace Traffy
{
    public static partial class Cast
    {
        public static int ToInt(this TrObject self)
        {
            if (self is TrInt integer)
                return (int) integer.value;
            throw new TypeError($"Cannot cast {self.Class.Name} to int");
        }

        public static float ToFloat(this TrObject self)
        {
            if (self is TrInt integer)
                return integer.value;
            if (self is TrFloat floating)
                return floating.value;
            throw new TypeError($"Cannot cast {self.Class.Name} to float");
        }
    }
}