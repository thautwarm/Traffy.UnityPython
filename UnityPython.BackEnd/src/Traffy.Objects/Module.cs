using System;
using System.Collections.Generic;
using System.Linq;

namespace Traffy.Objects
{
    public partial class TrModule : TrObject
    {

        public static TrModule CreateUninitialized(string name, ModuleSpec spec)
        {
            return new TrModule { m_Name = name, m_Spec = spec, m_Executed = false };
        }

        internal void InitInst()
        {
            if (m_Executed)
            {
                return;
            }
            m_Executed = true;
            RTS.baredict_add(Namespace, MK.Str("__name__"), MK.Str(m_Name));
            Initialization.Populate(Namespace);
            m_Spec.Exec(Namespace);
        }

        public string Name => m_Name;
        string m_Name;
        bool m_Executed;
        ModuleSpec m_Spec;
        public Dictionary<TrObject, TrObject> Namespace = RTS.baredict_create();

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        List<TrObject> TrObject.__array__ => null;

        bool TrObject.__bool__() => true;
        TrObject TrObject.__getitem__(TrObject item) => throw new TypeError("'module' object is not subscriptable");
        void TrObject.__setitem__(TrObject key, TrObject value) => throw new TypeError("'module' object is not subscriptable");
        bool TrObject.__getic__(Traffy.InlineCache.PolyIC ic, out Traffy.Objects.TrObject found) =>
            _read_module(ic.attribute, out found) || CLASS.__getic__(ic, out found);
        void TrObject.__setic__(Traffy.InlineCache.PolyIC ic, Traffy.Objects.TrObject value) => _write_module(ic.attribute, value);
        bool TrObject.__getic_refl__(Traffy.Objects.TrStr s, out Traffy.Objects.TrObject found) =>
            _read_module(s, out found) || CLASS.__getic_refl__(s, out found);
        void TrObject.__setic_refl__(TrStr s, TrObject value) => _write_module(s, value);

        public bool _read_module(TrStr name, out TrObject found)
        {
            return Namespace.TryGetValue(name, out found);
        }

        public void _write_module(TrStr name, TrObject value)
        {
            Namespace[name] = value;
        }


        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrModule>("module");
            TrClass.TypeDict[typeof(TrModule)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrModule))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

    }
}