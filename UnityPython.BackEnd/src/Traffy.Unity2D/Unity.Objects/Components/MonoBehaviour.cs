using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if !NOT_UNITY
using UnityEngine;

namespace Traffy.Unity2D
{
    [PyBuiltin]
    [UnitySpecific]
    public sealed partial class TrMonoBehaviour: TrUnityComponent
    {
        internal static bool user__get_component__(TrClass klass, TrGameObject uo, out TrUnityComponent component)
        {
            if (uo.Components.TryGetValue(klass.ClassId, out var components) && components.Count != 0)
            {
                component = components[0];
                return true;
            }
            component = null;
            return false;
        }

        internal static bool user__get_components__(TrClass klass, TrGameObject uo, out IEnumerable<TrUnityComponent> components_)
        {
            if (uo.Components.TryGetValue(klass.ClassId, out var components) && components.Count != 0)
            {
                components_ = components;
                return true;
            }
            components_ = null;
            return false;
        }

        internal static TrUnityComponent user__add_component__(TrClass klass, TrGameObject uo)
        {
            if (klass.UnityKind != TrClass.UnityComponentClassKind.UserComponent)
            {
                throw new TypeError($"Cannot add component {klass.Name}: class {klass.Name} does not have a __new__ from MonoBehaviour");
            }
            var components = uo.Components.GetOrUpdate(
                klass.ClassId,
                () => new List<TrUnityComponent>(1));
            var component = TrMonoBehaviour.Create(uo, klass);
            components.Add(component);
            return component;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrMonoBehaviour>("MonoBehaviour");
            CLASS.IsSealed = false;
            CLASS.IsInstanceFixed = false;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("MonoBehaviour.__new__", TrClass.new_notallow);
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsClassFixed = true;
        }
        private TrMonoBehaviour(TrGameObject uo, TrClass cls): base(uo)
        {
            INST_CLASS = cls;
        }
        public static TrClass CLASS;
        public TrClass INST_CLASS;
        public override TrClass Class => INST_CLASS;
        public override object Native => this;
        public override List<TrObject> __array__ { get; } = new List<TrObject>();

        public static TrMonoBehaviour Create(TrGameObject uo, TrClass cls)
        {
            return new TrMonoBehaviour(uo, cls);
        }

        public override void RemoveComponent()
        {
            if (baseObject.Components.TryGetValue(INST_CLASS.ClassId, out var components) && components.Count != 0)
            {
                components.Remove(this);
            }
        }

        [PyBind]
        public static void __init_subclass__(TrObject _, TrClass newcls)
        {
            if (RTS.issubclassof(newcls, CLASS))
            {
                if (newcls.UnityKind == TrClass.UnityComponentClassKind.NotUnity)
                {
                    newcls.UnityKind = TrClass.UnityComponentClassKind.UserComponent;
                    newcls.__add_component__ = user__add_component__;
                    newcls.__get_component__ = user__get_component__;
                    newcls.__get_components__ = user__get_components__;
                }
            }
            else
            {
                throw new TypeError($"MonoBehaviour.__init_subclass__: argument 1 must be a subclass of {CLASS.Name}, got {newcls.Name}");
            }
        }
    }
}
#endif