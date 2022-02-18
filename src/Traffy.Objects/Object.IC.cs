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

    }
}