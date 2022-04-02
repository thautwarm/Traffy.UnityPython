using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
using UnityEngine;
namespace Traffy.Unity2D
{

    public abstract class TrUnityComponent : TrUserObjectBase
    {
        public TraffyBehaviour backref = null;
        public abstract GameObject gameObject { get; }
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
                {
                    return MK.None();
                }
            }
        }
    }
}