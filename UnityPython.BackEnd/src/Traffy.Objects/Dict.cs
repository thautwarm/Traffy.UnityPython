using System;
using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Mapping))]
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

        public override bool __finditem__(TrObject key, TrRef refval)
        {
            return container.TryGetValue(key, out refval.value);
        }

        public override TrObject __reversed__()
        {
            throw new TypeError("'dict' object is not reversible");
        }

        public override TrObject __len__()
        {
            return MK.Int(container.Count);
        }

        public override bool __eq__(TrObject other)
        {
            if (other is TrDict dict)
            {
                return container.Count == dict.container.Count && container.All(kv => dict.container.TryGetValue(kv.Key, out var value) && value.__eq__(kv.Value));
            }
            throw new TypeError($"unsupported operand type(s) for ==: '{Class.Name}' and '{other.Class.Name}'");
        }

        public override bool __ne__(TrObject other)
        {
            if (other is TrDict dict)
            {
                return container.Count != dict.container.Count || container.Any(kv => !dict.container.TryGetValue(kv.Key, out var value) || !value.__eq__(kv.Value));
            }
            throw new TypeError($"unsupported operand type(s) for !=: '{Class.Name}' and '{other.Class.Name}'");
        }

        public override TrObject __or__(TrObject a)
        {
            if (a is TrDict dict)
            {
                var res = RTS.baredict_create();
                foreach (var kv in container)
                {
                    res[kv.Key] = kv.Value;
                }
                foreach (var kv in dict.container)
                {
                    res[kv.Key] = kv.Value;
                }
                return MK.Dict(res);
            }
            throw new TypeError($"unsupported operand type(s) for |: '{Class.Name}' and '{a.Class.Name}'");
        }

        public override IEnumerator<TrObject> __iter__()
        {
            return keys();
        }

        public override bool __contains__(TrObject a)
        {
            return container.ContainsKey(a);
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
            if (!object.ReferenceEquals(clsobj, CLASS))
            {
                throw new TypeError($"{clsobj.__repr__()} is not {CLASS.Name}");
            }
            var narg = args.Count;
            if (narg == 1)
                return MK.Dict(kwargs ?? RTS.baredict_create());
            if (narg != 2)
            {
                throw new TypeError($"dict expected 0 or 1 arguments, got {narg}");
            }
            Dictionary<TrObject, TrObject> res = kwargs ?? RTS.baredict_create();
            RTS.baredict_extend(res, args[1]);
            return MK.Dict(res);
        }

        [PyBind]
        public void clear()
        {
            container.Clear();
        }

        [PyBind]
        public TrDict copy()
        {
            return MK.Dict(container.Copy());
        }

        [PyBind]
        public static TrDict fromkeys(TrObject iterable, TrObject value = null)
        {
            value = value ?? MK.None();
            var itr = iterable.__iter__();
            var res = RTS.baredict_create();
            while (itr.MoveNext())
            {
                res[itr.Current] = value;
            }
            return MK.Dict(res);
        }

        [PyBind]
        public TrObject get(TrObject key, TrObject defaultVal = null)
        {
            if (container.TryGetValue(key, out var value))
            {
                return value;
            }
            return defaultVal ?? MK.None();
        }

        [PyBind]
        public IEnumerator<TrObject> keys()
        {
            foreach (var kv in container)
            {
                yield return kv.Key;
            }
        }

        [PyBind]
        public IEnumerator<TrObject> values()
        {
            foreach (var kv in container)
            {
                yield return kv.Value;
            }
        }

        [PyBind]
        public IEnumerator<TrObject> items()
        {
            foreach (var kv in container)
            {
                yield return MK.NTuple(kv.Key, kv.Value);
            }
        }

        [PyBind]
        public TrObject pop(TrObject key, TrObject defaultVal = null)
        {
            if (container.TryGetValue(key, out var value))
            {
                container.Remove(key);
                return value;
            }
            if (defaultVal == null)
            {
                throw new KeyError(key);
            }
            return defaultVal;
        }

        [PyBind]
        public TrObject setdefault(TrObject key, TrObject defaultVal)
        {
            if (container.TryGetValue(key, out var value))
            {
                return value;
            }
            container[key] = defaultVal;
            return defaultVal;
        }

        [PyBind]
        public void update(TrObject values)
        {
            RTS.baredict_extend(container, values);
        }
    }
}