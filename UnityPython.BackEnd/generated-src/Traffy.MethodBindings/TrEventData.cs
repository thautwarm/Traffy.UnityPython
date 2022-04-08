using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
#if !NOT_UNITY
namespace Traffy.Unity2D
{
    public sealed partial class TrEventData
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __read_clickCount(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrEventData)_arg).clickCount);
            }
            Action<TrObject, TrObject> __write_clickCount = null;
            CLASS["clickCount"] = TrProperty.Create(CLASS.Name + ".clickCount", __read_clickCount, __write_clickCount);
            static  Traffy.Objects.TrObject __read_clickTime(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrEventData)_arg).clickTime);
            }
            Action<TrObject, TrObject> __write_clickTime = null;
            CLASS["clickTime"] = TrProperty.Create(CLASS.Name + ".clickTime", __read_clickTime, __write_clickTime);
            static  Traffy.Objects.TrObject __read_hovered(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrEventData)_arg).hovered);
            }
            Action<TrObject, TrObject> __write_hovered = null;
            CLASS["hovered"] = TrProperty.Create(CLASS.Name + ".hovered", __read_hovered, __write_hovered);
            static  Traffy.Objects.TrObject __read_is_dragging(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrEventData)_arg).is_dragging);
            }
            Action<TrObject, TrObject> __write_is_dragging = null;
            CLASS["is_dragging"] = TrProperty.Create(CLASS.Name + ".is_dragging", __read_is_dragging, __write_is_dragging);
            static  Traffy.Objects.TrObject __read_is_scrolling(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrEventData)_arg).is_scrolling);
            }
            Action<TrObject, TrObject> __write_is_scrolling = null;
            CLASS["is_scrolling"] = TrProperty.Create(CLASS.Name + ".is_scrolling", __read_is_scrolling, __write_is_scrolling);
            static  Traffy.Objects.TrObject __read_scroll_delta_y(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrEventData)_arg).scroll_delta_y);
            }
            Action<TrObject, TrObject> __write_scroll_delta_y = null;
            CLASS["scroll_delta_y"] = TrProperty.Create(CLASS.Name + ".scroll_delta_y", __read_scroll_delta_y, __write_scroll_delta_y);
            static  Traffy.Objects.TrObject __read_is_pointer_moving(Traffy.Objects.TrObject _arg)
            {
                return Box.Apply(((Traffy.Unity2D.TrEventData)_arg).is_pointer_moving);
            }
            Action<TrObject, TrObject> __write_is_pointer_moving = null;
            CLASS["is_pointer_moving"] = TrProperty.Create(CLASS.Name + ".is_pointer_moving", __read_is_pointer_moving, __write_is_pointer_moving);
        }
    }
}
#endif

