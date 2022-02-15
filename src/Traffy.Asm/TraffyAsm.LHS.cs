using System;
using System.Collections.Generic;
using System.Linq;
using Traffy.Objects;

namespace Traffy.Asm
{

    using binary_func = Func<TrObject, TrObject, TrObject>;

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

    [Serializable]
    public class StoreListEx : TraffyLHS
    {
        public TraffyLHS[] before;
        public TraffyLHS unpack;
        public TraffyLHS[] after;
        public bool hasCont { get; set; }
        public void exec(Frame frame, TrObject o)
        {
            List<TrObject> itr = RTS.object_as_list(o);
            var left = itr.Count - before.Length - after.Length;
            if (itr.Count < 0)
            {
                throw RTS.exc_unpack_notenough(itr.Count, before.Length + after.Length);
            }
            for (int i = 0; i < before.Length; i++)
            {
                before[i].exec(frame, itr[i]);
            }
            for (int i = itr.Count - after.Length, j = 0; j < after.Length; j++, i++)
            {
                after[j].exec(frame, itr[i]);
            }
            itr = itr.GetRange(before.Length, left);
            o = RTS.object_from_list(itr);
            unpack.exec(frame, o);
        }

        public TraffyCoroutine cont(Frame frame, TrObject o)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro, TrObject o)
            {
                List<TrObject> itr = RTS.object_as_list(o);
                var left = itr.Count - before.Length - after.Length;
                if (itr.Count < 0)
                {
                    throw RTS.exc_unpack_notenough(itr.Count, before.Length + after.Length);
                }
                for (int i = 0; i < before.Length; i++)
                {
                    if (before[i].hasCont)
                    {
                        var cont_elt = before[i].cont(frame, itr[i]);
                        while (cont_elt.MoveNext(coro.Sent))
                            yield return cont_elt.Current;
                    }
                    else
                    {
                        before[i].exec(frame, itr[i]);
                    }
                }
                for (int i = itr.Count - after.Length, j = 0; j < after.Length; j++, i++)
                {
                    if (after[j].hasCont)
                    {
                        var cont_elt = after[j].cont(frame, itr[i]);
                        while (cont_elt.MoveNext(coro.Sent))
                            yield return cont_elt.Current;
                    }
                    else
                    {
                        after[j].exec(frame, itr[i]);
                    }
                }
                itr = itr.GetRange(before.Length, left);
                o = RTS.object_from_list(itr);
                if (unpack.hasCont)
                {
                    var cont_unpack = unpack.cont(frame, o);
                    while (cont_unpack.MoveNext(coro.Sent))
                        yield return cont_unpack.Current;
                }
                else
                {
                    unpack.exec(frame, o);
                }
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro, o);
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
        public TraffyLHS[] elts;
        public bool hasCont { get; set; }
        public void exec(Frame frame, TrObject o)
        {
            var itr = RTS.object_getiter(o);
            for (int i = 0; i < elts.Length; i++)
            {
                var elt = elts[i];
                if (!itr.MoveNext())
                {
                    throw RTS.exc_unpack_notenough(i, elts.Length);
                }
                elt.exec(frame, itr.Current);
            }
            if (itr.MoveNext())
            {
                throw RTS.exc_unpack_toomuch(elts.Length);
            }
        }

        public TraffyCoroutine cont(Frame frame, TrObject o)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro, TrObject o)
            {
                var itr = RTS.object_getiter(o);
                for (int i = 0; i < elts.Length; i++)
                {
                    var elt = elts[i];
                    if (!itr.MoveNext())
                    {
                        throw RTS.exc_unpack_notenough(i, elts.Length);
                    }
                    if (elt.hasCont)
                    {
                        var cont_elt = elt.cont(frame, itr.Current);
                        while (cont_elt.MoveNext(coro.Sent))
                        {
                            yield return cont_elt.Current;
                        }
                    }
                    else
                    {
                        elt.exec(frame, itr.Current);
                    }
                }
                if (itr.MoveNext())
                {
                    throw RTS.exc_unpack_toomuch(elts.Length);
                }
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro, o);
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

        public void execOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            var o = asm.exec(frame);
            var local = frame.load_local(slot);
            local = op(local, o);
            frame.store_local(slot, local);
        }

        public TraffyCoroutine cont(Frame frame, TrObject o)
        {
            throw new InvalidOperationException("local variable store cannot be async");
        }
        public TraffyCoroutine contOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro, binary_func op, TraffyAsm asm)
            {
                TrObject o;
                if (asm.hasCont)
                {
                    var cont_o = asm.cont(frame);
                    while (cont_o.MoveNext(coro.Sent))
                        yield return cont_o.Current;
                    o = cont_o.Result;
                }
                else
                {
                    o = asm.exec(frame);
                }
                var local = frame.load_local(slot);
                local = op(local, o);
                frame.store_local(slot, local);
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

        public void execOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            var o = asm.exec(frame);
            var global = frame.load_global(name);
            global = op(global, o);
            frame.store_global(name, global);
        }

        public TraffyCoroutine cont(Frame frame, TrObject o)
        {
            throw new InvalidOperationException("local variable store cannot be async");
        }
        public TraffyCoroutine contOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro, binary_func op, TraffyAsm asm)
            {
                TrObject o;
                if (asm.hasCont)
                {
                    var cont_o = asm.cont(frame);
                    while (cont_o.MoveNext(coro.Sent))
                        yield return cont_o.Current;
                    o = cont_o.Result;
                }
                else
                {
                    o = asm.exec(frame);
                }
                var global = frame.load_global(name);
                global = op(global, o);
                frame.store_global(name, global);
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

        public void exec(Frame frame, TrObject o)
        {
            var rt_value = value.exec(frame);
            var rt_item = item.exec(frame);
            RTS.object_setitem(rt_value, rt_item, o);
        }

        public void execOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            var rt_value = value.exec(frame);
            var rt_item = item.exec(frame);
            var res = RTS.object_getitem(rt_value, rt_item);
            res = op(res, asm.exec(frame));
            RTS.object_setitem(rt_value, rt_item, res);
        }

        public TraffyCoroutine contOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro, binary_func op, TraffyAsm asm)
            {
                TrObject rt_value;
                TrObject rt_item;

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

                TrObject res = RTS.object_getitem(rt_value, rt_item);
                if (asm.hasCont)
                {
                    var cont_o = asm.cont(frame);
                    while (cont_o.MoveNext(coro.Sent))
                        yield return cont_o.Current;
                    res = op(res, cont_o.Result);
                }
                else
                {
                    res = op(res, asm.exec(frame));
                }
                RTS.object_setitem(rt_value, rt_item, res);
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro, op, asm);
            return coro;
        }
        public TraffyCoroutine cont(Frame frame, TrObject o)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro, TrObject o)
            {
                TrObject rt_value;
                TrObject rt_item;

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
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro, o);
            return coro;
        }
    }
    [Serializable]
    public class StoreAttr : TraffyLHS
    {
        public TraffyAsm value;
        public string attr;

        public bool hasCont { get; set; }
        public void exec(Frame frame, TrObject o)
        {
            var rt_value = value.exec(frame);
            RTS.object_setattr(rt_value, attr, o);
        }

        public void execOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            var rt_value = value.exec(frame);
            var res = RTS.object_getattr(rt_value, attr);
            res = op(res, asm.exec(frame));
            RTS.object_setattr(rt_value, attr, res);
        }
        public TraffyCoroutine cont(Frame frame, TrObject o)
        {

            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro, TrObject o)
            {
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

                RTS.object_setattr(rt_value, attr, o);
            }

            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro, o);
            return coro;
        }

        public TraffyCoroutine contOp(Frame frame, binary_func op, TraffyAsm asm)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro, binary_func op, TraffyAsm asm)
            {
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

                TrObject res = RTS.object_getattr(rt_value, attr);
                if (asm.hasCont)
                {
                    var cont_o = asm.cont(frame);
                    while (cont_o.MoveNext(coro.Sent))
                        yield return cont_o.Current;
                    res = op(res, cont_o.Result);
                }
                else
                {
                    res = op(res, asm.exec(frame));
                }
                RTS.object_setattr(rt_value, attr, res);
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro, op, asm);
            return coro;
        }
    }

}