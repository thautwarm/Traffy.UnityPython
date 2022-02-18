using System;
using System.Collections.Generic;
using System.Linq;
using Traffy.InlineCache;

namespace Traffy.Objects
{

    public interface TrExceptionBase : TrUserObjectBase
    {
        public InlineCache.InlineCacheInstance CacheArgs { get; }
        public TrObject[] args
        {
            get => RTS.object_getic(this, CacheArgs).AsTuple().elts;
            set => RTS.object_setic(this, CacheArgs, MK.Tuple(value));
        }

        public static string TrException_repr(TrObject self)
        {
            return $"{self.Class.Name}({String.Join(", ", ((TrExceptionBase)self).args.Select(x => x.__repr__()))})";
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
    public class TrBaseException : Exception, TrExceptionBase
    {


        public List<TrObject> __array__ { get; set; } = new List<TrObject>(1);

        public TrBaseException() : base()
        {
            this.Base().args = new TrObject[0];
        }
        public TrBaseException(string msg) : base()
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }


        public static TrClass CLASS;
        public TrClass Class => CLASS;



        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());

        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("BaseException");
            CLASS.Name = "BaseException";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__repr] = TrSharpFunc.FromFunc("BaseException.__repr__", TrExceptionBase.TrException_repr);
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("BaseException.__new__", TrExceptionExt.datanew<TrBaseException>);
            TrClass.TypeDict[typeof(TrBaseException)] = CLASS;
        }

        [Mark(typeof(TrBaseException))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            ModuleInit.Prelude(CLASS);
        }
    }

    public class TrException : Exception, TrExceptionBase
    {
        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());
        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;
        public TrException() : base()
        {
            this.Base().args = new TrObject[0];
        }
        public TrException(string msg) : base(msg)
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ { get; } = new List<TrObject>(1);

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("Exception", TrBaseException.CLASS);
            CLASS.Name = "Exception";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("Exception.__new__", TrExceptionExt.datanew<TrException>);
            TrClass.TypeDict[typeof(TrException)] = CLASS;
        }
        [Mark(typeof(TrException))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    // fields: 'name', 'obj'
    public class AttributeError : Exception, TrExceptionBase
    {
        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());
        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;

        public List<TrObject> __array__ { get; } = new List<TrObject>(3);

        static InlineCache.InlineCacheInstance CacheName = new InlineCache.InlineCacheInstance("name".ToIntern());
        static InlineCache.InlineCacheInstance CacheObj = new InlineCache.InlineCacheInstance("obj".ToIntern());
        public void Init(TrObject obj, TrObject attr, string msg)
        {

            this.Base().args = new TrObject[] { MK.Str(msg) };
            this.Base()[CacheName] = attr; ;
            this.Base()[CacheObj] = obj;
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public AttributeError(TrObject obj, TrObject attr, string msg) : base(msg)
        {
            Init(obj, attr, msg);
        }

        public AttributeError() : base()
        {
            Init(RTS.object_none, RTS.object_none, "");
        }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("AttributeError", TrException.CLASS);
            CLASS.Name = "AttributeError";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("AttributeError.__new__", TrExceptionExt.datanew<AttributeError>);
            TrClass.TypeDict[typeof(AttributeError)] = CLASS;
        }
        [Mark(typeof(AttributeError))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }
    // fields: 'name'
    public class NameError : Exception, TrExceptionBase
    {
        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());
        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;
        static InlineCache.InlineCacheInstance CacheName = new InlineCache.InlineCacheInstance("name".ToIntern());

        // __array__
        public List<TrObject> __array__ { get; } = new List<TrObject>(2);

        void _Init(TrObject name, string msg)
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
            this.Base()[CacheName] = name;
        }

        public NameError(string name, string msg) : base(msg)
        {
            _Init(name.ToTr(), msg);
        }

        public NameError() : base()
        {
            _Init(RTS.object_none, "");
        }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("NameError", TrException.CLASS);
            CLASS.Name = "NameError";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("NameError.__new__", TrExceptionExt.datanew<NameError>);
            TrClass.TypeDict[typeof(NameError)] = CLASS;
        }
        [Mark(typeof(NameError))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    public class TypeError : Exception, TrExceptionBase
    {
        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());
        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;
        public List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public TypeError(string msg) : base(msg)
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public TypeError() : base()
        {
            this.Base().args = new TrObject[0];
        }


        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("TypeError", TrException.CLASS);
            CLASS.Name = "TypeError";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("TypeError.__new__", TrExceptionExt.datanew<TypeError>);
            TrClass.TypeDict[typeof(TypeError)] = CLASS;
        }

        [Mark(typeof(TypeError))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }


    public class ValueError : Exception, TrExceptionBase
    {
        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());
        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;
        public List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public ValueError(string msg) : base(msg)
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public ValueError() : base()
        {
            this.Base().args = new TrObject[0];
        }

        public static TrClass CLASS;
        public TrClass Class => CLASS;


        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("ValueError", TrException.CLASS);
            CLASS.Name = "ValueError";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("ValueError.__new__", TrExceptionExt.datanew<ValueError>);
            TrClass.TypeDict[typeof(ValueError)] = CLASS;
        }
        [Mark(typeof(ValueError))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }


    // fields: 'value'
    public class StopIteration : Exception, TrExceptionBase
    {
        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());
        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;
        static InlineCache.InlineCacheInstance CacheValue = new InlineCache.InlineCacheInstance("value".ToIntern());
        public List<TrObject> __array__ { get; } = new List<TrObject>(2);
        public StopIteration(TrObject value) : base()
        {
            this.Base().args = new TrObject[] { value };
            this.Base()[CacheValue] = value;
        }
        public StopIteration() : base()
        {
            this.Base().args = new TrObject[0];
            this.Base()[CacheValue] = RTS.object_none;
        }


        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("StopIteration", TrException.CLASS);
            CLASS.Name = "StopIteration";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("StopIteration.__new__", TrExceptionExt.datanew<StopIteration>);
            TrClass.TypeDict[typeof(StopIteration)] = CLASS;
        }

        [Mark(typeof(StopIteration))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    public class LookupError : Exception, TrExceptionBase
    {
        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());
        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;
        public List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public LookupError(string msg) : base(msg)
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public LookupError() : base()
        {
            this.Base().args = new TrObject[0];
        }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("LookupError", TrException.CLASS);
            CLASS.Name = "LookupError";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("LookupError.__new__", TrExceptionExt.datanew<LookupError>);
            TrClass.TypeDict[typeof(LookupError)] = CLASS;
        }
        [Mark(typeof(LookupError))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }


    public class KeyError : Exception, TrExceptionBase
    {
        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());
        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;
        public List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public KeyError(TrObject value) : base(value.__repr__())
        {
            this.Base().args = new TrObject[] { value };
        }
        public KeyError() : base()
        {
            this.Base().args = new TrObject[0];
        }

        public static TrClass CLASS;
        public TrClass Class => CLASS;


        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("KeyError", LookupError.CLASS);
            CLASS.Name = "KeyError";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("KeyError.__new__", TrExceptionExt.datanew<KeyError>);
            TrClass.TypeDict[typeof(KeyError)] = CLASS;
        }

        [Mark(typeof(KeyError))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    public class IndexError : Exception, TrExceptionBase
    {
        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());
        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;
        public List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public IndexError(string msg) : base(msg)
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public IndexError() : base()
        {
            this.Base().args = new TrObject[0];
        }


        public static TrClass CLASS;
        public TrClass Class => CLASS;


        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("IndexError", LookupError.CLASS);
            CLASS.Name = "IndexError";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("IndexError.__new__", TrExceptionExt.datanew<IndexError>);
            TrClass.TypeDict[typeof(IndexError)] = CLASS;
        }
        [Mark(typeof(IndexError))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    public class AssertionError : Exception, TrExceptionBase
    {
        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());
        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;
        public List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public AssertionError(TrObject value) : base(value.__repr__())
        {
            this.Base().args = new TrObject[] { value };
        }
        public AssertionError() : base()
        {
            this.Base().args = new TrObject[0];
        }

        public static TrClass CLASS;
        public TrClass Class => CLASS;


        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("AssertionError", TrException.CLASS);
            CLASS.Name = "AssertionError";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("AssertionError.__new__", TrExceptionExt.datanew<AssertionError>);
            TrClass.TypeDict[typeof(AssertionError)] = CLASS;
        }
        [Mark(typeof(AssertionError))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    // fields:
    // - 'msg': string
    // - 'name': string
    // - 'path': string
    public class ImportError : Exception, TrExceptionBase
    {
        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());
        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;
        static InlineCacheInstance CacheName = new InlineCacheInstance("name".ToIntern());
        static InlineCacheInstance CachePath = new InlineCacheInstance("path".ToIntern());
        static InlineCacheInstance CacheMsg = new InlineCacheInstance("msg".ToIntern());
        public List<TrObject> __array__ { get; } = new List<TrObject>(4);
        public ImportError(string name, string path, string msg) : base(msg)
        {
            var o_msg = MK.Str(msg);
            this.Base().args = new TrObject[] { o_msg };
            this.Base()[CacheName] = MK.Str(name);
            this.Base()[CachePath] = MK.Str(path);
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
        public TrClass Class => CLASS;


        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("ImportError", TrException.CLASS);
            CLASS.Name = "ImportError";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("ImportError.__new__", TrExceptionExt.datanew<ImportError>);
            TrClass.TypeDict[typeof(ImportError)] = CLASS;
        }
        [Mark(typeof(ImportError))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    public class RuntimeError : Exception, TrExceptionBase
    {
        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());
        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;
        public List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public RuntimeError(string msg) : base(msg)
        {
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public RuntimeError() : base()
        {
            this.Base().args = new TrObject[0];
        }

        public static TrClass CLASS;
        public TrClass Class => CLASS;


        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("RuntimeError", TrException.CLASS);
            CLASS.Name = "RuntimeError";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("RuntimeError.__new__", TrExceptionExt.datanew<RuntimeError>);
            TrClass.TypeDict[typeof(RuntimeError)] = CLASS;
        }
        [Mark(typeof(RuntimeError))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    public class NotImplementError : Exception, TrExceptionBase
    {
        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());
        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;
        public List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public NotImplementError(string msg) : base(msg)
        {

            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public NotImplementError() : base()
        {

            this.Base().args = new TrObject[0];
        }


        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("NotImplementError", RuntimeError.CLASS);
            CLASS.Name = "NotImplementError";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("NotImplementError.__new__", TrExceptionExt.datanew<NotImplementError>);
            TrClass.TypeDict[typeof(NotImplementError)] = CLASS;
        }
        [Mark(typeof(NotImplementError))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    public class NativeError : Exception, TrExceptionBase
    {
        public static InlineCache.InlineCacheInstance args_cache = new InlineCache.InlineCacheInstance("args".ToIntern());
        InlineCache.InlineCacheInstance TrExceptionBase.CacheArgs => args_cache;
        static InlineCacheInstance CacheTypeName = new InlineCacheInstance("typename".ToIntern());
        static InlineCacheInstance CacheMsg = new InlineCacheInstance("msg".ToIntern());
        public List<TrObject> __array__ { get; } = new List<TrObject>(1);
        public Exception Error;
        public object Native => Error;
        public bool __eq__(TrObject other)
        {
            if (other is NativeError)
            {
                return Error == ((NativeError)other).Error;
            }
            return false;
        }
        public NativeError(Exception native) : base(native.Message)
        {
            if (native is TrExceptionBase)
                throw new Exception("native error should not be a traffy error");
            Error = native;
            this.Base().args = new TrObject[] { MK.Str(native.Message) };
            this.Base()[CacheTypeName] = MK.Str(native.GetType().Name);
            this.Base()[CacheMsg] = MK.Str(native.Message);
        }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("NativeError", TrException.CLASS);
            CLASS.Name = "NativeError";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("NativeError.__new__", NativeError.datanew);
            CLASS[CLASS.ic__eq] = TrSharpFunc.FromFunc("NativeError.__eq__", (o, r) => ((NativeError)o).__eq__(r));
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(NativeError)] = CLASS;
        }

        [Mark(typeof(NativeError))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            ModuleInit.Prelude(CLASS);
        }

        private static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            // just report error
            throw new TypeError("cannot create native error manually");
        }
    }

}
