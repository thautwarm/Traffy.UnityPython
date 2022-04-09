using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if !NOT_UNITY

namespace Traffy.Unity2D
{
    [PyBuiltin]
    [UnitySpecific]
    public sealed partial class TrModule_unity2d : TrObject
    {
        public override List<TrObject> __array__ => null;

        public override TrClass Class => CLASS;

        public static TrClass CLASS;

        [PyBind]
        public static TrClass MonoBehaviour => TrMonoBehaviour.CLASS;
        [PyBind]
        public static TrClass EventTrigger => TrEventTriggerType.CLASS;
        [PyBind]
        public static TrClass EventData => TrEventData.CLASS;
        [PyBind]
        public static TrClass Vector2 => TrVector2.CLASS;
        [PyBind]
        public static TrClass Vector3 => TrVector3.CLASS;
        [PyBind]
        public static TrClass Canvas => TrCanvas.CLASS;
        [PyBind]
        public static TrClass CanvasGroup => TrCanvasGroup.CLASS;
        [PyBind]
        public static TrClass PolygonCollider2D => TrPolygonCollider2D.CLASS;
        [PyBind]
        public static TrClass RawImage => TrRawImage.CLASS;
        [PyBind]
        public static TrClass SpriteImage => TrSpriteImage.CLASS;
        [PyBind]
        public static TrClass Sprite => TrSprite.CLASS;
        [PyBind]
        public static TrClass ScrollRect => TrScrollRect.CLASS;
        [PyBind]
        public static TrClass Text => TrText.CLASS;
        [PyBind]
        public static TrClass UI => TrUI.CLASS;
        [PyBind]
        public static TrClass RectTransform => TrUI.CLASS;
        [PyBind]
        public static TrClass ImageResource => TrImageResource.CLASS;
        [PyBind]
        public static TrClass Color => TrColor.CLASS;

        [PyBind]
        public static TrObject getPersistentDataPath() => MK.Str(UnityEngine.Application.persistentDataPath);

        [PyBind]
        public static TrObject getProjectDir() => MK.Str(UnityRTS.Get.ProjectDirectory);

        [PyBind]
        public static void setProjectDirectory(string path)
        {
            UnityRTS.Get.ProjectDirectory = path;
            UnityRTS.Get.ReSetting();
        }


        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrModule_unity2d>("module_unity2d");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("module_unity2d.__new__", TrClass.new_notallow));
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;

            ModuleSystem.Modules["unity2d"] = CLASS;
        }
    }
}
#endif