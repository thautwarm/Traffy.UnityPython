using System;
using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;
using Traffy.InlineCache;

namespace Traffy.Objects
{

    public static class ExtExceptions
    {
        public static TrExceptionBase ToTr(this Exception e)
        {
            return RTS.exc_frombare(e);
        }
    }

    public abstract class TrExceptionBase : TrUserObjectBase
    {
        public Exception AsException() => RTS.exc_tobare(this);
        public abstract int IndexArgs { get; }
        public TrTraceback traceback;

        public static TrObject GetCause(TrObject _exc)
        {
            var exc = (TrExceptionBase)_exc;
            if (exc.traceback == null || exc.traceback.cause == null)
                return RTS.object_none;
            return exc.traceback.cause;
        }

        public readonly static TrProperty _obj_getcause = TrProperty.Create(TrSharpFunc.FromFunc("GetCause", GetCause), null);

        public TrExceptionBase UnsafeCause
        {
            set => traceback.cause = value;
            get => traceback.cause;
        }

        public TrObject[] args
        {
            get => this.GetInstField(IndexArgs, "args").AsTuple().elts;
            set => this.SetInstField(IndexArgs, "args", MK.Tuple(value));
        }

        public static string TrException_repr(TrObject self)
        {
            return $"{self.Class.Name}({String.Join(", ", ((TrExceptionBase)self).args.Select(x => x.__repr__()))})";
        }

        public static string TrException_str(TrObject self)
        {
            return String.Join(", ", ((TrExceptionBase)self).args.Select(x => x.__str__()));
        }

        public override string __repr__() => TrException_repr(this);

        public override string __str__() => TrException_str(this);

        public virtual string GetStackTrace()
        {
            return $"Traceback (most recent call last):\n{traceback?.GetStackTrace()}\n{Class.Name}: {__str__()}";
        }

        public static implicit operator TrExceptionBase(Exception e)
        {
            return RTS.exc_frombare(e);
        }

        public static implicit operator Exception(TrExceptionBase e)
        {
            return e.AsException();
        }
    }

    public static class TrExceptionExt
    {
        public static TrExceptionBase Base<T>(this T self) where T : TrExceptionBase
        {
            return self;
        }

        public static TrObject datanew<T>(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
            where T : TrExceptionBase, new()
        {
            TrObject clsobj = args[0];
            if (kwargs != null)
            {
                throw new TypeError($"{clsobj.AsClass.Name}() takes no keyword arguments");
            }
            var narg = args.Count;
            var s_args = new TrObject[narg - 1];
            for (int i = 1; i < narg; i++)
            {
                s_args[i - 1] = args[i];
            }
            var res = new T();
            res.Base().args = s_args;
            return res;
        }
    }
    public class TrBaseException : TrExceptionBase
    {
        public override string ToString() => this.Base().__repr__();

        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;

        public override List<TrObject> __array__ { get; } = new List<TrObject>(1);

        public TrBaseException() : base()
        {
            this.Base().args = new TrObject[0];
        }
        public TrBaseException(string msg) : base()
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }


        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("BaseException");
        }


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS.Name = "BaseException";

            CLASS[CLASS.ic__repr] = TrSharpFunc.FromFunc("BaseException.__repr__", TrExceptionBase.TrException_repr);
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("BaseException.__new__", TrExceptionExt.datanew<TrBaseException>);
            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            TrClass.TypeDict[typeof(TrBaseException)] = CLASS;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }
    }

    [PyInherit(typeof(TrBaseException))]
    public class TrException : TrExceptionBase
    {
        public override string ToString() => this.Base().__repr__();
        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;
        public TrException() : base()
        {
            this.Base().args = new TrObject[0];
        }
        public TrException(string msg)
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ { get; } = new List<TrObject>(1);

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("Exception");
        }


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            
            CLASS.Name = "Exception";

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("Exception.__new__", TrExceptionExt.datanew<TrException>);
            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            TrClass.TypeDict[typeof(TrException)] = CLASS;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }
    }

    // fields: 'name', 'obj'
    [PyInherit(typeof(TrException))]
    public class AttributeError : TrExceptionBase
    {
        public override string ToString() => this.Base().__repr__();
        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;

        public override List<TrObject> __array__ { get; } = new List<TrObject>(3);

        static InlineCache.PolyIC CacheName = new InlineCache.PolyIC("name".ToIntern());
        static InlineCache.PolyIC CacheObj = new InlineCache.PolyIC("obj".ToIntern());
        public void Init(TrObject obj, TrObject attr, string msg)
        {

            this.Base().args = new TrObject[] { MK.Str(msg) };
            this.Base()[CacheName] = attr; ;
            this.Base()[CacheObj] = obj;
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public AttributeError(TrObject obj, TrObject attr, string msg)
        {
            Init(obj, attr, msg);
        }

        public AttributeError() : base()
        {
            Init(RTS.object_none, RTS.object_none, "");
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("AttributeError");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {

            
            CLASS.Name = "AttributeError";

            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("AttributeError.__new__", TrExceptionExt.datanew<AttributeError>);
            TrClass.TypeDict[typeof(AttributeError)] = CLASS;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }
    }
    // fields: 'name'
    [PyInherit(typeof(TrException))]
    public class NameError : TrExceptionBase
    {
        public override string ToString() => this.Base().__repr__();
        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;
        static InlineCache.PolyIC CacheName = new InlineCache.PolyIC("name".ToIntern());

        public override List<TrObject> __array__ { get; } = new List<TrObject>(2);

        void _Init(TrObject name, string msg)
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
            this.Base()[CacheName] = name;
        }

        public NameError(string name, string msg)
        {
            _Init(name.ToTr(), msg);
        }

        public NameError() : base()
        {
            _Init(RTS.object_none, "");
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("NameError");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            
            CLASS.Name = "NameError";

            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("NameError.__new__", TrExceptionExt.datanew<NameError>);
            TrClass.TypeDict[typeof(NameError)] = CLASS;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }
    }

    [PyInherit(typeof(TrException))]
    public class TypeError : TrExceptionBase
    {
        public override string ToString() => this.Base().__repr__();
        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;

        public override List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public TypeError(string msg)
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public TypeError() : base()
        {
            this.Base().args = new TrObject[0];
        }


        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("TypeError");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            
            CLASS.Name = "TypeError";

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("TypeError.__new__", TrExceptionExt.datanew<TypeError>);
            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            TrClass.TypeDict[typeof(TypeError)] = CLASS;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }
    }


    [PyInherit(typeof(TrException))]
    public class ValueError : TrExceptionBase
    {
        public override string ToString() => this.Base().__repr__();
        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;

        public override List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public ValueError(string msg)
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public ValueError() : base()
        {
            this.Base().args = new TrObject[0];
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("ValueError");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            
            CLASS.Name = "ValueError";

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("ValueError.__new__", TrExceptionExt.datanew<ValueError>);
            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            TrClass.TypeDict[typeof(ValueError)] = CLASS;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }
    }


    // fields: 'value'
    [PyInherit(typeof(TrException))]
    public class StopIteration : TrExceptionBase
    {
        public override string ToString()
        {
            return this.Base().__repr__();
        }
        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;

        static InlineCache.PolyIC CacheValue = new InlineCache.PolyIC("value".ToIntern());
        public override List<TrObject> __array__ { get; } = new List<TrObject>(2);
        public StopIteration(TrObject value)
        {
            this.Base().args = new TrObject[] { value };
            this.Base()[CacheValue] = value;
        }
        public StopIteration()
        {
            this.Base().args = new TrObject[0];
            this.Base()[CacheValue] = RTS.object_none;
        }


        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("StopIteration");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            
            CLASS.Name = "StopIteration";

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("StopIteration.__new__", TrExceptionExt.datanew<StopIteration>);
            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            TrClass.TypeDict[typeof(StopIteration)] = CLASS;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }
    }

    [PyInherit(typeof(TrException))]
    public class LookupError : TrExceptionBase
    {
        public override string ToString() => this.Base().__repr__();
        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;

        public override List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public LookupError(string msg)
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public LookupError()
        {
            this.Base().args = new TrObject[0];
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("LookupError");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            
            CLASS.Name = "LookupError";

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("LookupError.__new__", TrExceptionExt.datanew<LookupError>);
            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            TrClass.TypeDict[typeof(LookupError)] = CLASS;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }
    }



    [PyInherit(typeof(LookupError))]
    public class KeyError : TrExceptionBase
    {
        public override string ToString() => this.Base().__repr__();
        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;

        public override List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public KeyError(TrObject value)
        {
            this.Base().args = new TrObject[] { value };
        }
        public KeyError() : base()
        {
            this.Base().args = new TrObject[0];
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("KeyError");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            
            CLASS.Name = "KeyError";

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("KeyError.__new__", TrExceptionExt.datanew<KeyError>);
            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            TrClass.TypeDict[typeof(KeyError)] = CLASS;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }
    }

    [PyInherit(typeof(LookupError))]
    public class IndexError : TrExceptionBase
    {
        public override string ToString() => this.Base().__repr__();
        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;

        public override List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public IndexError(string msg)
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public IndexError() : base()
        {
            this.Base().args = new TrObject[0];
        }


        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("IndexError");
        }


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            
            CLASS.Name = "IndexError";

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("IndexError.__new__", TrExceptionExt.datanew<IndexError>);
            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            TrClass.TypeDict[typeof(IndexError)] = CLASS;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }
    }

    [PyInherit(typeof(TrException))]

    public class AssertionError : TrExceptionBase
    {
        public override string ToString() => this.Base().__repr__();
        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;

        public override List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public AssertionError(TrObject value)
        {
            this.Base().args = new TrObject[] { value };
        }
        public AssertionError() : base()
        {
            this.Base().args = new TrObject[0];
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("AssertionError");
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            
            CLASS.Name = "AssertionError";

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("AssertionError.__new__", TrExceptionExt.datanew<AssertionError>);
            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            TrClass.TypeDict[typeof(AssertionError)] = CLASS;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }
    }

    // fields:
    // - 'msg': string
    // - 'name': string
    // - 'path': string
    [PyInherit(typeof(TrException))]
    public class ImportError : TrExceptionBase
    {
        public override string ToString() => this.Base().__repr__();
        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;

        static PolyIC CacheName = new PolyIC("name".ToIntern());
        static PolyIC CachePath = new PolyIC("path".ToIntern());
        static PolyIC CacheMsg = new PolyIC("msg".ToIntern());
        public override List<TrObject> __array__ { get; } = new List<TrObject>(4);
        public ImportError(string name, TrObject path, string msg)
        {
            var o_msg = MK.Str(msg);
            this.Base().args = new TrObject[] { o_msg };
            this.Base()[CacheName] = MK.Str(name);
            this.Base()[CachePath] = path;
            this.Base()[CacheMsg] = o_msg;
            this.Base().args = new TrObject[] { o_msg };
        }
        public ImportError() : base()
        {
            var o_msg = RTS.object_none;
            this.Base().args = new TrObject[] { o_msg };
            this.Base()[CacheName] = RTS.object_none;
            this.Base()[CachePath] = RTS.object_none;
            this.Base()[CacheMsg] = o_msg;
            this.Base().args = new TrObject[] { o_msg };
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("ImportError");
        }


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            
            CLASS.Name = "ImportError";

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("ImportError.__new__", TrExceptionExt.datanew<ImportError>);
            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            TrClass.TypeDict[typeof(ImportError)] = CLASS;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }
    }

    [PyInherit(typeof(TrException))]

    public class RuntimeError : TrExceptionBase
    {
        public override string ToString() => this.Base().__repr__();
        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;

        public override List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public RuntimeError(string msg)
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public RuntimeError() : base()
        {
            this.Base().args = new TrObject[0];
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("RuntimeError");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            
            CLASS.Name = "RuntimeError";

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("RuntimeError.__new__", TrExceptionExt.datanew<RuntimeError>);
            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            TrClass.TypeDict[typeof(RuntimeError)] = CLASS;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }
    }


    [PyInherit(typeof(RuntimeError))]
    public class NotImplementError : TrExceptionBase
    {
        public override string ToString() => this.Base().__repr__();
        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;

        public override List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public NotImplementError(string msg)
        {

            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public NotImplementError() : base()
        {

            this.Base().args = new TrObject[0];
        }


        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("NotImplementError");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            
            CLASS.Name = "NotImplementError";

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("NotImplementError.__new__", TrExceptionExt.datanew<NotImplementError>);
            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            TrClass.TypeDict[typeof(NotImplementError)] = CLASS;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }
    }

    [PyInherit(typeof(TrException))]
    public class NativeError : TrExceptionBase
    {
        public override string ToString() => this.Base().__repr__();
        static int _IndexArgs = -1;
        public override int IndexArgs => _IndexArgs;

        static int _IndexTypeName = -1;
        static int _IndexMsg = -1;

        public override List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public Exception Error;
        public override object Native => Error;
        public override bool __eq__(TrObject other)
        {
            if (other is NativeError inner)
            {
                return Error == inner.Error;
            }
            return false;
        }
        public NativeError(Exception native)
        {
            if (native is TrExceptionWrapper)
                throw new Exception("native error should not be a traffy error");
            Error = native;
            this.Base().args = new TrObject[] { MK.Str(native.Message) };
            this.Base().SetInstField(_IndexTypeName, "typename", MK.Str(native.GetType().Name));
            this.Base().SetInstField(_IndexMsg, "msg", MK.Str(native.Message));
        }

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("NativeError");
        }


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS.Name = "NativeError";

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("NativeError.__new__", NativeError.datanew);
            CLASS[CLASS.ic__eq] = TrSharpFunc.FromFunc("NativeError.__eq__", (o, r) => o.__eq__(r));
            _IndexArgs = CLASS.AddField("args");
            CLASS.AddProperty("__cause__", TrExceptionBase._obj_getcause);
            _IndexMsg = CLASS.AddField("msg");
            _IndexTypeName = CLASS.AddField("typename");
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(NativeError)] = CLASS;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        private static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            // just report error
            throw new TypeError("cannot create native error manually");
        }
        public override string GetStackTrace()
        {
            return $"Traceback (most recent call last):\n{traceback?.GetStackTrace()}\n{Class.Name}: {this.Base().__str__()}\n{Error.StackTrace}";
        }
    }

    public class TrExceptionWrapper: Exception
    {
        public TrExceptionBase TrO;
        public TrExceptionWrapper(TrExceptionBase trO): base (trO.args.Length > 0 ? trO.args[0].__str__() : "")
        {
            TrO = trO;
        }

        public override string ToString() => TrO.__repr__();
        public override string StackTrace => TrO.GetStackTrace();
    }

}
