using System;
using Traffy.InlineCache;
namespace Traffy.Objects
{
    public partial class TrClass
    {

        public bool __getic__(IC ic, out TrObject found) =>
            ic.ReadClass(this, out found);
        public void __setic__(IC ic, TrObject value) =>
            ic.WriteClass(this, value);

        public bool __getic__(string s, out TrObject found) =>
            IC.ReadClass(this, s, out found);

        public void __setic__(string s, TrObject value) =>
            IC.WriteClass(this, s, value);

        public TrObject this[InternedString s]
        {
            get => __getic__(s, out var value) ? value : null;
            set => __setic__(s, value);
        }

        public TrObject this[IC ic]
        {
            get => __getic__(ic, out var value) ? value : null;
            set => __setic__(ic.Name, value);
        }

        public MonoIC
            ic__init, ic__new, ic__str, ic__repr, ic__next, ic__add, ic__sub,
            ic__mul, ic__matmul, ic__floordiv, ic__truediv, ic__mod, ic__pow,
            ic__bitand, ic__bitor, ic__bitxor, ic__lshift, ic__rshift, ic__hash,
            ic__call, ic__contains, ic__getitem, ic__setitem, ic__iter, ic__len,
            ic__eq, ic__lt, ic__neg, ic__inv, ic__pos, ic__bool, ic__init_subclass,
            ic__getattr, ic__setattr;

        public void InitInlineCacheForMagicMethods()
        {
            ic__init = new MonoIC(MagicNames.i___init__);
            ic__new = new MonoIC(MagicNames.i___new__);
            ic__str = new MonoIC(MagicNames.i___str__);
            ic__repr = new MonoIC(MagicNames.i___repr__);
            ic__next = new MonoIC(MagicNames.i___next__);

            ic__add = new MonoIC(MagicNames.i___add__);
            ic__sub = new MonoIC(MagicNames.i___sub__);
            ic__mul = new MonoIC(MagicNames.i___mul__);
            ic__matmul = new MonoIC(MagicNames.i___matmul__);
            ic__floordiv = new MonoIC(MagicNames.i___floordiv__);
            ic__truediv = new MonoIC(MagicNames.i___truediv__);
            ic__mod = new MonoIC(MagicNames.i___mod__);
            ic__pow = new MonoIC(MagicNames.i___pow__);

            ic__bitand = new MonoIC(MagicNames.i___bitand__);
            ic__bitor = new MonoIC(MagicNames.i___bitor__);
            ic__bitxor = new MonoIC(MagicNames.i___bitxor__);

            ic__lshift = new MonoIC(MagicNames.i___lshift__);
            ic__rshift = new MonoIC(MagicNames.i___rshift__);

            ic__hash = new MonoIC(MagicNames.i___hash__);
            ic__call = new MonoIC(MagicNames.i___call__);
            ic__contains = new MonoIC(MagicNames.i___contains__);
            ic__getitem = new MonoIC(MagicNames.i___getitem__);
            ic__setitem = new MonoIC(MagicNames.i___setitem__);
            ic__getattr = new MonoIC(MagicNames.i___getattr__);
            ic__setattr = new MonoIC(MagicNames.i___setattr__);
            ic__iter = new MonoIC(MagicNames.i___iter__);
            ic__len = new MonoIC(MagicNames.i___len__);

            ic__eq = new MonoIC(MagicNames.i___eq__);
            ic__lt = new MonoIC(MagicNames.i___lt__);

            ic__neg = new MonoIC(MagicNames.i___neg__);
            ic__inv = new MonoIC(MagicNames.i___inv__);
            ic__pos = new MonoIC(MagicNames.i___pos__);
            ic__bool = new MonoIC(MagicNames.i___bool__);


            ic__init_subclass = new MonoIC(MagicNames.i___init_subclass__);
        }

        public int AddField(string name)
        {
            if (IsFixed)
                throw new TypeError($"{Name} class has no attribute {name} (immutable)");
            if (fieldCnt == Initialization.OBJECT_SHAPE_MAX_FIELD)
            {
                throw new TypeError($"class {Name} cannot add a shape for field {name} (more than 255 fields)");
            }
            if (__prototype__.TryGetValue(name, out var shape))
            {
                if (shape.Kind == AttributeKind.Field)
                    return shape.FieldIndex;
                throw new TypeError($"{name} is already a {shape.Kind}");
            }
            var Token = UpdatePrototype();
            var index = fieldCnt++;
            __prototype__[name] = Shape.MKField(name.ToIntern(), index);
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