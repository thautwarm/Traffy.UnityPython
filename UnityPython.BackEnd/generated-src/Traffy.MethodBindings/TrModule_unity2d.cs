using System;
using System.Collections.Generic;
using Traffy.Objects;
using Traffy.Annotations;
#if !NOT_UNITY
namespace Traffy.Unity2D
{
    public sealed partial class TrModule_unity2d
    {
        internal static void generated_BindMethods()
        {
            static  Traffy.Objects.TrObject __bind_getPersistentDataPath(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 0:
                        return Box.Apply(Traffy.Unity2D.TrModule_unity2d.getPersistentDataPath());
                    default:
                        throw new ValueError("getPersistentDataPath() requires 0 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["getPersistentDataPath"] = TrStaticMethod.Bind(CLASS.Name + "." + "getPersistentDataPath", __bind_getPersistentDataPath);
            static  Traffy.Objects.TrObject __bind_getProjectDir(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 0:
                        return Box.Apply(Traffy.Unity2D.TrModule_unity2d.getProjectDir());
                    default:
                        throw new ValueError("getProjectDir() requires 0 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["getProjectDir"] = TrStaticMethod.Bind(CLASS.Name + "." + "getProjectDir", __bind_getProjectDir);
            static  Traffy.Objects.TrObject __bind_setProjectDirectory(BList<TrObject> __args,Dictionary<TrObject,TrObject> __kwargs)
            {
                switch(__args.Count)
                {
                    case 1:
                    {
                        var _0 = Unbox.Apply(THint<string>.Unique,__args[0]);
                        Traffy.Unity2D.TrModule_unity2d.setProjectDirectory(_0);
                        return Traffy.MK.None();
                    }
                    default:
                        throw new ValueError("setProjectDirectory() requires 1 positional argument(s), got " + __args.Count);
                }
            }
            CLASS["setProjectDirectory"] = TrStaticMethod.Bind(CLASS.Name + "." + "setProjectDirectory", __bind_setProjectDirectory);
            CLASS["Color"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.Color);
            CLASS["Vector2"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.Vector2);
            CLASS["Vector3"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.Vector3);
            CLASS["ImageResource"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.ImageResource);
            CLASS["EventTriggerType"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.EventTriggerType);
            CLASS["EventData"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.EventData);
            CLASS["MonoBehaviour"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.MonoBehaviour);
            CLASS["GameObject"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.GameObject);
            CLASS["ComponentGroup"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.ComponentGroup);
            CLASS["UI"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.UI);
            CLASS["SpriteImage"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.SpriteImage);
            CLASS["Text"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.Text);
            CLASS["RawImage"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.RawImage);
            CLASS["Sprite"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.Sprite);
            CLASS["ScrollRect"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.ScrollRect);
            CLASS["PolygonCollider2D"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.PolygonCollider2D);
            CLASS["CanvasGroup"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.CanvasGroup);
            CLASS["Canvas"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.Canvas);
            CLASS["RectTransform"] = Traffy.Box.Apply(Traffy.Unity2D.TrModule_unity2d.RectTransform);
        }
    }
}
#endif

