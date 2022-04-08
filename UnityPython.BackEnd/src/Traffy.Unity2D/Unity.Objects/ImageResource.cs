using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;
using Traffy.Objects;
#if !NOT_UNITY
using UnityEngine;
using UnityEngine.UI;


namespace Traffy.Unity2D
{
    [PyBuiltin]
    [UnitySpecific]
    public sealed partial class TrImageResource: TrObject
    {
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrImageResource>("ImageResource");
            CLASS.IsSealed = true;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public override List<TrObject> __array__ => null;

        public override string __repr__()
        {
            return $"<ImageResource InstanceId={native.GetInstanceID()}>";
        }

        [PyBind(Name = nameof(__new__))]
        public static TrObject __new_sprite_resource__(BList<TrObject> __args, Dictionary<TrObject, TrObject> __kwargs)
        {
            throw new TypeError($"{CLASS.Name} has no constructor, use {CLASS.Name}.{nameof(load)} instead.");
        }

        [PyBind]
        public static TrImageResource load(string path)
        {
            var bytes = Traffy.IO.ReadFileBytes(path);
            var tex = Media.Cast(THint<Texture2D>.Unique, bytes, TextureFormat.RGBA32);
            return new TrImageResource(tex);
        }

        public static TrImageResource FromRaw(Texture2D tex)
        {
            var allocations = UnityRTS.Get.allocations;
            if (allocations.TryGetValue(tex, out var obj))
                return obj as TrImageResource;
            var uo = new TrImageResource(tex);
            allocations[tex] = uo;
            return uo;
        }

        [PyBind]
        public void destory()
        {
            if (s_sprite != null)
            {
                Object.Destroy(s_sprite);
                s_sprite = null;
            }
            Object.Destroy(native);
        }

        public readonly Texture2D native;
        Sprite s_sprite;

        public Sprite sprite
        {
            get
            {
                AssureSprite();
                return s_sprite;
            }
        }
        private TrImageResource(Texture2D tex)
        {
            native = tex;
            s_sprite = null;
        }
        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public void AssureSprite()
        {
            if (s_sprite != null)
                return;
            s_sprite = Sprite.Create(native, new Rect(0, 0, native.width, native.height), new Vector2(0.5f, 0.5f), UnityRTS.PixelPerUnit);
        }
    }
}

#endif