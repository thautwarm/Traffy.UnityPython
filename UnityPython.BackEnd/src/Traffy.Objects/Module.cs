using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Traffy.Annotations;

namespace Traffy.Objects
{
    [PyBuiltin]
    public partial class TrModule : TrObject
    {
        public override IEnumerable<(TrStr, TrObject)> GetDictItems()
        {
            foreach (var kv in Namespace)
            {
                if (kv.Key is TrStr s)
                {
                    yield return (s, kv.Value);
                }
            }
        }

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
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;

        public override bool __bool__() => true;
        public override string __repr__() => $"<module '{m_Name}'>";
        public override bool __getic__(Traffy.InlineCache.PolyIC ic, out Traffy.Objects.TrObject found) =>
            _read_module(ic.attribute, out found) || _read_module_from_type(ic, out found);
        public override void __setic__(Traffy.InlineCache.PolyIC ic, Traffy.Objects.TrObject value) => _write_module(ic.attribute, value);
        public override bool __getic_refl__(Traffy.Objects.TrStr s, out Traffy.Objects.TrObject found) =>
            _read_module(s, out found) || _read_module_from_type(s, out found);
        public override void __setic_refl__(TrStr s, TrObject value) => _write_module(s, value);

        public bool _read_module(TrStr name, out TrObject found)
        {
            return Namespace.TryGetValue(name, out found);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal bool _read_module_from_type(TrStr name, out TrObject found)
        {
            if (CLASS.__getic_refl__(name, out found))
            {
                if (found is TrProperty prop)
                {
                    found = prop.Get(this);
                }
                return true;
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal bool _read_module_from_type(Traffy.InlineCache.PolyIC ic, out TrObject found)
        {
            if (CLASS.__getic_refl__(ic.attribute, out found))
            {
                if (found is TrProperty prop)
                {
                    found = prop.Get(this);
                }
                return true;
            }
            return false;
        }

        public void _write_module(TrStr name, TrObject value)
        {
            Namespace[name] = value;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrModule>("module");
        }


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            // Initialization.Prelude(CLASS);
        }

        [PyBind]
        public TrObject __name__ => MK.Str(m_Name);

        [PyBind]
        public TrObject __dict__ => MK.Dict(Namespace);
    }
}