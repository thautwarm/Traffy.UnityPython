using System;
using System.Collections.Generic;

namespace Traffy
{
    public class TrSlice : TrObject
    {
        public Dictionary<TrObject, TrObject> __dict__ => null;
        public TrObject low;
        public TrObject high;
        public TrObject step;
        public TrClass Class => TrClass.NotImplementedClass;
        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            RTS.arg_check_positional_range(args, 1, 4);
            int narg = args.Count;
            if (narg == 1)
            {
                return new TrSlice { low = TrNone.Unique, high = TrNone.Unique, step = TrNone.Unique };
            }
            if (narg == 2)
            {
                return new TrSlice { low = TrNone.Unique, high = args[1], step = TrNone.Unique };
            }
            if (narg == 3)
            {
                return new TrSlice { low = args[1], high = args[2], step = TrNone.Unique };
            }
            if (narg == 4)
            {
                return new TrSlice { low = args[1], high = args[2], step = args[3] };
            }
            throw new TypeError($"invalid invocation of {args[0].AsClass.Name}");
        }
    }
    public class TrNotImplemented : TrObject
    {
        public Dictionary<TrObject, TrObject> __dict__ => null;

        public static TrNotImplemented Unique = new TrNotImplemented();
        public static bool unique_set = false;
        private TrNotImplemented()
        {
            if (unique_set)
                throw new InvalidOperationException("recreate singleton");
            unique_set = true;
        }

        public TrClass Class => TrClass.NotImplementedClass;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
    }

    public class TrSharpMethod: TrObject
    {
        public Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func;

        public TrObject self;

        public Dictionary<TrObject, TrObject> __dict__ => null;

        public TrClass Class => TrClass.BuiltinFuncClass;

        public TrObject __call__(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            args.AddLeft(self);
            var o = func(args, kwargs);
            args.PopLeft();
            return o;
        }

        // call types.MethodType

        public static TrObject Bind(TrObject func, TrObject self)
        {
            return new TrSharpMethod { func = func.__call__, self = self };
        }

        public static TrObject BindOrUnwrap(TrObject func, TrObject self)
        {
            return new TrSharpMethod { func = func.__call__, self = self };
        }
        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            RTS.arg_check_positional_only(args, 3);
            return Bind(args[1], args[2]);
        }
    }
    public class TrSharpFunc: TrObject
    {
        public Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func;

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

        private TrSharpFunc(Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func)
        {
            this.func = func;
        }

        public static TrSharpFunc FromFunc(Func<BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func)
        {
            return new TrSharpFunc(func);
        }

        public static TrSharpFunc FromFunc(Func<TrObject> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 0);
                return func();
            }
            return new TrSharpFunc(call);
        }

        public static TrSharpFunc FromFunc(Func<TrObject, TrObject> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return func(args[0]);
            }
            return new TrSharpFunc(call);
        }

        public static TrSharpFunc FromFunc(Func<TrObject, TrObject, TrObject> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 2);
                return func(args[0], args[1]);
            }
            return new TrSharpFunc(call);
        }

        public static TrSharpFunc FromFunc(Func<TrObject, TrObject, TrObject, TrObject> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 3);
                return func(args[0], args[1], args[2]);
            }
            return new TrSharpFunc(call);
        }

        public static TrSharpFunc FromFunc(Func<TrObject, TrObject, TrObject, TrObject, TrObject> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 4);
                return func(args[0], args[1], args[2], args[3]);
            }
            return new TrSharpFunc(call);
        }

        public static TrSharpFunc FromFunc(Func<TrObject, BList<TrObject>, Dictionary<TrObject, TrObject>, TrObject> func)
        {
             TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_atleast(args, 1);
                var self = args.PopLeft();
                var o = func(self, args, kwargs);
                args.AddLeft(self);
                return o;
            }
            return new TrSharpFunc(call);
        }
        public static TrSharpFunc FromFunc(Func<TrObject, string> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return MK.Str(func(args[0]));

            }
            return new TrSharpFunc(call);
        }

        public static TrSharpFunc FromFunc(Func<TrObject, int> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return MK.Int(func(args[0]));

            }
            return new TrSharpFunc(call);
        }

        public static TrSharpFunc FromFunc(Func<TrObject, bool> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return MK.Bool(func(args[0]));

            }
            return new TrSharpFunc(call);
        }

        public static TrSharpFunc FromFunc(Func<TrObject, IEnumerator<TrObject>> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 1);
                return RTS.coroutine_of_iter(func(args[0]));

            }
            return new TrSharpFunc(call);
        }
        public static TrSharpFunc FromFunc(Action<TrObject, TrObject, TrObject> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 3);
                func(args[0], args[1], args[2]);
                return RTS.object_none;

            }
            return new TrSharpFunc(call);
        }

        public static TrSharpFunc FromFunc(Func<TrObject, TrObject, bool> func)
        {
            TrObject call(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            {
                RTS.arg_check_positional_only(args, 2);
                return MK.Bool(func(args[0], args[1]));

            }
            return new TrSharpFunc(call);
        }
    }
}