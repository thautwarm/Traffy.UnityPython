using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Traffy.InlineCache;
namespace Traffy.Objects
{
    public partial class TrClass
    {

        public Dictionary<string, Shape> __prototype__ = new Dictionary<string, Shape>();

        public List<FieldShape> __instance_fields__ = new List<FieldShape>();

        public bool LoadCachedShape_WriteInst(string name, out Shape ad)
        {
            for (int i = 0; i < __instance_fields__.Count; i++)
            {
                var fieldshape = __instance_fields__[i];
                if (fieldshape.Get.Kind != AttributeKind.InstField)
                    throw new InvalidProgramException(
                        $"inline cache system failed: {fieldshape.Get.Name} should be an instance field cache.");

                if (fieldshape.Get.Name.Value == name)
                {
                    ad = fieldshape.Get;
                    return true;
                }
            }
            if (LoadCachedShape_ReadClass(name, out ad) && ad.Kind == AttributeKind.Property)
            {
                return true;
            }
            ad = null;
            return false;
        }

        public bool LoadCachedShape_ReadInst(string name, out Shape ad)
        {
            if (name != "__new__")
            {
                for (int i = 0; i < __instance_fields__.Count; i++)
                {
                    var fieldshape = __instance_fields__[i];
                    if (fieldshape.Get.Kind != AttributeKind.InstField)
                        throw new InvalidProgramException(
                            $"inline cache system failed: {fieldshape.Get.Name} should be an instance field cache.");

                    if (fieldshape.Get.Name.Value == name)
                    {
                        ad = fieldshape.Get;
                        return true;
                    }
                }
            }

            return LoadCachedShape_ReadClass(name, out ad);
        }

        public bool LoadCachedShape_ReadClass(string name, out Shape ad)
        {
            for (int i = 0; i < __mro.Length; i++)
            {
                var other_cls = __mro[i];
                if (other_cls.__prototype__.TryGetValue(name, out ad))
                {
                    if (ad.Kind == AttributeKind.InstField)
                    {
                        throw new InvalidProgramException("inline cache system failed: " + name + " is an instance field, not available to class " + Name);
                    }
                    return true;
                }
            }
            ad = null;
            return false;
        }

        public bool LoadCachedShape_TryWriteClass(string name, out Shape ad)
        {
            if (__prototype__.TryGetValue(name, out ad))
            {
                if (ad.Kind == AttributeKind.InstField)
                {
                    throw new InvalidProgramException("inline cache system failed: " + name + " is an instance field, not available to class " + Name);
                }
                return true;
            }
            return false;
        }

        public override bool __getic__(PolyIC ic, out TrObject found) =>
            ic.ICClass.ReadClass(this, out found);
        public override void __setic__(PolyIC ic, TrObject value) =>
            __setic_refl__(ic.attribute, value);

        public override bool __getic_refl__(TrStr s, out TrObject found) =>
            PolyIC.ReadClass_refl(this, s, out found);

        public override void __setic_refl__(TrStr s, TrObject value) =>
            PolyIC.WriteClass_refl(this, s, value);

        public TrObject this[InternedString s]
        {
            get => __getic_refl__(MK.IStr(s.Value), out var value) ? value : null;
            set => __setic_refl__(MK.IStr(s.Value), value);
        }

        public override TrObject this[string s]
        {
            get => __getic_refl__(MK.Str(s), out var value) ? value : null;
            set => __setic_refl__(MK.Str(s), value);
        }

        public override TrObject this[PolyIC ic]
        {
            get => __getic__(ic, out var value) ? value : null;
            set => __setic_refl__(MK.IStr(ic.Name), value);
        }

        public int AddField(string name) => AddFieldOrFind(name, out var _);

        public int AddFieldOrFind(string name, out FieldShape shape)
        {
            if (__instance_fields__.TryFindValue(x => x.Get.Name.Value == name, out shape))
            {
                if (shape.Get.Kind != AttributeKind.InstField)
                    throw new TypeError($"fatal: find a non-field shape in instance field areas of class {Name}.");
                return shape.Get.FieldIndex;
            }
            if (IsFixed)
                throw new TypeError($"{Name} class has no attribute {name} (immutable)");
            if (__instance_fields__.Count >= Initialization.OBJECT_SHAPE_MAX_FIELD)
            {
                throw new TypeError($"class {Name} cannot add a shape for field {name} (more than 255 fields)");
            }
            var Token = UpdatePrototype();
            var index = __instance_fields__.Count;
            shape = Shape.MKField(name.ToIntern(), index);
            __instance_fields__.Add(shape);
            return index;
        }

        public void AddProperty(string name, TrProperty prop)
        {
            if (__prototype__.TryGetValue(name, out var shape))
            {
                if (shape.Kind == AttributeKind.Property)
                    return;
                throw new TypeError($"{name} is already a {shape.Kind}");
            }
            var Token = UpdatePrototype();
            __prototype__[name] = Shape.MKProperty(name.ToIntern(), prop);
        }

        public void AddMethod(string name, TrObject meth)
        {
            if (__prototype__.TryGetValue(name, out var shape))
            {
                if (shape.Kind == AttributeKind.Method)
                    return;
                throw new TypeError($"{name} is already a {shape.Kind}");
            }
            var Token = UpdatePrototype();
            __prototype__[name] = Shape.MKMethod(name.ToIntern(), meth);
        }


    }
}