using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Interfaces;
using Traffy.Objects;
#if !NOT_UNITY
namespace Traffy.Unity2D
{
    [PyBuiltin]
    [UnitySpecific]
    public sealed partial class TrMonoBehaviour : TrUnityComponent
    {
        public static TrClass CLASS;
        private TrMonoBehaviour(TrGameObject baseObject) : base(baseObject)
        {
            throw new System.NotImplementedException();
        }

        public override TrClass Class => CLASS;
        public override List<TrObject> __array__ => null;
        
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
            var component = TrUnityUserComponent.Create(uo, klass);
            components.Add(component);
            return component;
        }

        [PyBind]
        public static void __init_subclass__(TrObject _, TrClass newcls)
        {
            if (RTS.issubclassof(newcls, CLASS) && newcls.UnityKind == TrClass.UnityComponentClassKind.NotUnity)
            {
                newcls.UnityKind = TrClass.UnityComponentClassKind.UserComponent;
                newcls.__add_component__ = user__add_component__;
                newcls.__get_component__ = user__get_component__;
                newcls.__get_components__ = user__get_components__;
            }
            else
            {
                throw new TypeError($"TraffyBehaviour.__init_subclass__: argument 1 must be a subclass of {CLASS.Name}, got {newcls.Name}");
            }
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrTypedDict>("TraffyBehaviour");
            CLASS.IsSealed = false;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }

        public override void RemoveComponent()
        {
            throw new System.NotImplementedException();
        }
    }
}
#endif