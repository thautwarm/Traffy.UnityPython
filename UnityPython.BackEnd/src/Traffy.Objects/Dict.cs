using System;
using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Collection))]
    public sealed partial class TrDict : TrObject
    {
        public Dictionary<TrObject, TrObject> container;

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;

        public override string __repr__() => "{" + String.Join(", ", container.Select(kv => $"{kv.Key.__repr__()}: {kv.Value.__repr__()}")) + "}";

        public override bool __bool__() => container.Count > 0;


        public override TrObject __getitem__(TrObject key)
        {
            return container.TryGetValue(key, out var value) ? value : throw new KeyError(key);
        }


        public override void __setitem__(TrObject key, TrObject value)
        {
            container[key] = value;
        }


        public override void __delitem__(TrObject key)
        {
            container.Remove(key);
        }

        public override TrObject __reversed__()
        {
            throw new TypeError("'dict' object is not reversible");
        }

        public override TrObject __len__()
        {
            return MK.Int(container.Count);
        }

        public override bool __eq__(TrObject eq)
        {
            if (eq is TrDict other)
            {
                return container.Count == other.container.Count && container.All(kv => other.container.TryGetValue(kv.Key, out var value) && value.__eq__(kv.Value));
            }
            throw new TypeError($"unsupported comparison for '{CLASS.Name}' and '{eq.Class.Name}'");
        }

        public override bool __ne__(TrObject other)
        {
            return !__eq__(other);
        }

        public override IEnumerator<TrObject> __iter__()
        {
            return keys();
        }

        [PyBind]
        public IEnumerator<TrObject> keys()
        {
            foreach(var kv in container)
            {
                yield return kv.Key;
            }
        }

        [PyBind]
        public IEnumerator<TrObject> values()
        {
            foreach(var kv in container)
            {
                yield return kv.Value;
            }
        }

        [PyBind]
        public IEnumerator<TrObject> items()
        {
            foreach(var kv in container)
            {
                yield return MK.NTuple( kv.Key, kv.Value );
            }
        }


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
           CLASS = TrClass.FromPrototype<TrDict>("dict");
        }


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("dict.__new__", TrDict.datanew));
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
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
    }

}