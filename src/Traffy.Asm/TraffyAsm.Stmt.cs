using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Traffy.Objects;

namespace Traffy.Asm
{
    using binary_func = Func<TrObject, TrObject, TrObject>;

    [Serializable]
    public class Block : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;
        public TraffyAsm[] suite;

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            for (int i = 0; i < suite.Length; i++)
            {
                suite[i].exec(frame);
                if (frame.CONT != STATUS.NORMAL)
                {
                    break;
                }
            }
            frame.traceback.Pop();
            return RTS.object_none;
        }

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                frame.traceback.Push(position);
                for (int i = 0; i < suite.Length; i++)
                {
                    var stmt = suite[i];
                    if (stmt.hasCont)
                    {
                        var cont = stmt.cont(frame);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                    }
                    else
                    {
                        stmt.exec(frame);
                    }
                    if (frame.CONT != STATUS.NORMAL)
                    {
                        break;
                    }
                }
                frame.traceback.Pop();
            }
            var coro = new TraffyCoroutine();
            coro.Result = RTS.object_none;
            coro.generator = mkCont(frame, coro);
            return coro;
        }
    }
    [Serializable]
    public class AugAssign : TraffyAsm
    {
        public bool hasCont => op < 0;
        public int op;
        public int position;
        public TraffyLHS lhs;
        public TraffyAsm rhs;
        private binary_func opfunc;

        [OnDeserialized]
        internal AugAssign OnDeserializedMethod()
        {
            opfunc = RTS.InplaceOOOFuncs[op < 0 ? -op : op];
            return this;
        }

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            var o = rhs.exec(frame);
            lhs.execOp(frame, opfunc, rhs);
            frame.traceback.Pop();
            return RTS.object_none;

        }

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                frame.traceback.Push(position);
                TrObject rt_rhs;
                if (rhs.hasCont)
                {
                    var cont = rhs.cont(frame);
                    while (cont.MoveNext(coro.Sent))
                        yield return cont.Current;
                    rt_rhs = cont.Result;
                }
                else
                {
                    rt_rhs = rhs.exec(frame);
                }
                {
                    var cont = lhs.contOp(frame, opfunc, rhs);
                    while (cont.MoveNext(coro.Sent))
                    {
                        yield return cont.Current;
                    }
                }
                frame.traceback.Pop();
            }
            var coro = new TraffyCoroutine();
            coro.Result = RTS.object_none;
            coro.generator = mkCont(frame, coro);
            return coro;
        }
    }

    [Serializable]
    public class Assign : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;
        public TraffyLHS lhs;
        public TraffyAsm rhs;

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                frame.traceback.Push(position);
                TrObject rt_rhs;
                if (rhs.hasCont)
                {
                    var cont = rhs.cont(frame);
                    while (cont.MoveNext(coro.Sent))
                        yield return cont.Current;
                    rt_rhs = cont.Result;
                }
                else
                {
                    rt_rhs = rhs.exec(frame);
                }
                if (lhs.hasCont)
                {
                    var cont = lhs.cont(frame, rt_rhs);
                    while (cont.MoveNext(coro.Sent))
                        yield return cont.Current;
                }
                else
                {
                    lhs.exec(frame, rt_rhs);
                }
                frame.traceback.Pop();
            }
            var coro = new TraffyCoroutine();
            coro.Result = RTS.object_none;
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            var o = rhs.exec(frame);
            lhs.exec(frame, o);
            frame.traceback.Pop();
            return RTS.object_none;
        }
    }
    [Serializable]
    public class Return : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;
        public TraffyAsm value;

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                frame.traceback.Push(position);
                TrObject rt_value;
                if (value.hasCont)
                {
                    var cont = value.cont(frame);
                    while (cont.MoveNext(coro.Sent))
                        yield return cont.Current;
                    rt_value = cont.Result;
                }
                else
                {
                    rt_value = value.exec(frame);
                }
                frame.CONT = STATUS.RETURN;
                frame.retval = rt_value;
                coro.Result = RTS.object_none;
                frame.traceback.Pop();
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_value = value.exec(frame);
            frame.CONT = STATUS.RETURN;
            frame.retval = rt_value;
            frame.traceback.Pop();
            return RTS.object_none;
        }
    }

    [Serializable]
    public class While : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;
        public TraffyAsm test;
        public TraffyAsm body;
        [System.Diagnostics.CodeAnalysis.AllowNull] public TraffyAsm orelse;

        public TraffyCoroutine cont(Frame frame)
        {

            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                frame.traceback.Push(position);
                while (true)
                {
                    TrObject rt_test;
                    if (test.hasCont)
                    {
                        var cont = test.cont(frame);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                        rt_test = cont.Result;
                    }
                    else
                    {
                        rt_test = test.exec(frame);
                    }
                    if (!RTS.object_bool(rt_test))
                    {
                        break;
                    }

                    TrObject rt_body;
                    if (body.hasCont)
                    {
                        var cont = body.cont(frame);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                        rt_body = cont.Result;
                    }
                    else
                    {
                        rt_body = body.exec(frame);
                    }

                    if (frame.CONT == STATUS.NORMAL)
                    {
                        continue;
                    }
                    else if (frame.CONT == STATUS.CONTINUE)
                    {
                        frame.CONT = STATUS.NORMAL;
                        continue;
                    }
                    break;
                }

                if (orelse != null && frame.CONT == STATUS.NORMAL)
                {
                    if (orelse.hasCont)
                    {
                        var cont = orelse.cont(frame);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                    }
                    else
                    {
                        orelse.exec(frame);
                    }
                }
                else if (frame.CONT == STATUS.BREAK)
                {
                    frame.CONT = STATUS.NORMAL;
                }

                coro.Result = RTS.object_none;
                frame.traceback.Pop();
            }

            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            TrObject val_return = RTS.object_none;
            while (RTS.object_bool(test.exec(frame)))
            {
                var rt_body = body.exec(frame);
                val_return = rt_body;
                if (frame.CONT == STATUS.NORMAL)
                {
                    continue;
                }
                else if (frame.CONT == STATUS.CONTINUE)
                {
                    frame.CONT = STATUS.NORMAL;
                    continue;
                }
                break;
            }

            if (orelse != null && frame.CONT == STATUS.NORMAL)
            {
                val_return = orelse.exec(frame);
            }
            else if (frame.CONT == STATUS.BREAK)
            {
                frame.CONT = STATUS.NORMAL;
            }

            frame.traceback.Pop();
            return val_return;
        }
    }

    [Serializable]
    public class ForIn : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;
        public TraffyLHS target;
        public TraffyAsm itr;
        public TraffyAsm body;
        [System.Diagnostics.CodeAnalysis.AllowNull] public TraffyAsm orelse;

        public TraffyCoroutine cont(Frame frame)
        {

            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                frame.traceback.Push(position);
                TrObject rt_itr;
                if (itr.hasCont)
                {
                    var cont = itr.cont(frame);
                    while (cont.MoveNext(coro.Sent))
                        yield return cont.Current;
                    rt_itr = cont.Result;
                }
                else
                {
                    rt_itr = itr.exec(frame);
                }

                var enumerator = RTS.object_getiter(rt_itr);
                while (enumerator.MoveNext())
                {
                    var target = enumerator.Current;

                    if (this.target.hasCont)
                    {
                        var cont = this.target.cont(frame, target);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                    }

                    TrObject rt_body;
                    if (body.hasCont)
                    {
                        var cont = body.cont(frame);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                        rt_body = cont.Result;
                    }
                    else
                    {
                        rt_body = body.exec(frame);
                    }

                    if (frame.CONT == STATUS.NORMAL)
                    {
                        continue;
                    }
                    else if (frame.CONT == STATUS.CONTINUE)
                    {
                        frame.CONT = STATUS.NORMAL;
                        continue;
                    }
                    break;
                }
                if (orelse != null && frame.CONT == STATUS.NORMAL)
                {
                    TrObject rt_orelse;
                    if (orelse.hasCont)
                    {
                        var cont = orelse.cont(frame);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                        rt_orelse = cont.Result;
                    }
                    else
                    {
                        rt_orelse = orelse.exec(frame);
                    }
                }
                else if (frame.CONT == STATUS.BREAK)
                {
                    frame.CONT = STATUS.NORMAL;
                }

                coro.Result = RTS.object_none;
                frame.traceback.Pop();
            }

            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_itr = itr.exec(frame);
            var enumerator = RTS.object_getiter(rt_itr);
            while (enumerator.MoveNext())
            {
                var target = enumerator.Current;
                this.target.exec(frame, target);
                var rt_body = body.exec(frame);
                if (frame.CONT == STATUS.NORMAL)
                {
                    continue;
                }
                else if (frame.CONT == STATUS.CONTINUE)
                {
                    frame.CONT = STATUS.NORMAL;
                    continue;
                }
                break;
            }
            if (orelse != null && frame.CONT == STATUS.NORMAL)
            {
                orelse.exec(frame);
            }
            else if (frame.CONT == STATUS.BREAK)
            {
                frame.CONT = STATUS.NORMAL;
            }

            frame.traceback.Pop();
            return RTS.object_none;
        }
    }

    public struct IfClause
    {
        public TraffyAsm cond;
        public TraffyAsm body;
    }
    [Serializable]
    public class IfThenElse : TraffyAsm
    {
        public bool hasCont { get; set; }

        public IfClause[] clauses;

        [System.Diagnostics.CodeAnalysis.AllowNull] public TraffyAsm orelse;

        public TrObject exec(Frame frame)
        {
            for (int i = 0; i < clauses.Length; i++)
            {
                var cond = clauses[i].cond;
                var rt_cond = cond.exec(frame);

                if (RTS.object_bool(rt_cond))
                {
                    var body = clauses[i].body;
                    var rt_body = cond.exec(frame);
                    return RTS.object_none;
                }
            }
            if (orelse != null)
            {
                orelse.exec(frame);
            }
            return RTS.object_none;
        }

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                for (int i = 0; i < clauses.Length; i++)
                {
                    var cond = clauses[i].cond;
                    TrObject rt_cond;
                    if (cond.hasCont)
                    {
                        var cont = cond.cont(frame);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                        rt_cond = cont.Result;
                    }
                    else
                    {
                        rt_cond = cond.exec(frame);
                    }

                    if (RTS.object_bool(rt_cond))
                    {
                        var body = clauses[i].body;
                        TrObject rt_body;
                        if (body.hasCont)
                        {
                            var cont = body.cont(frame);
                            while (cont.MoveNext(coro.Sent))
                                yield return cont.Current;
                            rt_body = cont.Result;
                        }
                        else
                        {
                            rt_body = body.exec(frame);
                        }
                        coro.Result = RTS.object_none;
                        yield break;
                    }
                }
                if (orelse != null)
                {
                    TrObject rt_orelse;
                    if (orelse.hasCont)
                    {
                        var cont = orelse.cont(frame);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                        rt_orelse = cont.Result;
                    }
                    else
                    {
                        rt_orelse = orelse.exec(frame);
                    }
                }
                coro.Result = RTS.object_none;
            }

            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }
    }

    [Serializable]
    public class Continue : TraffyAsm
    {
        public bool hasCont => false;

        public TraffyCoroutine cont(Frame frame)
        {
            throw new InvalidOperationException("continue statements shall not produce coroutines");
        }

        public TrObject exec(Frame frame)
        {
            frame.CONT = STATUS.CONTINUE;
            return RTS.object_none;
        }
    }

    [Serializable]
    public class Break : TraffyAsm
    {
        public bool hasCont => false;

        public TraffyCoroutine cont(Frame frame)
        {
            throw new InvalidOperationException("break statements shall not produce coroutines");
        }

        public TrObject exec(Frame frame)
        {
            frame.CONT = STATUS.BREAK;
            return RTS.object_none;
        }
    }

    [Serializable]
    public class Handler
    {
        [System.Diagnostics.CodeAnalysis.AllowNull] public TraffyAsm exc_type;
        [System.Diagnostics.CodeAnalysis.AllowNull] public TraffyLHS exc_bind;

        public TraffyAsm body;
    }

    [Serializable]
    public class Try : TraffyAsm
    {
        public bool hasCont { set; get; }
        public int position;
        public TraffyAsm body;
        public Handler[] handlers;
        [System.Diagnostics.CodeAnalysis.AllowNull] public TraffyAsm orelse;
        [System.Diagnostics.CodeAnalysis.AllowNull] public TraffyAsm final;

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            try
            {
                body.exec(frame);
                if (orelse != null)
                {
                    orelse.exec(frame);
                }
            }
            catch (System.Exception e)
            {
                foreach (var handler in handlers)
                {
                    if (handler.exc_type != null)
                    {
                        var exc_type = handler.exc_type;
                        var rt_exc = exc_type.exec(frame);
                        if (!RTS.exc_check_instance(e, rt_exc))
                            continue;
                    }
                    frame.set_exception(e);
                    if (handler.exc_bind != null)
                    {
                        TrObject rt_e = RTS.exc_frombare(e);
                        handler.exc_bind.exec(frame, rt_e);
                    }
                    handler.body.exec(frame);
                    frame.clear_exception();
                    goto handled;
                }
                throw;
            handled:;
            }
            finally
            {
                if (final != null)
                {
                    final.exec(frame);
                }
            }
            frame.traceback.Pop();
            return RTS.object_none;
        }

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                if (orelse != null)
                {
                    throw new InvalidProgramException("yield statements in try-else are not implemented.");
                }
                if (handlers.Length != 0)
                {
                    throw new InvalidProgramException("yield statements in try-catch are not implemented.");
                }
                if (final != null && final.hasCont)
                {
                    throw new InvalidProgramException("yield statements in finally blocks are not implemented.");
                }
                frame.traceback.Push(position);
                try
                {
                    TrObject rt_body;
                    if (body.hasCont)
                    {
                        var cont = body.cont(frame);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                        rt_body = cont.Result;
                    }
                    else
                    {
                        rt_body = body.exec(frame);
                    }
                }
                finally
                {
                    if (final != null)
                        final.exec(frame);
                }
                frame.traceback.Pop();
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }
    }

    [Serializable]
    public class Raise : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;

        [AllowNull] public TraffyAsm exc;

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                frame.traceback.Push(position);
                TrObject rt_exc;
                if (exc.hasCont)
                {
                    var cont = exc.cont(frame);
                    while (cont.MoveNext(coro.Sent))
                        yield return cont.Current;
                    rt_exc = cont.Result;
                }
                else
                {
                    rt_exc = exc.exec(frame);
                }
                Exception e = RTS.exc_tobare(rt_exc);
                throw e;
            }

            var coro = new TraffyCoroutine();
            coro.Result = RTS.object_none;
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            Exception e;
            if (exc != null)
            {
                var rt_exc = exc.exec(frame);
                e = RTS.exc_tobare(rt_exc);
                throw e;
            }
            e = frame.get_exception();
            if (e == null)
            {
                throw frame.exc_notset();
            }
            throw e;
        }
    }
}