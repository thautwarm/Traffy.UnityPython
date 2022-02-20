using Traffy.Objects;
namespace Traffy.InlineCache
{
    public enum AttributeKind
    {
        Field = 0,
        Property = 1,
        Method = 2,
        ClassField = 3,
        ClassMethod = 4
    }
    public class Shape
    {
        public InternedString Name;
        public AttributeKind Kind;
        public int FieldIndex;
        public TrProperty Property;
        public TrObject MethodOrClassFieldOrClassMethod;
        public TrObject Class;


        public static Shape MKField(InternedString name, int index) => new Shape
        {
            Name = name,
            Kind = AttributeKind.Field,
            FieldIndex = index
        };

        public static Shape MKProperty(InternedString name, TrProperty property) => new Shape
        {
            Name = name,
            Kind = AttributeKind.Property,
            Property = property
        };

        public static Shape MKMethod(InternedString name, TrObject method) =>
        new Shape
        {
            Name = name,
            Kind = AttributeKind.Method,
            MethodOrClassFieldOrClassMethod = method
        };

        public static Shape MKClassField(InternedString name, TrObject classfield) => new Shape
        {
            Name = name,
            Kind = AttributeKind.ClassField,
            MethodOrClassFieldOrClassMethod = classfield
        };

        public static Shape MKClassMethod(InternedString name, TrClass cls, TrObject classmethod) => new Shape
        {
            Name = name,
            Kind = AttributeKind.ClassMethod,
            MethodOrClassFieldOrClassMethod = classmethod,
            Class = cls
        };
    }


}