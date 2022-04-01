using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;

namespace Traffy.Modules
{
    [PyBuiltin]
    public sealed partial class TrModule_json : TrObject
    {
        public override List<TrObject> __array__ => null;

        public override TrClass Class => CLASS;

        public static TrClass CLASS;

        [PyBind]
        public static TrObject loads(string src)
        {
            return SimpleJSON.JSON.JSONToPy(SimpleJSON.JSON.Parse(src));
        }

        [PyBind]
        public static TrObject dumps(TrObject self, [PyBind.Keyword(Only = true)] int indent = 0)
        {
            if (indent == 0)
                return MK.Str(SimpleJSON.JSON.PyToJson(self).ToString());
            if (indent < 0)
                throw new ValueError("indent must be >= 0");
            return MK.Str(SimpleJSON.JSON.PyToJson(self).ToString(indent));
        }

        [PyBind]
        public static TrObject JSON => TrDict.CLASS
                                        .__or__(TrList.CLASS)
                                        .__or__(TrInt.CLASS)
                                        .__or__(TrFloat.CLASS)
                                        .__or__(TrStr.CLASS)
                                        .__or__(TrBool.CLASS)
                                        .__or__(TrNone.CLASS)
                                        ;


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrModule_json>("module_json");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("module_json.__new__", TrClass.new_notallow));
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;

            ModuleSystem.Modules["json"] = CLASS;
        }
    }
}