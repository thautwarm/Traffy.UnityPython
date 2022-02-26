using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Traffy.Objects
{
    using static JsonExt;


    // runtime
    public record TrFunc(
        Variable[] freevars,
        Dictionary<TrObject, TrObject> globals,
        (int slot, TrObject value)[] default_args,
        TrFuncPointer fptr
     ) : TrObject
    {

        public static TrClass CLASS;

        public TrClass Class => CLASS;

        public string __repr__()
        {
            return $"<function {fptr.metadata.codename}>";
        }


        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrFunc>("function");
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("function.__new__", TrFunc.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrFunc)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrFunc))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            var clsobj = args[0];
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
        TrObject AsObject => this;

        public List<TrObject> __array__ => null;

        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
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
            return TrCoroutine.Create(coroutine, frame);
        }
    }



}
