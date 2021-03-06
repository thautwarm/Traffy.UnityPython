using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Traffy.Annotations;

namespace Traffy.Objects
{
    // runtime
    [PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Callable))]
    public sealed partial class TrFunc : TrObject
    {
        public Variable[] freevars;

        [PyBind]
        public TrObject __globals__ => MK.Dict(globals);
        public Dictionary<TrObject, TrObject> globals;
        public (int slot, TrObject value)[] default_args;
        public TrFuncPointer fptr;

        public TrFunc(Variable[] freevars, Dictionary<TrObject, TrObject> globals, (int slot, TrObject value)[] default_args, TrFuncPointer fptr)
        {
            this.fptr = fptr;
            this.freevars = freevars;
            this.globals = globals;
            this.default_args = default_args;
        }

        public static TrClass CLASS;

        public override TrClass Class => CLASS;

        public override string __repr__()
        {
            return $"<function {fptr.metadata.codename}>";
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrFunc>("function");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("function.__new__", TrFunc.datanew);
            CLASS.IsSealed = true;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsClassFixed = true;
            Initialization.Prelude(CLASS);
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            var clsobj = args[0];
            throw new TypeError($"{clsobj.AsClass.Name} has no constructor");
        }
        TrObject AsObject => this;

        public override List<TrObject> __array__ => null;

        public override TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            return Execute(args, kwargs, null);
        }

        public TrObject Execute(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs, Frame frame)
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

#if FAST_ITER_BLIST
            var (ptr_now, ptr_end) = args.GetIteratorBounds();
            for (int i = 0; i < fptr.posargcount; i++, ptr_now++)
            {
                if (ptr_now >= ptr_end)
                {
                    if((object)localvars[i].Value == null)
                    {
                        var arg_string = fptr.metadata.localnames.GetRange(i, fptr.posargcount - i).Select(x => $"\"{x}\"").By(x => String.Join(", ", x));
                        throw new TypeError($"{this.AsObject.__repr__()} missing {fptr.posargcount - i} positional argument(s): {arg_string}");
                    }
                }
                else
                {
                    localvars[i].Value = args[ptr_now];
                }
            }
#else
            var args_itr = args.GetEnumerator();
            for (int i = 0; i < fptr.posargcount; i++)
            {
                if (!args_itr.MoveNext())
                {
                    if((object)localvars[i].Value != null)
                    {
                        continue;
                    }
                    var arg_string = fptr.metadata.localnames.GetRange(i, fptr.posargcount - i).Select(x => $"\"{x}\"").By(x => String.Join(", ", x));
                    throw new TypeError($"{this.AsObject.__repr__()} missing {fptr.posargcount - i} positional argument(s): {arg_string}");
                }
                localvars[i].Value = args_itr.Current;
            }
#endif
            if (fptr.hasvararg)
            {
                var vararg = RTS.barelist_create();
#if FAST_ITER_BLIST
                while (ptr_now < ptr_end)
                    RTS.barelist_add(vararg, args[ptr_now++]);
#else
                while (args_itr.MoveNext())
                    RTS.barelist_add(vararg, args_itr.Current);
#endif
                localvars[fptr.posargcount].Value = RTS.object_from_barearray(vararg.ToArray());
            }
            else
            {
#if FAST_ITER_BLIST
                if (ptr_now < ptr_end)
#else                
                if (args_itr.MoveNext())
#endif
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
                List<string> missing = null;
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
                        {
                            missing = missing ?? new List<string>();
                            missing.Add(fptr.kwindices[i].__repr__());
                        }
                            
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
                if (missing != null)
                    throw new TypeError($"{this.AsObject.__repr__()}  missing {missing.Count} keyword-only argument(s): {missing.By(x => String.Join(", ", x))} ");
            }
            else
            {
                if (i_kwstart < i_kwend)
                {
                    List<string> missing = null;
                    for (int i = i_kwstart; i < i_kwend; i++)
                        if (localvars[i].Value == null)
                        {
                            missing = missing ?? new List<string>();
                            missing.Add(fptr.kwindices[i].__repr__());
                        }
                    if (missing != null)
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
            if (frame == null)
                frame = Frame.Make(this, localvars);
            else
            {
                frame.localvars = localvars;
                frame.func = this;
            }
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
            return TrGenerator.Create(coroutine, frame);
        }
    }
}
