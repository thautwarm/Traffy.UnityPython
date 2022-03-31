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
    }

}