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
        public int position;
        public TraffyAsm left;
        public TraffyAsm right;


        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            TrObject rt_res;
            rt_res = left.exec(frame);
            if (!RTS.object_bool(rt_res))
            {
                goto end;
            }
            rt_res = right.exec(frame);
        end:
            frame.traceback.Pop();
            return rt_res;
        }

        // 'cont' is implemented by processing the continuations in the nested expressions
        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            TrObject rt_res;
            rt_res = left.hasCont ? await left.cont(frame) : left.exec(frame);
            if (!RTS.object_bool(rt_res))
            {
                goto end;
            }
            rt_res = right.hasCont ? await right.cont(frame) : right.exec(frame);
        end:
            frame.traceback.Pop();
            return rt_res;
        }
    }

    [Serializable]
    public class BoolOr2 : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;
        public TraffyAsm left;
        public TraffyAsm right;

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            TrObject rt_res;
            rt_res = left.exec(frame);
            if (RTS.object_bool(rt_res))
            {
                goto end;
            }
            rt_res = right.exec(frame);
        end:
            frame.traceback.Pop();
            return rt_res;
        }
        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            TrObject rt_res;
            rt_res = left.hasCont ? await left.cont(frame) : left.exec(frame);
            if (RTS.object_bool(rt_res))
            {
                goto end;
            }
            rt_res = right.hasCont ? await right.cont(frame) : right.exec(frame);
        end:
            frame.traceback.Pop();
            return rt_res;
        }
    }

    [Serializable]
    public class BoolAnd : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;
        public TraffyAsm left;
        public TraffyAsm[] comparators;

        public TrObject exec(Frame frame)
        {
            TrObject rt_res;
            frame.traceback.Push(position);
            rt_res = left.exec(frame);
            if (!RTS.object_bool(rt_res))
            {
                goto end;
            }

            foreach (var right in comparators)
            {
                rt_res = right.exec(frame);
                if (!RTS.object_bool(rt_res))
                    break;
            }
        end:
            frame.traceback.Pop();
            return rt_res;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            TrObject rt_res;
            rt_res = left.hasCont ? await left.cont(frame) : left.exec(frame);
            if (!RTS.object_bool(rt_res))
            {
                goto end;
            }

            foreach (var right in comparators)
            {
                rt_res = right.hasCont ? await right.cont(frame) : right.exec(frame);
                if (!RTS.object_bool(rt_res))
                    break;
            }
        end:
            frame.traceback.Pop();
            return rt_res;
        }


    }

    [Serializable]
    public class BoolOr : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;
        public TraffyAsm left;
        public TraffyAsm[] comparators;

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            TrObject rt_res;
            rt_res = left.exec(frame);
            if (RTS.object_bool(rt_res))
            {
                goto end;
            }

            foreach (var right in comparators)
            {
                rt_res = right.exec(frame);
                if (RTS.object_bool(rt_res))
                    break;
            }
        end:
            frame.traceback.Pop();
            return rt_res;
        }
        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            TrObject rt_res;
            rt_res = left.hasCont ? await left.cont(frame) : left.exec(frame);
            if (RTS.object_bool(rt_res))
            {
                goto end;
            }

            foreach (var right in comparators)
            {
                rt_res = right.hasCont ? await right.cont(frame) : right.exec(frame);
                if (RTS.object_bool(rt_res))
                    break;
            }
        end:
            frame.traceback.Pop();
            return rt_res;
        }
    }

    [Serializable]
    public class NamedExpr : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;

        public TraffyLHS lhs;

        public TraffyAsm expr;

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            TrObject rt_res;
            rt_res = expr.exec(frame);
            lhs.exec(frame, rt_res);
            frame.traceback.Pop();
            return rt_res;
        }
        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            TrObject rt_res;
            rt_res = expr.hasCont ? await expr.cont(frame) : expr.exec(frame);
            lhs.exec(frame, rt_res);
            frame.traceback.Pop();
            return rt_res;
        }
    }

    [Serializable]
    public class CmpOp : TraffyAsm
    {
        public bool hasCont => op < 0;
        public int op;
        public int position;
        public TraffyAsm left;

        public TraffyAsm[] comparators;
        private binary_func opfunc;

        [OnDeserialized]
        internal CmpOp OnDeserializedMethod()
        {
            var bop = op < 0 ? -op : op;
            opfunc = RTS.OOOFuncs[bop];
            return this;
        }

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_l = left.exec(frame);
            TrObject rt_res = RTS.object_none;
            foreach (var operand in comparators)
            {
                var rt_r = operand.exec(frame);
                rt_res = opfunc(rt_l, rt_r);
                if (RTS.object_bool(rt_res))
                {
                    rt_l = rt_r;
                    continue;
                }
                break;
            }
            frame.traceback.Pop();
            return rt_res;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_l = left.hasCont ? await left.cont(frame) : left.exec(frame);
            TrObject rt_res = RTS.object_none;
            foreach (var operand in comparators)
            {
                var rt_r = operand.hasCont ? await operand.cont(frame) : operand.exec(frame);
                rt_res = opfunc(rt_l, rt_r);
                if (RTS.object_bool(rt_res))
                {
                    rt_l = rt_r;
                    continue;
                }
                break;
            }
            frame.traceback.Pop();
            return rt_res;
        }
    }

    [Serializable]
    public class BinOp : TraffyAsm
    {
        public bool hasCont => op < 0;
        public int op;
        public int position;
        public TraffyAsm left;
        public TraffyAsm right;
        private binary_func opfunc;

        [OnDeserialized]
        internal BinOp OnDeserializedMethod()
        {
            opfunc = RTS.OOOFuncs[op < 0 ? -op : op];
            return this;
        }

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_l = left.exec(frame);
            var rt_r = right.exec(frame);
            var rt_res = opfunc(rt_l, rt_r);
            frame.traceback.Pop();
            return rt_res;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_l = left.hasCont ? await left.cont(frame) : left.exec(frame);
            var rt_r = right.hasCont ? await right.cont(frame) : right.exec(frame);
            var rt_res = opfunc(rt_l, rt_r);
            frame.traceback.Pop();
            return rt_res;
        }
    }

    [Serializable]
    public class UnaryOp : TraffyAsm
    {
        public bool hasCont => op < 0;
        public int op;
        public int position;
        public TraffyAsm operand;
        private unary_func opfunc;

        [OnDeserialized]
        internal UnaryOp OnDeserializedMethod()
        {
            opfunc = RTS.OOFuncs[op < 0 ? -op : op];
            return this;
        }

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_operand = operand.exec(frame);
            var rt_res = opfunc(rt_operand);
            frame.traceback.Pop();
            return rt_res;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_operand = operand.hasCont ? await operand.cont(frame) : operand.exec(frame);
            var rt_res = opfunc(rt_operand);
            frame.traceback.Pop();
            return rt_res;
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
            var rt_res = new TrFunc(
                freevars: freevars,
                globals: frame.func.globals,
                default_args: rt_default_args,
                fptr: fptr
            );

            return rt_res;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
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
                    rt_default_args[i] = (default_args[i].slot, default_args[i].value.hasCont ? await default_args[i].value.cont(frame) : default_args[i].value.exec(frame));
                }
            }
            else
            {
                rt_default_args = empty_default_args;
            }
            var rt_res = new TrFunc(
                freevars: freevars,
                globals: frame.func.globals,
                default_args: rt_default_args,
                fptr: fptr
            );

            return rt_res;
        }
    }

    [Serializable]
    public class CallEx : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;
        public TraffyAsm func;
        public SequenceElement[] args;
        public (TrObject key, TraffyAsm value)[] kwargs;

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);

            var rt_func = func.exec(frame);
            var rt_args = new BList<TrObject> { };
            for (int i = 0; i < args.Length; i++)
            {
                var elt = args[i].value.exec(frame);
                if (args[i].unpack)
                {
                    var rt_itr = RTS.object_getiter(elt);
                    while (rt_itr.MoveNext())
                    {
                        rt_args.Add(rt_itr.Current);
                    }
                }
                else
                {
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

            var rt_res = RTS.object_call_ex(rt_func, rt_args, rt_kwargs);
            frame.traceback.Pop();
            return rt_res;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);

            var rt_func = func.hasCont ? await func.cont(frame) : func.exec(frame);
            var rt_args = new BList<TrObject> { };
            for (int i = 0; i < args.Length; i++)
            {
                var elt = args[i].value.hasCont ? await args[i].value.cont(frame) : args[i].value.exec(frame);
                if (args[i].unpack)
                {
                    var rt_itr = RTS.object_getiter(elt);
                    while (rt_itr.MoveNext())
                    {
                        rt_args.Add(rt_itr.Current);
                    }
                }
                else
                {
                    rt_args.Add(elt);
                }
            }
            Dictionary<TrObject, TrObject> rt_kwargs = null;
            if (kwargs.Length != 0)
            {
                rt_kwargs = RTS.baredict_create();
                foreach (var (key, value) in kwargs)
                {
                    var rt_value = value.hasCont ? await value.cont(frame) : value.exec(frame);
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

            var rt_res = RTS.object_call_ex(rt_func, rt_args, rt_kwargs);
            frame.traceback.Pop();
            return rt_res;
        }
    }


    [Serializable]
    public class GuessVar : TraffyAsm // used for CPython class scope semantics
    {
        public TrStr name;
        public int position;
        public bool hasCont => false;

        public MonoAsync<TrObject> cont(Frame frame)
        {
            throw new InvalidOperationException("variables shall not produce coroutines");
        }

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            var metadata = frame.func.fptr.metadata;
            int idx = 0;
            TrObject res;
            if ((idx = metadata.localnames.IndexOf(name.value)) != -1
               && (res = frame.localvars[idx].Value) != null)
            {
                goto end;
            }
            if ((idx = metadata.freenames.IndexOf(name.value)) != -1
               && (res = frame.func.freevars[idx].Value) != null)
            {
                goto end;
            }
            if (frame.func.globals.TryGetValue(name, out res))
            {
                goto end;
            }
            throw new NameError(name.value, $"name '{name.value}' is not defined");
        end:
            frame.traceback.Pop();
            return res;
        }
    }

    [Serializable]
    public class LocalVar : TraffyAsm
    {
        public int slot;
        public int position;
        public bool hasCont => false;

        public MonoAsync<TrObject> cont(Frame frame)
        {
            throw new InvalidOperationException("variables shall not produce coroutines");
        }

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            var localval = frame.load_local(slot);
            frame.traceback.Pop();
            return localval;
        }
    }
    [Serializable]
    public class GlobalVar : TraffyAsm
    {
        public TrObject name;
        public int position;
        public bool hasCont => false;

        public MonoAsync<TrObject> cont(Frame frame)
        {
            throw new InvalidOperationException("variables shall not produce coroutines");
        }

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            var globalval = frame.load_global(name);
            frame.traceback.Pop();
            return globalval;
        }
    }
    [Serializable]
    public class Constant : TraffyAsm
    {
        public TrObject o;

        public bool hasCont => false;

        public MonoAsync<TrObject> cont(Frame frame)
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
        public int position;
        public DictEntry[] entries;

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            Dictionary<TrObject, TrObject> dict = RTS.baredict_create();
            for (int i = 0; i < entries.Length; i++)
            {
                DictEntry entry = entries[i];
                if (entry.key == null)
                {
                    var rt_map = entry.value.exec(frame);
                    RTS.baredict_extend(dict, rt_map);
                }
                else
                {
                    var rt_key = entry.key.exec(frame);
                    var rt_value = entry.value.exec(frame);
                    RTS.baredict_add(dict, rt_key, rt_value);
                }
            }
            var rt_res = RTS.object_from_baredict(dict);
            frame.traceback.Pop();
            return rt_res;
        }
        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            Dictionary<TrObject, TrObject> dict = RTS.baredict_create();
            for (int i = 0; i < entries.Length; i++)
            {
                DictEntry entry = entries[i];
                if (entry.key == null)
                {
                    var rt_map = entry.value.hasCont ? await entry.value.cont(frame) : entry.value.exec(frame);
                    RTS.baredict_extend(dict, rt_map);
                }
                else
                {
                    var rt_key = entry.key.hasCont ? await entry.key.cont(frame) : entry.key.exec(frame);
                    var rt_value = entry.value.hasCont ? await entry.value.cont(frame) : entry.value.exec(frame);
                    RTS.baredict_add(dict, rt_key, rt_value);
                }
            }
            var rt_res = RTS.object_from_baredict(dict);
            frame.traceback.Pop();
            return rt_res;
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
        public int position;
        public SequenceElement[] elements;

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
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
            var rt_res = RTS.object_from_barelist(lst);
            frame.traceback.Pop();
            return rt_res;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            List<TrObject> lst = RTS.barelist_create();
            for (int i = 0; i < elements.Length; i++)
            {
                SequenceElement elt = elements[i];
                var rt_each = elt.value.hasCont ? await elt.value.cont(frame) : elt.value.exec(frame);
                if (elt.unpack)
                    RTS.barelist_extend(lst, rt_each);
                else
                    RTS.barelist_add(lst, rt_each);
            }
            var rt_res = RTS.object_from_barelist(lst);
            frame.traceback.Pop();
            return rt_res;
        }
    }

    [Serializable]
    public class Tuple : TraffyAsm
    {

        public bool hasCont { get; set; }
        public int position;
        public SequenceElement[] elements;

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
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
            var rt_res = RTS.object_from_barearray(lst.ToArray());
            frame.traceback.Pop();
            return rt_res;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            List<TrObject> lst = RTS.barelist_create();
            for (int i = 0; i < elements.Length; i++)
            {
                SequenceElement elt = elements[i];
                var rt_each = elt.value.hasCont ? await elt.value.cont(frame) : elt.value.exec(frame);
                if (elt.unpack)
                    RTS.barelist_extend(lst, rt_each);
                else
                    RTS.barelist_add(lst, rt_each);
            }
            var rt_res = RTS.object_from_barearray(lst.ToArray());
            frame.traceback.Pop();
            return rt_res;
        }
    }



    [Serializable]
    public class Set : TraffyAsm
    {

        public bool hasCont { get; set; }
        public int position;
        public SequenceElement[] elements;

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
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
            var rt_res = RTS.object_from_bareset(set);
            frame.traceback.Pop();
            return rt_res;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            HashSet<TrObject> set = RTS.bareset_create();
            for (int i = 0; i < elements.Length; i++)
            {
                SequenceElement elt = elements[i];
                var rt_each = elt.value.hasCont ? await elt.value.cont(frame) : elt.value.exec(frame);
                if (elt.unpack)
                    RTS.bareset_extend(set, rt_each);
                else
                    RTS.bareset_add(set, rt_each);
            }
            var rt_res = RTS.object_from_bareset(set);
            frame.traceback.Pop();
            return rt_res;
        }
    }

    [Serializable]
    public class Attribute : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;

        public TraffyAsm value;
        public TrStr attr;

        private InlineCache.PolyIC ic;

        [OnDeserialized]
        public Attribute OnDeserialized()
        {
            attr = attr.Interned();
            if (attr.value == null)
            {
                throw new InvalidProgramException("attr.value is null");
            }
            ic = new InlineCache.PolyIC(attr);
            return this;
        }

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_value = value.exec(frame);
            rt_value = RTS.object_getic(rt_value, ic);
            frame.traceback.Pop();
            return rt_value;
        }
        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_value = value.hasCont ? await value.cont(frame) : value.exec(frame);
            rt_value = RTS.object_getic(rt_value, ic);
            frame.traceback.Pop();
            return rt_value;
        }
    }

    [Serializable]
    public class Subscript : TraffyAsm
    {
        public bool hasCont { get; set; }
        public int position;

        public TraffyAsm value;
        public TraffyAsm item;

        public TrObject exec(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_value = value.exec(frame);
            var rt_item = item.exec(frame);
            rt_value = RTS.object_getitem(rt_value, rt_item);
            frame.traceback.Pop();
            return rt_value;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            frame.traceback.Push(position);
            var rt_value = value.hasCont ? await value.cont(frame) : value.exec(frame);
            var rt_item = item.hasCont ? await item.cont(frame) : item.exec(frame);
            rt_value = RTS.object_getitem(rt_value, rt_item);
            frame.traceback.Pop();
            return rt_value;
        }
    }

    [Serializable]
    public class Yield : TraffyAsm
    {
        public bool hasCont => true;
        public int position;
        public TraffyAsm value;
        public async MonoAsync<TrObject> cont(Frame frame)
        {
            return await ExtMonoAsyn.Yield(value.hasCont ? await value.cont(frame) : value.exec(frame));
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
        public int position;

        public TraffyAsm value;

        public TrObject exec(Frame frame)
        {
            throw new NotImplementedException();
        }


        private async MonoAsync<TrObject> contIfInnerCont(Frame frame)
        {
            var rt_value = await value.cont(frame);
            MonoAsync<TrObject> co = RTS.coroutine_of_object(rt_value);
            return await co;
        }

        public async MonoAsync<TrObject> cont(Frame frame)
        {
            var rt_value = value.hasCont ? await value.cont(frame) : value.exec(frame);
            MonoAsync<TrObject> co = RTS.coroutine_of_object(rt_value);
            return await co;
        }
    }
}
