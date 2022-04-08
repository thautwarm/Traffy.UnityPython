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
    public abstract class TrUIComponent : TrUnityComponent
    {
        TrUI _ui;

        [PyBind]
        public TrUI ui
        {
            get
            {
                if (_ui == null)
                {
                    var rect = baseObject.gameObject.GetComponent<RectTransform>();
                    if (rect == null)
                        throw new ValueError("TrRawImage: RectTransform is not found!");
                    _ui = TrUI.FromRaw(baseObject, rect);
                }
                return _ui;
            }
        }

        [PyBind]
        public TrObject width
        {
            set
            {
                ui.native.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value.ToFloat());
            }

            get => MK.Float(ui.native.rect.width);
        }

        [PyBind]
        public TrObject height
        {
            set
            {
                ui.native.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value.ToFloat());
            }

            get => MK.Float(ui.native.rect.height);
        }

        protected TrUIComponent(TrGameObject baseObject): base(baseObject)
        { }
    }
}

#endif