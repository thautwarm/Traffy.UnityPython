using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;
using Traffy.Objects;

#if !NOT_UNITY
using UnityEngine;
using UnityEngine.UI;
namespace Traffy.Unity2D
{
    [UnitySpecific]
    [PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Iterable))]
    public sealed partial class TrComponentGroup : TrObject
    {

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrComponentGroup>("ComponentGroup");
        }


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
        }

        public static TrClass CLASS;
        public override List<TrObject> __array__ => null;
        public override TrClass Class => CLASS;
        TrGameObject obj;
        TrClass componentType;
        public TrComponentGroup(TrGameObject uo, TrClass componentType)
        {
            this.obj = uo;
            this.componentType = componentType;
        }

        public override IEnumerator<TrObject> __iter__()
        {
            if(componentType.__get_components__(componentType, obj, out var components))
            {
                return components.GetEnumerator();
            }
            else
            {
                return MK.EmptyObjectEnumerator;
            }
        }

        [PyBind]
        public TrObject peek
        {
            get
            {
                if (componentType.__get_component__(componentType, obj, out var component))
                {
                    return component;
                }
                return MK.None();
            }
        }

        [PyBind]
        public TrUnityComponent add(TrObject gameState, TrObject parameter = null)
        {
            var comp = componentType.__add_component__(componentType, obj);
            if (componentType.__getic__(componentType.ic__init, out var cls_init))
            {
                cls_init.Call(comp, gameState, parameter ?? MK.None());
            }
            return comp;
        }
    }
}
#endif