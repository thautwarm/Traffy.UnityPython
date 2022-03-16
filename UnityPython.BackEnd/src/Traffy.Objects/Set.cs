using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [PyBuiltin]
    public partial class TrSet : TrObject
    {
        public HashSet<TrObject> container;

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;
        public override IEnumerator<TrObject> __iter__() => container.GetEnumerator();

        public override string __repr__() => "{" + string.Join(", ", container.Select(x => x.__repr__())) + "}";


        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrSet>("set");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("set.__new__", TrSet.datanew);
            CLASS.IsSealed = true;
            TrClass.TypeDict[typeof(TrSet)] = CLASS;
        }
        [Traffy.Annotations.Mark(typeof(TrSet))]
        static void _SetupClasses()
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
    }

}