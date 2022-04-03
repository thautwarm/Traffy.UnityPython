using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if UNITY_VERSION
using UnityEngine;
#endif



namespace Traffy.Unity2D
{

    //     public static void add_event_handler(GameObject o, EventTriggerType ev, Action<PointerEventData> callback)
    //     {
    //         var trigger = o.GetComponent<EventTrigger>();
    //         if (trigger == null)
    //         {
    //             if (!o.IsUIObject())
    //                 o.AddComponent<Collider2D>();
    //             trigger = o.AddComponent<EventTrigger>();
    //         }
    //         var entry = new EventTrigger.Entry();
    //         entry.eventID = ev;
    //         entry.callback.AddListener(x => callback((PointerEventData)x));
    //         trigger.triggers.Add(entry);
    //     }

    //     public static DObj on_event(GameObject o)
    //     {
    //         DObj call(DObj ev_, DObj callback)
    //         {
    //             var ev = (EventTriggerType)((DInt)ev_).value;
    //             add_event_handler(o, ev, data => callback.__call__((VNEventData)data));
    //             return DNone.unique;
    //         }
    //         return MK.Func2("on_event", call);
    //     }

    //     public static void set_image_for_sprite(GameObject o, DObj child)
    //     {
    //         var render = o.GetComponent<SpriteRenderer>();
    //         if (render == null)
    //         {
    //             render = o.AddComponent<SpriteRenderer>();
    //         }
    //         render.sprite = LoadResource<Sprite>(((DString)child).value);
    //     }

    //     public static DObj ui_support_image(GameObject o)
    //     {
    //         var image = o.GetComponent<RawImage>();
    //         if (image == null)
    //         {
    //             image = o.AddComponent<RawImage>();
    //         }
    //         return o.to_diana(VNBindings.instance.hasUIImage);
    //     }

    //     public static void ui_image_set_resource_path(GameObject o, DObj child)
    //     {
    //         var image = o.GetComponent<RawImage>();
    //         if (image == null)
    //         {
    //             image = o.AddComponent<RawImage>();
    //         }
    //         image.texture = LoadResource<Texture>(((DString)child).value);
    //     }

    //     public static void on_value_changed_dropdown(GameObject o, DObj f)
    //     {
    //         var dropdown = o.GetComponent<Dropdown>();
    //         if (dropdown != null)
    //         {
    //             dropdown.onValueChanged.AddListener(i => f.__call__(MK.Int(i)));
    //             return;
    //         }
    //     }

    //     public static void on_value_changed_toggle(GameObject o, DObj f)
    //     {
    //         var toggle = o.GetComponent<Toggle>();
    //         if (toggle != null)
    //         {
    //             toggle.onValueChanged.AddListener(i => f.__call__(MK.Int(i)));
    //             return;
    //         }
    //     }

    //     public static void on_value_changed_slider(GameObject o, DObj f)
    //     {
    //         var slider = o.GetComponent<Slider>();
    //         if (slider != null)
    //         {
    //             slider.onValueChanged.AddListener(i => f.__call__(MK.Float(i)));
    //             return;
    //         }
    //     }

    //     public static void set_parent(GameObject child, DObj parent)
    //     {
    //         if (parent is DNone)
    //         {
    //             child.transform.parent = VNBindings.instance.canvas.transform;
    //             return;
    //         }
    //         child.gameObject.transform.SetParent(((VNObject)parent).gameObject.transform, false);
    //     }

    //     public static DObj get_parent(GameObject child)
    //     {
    //         if (child.transform.parent)
    //         {
    //             return child.transform.parent.gameObject.to_diana(VNBindings.instance.hasTransform);
    //         }
    //         return DNone.unique;
    //     }

    //     public static void reset_size_according_to_image(GameObject o)
    //     {
    //         int width, height;
    //         var render = o.GetComponentInChildren<SpriteRenderer>();
    //         if (render)
    //         {
    //             width = render.sprite.texture.width;
    //             height = render.sprite.texture.height;
    //             set_sprite_height(o, MK.Int(height));
    //             set_sprite_width(o, MK.Int(width));
    //             return;
    //         }

    //         var image = o.GetComponentInChildren<RawImage>();
    //         if (image)
    //         {
    //             width = image.texture.width;
    //             height = image.texture.height;
    //             set_rect_height(o, MK.Int(height));
    //             set_rect_width(o, MK.Int(width));
    //             return;
    //         }
    //     }
    //     public static DObj get_scrollrect_content(GameObject child)
    //     {
    //         var content = child.GetComponent<ScrollRect>().content;
    //         if (content == null)
    //             return DNone.unique;

    //         return content.gameObject.GetComponent<RectTransform>().gameObject.to_diana(VNBindings.instance.hasTransform);
    //     }

    //     public static DObj get_active(GameObject child)
    //     {
    //         return MK.Int(child.activeInHierarchy);
    //     }

    //     public static void set_active(GameObject child, DObj b)
    //     {
    //         child.SetActive(((int)(DInt)b) != 0);
    //     }

    //     public static DObj get_videoplayer_targetCameraAlpha(GameObject o)
    //     {
    //         return (DFloat)o.GetComponent<VideoPlayer>().targetCameraAlpha;
    //     }

    //     public static void set_videoplayer_targetCameraAlpha(GameObject o, DObj targetCameraAlpha)
    //     {
    //         o.GetComponent<VideoPlayer>().targetCameraAlpha = targetCameraAlpha.toFlt();
    //     }

    //     public static DObj get_videoplayer_url(GameObject o)
    //     {
    //         return (DString)o.GetComponent<VideoPlayer>().url;
    //     }

    //     public static void set_videoplayer_url(GameObject o, DObj url)
    //     {
    //         o.GetComponent<VideoPlayer>().url = url.toStr();
    //     }
    //     public static DObj get_videoplayer_frame(GameObject o)
    //     {
    //         return (DInt)o.GetComponent<VideoPlayer>().frame;
    //     }

    //     public static DObj get_videoplayer_length(GameObject o)
    //     {
    //         return (DFloat)o.GetComponent<VideoPlayer>().length;
    //     }
    //     public static void set_videoplayer_frame(GameObject o, DObj frame)
    //     {
    //         o.GetComponent<VideoPlayer>().frame = frame.toInt();
    //     }

    //     public static DObj get_videoplayer_time(GameObject o)
    //     {
    //         return (DFloat)o.GetComponent<VideoPlayer>().time;
    //     }

    //     public static void set_videoplayer_time(GameObject o, DObj time)
    //     {
    //         o.GetComponent<VideoPlayer>().time = time.toFlt();
    //     }

    //     public static void videoplayer_playto(GameObject videoObject, DObj o)
    //     {
    //         var videoPlayer = videoObject.GetComponent<VideoPlayer>();
    //         var targetObj = o.cast<VNObject>().gameObject;
    //         if (videoPlayer != null)
    //         {
    //             var rawImage = targetObj.GetComponent<RawImage>();
    //             if (rawImage != null)
    //                 rawImage.texture = videoPlayer.targetTexture;
    //         }
    //         return;
    //     }

    // }
}