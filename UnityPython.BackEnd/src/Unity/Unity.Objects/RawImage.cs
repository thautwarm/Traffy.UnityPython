using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
using UnityEngine;
namespace Traffy.Unity2D
{
    // public sealed partial class RawImage: TrObject
    // {
    //     RawImage rawImage;
    //     public TraffyBehaviour backref = null;
    // }
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