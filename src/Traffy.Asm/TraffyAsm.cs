using System;
using System.Collections.Generic;
using Traffy.Objects;

namespace Traffy.Asm
{
    using binary_func = Func<TrObject, TrObject, TrObject>;

    public interface TraffyAsm
    {
        public bool hasCont { get; }
        public TrObject exec(Frame frame);
        public TraffyCoroutine cont(Frame frame);
    }
    public interface TraffyLHS
    {
        public bool hasCont { get; }
        public void exec(Frame frame, TrObject o);
        public void execOp(Frame frame, binary_func op, TraffyAsm asm);
        public TraffyCoroutine cont(Frame frame, TrObject o);
        public TraffyCoroutine contOp(Frame frame, binary_func op, TraffyAsm asm);

    }

}