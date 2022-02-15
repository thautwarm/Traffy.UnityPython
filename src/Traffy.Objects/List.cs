using System.Collections.Generic;

namespace Traffy.Objects
{
    public partial class TrList: TrObject
    {
        public List<TrObject> container;

        public Dictionary<TrObject, TrObject> __dict__ => null;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype<TrList>();
            CLASS.Name = "list";
            CLASS.__new = TrList.datanew;
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }

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

}