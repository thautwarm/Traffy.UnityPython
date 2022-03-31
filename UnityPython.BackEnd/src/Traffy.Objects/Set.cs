using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [PyInherit(typeof(Traffy.Interfaces.Collection))]
    [PyBuiltin]
    public partial class TrSet : TrObject
    {
        public HashSet<TrObject> container;

        public static TrClass CLASS;
        public override TrClass Class => CLASS;
        public override object Native => container;

        public override List<TrObject> __array__ => null;
        public override IEnumerator<TrObject> __iter__() => container.GetEnumerator();

        public override bool __eq__(TrObject other)
        {
            if (other is TrSet otherSet)
            {
                return container.SetEquals(otherSet.container);
            }
            return false;
        }

        public override bool __lt__(TrObject other)
        {
            if (other is TrSet otherSet)
            {
                return container.IsProperSubsetOf(otherSet.container);
            }
            return false;
        }

        public override bool __le__(TrObject other)
        {
            if (other is TrSet otherSet)
            {
                return container.IsSubsetOf(otherSet.container);
            }
            return false;
        }

        public override bool __gt__(TrObject other)
        {
            if (other is TrSet otherSet)
            {
                return container.IsProperSupersetOf(otherSet.container);
            }
            return false;
        }

        public override bool __ge__(TrObject other)
        {
            if (other is TrSet otherSet)
            {
                return container.IsSupersetOf(otherSet.container);
            }
            return false;
        }

        public override TrObject __and__(TrObject a)
        {
            if (a is TrSet otherSet)
            {
                var newset = RTS.bareset_create(this);
                newset.IntersectWith(otherSet.container);
                return MK.Set(newset);
            }
            throw new TypeError("unsupported operand type(s) for &: 'set' and '" + a.Class.Name + "'");
        }

        public override TrObject __or__(TrObject a)
        {
            if (a is TrSet otherSet)
            {
                var newset = RTS.bareset_create(this);
                newset.UnionWith(otherSet.container);
                return MK.Set(newset);
            }
            throw new TypeError("unsupported operand type(s) for |: 'set' and '" + a.Class.Name + "'");
        }

        public override TrObject __xor__(TrObject a)
        {
            if (a is TrSet otherSet)
            {
                var newset = RTS.bareset_create(this);
                newset.SymmetricExceptWith(otherSet.container);
                return MK.Set(newset);
            }
            throw new TypeError("unsupported operand type(s) for ^: 'set' and '" + a.Class.Name + "'");
        }

        public override bool __contains__(TrObject e)
        {
            return container.Contains(e);
        }

        public override TrObject __len__()
        {
            return MK.Int(container.Count);
        }

        public override TrObject __sub__(TrObject a)
        {
            if (a is TrSet otherSet)
            {
                var newset = RTS.bareset_create(this);
                newset.ExceptWith(otherSet.container);
                return MK.Set(newset);
            }
            throw new TypeError("unsupported operand type(s) for -: 'set' and '" + a.Class.Name + "'");
        }

        public override TrObject __add__(TrObject a)
        {
            if (a is TrSet otherSet)
            {
                var newset = RTS.bareset_create(this);
                newset.UnionWith(otherSet.container);
                return MK.Set(newset);
            }
            throw new TypeError("unsupported operand type(s) for -: 'set' and '" + a.Class.Name + "'");
        }
        
        public override string __repr__() => "{" + string.Join(", ", container.Select(x => x.__repr__())) + "}";

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrSet>("set");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("set.__new__", TrSet.datanew);
            CLASS.IsSealed = true;
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

        [PyBind]
        public void add(TrObject elt)
        {
            container.Add(elt);
        }

        [PyBind]
        public void clear()
        {
            container.Clear();
        }

        [PyBind]
        public HashSet<TrObject> copy()
        {
            return RTS.bareset_create(this);
        }

        [PyBind]
        public void difference_update(HashSet<TrObject> set)
        {
            container.ExceptWith(set);
            return;
        }
        
        [PyBind]
        public void discard(TrObject elt)
        {
            container.Remove(elt);
        }

        [PyBind]
        public HashSet<TrObject> intersection(HashSet<TrObject> set)
        {
            var newset = RTS.bareset_create(this);
            newset.IntersectWith(set);
            return newset;
        }

        [PyBind]
        public void intersection_update(HashSet<TrObject> set)
        {
            container.IntersectWith(set);
            return;
        }

        [PyBind]
        public bool isdisjoint(HashSet<TrObject> set)
        {
            return !container.Overlaps(set);
        }

        [PyBind]
        public bool issubset(HashSet<TrObject> set)
        {
            return container.IsSubsetOf(set);
        }

        [PyBind]
        public bool issuperset(HashSet<TrObject> set)
        {
            return container.IsSupersetOf(set);
        }

        [PyBind]
        public TrObject pop()
        {
            if (container.Count == 0)
                throw new KeyError("pop from an empty set");
            var res = container.First();
            container.Remove(res);
            return res;
        }

        [PyBind]
        public TrObject remove(TrObject elt)
        {
            if (!container.Remove(elt))
                throw new KeyError(elt);
            return elt;
        }

        [PyBind]
        public HashSet<TrObject> symmetric_difference(HashSet<TrObject> set)
        {
            var newset = RTS.bareset_create(this);
            newset.SymmetricExceptWith(set);
            return newset;
        }

        [PyBind]
        public void symmetric_difference_update(HashSet<TrObject> set)
        {
            container.SymmetricExceptWith(set);    
        }

        [PyBind]
        public HashSet<TrObject> union(HashSet<TrObject> set)
        {
            var newset = RTS.bareset_create(this);
            newset.UnionWith(set);
            return newset;
        }

        [PyBind]
        public void update(IEnumerator<TrObject> itr)
        {
            while (itr.MoveNext())
            {
                container.Add(itr.Current);
            }
        }
    }

}