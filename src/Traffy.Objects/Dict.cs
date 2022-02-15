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

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype<TrDict>();
            CLASS.Name = "dict";
            CLASS.__new = TrDict.datanew;
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
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