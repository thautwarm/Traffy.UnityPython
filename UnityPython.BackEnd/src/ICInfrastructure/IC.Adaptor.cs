using System;
using System.Runtime.CompilerServices;
using Traffy.Objects;
namespace Traffy.InlineCache
{
    public partial class PolyIC
    {
        [MethodImpl(MethodImplOptionsCompat.Best)]
        public bool Read(TrObject self, out TrObject ob)
        {
            var cls = self as TrClass;
            if (null == (object)cls)
                return ReadInst(self, out ob);
            return ReadClass(cls, out ob);
        }


        public void Write(TrObject self, TrObject value)
        {
            if (self is TrClass cls)
            {
                WriteClass(cls, value);
                return;
            }
            WriteInst(self, value);
        }

        public static bool ReadClass(TrClass Class, string name, out TrObject value)
        {
            if (Class.LoadCachedShape_ReadClass(name, out var shape))
            {
                return ReadClass(Class, shape, out value);
            }
            value = null;
            return false;
        }
        public static bool ReadClass(TrClass Class, Shape shape, out TrObject value)
        {
            if ((object)shape == null)
            {
                value = null;
                return false;
            }

            switch (shape.Kind)
            {
                case AttributeKind.InstField:
                    throw new InvalidProgramException($"inline cache system failed: {shape.Name} is an instance field, not available to class {Class.Name}.");
                case AttributeKind.Property:
                    value = shape.Property;
                    return true;
                case AttributeKind.Method:
                    value = shape.MethodOrClassFieldOrClassMethod;
                    return true;
                case AttributeKind.ClassField:
                    value = shape.MethodOrClassFieldOrClassMethod;
                    return true;
                case AttributeKind.ClassMethod:
                    value = shape.MethodOrClassFieldOrClassMethod;
                    return true;
                default:
                    throw new System.Exception("unexpected kind");
            }
        }

        public static void WriteClass(TrClass Class, string s, TrObject value)
        {
            if (Class.IsFixed)
                throw new AttributeError(Class, MK.Str(s), $"class {Class.Name} has no attribute {s}");

            if (Class.LoadCachedShape_TryWriteClass(s, out var ad))
            {
                Class.UpdatePrototype();
                if (value is TrProperty prop)
                {
                    ad.Property = prop;
                    ad.Kind = AttributeKind.Property;
                    ad.MethodOrClassFieldOrClassMethod = null;
                    ad.Class = null;
                }
                else if (value is TrSharpFunc || value is TrFunc)
                {
                    ad.MethodOrClassFieldOrClassMethod = value;
                    ad.Kind = AttributeKind.Method;
                    ad.Class = null;
                    ad.Property = null;
                }
                else if (value is TrClassMethod classmethod)
                {
                    ad.Kind = AttributeKind.ClassMethod;
                    ad.MethodOrClassFieldOrClassMethod = classmethod;
                    ad.Class = Class;
                    ad.Property = null;
                }
                else if (value is TrStaticMethod staticmethod)
                {
                    ad.Kind = AttributeKind.ClassField;
                    ad.MethodOrClassFieldOrClassMethod = staticmethod;
                    ad.Class = null;
                    ad.Property = null;
                }
                else
                {
                    ad.Kind = AttributeKind.ClassField;
                    ad.MethodOrClassFieldOrClassMethod = value;
                    ad.Class = null;
                    ad.Property = null;
                }
                return;
            }

            Class.UpdatePrototype();

            Shape ad_;

            {
                if (value is TrProperty prop)
                {
                    ad_ = Shape.MKProperty(s.ToIntern(), property: prop);
                }
                else if (value is TrSharpFunc || value is TrFunc)
                {
                    ad_ = Shape.MKMethod(s.ToIntern(), method: value);
                }
                else if (value is TrClassMethod classmethod)
                {
                    ad_ = Shape.MKClassMethod(s.ToIntern(), Class, classmethod: classmethod.func);
                }
                else if (value is TrStaticMethod staticmethod)
                {
                    ad_ = Shape.MKClassField(s.ToIntern(), staticmethod.func);
                }
                else
                {
                    ad_ = Shape.MKClassField(s.ToIntern(), value);
                }
            }

            Class.__prototype__.Add(ad_.Name.Value, ad_);
        }

        public static bool ReadInst(TrObject self, string s, out TrObject ob)
        {
            if (!self.Class.LoadCachedShape_ReadInst(s, out var shape))
            {
                ob = null;
                return false;
            }
            if (shape == null)
            {
                ob = null;
                return false;
            }
            return self.ReadInst(shape, out ob);
        }

        public static void WriteInst(TrObject self, string s, TrObject value)
        {
            if (self.Class.LoadCachedShape_WriteInst(s, out var ad))
            {
                WriteInst(self, ad, value);
                return;
            }
            if (self.__array__ == null)
                throw new AttributeError(self, MK.Str(s), $"object {self.Class.Name} has no attribute {s} (immutable)");

            int index = self.Class.AddField(s);
            self.SetInstField(index, s, value);
        }

        public static void WriteInst(TrObject self, Shape shape, TrObject value)
        {
            if (self.__array__ == null || self.Class.IsFixed)
                throw new AttributeError(self, MK.IStr(shape.Name), $"object {self.Class.Name} has no attribute {shape.Name}");
            self.SetInstField(shape.FieldIndex, shape.Name.Value, value);
        }

    }
}