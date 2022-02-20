using System;
using System.Collections.Generic;
using System.Linq;

namespace Traffy.Objects
{
    public partial class TrDict : TrObject
    {
        public Dictionary<TrObject, TrObject> container;

        public Dictionary<TrObject, TrObject> __dict__ => null;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;

        public bool __bool__() => container.Count > 0;

        public bool __getitem__(TrObject key, TrRef found)
        {
            return container.TryGetValue(key, out found.value);
        }

        public void __setitem__(TrObject key, TrObject value)
        {
            container[key] = value;
        }


        [Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.CreateClass("dict");
            CLASS.Name = "dict";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("dict.__new__", TrDict.datanew));
            CLASS[CLASS.ic__bool] = TrSharpFunc.FromFunc("dict.__new__", o => ((TrDict)o).__bool__());
            TrClass.TypeDict[typeof(TrDict)] = CLASS;
            // TODO: __init_subclass__
        }

        [Mark(typeof(TrDict))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            // XXX: more argument validation
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Dict();
            if (narg == 2 && kwargs == null)
            {
                Dictionary<TrObject, TrObject> res = RTS.baredict_create();
                RTS.baredict_extend(res, args[1]);
                return MK.Dict(res);
            }
            else if (kwargs != null)
            {
                Dictionary<TrObject, TrObject> res = RTS.baredict_create();
                foreach (var kv in kwargs)
                    res.Add(kv.Key, kv.Value);
                return MK.Dict(res);
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }


        public string __repr__() =>
            "{" + String.Join(", ", container.Select(kv => $"{kv.Key.__repr__()}: {kv.Value.__repr__()}")) + "}";
    }

}