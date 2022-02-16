using System;
using System.Collections.Generic;
using System.Linq;

namespace Traffy.Objects
{

    public interface TrExceptionBase : TrObject
    {

        public static string TrException_repr(TrObject self)
        {
            TrObject[] elts = elts = ((TrExceptionBase)self).args;
            return $"{self.Class.Name}({String.Join(", ", elts.Select(x => x.__repr__()))})";
        }

        public TrObject[] args
        {
            set => ((TrObject)this).__setattr__("args".ToTr(), MK.Tuple(value));
            get
            {
                if (!((TrObject)this).__getattr__("args".ToTr(), out var tupleobject))
                {
                    return MK.Tuple().elts;
                }
                else
                {
                    return tupleobject.AsTuple().elts;
                }
            }
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

        public TrBaseException() : base()
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[0];
        }
        public TrBaseException(string msg) : base()
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }

        public Dictionary<TrObject, TrObject> __dict__ { get; set; }

        public static TrClass CLASS;
        public TrClass Class => CLASS;


        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("BaseException");
            CLASS.__repr = TrExceptionBase.TrException_repr;
            CLASS.Name = "BaseException";
            CLASS.Fixed = true;
            CLASS.__new = TrExceptionExt.datanew<TrBaseException>;
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    public class TrException : Exception, TrExceptionBase
    {
        public Dictionary<TrObject, TrObject> __dict__ { get; set; }
        public TrException() : base()
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[0];
        }
        public TrException(string msg) : base(msg)
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }



        public static TrClass CLASS;
        public TrClass Class => CLASS;


        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("Exception", TrBaseException.CLASS);
            CLASS.Name = "Exception";
            CLASS.__new = TrExceptionExt.datanew<TrException>;
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }


    // fields: 'name', 'obj'
    public class AttributeError : Exception, TrExceptionBase
    {
        public AttributeError(TrObject obj, TrObject attr, string msg) : base(msg)
        {
            __dict__ = RTS.baredict_create();
            RTS.baredict_set(__dict__, "name".ToTr(), attr);
            RTS.baredict_set(__dict__, "obj".ToTr(), obj);
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public AttributeError() : base()
        {
            __dict__ = RTS.baredict_create();
            RTS.baredict_set(__dict__, "name".ToTr(), RTS.object_none);
            RTS.baredict_set(__dict__, "obj".ToTr(), RTS.object_none);
            this.Base().args = new TrObject[0];
        }
        public Dictionary<TrObject, TrObject> __dict__ { get; set; }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("AttributeError", TrException.CLASS);
            CLASS.Name = "AttributeError";
            CLASS.__new = TrExceptionExt.datanew<AttributeError>;
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }
    // fields: 'name'
    public class NameError : Exception, TrExceptionBase
    {
        public NameError(string name, string msg) : base(msg)
        {
            __dict__ = RTS.baredict_create();
            RTS.baredict_set(__dict__, "name".ToTr(), name.ToTr());
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public NameError() : base()
        {
            __dict__ = RTS.baredict_create();
            RTS.baredict_set(__dict__, "name".ToTr(), RTS.object_none);
            this.Base().args = new TrObject[0];
        }
        public Dictionary<TrObject, TrObject> __dict__ { get; set; }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("NameError", TrException.CLASS);
            CLASS.Name = "NameError";
            CLASS.__new = TrExceptionExt.datanew<NameError>;
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    public class TypeError : Exception, TrExceptionBase
    {
        public TypeError(string msg) : base(msg)
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public TypeError() : base()
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[0];
        }
        public Dictionary<TrObject, TrObject> __dict__ { get; set; }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("TypeError", TrException.CLASS);
            CLASS.Name = "TypeError";
            CLASS.__new = TrExceptionExt.datanew<TypeError>;
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }


    public class ValueError : Exception, TrExceptionBase
    {
        public ValueError(string msg) : base(msg)
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public ValueError() : base()
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[0];
        }
        public Dictionary<TrObject, TrObject> __dict__ { get; set; }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("ValueError", TrException.CLASS);
            CLASS.Name = "ValueError";
            CLASS.__new = TrExceptionExt.datanew<ValueError>;
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }


    // fields: 'value'
    public class StopIteration : Exception, TrExceptionBase
    {
        public StopIteration(TrObject value) : base()
        {
            __dict__ = RTS.baredict_create();
            RTS.baredict_set(__dict__, "value".ToTr(), value);
            this.Base().args = new TrObject[] { value };
        }
        public StopIteration() : base()
        {
            __dict__ = RTS.baredict_create();
            RTS.baredict_set(__dict__, "value".ToTr(), RTS.object_none);
            this.Base().args = new TrObject[0];
        }
        public Dictionary<TrObject, TrObject> __dict__ { get; set; }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("StopIteration", TrException.CLASS);
            CLASS.Name = "StopIteration";
            CLASS.__new = TrExceptionExt.datanew<StopIteration>;
        }
        [InitSetup(InitOrder.SetupClassObjects)]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    public class LookupError : Exception, TrExceptionBase
    {
        public LookupError(string msg) : base(msg)
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public LookupError() : base()
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[0];
        }
        public Dictionary<TrObject, TrObject> __dict__ { get; set; }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("LookupError", TrException.CLASS);
            CLASS.Name = "LookupError";
            CLASS.__new = TrExceptionExt.datanew<LookupError>;
        }
        [InitSetup(InitOrder.SetupClassObjects)]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }


    public class KeyError : Exception, TrExceptionBase
    {
        public KeyError(TrObject value) : base(value.__repr__())
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[] { value };
        }
        public KeyError() : base()
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[0];
        }
        public Dictionary<TrObject, TrObject> __dict__ { get; set; }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("KeyError", LookupError.CLASS);
            CLASS.Name = "KeyError";
            CLASS.__new = TrExceptionExt.datanew<KeyError>;
        }

        [InitSetup(InitOrder.SetupClassObjects)]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    public class IndexError : Exception, TrExceptionBase
    {
        public IndexError(string msg) : base(msg)
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public IndexError() : base()
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[0];
        }
        public Dictionary<TrObject, TrObject> __dict__ { get; set; }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("IndexError", LookupError.CLASS);
            CLASS.Name = "IndexError";
            CLASS.__new = TrExceptionExt.datanew<IndexError>;
        }
        [InitSetup(InitOrder.SetupClassObjects)]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    public class AssertionError : Exception, TrExceptionBase
    {
        public AssertionError(TrObject value) : base(value.__repr__())
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[] { value };
        }
        public AssertionError() : base()
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[0];
        }
        public Dictionary<TrObject, TrObject> __dict__ { get; set; }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("AssertionError", TrException.CLASS);
            CLASS.Name = "AssertionError";
            CLASS.__new = TrExceptionExt.datanew<AssertionError>;
        }
        [InitSetup(InitOrder.SetupClassObjects)]
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
        public ImportError(string name, string path, string msg) : base(msg)
        {
            __dict__ = RTS.baredict_create();
            RTS.baredict_set(__dict__, "name".ToTr(), MK.Str(name));
            RTS.baredict_set(__dict__, "path".ToTr(), MK.Str(path));
            RTS.baredict_set(__dict__, "msg".ToTr(), MK.Str(msg));
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public ImportError() : base()
        {
            __dict__ = RTS.baredict_create();
            RTS.baredict_set(__dict__, "name".ToTr(), RTS.object_none);
            RTS.baredict_set(__dict__, "path".ToTr(), RTS.object_none);
            RTS.baredict_set(__dict__, "msg".ToTr(), RTS.object_none);
            this.Base().args = new TrObject[0];
        }
        public Dictionary<TrObject, TrObject> __dict__ { get; set; }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("ImportError", TrException.CLASS);
            CLASS.Name = "ImportError";
            CLASS.__new = TrExceptionExt.datanew<ImportError>;
        }
        [InitSetup(InitOrder.SetupClassObjects)]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    public class RuntimeError : Exception, TrExceptionBase
    {
        public RuntimeError(string msg) : base(msg)
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public RuntimeError() : base()
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[0];
        }
        public Dictionary<TrObject, TrObject> __dict__ { get; set; }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("RuntimeError", TrException.CLASS);
            CLASS.Name = "RuntimeError";
            CLASS.__new = TrExceptionExt.datanew<RuntimeError>;
        }
        [InitSetup(InitOrder.SetupClassObjects)]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

    public class NotImplementError : Exception, TrExceptionBase
    {
        public NotImplementError(string msg) : base(msg)
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[] { MK.Str(msg) };
        }
        public NotImplementError() : base()
        {
            __dict__ = RTS.baredict_create();
            this.Base().args = new TrObject[0];
        }
        public Dictionary<TrObject, TrObject> __dict__ { get; set; }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype("NotImplementError", RuntimeError.CLASS);
            CLASS.Name = "NotImplementError";
            CLASS.__new = TrExceptionExt.datanew<NotImplementError>;
        }
        [InitSetup(InitOrder.SetupClassObjects)]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }

}
