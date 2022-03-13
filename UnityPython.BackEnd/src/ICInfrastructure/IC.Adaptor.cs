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
            return self.__getic__(this, out ob);
        }

        public void Write(TrObject self, TrObject value)
        {
            self.__setic__(this, value);
        }

        public static bool ReadClass_refl(TrClass Class, TrStr name, out TrObject value)
        {
            if (Class.LoadCachedShape_ReadClass(name.value, out var shape))
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

        public static void WriteClass_refl(TrClass Class, TrStr s, TrObject value)
        {
            if (Class.IsFixed)
                throw new AttributeError(Class, s, $"class {Class.Name} has no attribute {s}");

            if (Class.LoadCachedShape_TryWriteClass(s.value, out var ad))
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
            var attr = s.GetInternedString();

            {
                if (value is TrProperty prop)
                {
                    ad_ = Shape.MKProperty(attr, property: prop);
                }
                else if (value is TrSharpFunc || value is TrFunc)
                {
                    ad_ = Shape.MKMethod(attr, method: value);
                }
                else if (value is TrClassMethod classmethod)
                {
                    ad_ = Shape.MKClassMethod(attr, Class, classmethod: classmethod.func);
                }
                else if (value is TrStaticMethod staticmethod)
                {
                    ad_ = Shape.MKClassField(attr, staticmethod.func);
                }
                else
                {
                    ad_ = Shape.MKClassField(attr, value);
                }
            }

            Class.__prototype__.Add(ad_.Name.Value, ad_);
        }

        public static bool ReadInst_refl(TrObject self, TrStr s, out TrObject ob)
        {
            if (!self.Class.LoadCachedShape_ReadInst(s.value, out var shape))
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

        public static void WriteInst_refl(TrObject self, TrStr s, TrObject value)
        {
            if (self.Class.LoadCachedShape_WriteInst(s.value, out var ad))
            {
                WriteInst(self, ad, value);
                return;
            }
            if (self.__array__ == null)
                throw new AttributeError(self, s, $"object {self.Class.Name} has no attribute {s} (immutable)");

            int index = self.Class.AddField(s.value);
            self.SetInstField(index, s.value, value);
        }

        public static void WriteInst(TrObject self, Shape shape, TrObject value)
        {
            if (shape.Kind == AttributeKind.Property)
            {
                shape.Property.Set(self, value);
                return;
            }
            if (self.__array__ == null || self.Class.IsFixed)
                throw new AttributeError(self, MK.IStr(shape.Name), $"object {self.Class.Name} has no attribute {shape.Name}");
            self.SetInstField(shape.FieldIndex, shape.Name.Value, value);
        }

    }
}