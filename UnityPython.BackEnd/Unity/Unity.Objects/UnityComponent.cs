using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
#if UNITY_VERSION
using UnityEngine;
#endif

namespace Traffy.Unity2D
{

    public abstract class TrUnityComponent : TrUserObjectBase
    {
        public TraffyBehaviour backref = null;
#if UNITY_VERSION
        public abstract GameObject gameObject { get; }
#endif
        public override List<TrObject> __array__ 
        {
            get
            {
#if UNITY_VERSION
                var tb = gameObject.GetComponent<TraffyBehaviour>();
                if (tb != null)
                    return tb.TraffyObjects;
#endif                
                return null;
            }
        }
        public abstract bool IsUserObject();
        public static TrObject cannot_inst_component(TrClass clsobj, BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError($"cannot directory instantiate class {clsobj.Name}. \n" +
                                $"Maybe you mean 'gameObject.AddComponent({clsobj.Name})'?");
        }

        [PyBind]
        public TrObject baseobject
        {
            get
            {
#if UNITY_VERSION
                if (IsUserObject())
                {
                    var tb = gameObject.GetComponent<TraffyBehaviour>();
                    if (tb == null)
                    {
                        tb = gameObject.AddComponent<TraffyBehaviour>();
                        tb.TraffyObjects = tb.TraffyObjects ?? new List<TrObject>();
                    }
                    return new TrUnityObject(tb);
                }
                else
#endif
                    return MK.None();
            }
        }
    }
}