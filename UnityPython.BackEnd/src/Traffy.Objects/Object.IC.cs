using System;
using Traffy.InlineCache;

namespace Traffy.Objects
{
    public partial interface TrObject
    {

        public TrObject this[IC ic]
        {
            get => __getic__(ic, out var ob) ? ob : null;
            set => __setic__(ic, value);
        }
        public TrObject this[string s]
        {
            get => __getic__(s, out var ob) ? ob : null;
            set => __setic__(s, value);
        }

        public bool __getic__(IC ic, out TrObject found) =>
            ic.ReadInst(this, out found);
        public void __setic__(IC ic, TrObject value) =>
            ic.WriteInst(this, value);

        public bool __getic__(string s, out TrObject found) =>
            IC.ReadInst(this, s, out found);

        public void __setic__(string s, TrObject value) =>
            IC.WriteInst(this, s, value);


        public TrObject GetInstField(int FieldIndex, string name)
        {
            if (__array__ == null || __array__.Count <= FieldIndex)
                throw new AttributeError(this, MK.Str(name), $"{Class.Name} has no attribute {name}");
            return __array__[FieldIndex];
        }
        public void SetInstField(int FieldIndex, string name, TrObject value)
        {
            if (__array__ == null)
                throw new AttributeError(this, MK.Str(name), $"object {Class.Name} has no attribute {name}");

            if (FieldIndex < __array__.Count)
            {
                __array__[FieldIndex] = value;
            }
            else
            {
                for (int j = __array__.Count; j < FieldIndex + 1; j++)
                    __array__.Add(null);
                __array__[FieldIndex] = value;
            }
        }
    }
}