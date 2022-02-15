using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace Traffy
{


    [Serializable]
    public partial class TrInt: TrObject
    {
        public Int64 value;
        public object Native => value;
        public Dictionary<TrObject, TrObject> __dict__ => null;

        public TrClass Class => TrClass.IntClass;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Int(0L);
            if (narg == 2 && kwargs == null)
            {
                var arg = args[1];
                switch(arg)
                {
                    case TrInt _: return arg;
                    case TrFloat v: return MK.Int((int) v.value);
                    case TrStr v: return RTS.parse_int(v.value);
                    case TrBool v: return MK.Int(v.value ? 1L: 0L);
                    default:
                        throw new InvalidCastException($"cannot cast {arg.Class.Name} objects to {clsobj.AsClass.Name}");
                }
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
    }

    [Serializable]
    public partial class TrFloat: TrObject
    {
        public float value;
        public Dictionary<TrObject, TrObject> __dict__ => null;

        public TrClass Class => TrClass.FloatClass;

        public object Native => value;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Float(0.0f);
            if (narg == 2 && kwargs == null)
            {
                var arg = args[1];
                switch(arg)
                {
                    case TrFloat _: return arg;
                    case TrInt v: return MK.Float(v.value);
                    case TrStr v: return RTS.parse_float(v.value);
                    case TrBool v: return MK.Float(v.value ? 1.0f: 0.0f);
                    default:
                        throw new InvalidCastException($"cannot cast {arg.Class.Name} objects to {clsobj.AsClass.Name}");
                }
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
    }

    [Serializable]
    public partial class TrStr: TrObject
    {
        public string value;

        public Dictionary<TrObject, TrObject> __dict__ => throw new NotImplementedException();

        public TrClass Class => TrClass.StrClass;

        public object Native => value;

        public bool __eq__(TrObject other)
        {
            return other is TrStr s && s.value == value;
        }

        public bool __lt__(TrObject other)
        {
            return other is TrStr s && value.SequenceLessThan(s.value);
        }

        public bool __le__(TrObject other)
        {
            return other is TrStr s && value.SequenceLessEqualThan(s.value);
        }

        public int __hash__()
        {
            return value.GetHashCode();
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Str("");
            if (narg == 2 && kwargs == null)
            {
                var arg = args[1];
                if (arg is TrStr)
                    return arg;
                return MK.Str(arg.__str__());
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
    }

    [Serializable]
    public partial class TrNone : TrObject
    {
        public Dictionary<TrObject, TrObject> __dict__ => null;

        public object Native => this;

        public TrClass Class => TrClass.NoneClass;

        public static bool unique_set = false;
        public static TrNone Unique = new TrNone();

        [OnDeserialized]
        TrNone _Singleton()
        {
            return Unique;
        }
        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.None();
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
    }
    [Serializable]
    public partial class TrBool: TrObject
    {
        public bool value;

        public bool __bool__() => value;
        public object Native => value;

        public static TrBool TrBool_True;
        public static TrBool TrBool_False;

        private TrBool(bool v)
        {
            value = v;
        }

        static TrBool()
        {
            TrBool_True = new TrBool(true);
            TrBool_False = new TrBool(false);
        }

        public Dictionary<TrObject, TrObject> __dict__ => throw new NotImplementedException();

        public TrClass Class => TrClass.BoolClass;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Bool(false);
            if (narg == 2 && kwargs == null)
            {
                var arg = args[1];
                switch(arg)
                {
                    case TrFloat _: return arg;
                    case TrInt v: return MK.Float(v.value);
                    case TrStr v: return RTS.parse_float(v.value);
                    case TrBool v: return MK.Float(v.value ? 1.0f: 0.0f);
                    default:
                        throw new InvalidCastException($"cannot cast {arg.Class.Name} objects to {clsobj.AsClass.Name}");
                }
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
    }

    [Serializable]
    public partial  class TrTuple: TrObject
    {
        public TrObject[] elts;

        public Dictionary<TrObject, TrObject> __dict__ => throw new NotImplementedException();

        public TrClass Class => TrClass.TupleClass;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Tuple();
            if (narg == 2 && kwargs == null)
            {
                return MK.Tuple(RTS.object_as_array(args[1]));
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }

        public string __repr__() =>
            elts.Length == 1 ? $"({elts[0].__repr__()},)" : "(" + String.Join(", ", elts.Select(x => x.__repr__())) + ")";
    }

    public partial class TrList: TrObject
    {
        public List<TrObject> container;

        public Dictionary<TrObject, TrObject> __dict__ => null;

        public TrClass Class => TrClass.ListClass;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.List();
            if (narg == 2 && kwargs == null)
            {
                return MK.List(RTS.object_to_list(args[1]));
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }

        public IEnumerator<TrObject> __iter__()
        {
            return container.GetEnumerator();
        }

        public TrObject __len__() => MK.Int(container.Count);
    }

    public partial class TrSet: TrObject
    {
        public HashSet<TrObject> container;

        public Dictionary<TrObject, TrObject> __dict__ => null;

        public TrClass Class => TrClass.SetClass;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 1)
                return MK.Set();
            if (narg == 2 && kwargs == null)
            {
                HashSet<TrObject> res = RTS.bareset_create();
                RTS.bareset_extend(res, args[1]);
                return MK.Set(res);
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }
    }

    public class TraffyComparer : IEqualityComparer<TrObject>
    {
        public bool Equals(TrObject x, TrObject y)
        {
            return x.__eq__(y);
        }

        public int GetHashCode([DisallowNull] TrObject obj)
        {
            return obj.__hash__();
        }
    }
    public partial class TrDict: TrObject
    {
        public Dictionary<TrObject, TrObject> container;

        public Dictionary<TrObject, TrObject> __dict__ => null;

        public TrClass Class => TrClass.DictClass;

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
                foreach(var kv in kwargs)
                    res.Add(kv.Key, kv.Value);
                return MK.Dict(res);
            }
            throw new TypeError($"invalid invocation of {clsobj.AsClass.Name}");
        }

        public string __repr__() =>
            "{" + String.Join(", ", container.Select(kv => $"{kv.Key.__repr__()}: {kv.Value.__repr__()}")) + "}";
    }

}