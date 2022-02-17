using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Traffy.Objects
{
    using static JsonExt;
    public static class ListExt
    {
        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static void Push(this List<TrObject> self, TrObject o)
        {
            self.Add(o);
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]

        public static TrObject Peek(this List<TrObject> self)
        {
            return self[self.Count - 1];
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static TrObject Pop(this List<TrObject> self)
        {
            var i = self.Count - 1;
            var a = self[i];
            self.RemoveAt(i);
            return a;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static (TrObject, TrObject) Pop2(this List<TrObject> self)
        {
            var i = self.Count - 1;
            var a = self[i - 1];
            var b = self[i];
            self.RemoveAt(i);
            self.RemoveAt(i - 1);
            return (a, b);
        }


        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static void PopN(this List<TrObject> self, List<TrObject> other, int n)
        {
            int c = self.Count;
            for (int i = c - n; i < c; i++)
            {
                other.Add(self[i]);
            }
            self.RemoveRange(c - n, n);
            return;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static void PopN(this List<TrObject> self, TrObject[] other, int n)
        {
            int c = self.Count;
            self.CopyTo(c - n, other, 0, n);
            self.RemoveRange(c - n, n);
            return;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static void PopN(this List<TrObject> self, Dictionary<TrObject, TrObject> other, int n)
        {
            int c = self.Count;
            int start = c - n - n;
            for (int i = start; i < c; i += 2)
            {
                other[self[i]] = self[i + 1];
            }
            self.RemoveRange(start, n + n);
            return;
        }
    }

    public class Variable
    {
        public TrObject Value;
        public Variable(TrObject value)
        {
            Value = value;
        }
    }

    [Serializable]
    public struct Postion
    {
        public int line;
        public int col;
    }

    [Serializable]
    public class Span
    {
        public Postion start;
        public Postion end;
        public string filename;
    }

    [Serializable]
    public class Metadata
    {
        public Span[] positions;
        public string[] localnames;
        public string[] freenames;
        public string codename;
        public string filename;

        [OnDeserialized]
        internal Metadata OnDeserializedMethod()
        {
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i].filename = filename;
            }
            return this;
        }
    }

    // |arg|vararg|kwonlys|kwarg|
    [Serializable]
    public class TrFuncPointer
    {
        public bool hasvararg;
        public bool haskwarg;
        public int posargcount;
        public int allargcount;
        public Dictionary<int, TrObject> kwindices;
        public Traffy.Asm.TraffyAsm code;
        public Metadata metadata;
        static Variable[] empty_freevars = new Variable[0];
        static (int, TrObject)[] empty_default_args = new (int, TrObject)[0];
        public void Exec(Dictionary<TrObject, TrObject> globals)
        {
            if (code.hasCont)
            {
                throw new InvalidOperationException("cannot eval the code that has continuations");
            }
            var func = new TrFunc(empty_freevars, globals, empty_default_args, this);
            ((TrObject)func).Call();
        }
    }

    // runtime
    public record TrFunc(
        Variable[] freevars,
        Dictionary<TrObject, TrObject> globals,
        (int slot, TrObject value)[] default_args,
        TrFuncPointer fptr
     ) : TrObject
    {
        public Dictionary<TrObject, TrObject> __dict__ => null;

        public static TrClass CLASS;

        public TrClass Class => CLASS;


        [Mark(ModuleInit.ClasInitToken)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrFunc>();
            CLASS.Name = "function";
            CLASS.IsFixed = true;
            CLASS.IsSealed = true;
            CLASS.__new = TrDict.datanew;
            TrClass.TypeDict[typeof(TrFunc)] = CLASS;
        }

        [Mark(typeof(TrFunc))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            var clsobj = args[0];
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
        TrObject AsObject => this;
        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            var localvars = new Variable[fptr.metadata.localnames.Length];
            if (default_args.Length != 0)
                for (int i = 0, j = 0; i < localvars.Length; i++)
                {
                    if (j < default_args.Length && default_args[j].slot == i)
                        localvars[i] = new Variable(default_args[j++].value);
                    else
                        localvars[i] = new Variable(null);
                }
            else
                for (int i = 0; i < localvars.Length; i++)
                {
                    localvars[i] = new Variable(null);
                }
            var args_itr = args.GetEnumerator();
            for (int i = 0; i < fptr.posargcount; i++)
            {
                if (!args_itr.MoveNext())
                {
                    var arg_string = fptr.metadata.localnames.GetRange(i, fptr.posargcount - i).Select(x => $"\"{x}\"").By(x => String.Join(", ", x));
                    throw new TypeError($"{this.AsObject.__repr__()} missing {fptr.posargcount - i} positional argument(s): {arg_string}");
                }
                localvars[i].Value = args_itr.Current;
            }
            if (fptr.hasvararg)
            {
                var vararg = RTS.barelist_create();
                while (args_itr.MoveNext())
                {
                    RTS.barelist_add(vararg, args_itr.Current);
                }
                localvars[fptr.posargcount].Value = RTS.object_from_barearray(vararg.ToArray());
            }
            else
            {
                if (args_itr.MoveNext())
                {
                    throw new TypeError($"{this.AsObject.__repr__()} takes {fptr.posargcount} positional argument(s) but {args.Count} were given");
                }
            }
            int i_kwstart, i_kwend;
            if (fptr.hasvararg)
                i_kwstart = fptr.posargcount + 1;
            else
                i_kwstart = fptr.posargcount;
            if (fptr.haskwarg)
                i_kwend = fptr.allargcount - 1;
            else
                i_kwend = fptr.allargcount;

            if (kwargs != null)
            {
                var missing = new List<string>();
                var dictargs = kwargs.Copy();
                for (int i = i_kwstart; i < i_kwend; i++)
                {
                    if (dictargs.TryPop(fptr.kwindices[i], out var arg))
                    {
                        localvars[i].Value = arg;
                    }
                    else
                    {
                        if (localvars[i].Value == null)
                            missing.Add(fptr.kwindices[i].__repr__());
                    }
                }
                if (fptr.haskwarg)
                {
                    localvars[fptr.allargcount - 1].Value = RTS.object_from_baredict(dictargs);
                }
                else
                {
                    if (dictargs.Count != 0)
                        throw new TypeError($"{this.AsObject.__repr__()}  got unexpected keyword argument(s): {dictargs.Keys.Select(x => x.__repr__()).By(x => String.Join(", ", x))} ");
                }
                if (missing.Count != 0)
                    throw new TypeError($"{this.AsObject.__repr__()}  missing {missing.Count} keyword-only argument(s): {missing.By(x => String.Join(", ", x))} ");
            }
            else
            {
                if (i_kwstart < i_kwend)
                {
                    var missing = new List<string>();
                    for (int i = i_kwstart; i < i_kwend; i++)
                        if (localvars[i].Value == null)
                            missing.Add(fptr.kwindices[i].__repr__());
                    if (missing.Count != 0)
                    {
                        var msg = missing.By(x => String.Join(", ", x));
                        throw new TypeError($"{this.AsObject.__repr__()}  missing {i_kwend - i_kwstart} keyword-only argument(s): {msg} ");
                    }
                }
                if (fptr.haskwarg)
                {
                    localvars[fptr.allargcount - 1].Value = RTS.object_from_baredict(RTS.baredict_create());
                }
            }
            var frame = Frame.Make(this, localvars);
            if (!fptr.code.hasCont)
            {
                try
                {
                    fptr.code.exec(frame);
                }
                catch (Exception e)
                {
                    throw RTS.exc_wrap_frame(e, frame);
                }
                return frame.retval;
            }
            var coroutine = fptr.code.cont(frame);
            coroutine.AsTopLevelGenerator(frame);
            return coroutine;
        }
    }

    public enum STATUS
    {
        NORMAL,
        RETURN,
        BREAK,
        CONTINUE,
    }
    public class Frame
    {
        public TrFunc func;
        public Exception err;
        public Variable[] localvars;
        public STATUS CONT;
        public TrObject retval;
        public Stack<int> traceback;

        public static Frame Make(TrFunc func, Variable[] localvars) => new Frame
        {
            func = func,
            localvars = localvars,
            retval = RTS.object_none,
            traceback = new Stack<int>()
        };

        internal Variable load_reference(int operand)
        {
            if (operand < 0)
            {
                return func.freevars[-operand - 1];
            }
            return localvars[operand];
        }
        internal void delete_local(int operand)
        {
            throw new NotImplementedException();
        }

        internal void store_local(int operand, TrObject o)
        {
            if (operand < 0)
            {
                func.freevars[-operand - 1].Value = o;
                return;
            }
            localvars[operand].Value = o;
        }

        internal TrObject load_local(int operand)
        {
            if (operand < 0)
            {
                return func.freevars[-operand - 1].Value;
            }
            return localvars[operand].Value;
        }

        internal void delete_global(TrObject v)
        {
            throw new NotImplementedException();
        }

        internal void store_global(TrObject v, TrObject trObject)
        {
            func.globals[v] = trObject;
        }

        internal TrObject load_global(TrObject name)
        {
            if (func.globals.TryGetValue(name, out var v))
            {
                return v;
            }
            throw new NameError("global", name.__str__());
        }

        internal void clear_exception()
        {
            err = null;
        }

        internal void set_exception(Exception e)
        {
            err = e;
        }

        internal Exception get_exception()
        {
            return err;
        }

        internal bool has_exception()
        {
            return err != null;
        }

        internal Exception exc_notset()
        {
            throw new NotImplementedException();
        }
    }

}
