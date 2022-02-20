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
        public TraffyAsm[] suite;

        public TrObject exec(Frame frame)
        {
            for (int i = 0; i < suite.Length; i++)
            {
                suite[i].exec(frame);
                if (frame.CONT != STATUS.NORMAL)
                {
                    break;
                }
            }
            return RTS.object_none;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            for (int i = 0; i < suite.Length; i++)
            {
                if (suite[i].hasCont)
                    await suite[i].cont(frame);
                else
                    suite[i].exec(frame);

                if (frame.CONT != STATUS.NORMAL)
                {
                    break;
                }
            }
            return RTS.object_none;
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

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            var o = rhs.hasCont ? await rhs.cont(frame) : rhs.exec(frame);
            if (lhs.hasCont)
                await lhs.contOp(frame, opfunc, rhs);
            else
                lhs.execOp(frame, opfunc, rhs);
            frame.traceback.Pop();
            return RTS.object_none;
        }
    }

    [Serializable]
    public class Assign : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;
        public TraffyLHS lhs;
        public TraffyAsm rhs;

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            var o = rhs.exec(frame);
            lhs.exec(frame, o);
            frame.traceback.Pop();
            return RTS.object_none;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            var o = rhs.hasCont ? await rhs.cont(frame) : rhs.exec(frame);
            if (lhs.hasCont)
                await lhs.cont(frame, o);
            else
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
        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_value = value.exec(frame);
            frame.CONT = STATUS.RETURN;
            frame.retval = rt_value;
            frame.traceback.Pop();
            return RTS.object_none;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_value = value.hasCont ? await value.cont(frame) : value.exec(frame);
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

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            TrObject val_return = RTS.object_none;
            while (RTS.object_bool(test.hasCont ? await test.cont(frame) : test.exec(frame)))
            {
                var rt_body = body.hasCont ? await body.cont(frame) : body.exec(frame);
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
                val_return = orelse.hasCont ? await orelse.cont(frame) : orelse.exec(frame);
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

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_itr = itr.hasCont ? await itr.cont(frame) : itr.exec(frame);
            var enumerator = RTS.object_getiter(rt_itr);
            while (enumerator.MoveNext())
            {
                var target = enumerator.Current;
                if (this.target.hasCont)
                    await this.target.cont(frame, target);
                else
                    this.target.exec(frame, target);
                var rt_body = body.hasCont ? await body.cont(frame) : body.exec(frame);
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
                    await orelse.cont(frame);
                else
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

    [Serializable]
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
                    var rt_body = body.exec(frame);
                    return RTS.object_none;
                }
            }
            if (orelse != null)
            {
                orelse.exec(frame);
            }
            return RTS.object_none;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            for (int i = 0; i < clauses.Length; i++)
            {
                var cond = clauses[i].cond;
                var rt_cond = cond.hasCont ? await cond.cont(frame) : cond.exec(frame);

                if (RTS.object_bool(rt_cond))
                {
                    var body = clauses[i].body;
                    var rt_body = body.hasCont ? await body.cont(frame) : body.exec(frame);
                    return RTS.object_none;
                }
            }
            if (orelse != null)
            {
                if (orelse.hasCont)
                    await orelse.cont(frame);
                else
                    orelse.exec(frame);
            }
            return RTS.object_none;
        }
    }

    [Serializable]
    public class Continue : TraffyAsm
    {
        public bool hasCont => false;

        public MonoAsync<TrObject> cont(Frame frame)
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

        public MonoAsync<TrObject> cont(Frame frame)
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
            frame.mark();
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
                    frame.restore();
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

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            frame.mark();
            try
            {
                var rt_body = body.hasCont ? await body.cont(frame) : body.exec(frame);
                if (orelse != null)
                {
                    if (orelse.hasCont)
                        await orelse.cont(frame);
                    else
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
                        var rt_exc = exc_type.hasCont ? await exc_type.cont(frame) : exc_type.exec(frame);
                        if (!RTS.exc_check_instance(e, rt_exc))
                            continue;
                    }
                    frame.set_exception(e);
                    if (handler.exc_bind != null)
                    {
                        TrObject rt_e = RTS.exc_frombare(e);
                        if (handler.exc_bind.hasCont)
                            await handler.exc_bind.cont(frame, rt_e);
                        else
                            handler.exc_bind.exec(frame, rt_e);
                    }
                    var rt_body = handler.body.hasCont ? await handler.body.cont(frame) : handler.body.exec(frame);
                    frame.clear_exception();
                    frame.restore();
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
    }

    [Serializable]
    public class Raise : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;

        [AllowNull] public TraffyAsm exc;

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

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            Exception e;
            if (exc != null)
            {
                var rt_exc = exc.hasCont ? await exc.cont(frame) : exc.exec(frame);
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

    [Serializable]
    public class DefClass : TraffyAsm
    {
        public bool hasCont { get; set; }
        public TraffyAsm[] bases;
        public Lambda body;

        public TrObject exec(Frame frame)
        {
            var rt_bases = new TrObject[bases.Length];
            for (int i = 0; i < bases.Length; i++)
            {
                rt_bases[i] = bases[i].exec(frame);
            }
            var rt_body = body.exec(frame);
            if (!(rt_body is TrFunc ufunc))
            {
                throw new InvalidOperationException("class body must be a user function");
            }
            var localnames = ufunc.fptr.metadata.localnames;
            var subframe = Frame.UnsafeMake();
            ufunc.Execute(new BList<TrObject>(), null, subframe);
            var ns = RTS.baredict_create();
            for(int i = 0; i < localnames.Length; i++)
            {
                var name = localnames[i];
                var value = subframe.load_local(i);
                RTS.baredict_set(ns, MK.Str(name), value);
            }
            var rt_cls = RTS.class_new(ufunc.fptr.metadata.codename, rt_bases, ns);
            return rt_cls;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            var rt_bases = new TrObject[bases.Length];
            for (int i = 0; i < bases.Length; i++)
            {
                rt_bases[i] = bases[i].hasCont ? await bases[i].cont(frame) : bases[i].exec(frame);
            }
            var rt_body = body.hasCont ? await body.cont(frame) : body.exec(frame);
            if (!(rt_body is TrFunc ufunc))
            {
                throw new InvalidOperationException("class body must be a user function");
            }
            var localnames = ufunc.fptr.metadata.localnames;
            var subframe = Frame.UnsafeMake();
            ufunc.Execute(new BList<TrObject>(), null, subframe);
            var ns = RTS.baredict_create();
            for (int i = 0; i < localnames.Length; i++)
            {
                var name = localnames[i];
                var value = subframe.load_local(i);
                RTS.baredict_set(ns, MK.Str(name), value);
            }
            var rt_cls = RTS.class_new(ufunc.fptr.metadata.codename, rt_bases, ns);
            return rt_cls;
        }
    }
}