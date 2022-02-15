using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Traffy.Objects;

namespace Traffy.Asm
{
    using binary_func = Func<TrObject, TrObject, TrObject>;
    using unary_func = Func<TrObject, TrObject>;

    [Serializable]
    public class BoolAnd2 : TraffyAsm
    {
        public bool hasCont { get; set; }
        public TraffyAsm left;
        public TraffyAsm right;

        public TraffyCoroutine cont(Frame frame)
        {
            var coro = new TraffyCoroutine();
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                TrObject rt_val;
                if (left.hasCont)
                {
                    var cont_left = left.cont(frame);
                    while (cont_left.MoveNext(coro.Sent))
                    {
                        yield return cont_left.Current;
                    }
                    rt_val = cont_left.Result;
                }
                else
                {
                    rt_val = left.exec(frame);
                }

                if (!RTS.object_bool(rt_val))
                {
                    coro.Result = rt_val;
                    yield break;
                }

                if (right.hasCont)
                {
                    var cont_right = right.cont(frame);
                    while (cont_right.MoveNext(coro.Sent))
                    {
                        yield return cont_right.Current;
                    }
                    rt_val = cont_right.Result;
                }
                else
                {
                    rt_val = left.exec(frame);
                }
                coro.Result = rt_val;
                yield break;
            }
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            var rt_left = left.exec(frame);
            if (!RTS.object_bool(rt_left))
            {
                return rt_left;
            }
            return right.exec(frame);
        }
    }

    [Serializable]
    public class BoolOr2 : TraffyAsm
    {
        public bool hasCont { get; set; }
        public TraffyAsm left;
        public TraffyAsm right;

        public TrObject exec(Frame frame)
        {
            var rt_left = left.exec(frame);
            if (RTS.object_bool(rt_left))
            {
                return rt_left;
            }
            return right.exec(frame);
        }
        public TraffyCoroutine cont(Frame frame)
        {
            var coro = new TraffyCoroutine();
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                TrObject rt_val;
                if (left.hasCont)
                {
                    var cont_left = left.cont(frame);
                    while (cont_left.MoveNext(coro.Sent))
                    {
                        yield return cont_left.Current;
                    }
                    rt_val = cont_left.Result;
                }
                else
                {
                    rt_val = left.exec(frame);
                }

                if (RTS.object_bool(rt_val))
                {
                    coro.Result = rt_val;
                    yield break;
                }

                if (right.hasCont)
                {
                    var cont_right = right.cont(frame);
                    while (cont_right.MoveNext(coro.Sent))
                    {
                        yield return cont_right.Current;
                    }
                    rt_val = cont_right.Result;
                }
                else
                {
                    rt_val = left.exec(frame);
                }
                coro.Result = rt_val;
                yield break;
            }
            coro.generator = mkCont(frame, coro);
            return coro;
        }
    }

    [Serializable]
    public class BoolAnd : TraffyAsm
    {
        public bool hasCont { get; set; }
        public TraffyAsm left;
        public TraffyAsm[] comparators;

        public TraffyCoroutine cont(Frame frame)
        {
            var coro = new TraffyCoroutine();
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                TrObject rt_left;
                if (left.hasCont)
                {
                    var cont_left = left.cont(frame);
                    while (cont_left.MoveNext(coro.Sent))
                    {
                        yield return cont_left.Current;
                    }
                    rt_left = cont_left.Result;
                }
                else
                {
                    rt_left = left.exec(frame);
                }

                if (!RTS.object_bool(rt_left))
                {
                    coro.Result = rt_left;
                    yield break;
                }

                foreach (var right in comparators)
                {
                    if (left.hasCont)
                    {
                        var cont_right = right.cont(frame);
                        while (cont_right.MoveNext(coro.Sent))
                        {
                            yield return cont_right.Current;
                        }
                        rt_left = cont_right.Result;
                    }
                    else
                    {
                        rt_left = right.exec(frame);
                    }
                    if (!RTS.object_bool(rt_left))
                        break;
                }
                coro.Result = rt_left;
                yield break;
            }
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            TrObject rt_left = left.exec(frame);
            if (!RTS.object_bool(rt_left))
                return rt_left;

            foreach (var right in comparators)
            {
                rt_left = right.exec(frame);
                if (!RTS.object_bool(rt_left))
                    break;
            }
            return rt_left;
        }
    }

    [Serializable]
    public class BoolOr : TraffyAsm
    {
        public bool hasCont { get; set; }
        public TraffyAsm left;
        public TraffyAsm[] comparators;

        public TraffyCoroutine cont(Frame frame)
        {
            var coro = new TraffyCoroutine();
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                TrObject rt_left;
                if (left.hasCont)
                {
                    var cont_left = left.cont(frame);
                    while (cont_left.MoveNext(coro.Sent))
                    {
                        yield return cont_left.Current;
                    }
                    rt_left = cont_left.Result;
                }
                else
                {
                    rt_left = left.exec(frame);
                }

                if (RTS.object_bool(rt_left))
                {
                    coro.Result = rt_left;
                    yield break;
                }

                foreach (var right in comparators)
                {
                    if (left.hasCont)
                    {
                        var cont_right = right.cont(frame);
                        while (cont_right.MoveNext(coro.Sent))
                        {
                            yield return cont_right.Current;
                        }
                        rt_left = cont_right.Result;
                    }
                    else
                    {
                        rt_left = right.exec(frame);
                    }
                    if (RTS.object_bool(rt_left))
                        break;
                }
                coro.Result = rt_left;
                yield break;
            }
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            TrObject rt_left = left.exec(frame);
            if (RTS.object_bool(rt_left))
                return rt_left;

            foreach (var right in comparators)
            {
                rt_left = right.exec(frame);
                if (RTS.object_bool(rt_left))
                    break;
            }
            return rt_left;
        }
    }

    [Serializable]
    public class NamedExpr : TraffyAsm
    {
        public bool hasCont { get; set; }

        public TraffyLHS lhs;

        public TraffyAsm expr;

        public TraffyCoroutine cont(Frame frame)
        {

            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                TrObject o;
                if (expr.hasCont)
                {
                    var cont_expr = expr.cont(frame);
                    while (cont_expr.MoveNext(coro.Sent))
                    {
                        yield return cont_expr.Current;
                    }
                    o = cont_expr.Result;
                }
                else
                {
                    o = expr.exec(frame);
                }
                if (lhs.hasCont)
                {
                    var cont_lhs = lhs.cont(frame, o);
                    while (cont_lhs.MoveNext(coro.Sent))
                    {
                        yield return cont_lhs.Current;
                    }
                }
                else
                {
                    lhs.exec(frame, o);
                }
                coro.Result = o;
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            var o = expr.exec(frame);
            lhs.exec(frame, o);
            return o;
        }
    }

    [Serializable]
    public class CmpOp : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int op;
        public TraffyAsm left;

        public TraffyAsm[] comparators;
        private binary_func opfunc;

        [OnDeserialized]
        internal CmpOp OnDeserializedMethod()
        {
            opfunc = RTS.OOOFuncs[op];
            return this;
        }

        public TrObject exec(Frame frame)
        {
            var l = left.exec(frame);
            TrObject res = RTS.object_none;
            foreach (var operand in comparators)
            {
                var r = operand.exec(frame);
                res = opfunc(l, r);
                if (RTS.object_bool(res))
                {
                    l = r;
                    continue;
                }
                break;
            }
            return res;
        }

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                TrObject rt_left;
                if (left.hasCont)
                {
                    var cont = left.cont(frame);
                    while (cont.MoveNext(coro.Sent))
                        yield return cont.Current;
                    rt_left = cont.Result;
                }
                else
                {
                    rt_left = left.exec(frame);
                }

                TrObject res = RTS.object_none;
                foreach (var operand in comparators)
                {
                    TrObject rt_operand;
                    if (operand.hasCont)
                    {
                        var cont = operand.cont(frame);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                        rt_operand = cont.Result;
                    }
                    else
                    {
                        rt_operand = operand.exec(frame);
                    }

                    res = opfunc(rt_left, rt_operand);
                    if (RTS.object_bool(res))
                    {
                        rt_left = rt_operand;
                        continue;
                    }
                    break;
                }
                coro.Result = res;
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }
    }

    [Serializable]
    public class BinOp : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int op;
        public TraffyAsm left;

        public TraffyAsm right;
        private binary_func opfunc;

        [OnDeserialized]
        internal BinOp OnDeserializedMethod()
        {
            opfunc = RTS.OOOFuncs[op];
            return this;
        }

        public TrObject exec(Frame frame)
        {
            var l = left.exec(frame);
            var r = right.exec(frame);
            return opfunc(l, r);
        }

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                TrObject l;
                if (left.hasCont)
                {
                    var l_cont = left.cont(frame);
                    while (l_cont.MoveNext(coro.Sent))
                    {
                        yield return l_cont.Current;
                    }
                    l = l_cont.Result;
                }
                else
                {
                    l = left.exec(frame);
                }

                TrObject r;
                if (right.hasCont)
                {
                    var r_cont = right.cont(frame);
                    while (r_cont.MoveNext(coro.Sent))
                    {
                        yield return r_cont.Current;
                    }
                    r = r_cont.Result;
                }
                else
                {
                    r = right.exec(frame);
                }
                coro.Result = opfunc(l, r);
                yield break;
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }
    }

    [Serializable]
    public class UnaryOp : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int op;
        public TraffyAsm operand;
        private unary_func opfunc;

        [OnDeserialized]
        internal UnaryOp OnDeserializedMethod()
        {
            opfunc = RTS.OOFuncs[op];
            return this;
        }

        public TrObject exec(Frame frame)
        {
            var val = operand.exec(frame);
            return opfunc(val);
        }

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                TrObject val;
                if (!operand.hasCont)
                {
                    val = operand.exec(frame);
                }
                else
                {
                    var cont = operand.cont(frame);
                    while (cont.MoveNext(coro.Sent))
                        yield return cont.Current;
                    val = cont.Result;
                }
                coro.Result = opfunc(val);
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }
    }

    [Serializable]
    public struct DefaultArgEntry
    {
        public int slot;
        public TraffyAsm value;
    }

    [Serializable]
    public class Lambda : TraffyAsm
    {
        private static Variable[] empty_freevars = new Variable[0];
        private static (int, TrObject)[] empty_default_args = new (int, TrObject)[0];
        public TrFuncPointer fptr;
        public DefaultArgEntry[] default_args;
        public int[] freeslots;

        public bool hasCont { get; set; }

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                Variable[] freevars;
                if (freeslots.Length != 0)
                {
                    freevars = new Variable[freeslots.Length];
                    for (int i = 0; i < freevars.Length; i++)
                    {
                        freevars[i] = frame.load_reference(freeslots[i]);
                    }
                }
                else
                {
                    freevars = empty_freevars;
                }

                (int, TrObject)[] rt_default_args;
                if (default_args.Length != 0)
                {
                    rt_default_args = new (int, TrObject)[default_args.Length];
                    for (int i = 0; i < rt_default_args.Length; i++)
                    {
                        var value = default_args[i].value;
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
                        rt_default_args[i] = (default_args[i].slot, rt_value);
                    }
                }
                else
                {
                    rt_default_args = empty_default_args;
                }
                coro.Result = new TrFunc(
                    freevars: freevars,
                    globals: frame.func.globals,
                    default_args: rt_default_args,
                    fptr: fptr
                );
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            Variable[] freevars;
            if (freeslots.Length != 0)
            {
                freevars = new Variable[freeslots.Length];
                for (int i = 0; i < freevars.Length; i++)
                {
                    freevars[i] = frame.load_reference(freeslots[i]);
                }
            }
            else
            {
                freevars = empty_freevars;
            }

            (int, TrObject)[] rt_default_args;
            if (default_args.Length != 0)
            {
                rt_default_args = new (int, TrObject)[default_args.Length];
                for (int i = 0; i < rt_default_args.Length; i++)
                {
                    rt_default_args[i] = (default_args[i].slot, default_args[i].value.exec(frame));
                }
            }
            else
            {
                rt_default_args = empty_default_args;
            }
            return new TrFunc(
                freevars: freevars,
                globals: frame.func.globals,
                default_args: rt_default_args,
                fptr: fptr
            );
        }
    }

    [Serializable]
    public class CallEx : TraffyAsm
    {
        public bool hasCont { get; set; }
        public TraffyAsm func;
        public SequenceElement[] args;
        public (TrObject key, TraffyAsm value)[] kwargs;

        public TrObject exec(Frame frame)
        {
            var rt_func = func.exec(frame);
            var rt_args = new BList<TrObject> { };
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].unpack)
                {
                    var elt = args[i].value.exec(frame);
                    var itr = RTS.object_getiter(elt);
                    while (itr.MoveNext())
                    {
                        rt_args.Add(itr.Current);
                    }
                }
                else
                {
                    var elt = args[i].value.exec(frame);
                    rt_args.Add(elt);
                }
            }
            Dictionary<TrObject, TrObject> rt_kwargs = null;
            if (kwargs.Length != 0)
            {
                rt_kwargs = RTS.baredict_create();
                foreach (var (key, value) in kwargs)
                {
                    var rt_value = value.exec(frame);
                    if (key == null)
                    {
                        RTS.baredict_extend(rt_kwargs, rt_value);
                    }
                    else
                    {
                        RTS.baredict_add(rt_kwargs, key, rt_value);
                    }
                }
            }
            return RTS.object_call_ex(rt_func, rt_args, rt_kwargs);
        }

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                TrObject rt_func;
                if (func.hasCont)
                {
                    var cont = func.cont(frame);
                    while (cont.MoveNext(coro.Sent))
                        yield return cont.Current;
                    rt_func = cont.Result;
                }
                else
                {
                    rt_func = func.exec(frame);
                }
                var rt_args = new BList<TrObject> { };
                for (int i = 0; i < args.Length; i++)
                {
                    var elt = args[i].value;

                    TrObject rt_elt;
                    if (elt.hasCont)
                    {
                        var cont = elt.cont(frame);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                        rt_elt = cont.Result;
                    }
                    else
                    {
                        rt_elt = elt.exec(frame);
                    }

                    if (args[i].unpack)
                    {
                        var itr = RTS.object_getiter(rt_elt);
                        while (itr.MoveNext())
                        {
                            rt_args.Add(itr.Current);
                        }
                    }
                    else
                    {
                        rt_args.Add(rt_elt);
                    }
                }
                Dictionary<TrObject, TrObject> rt_kwargs = null;
                if (kwargs.Length != 0)
                {
                    rt_kwargs = RTS.baredict_create();
                    foreach (var (key, value) in kwargs)
                    {

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

                        if (key == null)
                        {
                            RTS.baredict_extend(rt_kwargs, rt_value);
                        }
                        else
                        {
                            RTS.baredict_add(rt_kwargs, key, rt_value);
                        }
                    }
                }
                coro.Result = RTS.object_call_ex(rt_func, rt_args, rt_kwargs);
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }
    }

    // [Serializable]
    // public class Call : TraffyAsm
    // {
    //     public bool hasCont { get; set; }
    //     public TraffyAsm func;
    //     public TraffyAsm[] args;

    //     public TrObject exec(Frame frame)
    //     {
    //         var rt_func = func.exec(frame);
    //         var rt_args = new BList<TrObject>();
    //         for (int i = 0; i < args.Length; i++)
    //         {
    //             rt_args.Add(args[i].exec(frame));
    //         }
    //         return RTS.object_call(rt_func, rt_args);
    //     }

    //     public TraffyCoroutine cont(Frame frame)
    //     {
    //         IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
    //         {
    //             TrObject rt_func;
    //             if (func.hasCont)
    //             {
    //                 var cont = func.cont(frame);
    //                 while (cont.MoveNext(coro.Sent))
    //                     yield return cont.Current;
    //                 rt_func = cont.Result;
    //             }
    //             else
    //             {
    //                 rt_func = func.exec(frame);
    //             }

    //             var rt_args = new BList<TrObject>();
    //             for (int i = 0; i < args.Length; i++)
    //             {
    //                 TrObject rt_arg;
    //                 var arg = args[i];
    //                 if (arg.hasCont)
    //                 {
    //                     var cont = arg.cont(frame);
    //                     while (cont.MoveNext(coro.Sent))
    //                         yield return cont.Current;
    //                     rt_arg = cont.Result;
    //                 }
    //                 else
    //                 {
    //                     rt_arg = arg.exec(frame);
    //                 }
    //                 rt_args.Add(rt_arg);
    //             }
    //             coro.Result = RTS.object_call(rt_func, rt_args);
    //         }
    //         var coro = new TraffyCoroutine();
    //         coro.generator = mkCont(frame, coro);
    //         return coro;
    //     }
    // }

    [Serializable]
    public class LocalVar : TraffyAsm
    {
        public int slot;
        public bool hasCont => false;

        public TraffyCoroutine cont(Frame frame)
        {
            throw new InvalidOperationException("variables shall not produce coroutines");
        }

        public TrObject exec(Frame frame)
        {
            return frame.load_local(slot);
        }
    }
    [Serializable]
    public class GlobalVar : TraffyAsm
    {
        public TrObject name;
        public bool hasCont => false;

        public TraffyCoroutine cont(Frame frame)
        {
            throw new InvalidOperationException("variables shall not produce coroutines");
        }

        public TrObject exec(Frame frame)
        {
            return frame.load_global(name);
        }
    }
    [Serializable]
    public class Constant : TraffyAsm
    {
        public TrObject o;

        public bool hasCont => false;

        public TraffyCoroutine cont(Frame frame)
        {
            throw new InvalidOperationException("constants shall not produce coroutines");
        }

        public TrObject exec(Frame frame)
        {
            return o;
        }
    }
    [Serializable]
    public struct DictEntry
    {
        [System.Diagnostics.CodeAnalysis.AllowNull] public TraffyAsm key;
        public TraffyAsm value;
    }

    [Serializable]
    public class Dict : TraffyAsm
    {

        public bool hasCont { get; set; }
        public DictEntry[] entries;

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                Dictionary<TrObject, TrObject> dict = RTS.baredict_create();
                for (int i = 0; i < entries.Length; i++)
                {
                    DictEntry entry = entries[i];
                    if (entry.key == null)
                    {
                        var map = entry.value;
                        TrObject rt_map;
                        if (map.hasCont)
                        {
                            var cont = map.cont(frame);
                            while (cont.MoveNext(coro.Sent))
                                yield return cont.Current;
                            rt_map = cont.Result;
                        }
                        else
                        {
                            rt_map = map.exec(frame);
                        }
                        RTS.baredict_extend(dict, rt_map);
                    }
                    else
                    {
                        var key = entry.key;
                        var value = entry.value;
                        TrObject rt_key;
                        if (key.hasCont)
                        {
                            var cont = key.cont(frame);
                            while (cont.MoveNext(coro.Sent))
                                yield return cont.Current;
                            rt_key = cont.Result;
                        }
                        else
                        {
                            rt_key = key.exec(frame);
                        }

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

                        RTS.baredict_add(dict, rt_key, rt_value);
                    }
                }
                coro.Result = RTS.object_from_baredict(dict);
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            Dictionary<TrObject, TrObject> dict = RTS.baredict_create();
            for (int i = 0; i < entries.Length; i++)
            {
                DictEntry entry = entries[i];
                if (entry.key == null)
                {
                    var map = entry.value.exec(frame);
                    RTS.baredict_extend(dict, map);
                }
                else
                {
                    var rt_key = entry.key.exec(frame);
                    var rt_value = entry.value.exec(frame);
                    RTS.baredict_add(dict, rt_key, rt_value);
                }
            }
            return RTS.object_from_baredict(dict);
        }
    }

    [Serializable]
    public struct SequenceElement
    {
        public bool unpack;
        public TraffyAsm value;
    }

    [Serializable]
    public class List : TraffyAsm
    {

        public bool hasCont { get; set; }
        public SequenceElement[] elements;

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                List<TrObject> lst = RTS.barelist_create();
                for (int i = 0; i < elements.Length; i++)
                {
                    SequenceElement elt = elements[i];
                    var each = elt.value;
                    TrObject rt_each;
                    if (each.hasCont)
                    {
                        var cont = each.cont(frame);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                        rt_each = cont.Result;
                    }
                    else
                    {
                        rt_each = each.exec(frame);
                    }
                    if (elt.unpack)
                        RTS.barelist_extend(lst, rt_each);
                    else
                        RTS.barelist_add(lst, rt_each);
                }
                coro.Result = RTS.object_from_barelist(lst);
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            List<TrObject> lst = RTS.barelist_create();
            for (int i = 0; i < elements.Length; i++)
            {
                SequenceElement elt = elements[i];
                var rt_each = elt.value.exec(frame);
                if (elt.unpack)
                    RTS.barelist_extend(lst, rt_each);
                else
                    RTS.barelist_add(lst, rt_each);
            }
            return RTS.object_from_barelist(lst);
        }
    }

    [Serializable]
    public class Tuple : TraffyAsm
    {

        public bool hasCont { get; set; }
        public SequenceElement[] elements;

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                List<TrObject> lst = RTS.barelist_create();
                for (int i = 0; i < elements.Length; i++)
                {
                    SequenceElement elt = elements[i];
                    var each = elt.value;
                    TrObject rt_each;
                    if (each.hasCont)
                    {
                        var cont = each.cont(frame);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                        rt_each = cont.Result;
                    }
                    else
                    {
                        rt_each = each.exec(frame);
                    }
                    if (elt.unpack)
                        RTS.barelist_extend(lst, rt_each);
                    else
                        RTS.barelist_add(lst, rt_each);
                }
                coro.Result = RTS.object_from_barearray(lst.ToArray());
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            List<TrObject> lst = RTS.barelist_create();
            for (int i = 0; i < elements.Length; i++)
            {
                SequenceElement elt = elements[i];
                var rt_each = elt.value.exec(frame);
                if (elt.unpack)
                    RTS.barelist_extend(lst, rt_each);
                else
                    RTS.barelist_add(lst, rt_each);
            }
            return RTS.object_from_barearray(lst.ToArray());
        }
    }



    [Serializable]
    public class Set : TraffyAsm
    {

        public bool hasCont { get; set; }
        public SequenceElement[] elements;

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
                HashSet<TrObject> set = RTS.bareset_create();
                for (int i = 0; i < elements.Length; i++)
                {
                    SequenceElement elt = elements[i];
                    var each = elt.value;
                    TrObject rt_each;
                    if (each.hasCont)
                    {
                        var cont = each.cont(frame);
                        while (cont.MoveNext(coro.Sent))
                            yield return cont.Current;
                        rt_each = cont.Result;
                    }
                    else
                    {
                        rt_each = each.exec(frame);
                    }
                    if (elt.unpack)
                        RTS.bareset_extend(set, rt_each);
                    else
                        RTS.bareset_add(set, rt_each);
                }
                coro.Result = RTS.object_from_bareset(set);
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            HashSet<TrObject> set = RTS.bareset_create();
            for (int i = 0; i < elements.Length; i++)
            {
                SequenceElement elt = elements[i];
                var rt_each = elt.value.exec(frame);
                if (elt.unpack)
                    RTS.bareset_extend(set, rt_each);
                else
                    RTS.bareset_add(set, rt_each);
            }
            return RTS.object_from_bareset(set);
        }
    }

    [Serializable]
    public class Attribute : TraffyAsm
    {
        public bool hasCont { get; set; }

        public TraffyAsm value;
        public string attr;
        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
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
                coro.Result = RTS.object_getattr(rt_value, attr);
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            var rt_value = value.exec(frame);
            return RTS.object_getattr(rt_value, attr);
        }
    }

    [Serializable]
    public class Subscript : TraffyAsm
    {
        public bool hasCont { get; set; }

        public TraffyAsm value;
        public TraffyAsm item;
        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
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

                TrObject rt_item;
                if (item.hasCont)
                {
                    var cont = item.cont(frame);
                    while (cont.MoveNext(coro.Sent))
                        yield return cont.Current;
                    rt_item = cont.Result;
                }
                else
                {
                    rt_item = item.exec(frame);
                }
                coro.Result = RTS.object_getitem(rt_value, rt_item);
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            var rt_value = value.exec(frame);
            var rt_item = item.exec(frame);
            return RTS.object_getitem(rt_value, rt_item);
        }
    }

    [Serializable]
    public class Yield : TraffyAsm
    {
        public bool hasCont => true;
        public TraffyAsm value;
        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
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
                yield return rt_value;
                coro.Result = coro.Sent;
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }

        public TrObject exec(Frame frame)
        {
            throw new InvalidOperationException("yield statements cannot be executed in non-generator contexts.");
        }
    }

    [Serializable]
    public class YieldFrom : TraffyAsm
    {
        public bool hasCont => true;

        public TraffyAsm value;

        public TrObject exec(Frame frame)
        {
            throw new NotImplementedException();
        }

        public TraffyCoroutine cont(Frame frame)
        {
            IEnumerator<TrObject> mkCont(Frame frame, TraffyCoroutine coro)
            {
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

                TraffyCoroutine co = RTS.coroutine_of_object(rt_value);
                while (co.MoveNext(coro.Sent))
                    yield return co.Current;
                coro.Result = co.Result;
            }
            var coro = new TraffyCoroutine();
            coro.generator = mkCont(frame, coro);
            return coro;
        }
    }
}
