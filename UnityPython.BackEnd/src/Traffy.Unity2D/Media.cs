#if !NOT_UNITY
using UnityEngine;
namespace Traffy.Unity2D
{
    public static class Media
    {

        
        public enum ImageFormat
        {
            PNG,
            JPG
        }
        public static Texture2D Cast(this THint<Texture2D> _, byte[] bytes, TextureFormat format)
        {
            var tex = new Texture2D(2, 2, format, false);
            tex.LoadImage(bytes);
            return tex;
        }

        public static byte[] Cast(this THint<byte[]> _, Texture2D tex)
        {
            return tex.EncodeToPNG();
        }
        public static Sprite Cast(this THint<Sprite> _, Texture2D tex)
        {
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
    }

}
#endif