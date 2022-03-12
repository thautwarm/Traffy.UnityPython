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

        static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;

        public bool __bool__() => true;
        TrObject TrObject.__getitem__(TrObject item) => throw new TypeError("'module' object is not subscriptable");
        void TrObject.__setitem__(TrObject key, TrObject value) => throw new TypeError("'module' object is not subscriptable");

        public TrObject __getattr__(TrObject name)
        {
            if (Namespace.TryGetValue(name, out var value))
            {
                return value;
            }
            throw new AttributeError(this, name, $"No attribute {name.__repr__()}");
        }

        public void __setattr__(TrObject name, TrObject value)
        {
            if (name is TrStr)
                Namespace[name] = value;
            else
                throw new TypeError($"Attribute name must be a string, got {name.Class.Name}");
        }

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrModule>("module");
            CLASS.InstanceUseInlineCache = false;
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