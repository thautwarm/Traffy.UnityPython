using System;
using System.Collections.Generic;
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

        public bool __getic__(PolyIC ic, out TrObject found) =>
            ic.ReadClass(this, out found);
        public void __setic__(PolyIC ic, TrObject value) =>
            ic.WriteClass(this, value);

        public bool __getic__(string s, out TrObject found) =>
            PolyIC.ReadClass(this, s, out found);

        public void __setic__(string s, TrObject value) =>
            PolyIC.WriteClass(this, s, value);

        public TrObject this[InternedString s]
        {
            get => __getic__(s.Value, out var value) ? value : null;
            set => __setic__(s.Value, value);
        }

        public TrObject this[PolyIC ic]
        {
            get => __getic__(ic, out var value) ? value : null;
            set => __setic__(ic.Name.Value, value);
        }

        public PolyIC
            ic__init, ic__new, ic__str, ic__repr, ic__next, ic__add, ic__sub,
            ic__mul, ic__matmul, ic__floordiv, ic__truediv, ic__mod, ic__pow,
            ic__bitand, ic__bitor, ic__bitxor, ic__lshift, ic__rshift, ic__hash,
            ic__call, ic__contains, ic__getitem, ic__setitem, ic__iter, ic__len,
            ic__eq, ic__ne, ic__lt, ic__le, ic__gt, ic__ge,
            ic__neg, ic__inv, ic__pos, ic__bool, ic__init_subclass,
            ic__getattr, ic__setattr;

        public void InitInlineCacheForMagicMethods()
        {
            ic__init = new PolyIC(MagicNames.i___init__);
            ic__new = new PolyIC(MagicNames.i___new__);
            ic__str = new PolyIC(MagicNames.i___str__);
            ic__repr = new PolyIC(MagicNames.i___repr__);
            ic__next = new PolyIC(MagicNames.i___next__);

            ic__add = new PolyIC(MagicNames.i___add__);
            ic__sub = new PolyIC(MagicNames.i___sub__);
            ic__mul = new PolyIC(MagicNames.i___mul__);
            ic__matmul = new PolyIC(MagicNames.i___matmul__);
            ic__floordiv = new PolyIC(MagicNames.i___floordiv__);
            ic__truediv = new PolyIC(MagicNames.i___truediv__);
            ic__mod = new PolyIC(MagicNames.i___mod__);
            ic__pow = new PolyIC(MagicNames.i___pow__);

            ic__bitand = new PolyIC(MagicNames.i___bitand__);
            ic__bitor = new PolyIC(MagicNames.i___bitor__);
            ic__bitxor = new PolyIC(MagicNames.i___bitxor__);

            ic__lshift = new PolyIC(MagicNames.i___lshift__);
            ic__rshift = new PolyIC(MagicNames.i___rshift__);

            ic__hash = new PolyIC(MagicNames.i___hash__);
            ic__call = new PolyIC(MagicNames.i___call__);
            ic__contains = new PolyIC(MagicNames.i___contains__);
            ic__getitem = new PolyIC(MagicNames.i___getitem__);
            ic__setitem = new PolyIC(MagicNames.i___setitem__);
            ic__getattr = new PolyIC(MagicNames.i___getattr__);
            ic__setattr = new PolyIC(MagicNames.i___setattr__);
            ic__iter = new PolyIC(MagicNames.i___iter__);
            ic__len = new PolyIC(MagicNames.i___len__);

            ic__eq = new PolyIC(MagicNames.i___eq__);
            ic__ne = new PolyIC(MagicNames.i___ne__);
            ic__lt = new PolyIC(MagicNames.i___lt__);
            ic__le = new PolyIC(MagicNames.i___le__);
            ic__gt = new PolyIC(MagicNames.i___gt__);
            ic__ge = new PolyIC(MagicNames.i___ge__);

            ic__neg = new PolyIC(MagicNames.i___neg__);
            ic__inv = new PolyIC(MagicNames.i___inv__);
            ic__pos = new PolyIC(MagicNames.i___pos__);
            ic__bool = new PolyIC(MagicNames.i___bool__);


            ic__init_subclass = new PolyIC(MagicNames.i___init_subclass__);
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
            if (__instance_fields__.Count == Initialization.OBJECT_SHAPE_MAX_FIELD)
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