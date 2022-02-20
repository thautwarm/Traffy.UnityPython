using System;
using System.Collections.Generic;
using Traffy.Objects;

namespace Traffy.Asm
{
    using binary_func = Func<TrObject, TrObject, TrObject>;

    public interface TraffyAsm
    {
        public bool hasCont { get; }

        // if hasCont is true, then 'exec' shall not be called;
        // instead, the method 'cont' is called
        public TrObject exec(Frame frame);
        public MonoAsync<TrObject> cont(Frame frame);
    }
    public interface TraffyLHS
    {
        public bool hasCont { get; }
        public void exec(Frame frame, TrObject o);
        public void execOp(Frame frame, binary_func op, TraffyAsm asm);
        public MonoAsync<TrObject> cont(Frame frame, TrObject o);
        public MonoAsync<TrObject> contOp(Frame frame, binary_func op, TraffyAsm asm);

    }

}