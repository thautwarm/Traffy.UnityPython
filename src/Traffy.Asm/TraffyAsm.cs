using System;
using System.Collections;
using System.Collections.Generic;

namespace Traffy
{
    using binary_func = Func<TrObject, TrObject, TrObject>;
    public class TraffyCoroutine : IEnumerator<TrObject>, TrObject
    {
        // at the end of such generator, result is set.
        private IEnumerator<TrObject> m_generator;
        public TrObject Result;
        public TrObject Sent;
        public IEnumerator<TrObject> generator { set => m_generator = value; }
        public bool MoveNext(TrObject o)
        {
            Sent = o;
            return m_generator.MoveNext();
        }
        public TraffyCoroutine()
        {
            this.Sent = RTS.object_none;
            this.Result = null;
        }

        public TrObject Current => m_generator.Current;

        object IEnumerator.Current => Current;

        public Dictionary<TrObject, TrObject> __dict__ => null;

        public TrClass Class => TrClass.GeneratorClass;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError($"invalid invocation of {args[0].AsClass.Name}");
        }

        public IEnumerator<TrObject> ToIEnumerator()
        {
            Sent = RTS.object_none;
            while (m_generator.MoveNext())
            {
                yield return Current;
            }
        }

        public bool MoveNext()
        {
            return MoveNext(RTS.object_none);
        }

        public void Reset()
        {
            throw new NotSupportedException("cannot reset Traffy coroutines");
        }

        public void Dispose()
        {
            Sent = RTS.object_none;
        }

        public IEnumerator<TrObject> __iter__() => ToIEnumerator();
        public TrObject __next__() =>
            MoveNext() ? Current : throw new StopIteration(Result);
    }

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

    public class MultiAssign : TraffyLHS
    {
        public bool hasCont { set; get; }
        public TraffyLHS[] targets;

        public TraffyCoroutine cont(Frame frame, TrObject o)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                foreach (var lhs in targets)
                {
                    if (lhs.hasCont)
                    {
                        var cont = lhs.cont(frame, o);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                    }
                    else
                    {
                        lhs.exec(frame, o);
                    }
                }
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TraffyCoroutine contOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            throw new NotImplementedException();
        }

        public void exec(Frame frame, TrObject o)
        {
            foreach (var lhs in targets)
            {
                lhs.exec(frame, o);
            }
        }

        public void execOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            throw new NotImplementedException();
        }
    }

}