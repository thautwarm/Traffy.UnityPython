using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Traffy.Objects;

namespace Traffy.Asm
{

    using binary_func = Func<TrObject, TrObject, TrObject>;

    public class MultiAssign : TraffyLHS
    {
        public bool hasCont { set; get; }
        public TraffyLHS[] targets;

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

        public TraffyCoroutine cont(Frame frame, TrObject o)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {

                foreach (var lhs in targets)
                {
                    if (lhs.hasCont)
                    {
                        var cont_lhs = lhs.cont(frame, o);
                        while (cont_lhs.MoveNext(coro.Sent))
                            yield return cont_lhs.Current;
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
    }

    [Serializable]
    public class StoreListEx : TraffyLHS
    {
        public TraffyLHS[] before;
        public TraffyLHS unpack;
        public TraffyLHS[] after;
        public int position;
        public bool hasCont { get; set; }
        public void exec(Frame frame, TrObject o)
        {
            frame.traceback.Push(position);

            List<TrObject> rt_itr = RTS.object_as_list(o);
            var left = rt_itr.Count - before.Length - after.Length;
            if (rt_itr.Count < 0)
            {
                throw RTS.exc_unpack_notenough(rt_itr.Count, before.Length + after.Length);
            }
            for (int i = 0; i < before.Length; i++)
            {
                before[i].exec(frame, rt_itr[i]);
            }
            for (int i = rt_itr.Count - after.Length, j = 0; j < after.Length; j++, i++)
            {
                after[j].exec(frame, rt_itr[i]);
            }
            rt_itr = rt_itr.GetRange(before.Length, left);

            unpack.exec(frame, RTS.object_from_list(rt_itr));

            frame.traceback.Pop();
        }

        public TraffyCoroutine cont(Frame frame, TrObject o)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                frame.traceback.Push(position);

                List<TrObject> rt_itr = RTS.object_as_list(o);
                var left = rt_itr.Count - before.Length - after.Length;
                if (rt_itr.Count < 0)
                {
                    throw RTS.exc_unpack_notenough(rt_itr.Count, before.Length + after.Length);
                }
                for (int i = 0; i < before.Length; i++)
                {
                    if (before[i].hasCont)
                    {
                        var cont_before_i = before[i].cont(frame, rt_itr[i]);
                        while (cont_before_i.MoveNext(coro.Sent))
                            yield return cont_before_i.Current;
                    }
                    else
                    {
                        before[i].exec(frame, rt_itr[i]);
                    }
                }
                for (int i = rt_itr.Count - after.Length, j = 0; j < after.Length; j++, i++)
                {
                    if (after[j].hasCont)
                    {
                        var cont_after_j = after[j].cont(frame, rt_itr[i]);
                        while (cont_after_j.MoveNext(coro.Sent))
                            yield return cont_after_j.Current;
                    }
                    else
                    {
                        after[j].exec(frame, rt_itr[i]);
                    }
                }
                rt_itr = rt_itr.GetRange(before.Length, left);

                if (unpack.hasCont)
                {
                    var cont_unpack = unpack.cont(frame, RTS.object_from_list(rt_itr));
                    while (cont_unpack.MoveNext(coro.Sent))
                        yield return cont_unpack.Current;
                }
                else
                {
                    unpack.exec(frame, RTS.object_from_list(rt_itr));
                }

                frame.traceback.Pop();
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }


        public void execOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            throw new InvalidOperationException("augassign is invalid for left-hand side list/tuple(s)");
        }

        public TraffyCoroutine contOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            throw new InvalidOperationException("augassign is invalid for left-hand side list/tuple(s)");
        }
    }
    [Serializable]
    public class StoreList : TraffyLHS
    {
        public bool hasCont { get; set; }
        public int position;
        public TraffyLHS[] elts;
        public void exec(Frame frame, TrObject o)
        {
            frame.traceback.Push(position);

            var rt_itr = RTS.object_getiter(o);
            for (int i = 0; i < elts.Length; i++)
            {
                var elt = elts[i];
                if (!rt_itr.MoveNext())
                {
                    throw RTS.exc_unpack_notenough(i, elts.Length);
                }
                elt.exec(frame, rt_itr.Current);
            }
            if (rt_itr.MoveNext())
            {
                throw RTS.exc_unpack_toomuch(elts.Length);
            }

            frame.traceback.Pop();
        }

        public TraffyCoroutine cont(Frame frame, TrObject o)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                frame.traceback.Push(position);

                var rt_itr = RTS.object_getiter(o);
                for (int i = 0; i < elts.Length; i++)
                {
                    var elt = elts[i];

                    if (!rt_itr.MoveNext())
                    {
                        throw RTS.exc_unpack_notenough(i, elts.Length);
                    }

                    if (elt.hasCont)
                    {
                        var cont_elt = elt.cont(frame, rt_itr.Current);
                        while (cont_elt.MoveNext(coro.Sent))
                            yield return cont_elt.Current;
                    }
                    else
                    {
                        elt.exec(frame, rt_itr.Current);
                    }
                }
                if (rt_itr.MoveNext())
                {
                    throw RTS.exc_unpack_toomuch(elts.Length);
                }

                frame.traceback.Pop();
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public void execOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            throw new InvalidOperationException("augassign is invalid for left-hand side list/tuple(s)");
        }

        public TraffyCoroutine contOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            throw new InvalidOperationException("augassign is invalid for left-hand side list/tuple(s)");
        }
    }
    [Serializable]
    public class StoreLocal : TraffyLHS
    {
        public int slot;
        public bool hasCont => false;
        public void exec(Frame frame, TrObject o)
        {
            frame.store_local(slot, o);
        }

        public void execOp(Frame frame, binary_func op, TraffyAsm rhs)
        {

            var rt_rhs = rhs.exec(frame);
            var localval = frame.load_local(slot);
            localval = op(localval, rt_rhs);
            frame.store_local(slot, localval);
        }

        public TraffyCoroutine cont(Frame frame, TrObject o)
        {
            throw new InvalidOperationException("local variable store cannot be async");
        }

        public TraffyCoroutine contOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro, binary_func op, TraffyAsm rhs)
            {
                TrObject rt_rhs;
                if (rhs.hasCont)
                {
                    var cont_rhs = rhs.cont(frame);
                    while (cont_rhs.MoveNext(coro.Sent))
                        yield return cont_rhs.Current;
                    rt_rhs = cont_rhs.Result;
                }
                else
                {
                    rt_rhs = rhs.exec(frame);
                }
                var localval = frame.load_local(slot);
                localval = op(localval, rt_rhs);
                frame.store_local(slot, localval);
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro, op, asm);
            return coro;
        }
    }

    [Serializable]
    public class StoreGlobal : TraffyLHS
    {
        public TrObject name;
        public bool hasCont => false;
        public void exec(Frame frame, TrObject o)
        {
            frame.store_global(name, o);
        }

        public void execOp(Frame frame, binary_func op, TraffyAsm rhs)
        {
            var rt_rhs = rhs.exec(frame);
            var globalval = frame.load_global(name);
            globalval = op(globalval, rt_rhs);
            frame.store_global(name, globalval);
        }

        public TraffyCoroutine cont(Frame frame, TrObject o)
        {
            throw new InvalidOperationException("local variable store cannot be async");
        }
        public TraffyCoroutine contOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro, binary_func op, TraffyAsm rhs)
            {
                TrObject rt_rhs;
                if (rhs.hasCont)
                {
                    var cont_rhs = rhs.cont(frame);
                    while (cont_rhs.MoveNext(coro.Sent))
                        yield return cont_rhs.Current;
                    rt_rhs = cont_rhs.Result;
                }
                else
                {
                    rt_rhs = rhs.exec(frame);
                }
                var globalval = frame.load_global(name);
                globalval = op(globalval, rt_rhs);
                frame.store_global(name, globalval);
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro, op, asm);
            return coro;
        }
    }

    [Serializable]
    public class StoreItem : TraffyLHS
    {
        public TraffyAsm value;
        public TraffyAsm item;
        public bool hasCont { get; set; }
        public int position;

        public void exec(Frame frame, TrObject o)
        {
            frame.traceback.Push(position);
            var rt_value = value.exec(frame);
            var rt_item = item.exec(frame);
            RTS.object_setitem(rt_value, rt_item, o);
            frame.traceback.Pop();
        }

        public void execOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            frame.traceback.Push(position);
            var rt_value = value.exec(frame);
            var rt_item = item.exec(frame);
            var rt_res = RTS.object_getitem(rt_value, rt_item);
            rt_res = op(rt_res, asm.exec(frame));
            RTS.object_setitem(rt_value, rt_item, rt_res);
            frame.traceback.Pop();
        }

        public TraffyCoroutine contOp(Frame frame, binary_func op, TraffyAsm rhs)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro, binary_func op, TraffyAsm rhs)
            {
                frame.traceback.Push(position);

                TrObject rt_value;
                if (value.hasCont)
                {
                    var cont_value = value.cont(frame);
                    while (cont_value.MoveNext(coro.Sent))
                        yield return cont_value.Current;
                    rt_value = cont_value.Result;
                }
                else
                {
                    rt_value = value.exec(frame);
                }

                TrObject rt_item;
                if (item.hasCont)
                {
                    var cont_item = item.cont(frame);
                    while (cont_item.MoveNext(coro.Sent))
                        yield return cont_item.Current;
                    rt_item = cont_item.Result;
                }
                else
                {
                    rt_item = item.exec(frame);
                }

                TrObject rt_res = RTS.object_getitem(rt_value, rt_item);
                if (rhs.hasCont)
                {
                    var cont_rhs = rhs.cont(frame);
                    while (cont_rhs.MoveNext(coro.Sent))
                        yield return cont_rhs.Current;
                    rt_res = op(rt_res, cont_rhs.Result);
                }
                else
                {
                    rt_res = op(rt_res, rhs.exec(frame));
                }
                RTS.object_setitem(rt_value, rt_item, rt_res);

                frame.traceback.Pop();
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro, op, rhs);
            return coro;
        }
        public TraffyCoroutine cont(Frame frame, TrObject o)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                frame.traceback.Push(position);

                TrObject rt_value;
                if (value.hasCont)
                {
                    var cont_value = value.cont(frame);
                    while (cont_value.MoveNext(coro.Sent))
                        yield return cont_value.Current;
                    rt_value = cont_value.Result;
                }
                else
                {
                    rt_value = value.exec(frame);
                }

                TrObject rt_item;
                if (item.hasCont)
                {
                    var cont_item = item.cont(frame);
                    while (cont_item.MoveNext(coro.Sent))
                        yield return cont_item.Current;
                    rt_item = cont_item.Result;
                }
                else
                {
                    rt_item = item.exec(frame);
                }

                RTS.object_setitem(rt_value, rt_item, o);

                frame.traceback.Pop();
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }
    }
    [Serializable]
    public class StoreAttr : TraffyLHS
    {
        public int position;

        public bool hasCont { get; set; }
        public TraffyAsm value;
        public TrStr attr;

        private InlineCache.PolyIC ic;

        [OnDeserialized]
        private StoreAttr OnDeserialized()
        {
            // check if attr.value is null
            if (attr.value == null)
            {
                throw new InvalidProgramException("attr.value is null");
            }
            ic = new InlineCache.PolyIC(attr);
            return this;
        }

        public void exec(Frame frame, TrObject o)
        {
            frame.traceback.Push(position);
            var rt_value = value.exec(frame);
            RTS.object_setic(rt_value, ic, o);
            frame.traceback.Pop();
        }

        public void execOp(Frame frame, binary_func op, TraffyAsm rhs)
        {
            frame.traceback.Push(position);
            var rt_value = value.exec(frame);
            var rt_res = RTS.object_getic(rt_value, ic);
            rt_res = op(rt_res, rhs.exec(frame));
            RTS.object_setic(rt_value, ic, rt_res);
            frame.traceback.Pop();
        }
        public TraffyCoroutine cont(Frame frame, TrObject o)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro, InlineCache.PolyIC selector)
            {
                frame.traceback.Push(position);

                var rt_value = value.exec(frame);
                if (value.hasCont)
                {
                    var cont_value = value.cont(frame);
                    while (cont_value.MoveNext(coro.Sent))
                        yield return cont_value.Current;
                    rt_value = cont_value.Result;
                }
                RTS.object_setic(rt_value, selector, o);

                frame.traceback.Pop();
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro, ic);
            return coro;
        }

        public TraffyCoroutine contOp(Frame frame, binary_func op, TraffyAsm rhs)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro, binary_func op, TraffyAsm rhs, InlineCache.PolyIC selector)
            {
                frame.traceback.Push(position);

                var rt_value = value.exec(frame);
                if (value.hasCont)
                {
                    var cont_value = value.cont(frame);
                    while (cont_value.MoveNext(coro.Sent))
                        yield return cont_value.Current;
                    rt_value = cont_value.Result;
                }

                var rt_res = RTS.object_getic(rt_value, selector);
                if (rhs.hasCont)
                {
                    var cont_rhs = rhs.cont(frame);
                    while (cont_rhs.MoveNext(coro.Sent))
                        yield return cont_rhs.Current;
                    rt_res = op(rt_res, cont_rhs.Result);
                }
                else
                {
                    rt_res = op(rt_res, rhs.exec(frame));
                }
                RTS.object_setic(rt_value, selector, rt_res);

                frame.traceback.Pop();
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro, op, rhs, ic);
            return coro;

        }
    }

}