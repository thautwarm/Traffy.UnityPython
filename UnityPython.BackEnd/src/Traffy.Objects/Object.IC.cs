using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Traffy.InlineCache;

namespace Traffy.Objects
{
    public partial interface TrObject
    {

        public TrObject this[PolyIC ic]
        {
            get => __getic__(ic, out var ob) ? ob : null;
            set => __setic__(ic, value);
        }
        public TrObject this[string s]
        {
            get => __getic_refl__(MK.Str(s), out var ob) ? ob : null;
            set => __setic_refl__(MK.Str(s), value);
        }

        public bool __getic__(PolyIC ic, out TrObject found) =>
            ic.ICInstance.ReadInst(this, out found);
        public void __setic__(PolyIC ic, TrObject value) =>
            ic.ICInstance.WriteInst(this, value);

        public bool __getic_refl__(TrStr s, out TrObject found) =>
            PolyIC.ReadInst_refl(this, s, out found);

        public void __setic_refl__(TrStr s, TrObject value) =>
            PolyIC.WriteInst_refl(this, s, value);


        public TrObject GetInstField(int FieldIndex, string name)
        {
            if (__array__ == null || __array__.Count <= FieldIndex)
                throw new AttributeError(this, MK.Str(name), $"{Class.Name} has no attribute {name}");
            return __array__[FieldIndex];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInstField(int FieldIndex, string name, TrObject value)
        {
            var array = __array__;
            if (null == (object)array)
                throw new AttributeError(this, MK.Str(name), $"object {Class.Name} has no attribute {name}");

            if (FieldIndex < array.Count)
            {
                array[FieldIndex] = value;
            }
            else
            {
                for (int j = array.Count; j < FieldIndex + 1; j++)
                    array.Add(null);
                array[FieldIndex] = value;
            }
        }


        [MethodImpl(MethodImplOptionsCompat.Best)]
        public bool ReadInst(Shape shape, out TrObject ob)
        {
            switch (shape.Kind)
            {
                case AttributeKind.InstField:
                    {

                        List<TrObject> array = __array__;
#if DEBUG
                        Debug.Assert(array != null);
#endif
                        if (shape.FieldIndex < array.Count
                            && null != (object)(ob = array[shape.FieldIndex]))
                        {
                            return true;
                        }
                        ob = null;
                        return false;
                    }

                case AttributeKind.Property:
                    ob = shape.Property.Get(this);
                    return true;
                case AttributeKind.Method:
                    {
                        var func = shape.MethodOrClassFieldOrClassMethod;
                        ob = TrSharpMethod.Bind(func, this);
                        return true;
                    }
                case AttributeKind.ClassField:
                    ob = shape.MethodOrClassFieldOrClassMethod;
                    return true;
                case AttributeKind.ClassMethod:
                    {
                        var func = shape.MethodOrClassFieldOrClassMethod;
                        ob = TrSharpMethod.Bind(func, Class);
                        return true;
                    }
                default:
                    throw new System.Exception("unexpected kind");
            }
        }
    }
}