using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace Traffy.Objects
{
    public sealed class TrSharpFunc : TrObject
    {
        public string name;
        [NotNull] public Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func;
        string TrObject.__repr__() => $"<function {name}>";
        bool TrObject.__bool__() => true;
        public static TrClass CLASS;
        public TrClass Class => CLASS;
        List<TrObject> TrObject.__array__ => null;

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrSharpFunc>("builtin_function");
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("builtin_function.__new__", TrSharpFunc.datanew));
            TrClass.TypeDict[typeof(TrSharpFunc)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrSharpFunc))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }

        TrObject TrObject.__call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            try
            {
                return func(args, kwargs);
            }
            catch (Exception e)
            {
                var exc = RTS.exc_wrap_builtin(e, name);
                throw exc;
            }
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


        public static TrSharpFunc FromFunc(string name, Func<TrObject, TrRef, bool> func)
        {

            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 2);
                return func(args[0], (TrRef)args[1]).ToTr();
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

        public static TrSharpFunc FromFunc(string name, Func<TrClass, BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func)
        {

            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_atleast(args, 1);
                var clsobj = args.PopLeft();
                var cls = clsobj as TrClass;
                if (cls == null)
                    throw new TypeError($"{cls.Class.Name} object is not a class");
                var o = func(cls, args, kwargs);
                args.AddLeft(cls);
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

        public static TrSharpFunc FromFunc(string name, Func<TrObject, Awaitable<TrObject>> func)
        {

            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return TrGenerator.Create(func(args[0]));
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


        public static TrSharpFunc FromFunc(string name, Action<TrObject, TrObject> func)
        {

            if (func == null)
                throw new InvalidProgramException("func is null");
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 2);
                func(args[0], args[1]);
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