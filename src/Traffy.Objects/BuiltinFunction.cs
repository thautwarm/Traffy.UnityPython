using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Traffy.Objects
{
    public class TrSharpFunc : TrObject
    {
        public string __repr__() => $"<function {name}>";
        [NotNull] public Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func;
        public bool __bool__() => true;
        public string name;
        public Dictionary<TrObject, TrObject> __dict__ => null;
        public static TrClass CLASS;
        public TrClass Class => CLASS;
        public List<TrObject> __array__ => null;

        [Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrSharpFunc>();
            CLASS.Name = "builtin_function";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("builtin_function.__new__", TrSharpFunc.datanew));
            TrClass.TypeDict[typeof(TrSharpFunc)] = CLASS;
        }

        [Mark(typeof(TrSharpFunc))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }

        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            return func(args, kwargs);
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }

        private TrSharpFunc(string name, Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func)
        {
            if (func == null)
                throw new InvalidProgramException("func is null");
            this.func = func;
            this.name = name;
        }

        public static TrSharpFunc FromFunc(string name, Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func)
        {
            if (func == null)
                throw new InvalidProgramException("func is null");
            return new TrSharpFunc(name, func);
        }


        public static TrSharpFunc FromFunc(string name, Func<TrObject> func)
        {

            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 0);
                return func();
            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, TrObject> func)
        {

            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return func(args[0]);
            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, TrObject, TrObject> func)
        {
            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 2);
                return func(args[0], args[1]);
            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, TrObject, TrObject, TrObject> func)
        {
            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 3);
                return func(args[0], args[1], args[2]);
            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, TrObject, TrRef, bool> func)
        {

            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 3);
                return func(args[0], args[1], (TrRef)args[2]).ToTr();
            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, TrObject, TrObject, TrObject, TrObject> func)
        {

            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 4);
                return func(args[0], args[1], args[2], args[3]);
            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func)
        {

            if (func == null)
                throw new InvalidProgramException("func is null");
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

            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return MK.Str(func(args[0]));

            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, int> func)
        {
            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return MK.Int(func(args[0]));

            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, bool> func)
        {

            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return MK.Bool(func(args[0]));

            }
            return new TrSharpFunc(name, call);
        }

        public static TrSharpFunc FromFunc(string name, Func<TrObject, IEnumerator<TrObject>> func)
        {

            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return MK.Iter(func(args[0]));
            }
            return new TrSharpFunc(name, call);
        }
        public static TrSharpFunc FromFunc(string name, Action<TrObject, TrObject, TrObject> func)
        {

            if (func == null)
                throw new InvalidProgramException("func is null");
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

            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 2);
                return MK.Bool(func(args[0], args[1]));

            }
            return new TrSharpFunc(name, call);
        }
    }
}