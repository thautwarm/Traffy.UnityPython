using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if UNITY_VERSION
using UnityEngine;
using UnityEngine.UI;
#endif


namespace Traffy.Unity2D
{
    public sealed partial class TrRawImage: TrUnityComponent
    {

#if UNITY_VERSION
        RawImage rawImage;
        public override GameObject gameObject => rawImage.rectTransform.gameObject;
#endif

        public static TrClass CLASS;
        public override TrClass Class => throw new System.NotImplementedException();

        public override bool IsUserObject() => false;

        // [PyBind]
        // public TrObject alpha
        // {
        //     get => MK.Int(rawImage.color.a);
        //     set => rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, value.NumToFloat());   
        // }
    }
    // public TrObject get_alpha_image(GameObject o)
    //     {
    //         var image = o.GetComponent<RawImage>();
    //         if (image != null)
    //         {
    //             return MK.Float(image.color.a);
    //         }
    //         throw invalid_access(o, "alpha");
    //         // TODO: warning
    //     }
    //     public static void set_alpha_image(GameObject o, DObj i)
    //     {
    //         var image = o.GetComponent<RawImage>();
    //         if (image != null)
    //         {
    //             var color = image.color;
    //             float alpha;

    //             switch (i)
    //             {
    //                 case DInt di:

    //                     invalidate_int_range("color alpha", di.value, high: 255);
    //                     alpha = di.value / 255.0f;
    //                     break;
    //                 case DFloat df:
    //                     invalidate_float_range("color alpha", df.value, high: 1.0f);
    //                     alpha = df.value;
    //                     break;
    //                 default:
    //                     throw new TypeError("alpha should be 0-255 integer or 0-1 float.");
    //             };
    //             color.a = alpha;
    //             image.color = color;
    //         }
    //         // TODO: warning
    //     }

}