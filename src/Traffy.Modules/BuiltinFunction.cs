using System;
using System.Collections.Generic;

namespace Traffy
{
    public class TrSharpFunc : TrObject
    {
        public string __repr__() => $"<function {name}>";
        public Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func;

        public string name;

        public Dictionary<TrObject, TrObject> __dict__ => null;

        public TrClass Class => TrClass.BuiltinFuncClass;

        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            if (func == null)
                return TrNone.Unique;
            return func(args, kwargs);
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }

        private TrSharpFunc(string name, Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func)
        {
            this.func = func;
            this.name = name;
        }

        public static TrSharpFunc FromFunc(string name, Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func)
        {
            return new TrSharpFunc(name, func);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 0);
                return func();
            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, TrObject> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return func(args[0]);
            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, TrObject, TrObject> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 2);
                return func(args[0], args[1]);
            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, TrObject, TrObject, TrObject> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 3);
                return func(args[0], args[1], args[2]);
            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, TrObject, TrObject, TrObject, TrObject> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 4);
                return func(args[0], args[1], args[2], args[3]);
            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_atleast(args, 1);
                var self = args.PopLeft();
                var o = func(self, args, kwargs);
                args.AddLeft(self);
                return o;
            }
            return new TrSharpFunc(name, call);
        }
        public static TrSharpFunc FromFunc(string name, Func<TrObject, string> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return MK.Str(func(args[0]));

            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, int> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return MK.Int(func(args[0]));

            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, bool> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return MK.Bool(func(args[0]));

            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, IEnumerator<TrObject>> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return RTS.coroutine_of_iter(func(args[0]));

            }
            return new TrSharpFunc(name, call);
        }
        public static TrSharpFunc FromFunc(string name, Action<TrObject, TrObject, TrObject> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 3);
                func(args[0], args[1], args[2]);
                return RTS.object_none;

            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, TrObject, bool> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 2);
                return MK.Bool(func(args[0], args[1]));

            }
            return new TrSharpFunc(name, call);
        }
    }
}