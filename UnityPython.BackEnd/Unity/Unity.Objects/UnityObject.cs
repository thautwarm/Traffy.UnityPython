using System.Collections.Generic;
using Traffy.Annotations;
using Traffy.Objects;
using UnityEngine;

namespace Traffy.Unity2D
{
    [PyBuiltin]
    public sealed partial class TrUnityObject : TrUnityComponent
    {
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.CreateClass("unity");
        }
        
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc(".__new__", cannot_inst_component));
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = false;
            Initialization.Prelude(CLASS);
        }
        TraffyBehaviour traffy;
        public TraffyBehaviour Raw => traffy;
        public static TrClass CLASS;
        public override TrClass Class => CLASS;public override bool IsUserObject() => true;
        public override GameObject gameObject => traffy.gameObject;
        public override List<TrObject> __array__ => traffy.TraffyObjects;

        public TrUnityObject(TraffyBehaviour traffy)
        {
            this.traffy = traffy;
        }

        [PyBind]
        public TrObject name
        {
            set
            {
                traffy.name = value.AsStr();
            }

            get
            {
                return MK.Str(traffy.name ?? "");
            }
        }

        [PyBind]
        public TrObject x
        {
            set
            {
                var pos = traffy.transform.localPosition;
                pos.x = value.NumToFloat();
                traffy.transform.localPosition = pos;
            }

            get
            {
                return MK.Float(traffy.transform.localPosition.x);
            }
        }

        [PyBind]
        public TrObject y
        {
            set
            {
                var pos = traffy.transform.localPosition;
                pos.y = value.NumToFloat();
                traffy.transform.localPosition = pos;
            }

            get
            {
                return MK.Float(traffy.transform.localPosition.y);
            }
        }

        [PyBind]
        public TrObject z
        {
            set
            {
                var pos = traffy.transform.localPosition;
                pos.z = value.NumToFloat();
                traffy.transform.localPosition = pos;
            }

            get
            {
                return MK.Float(traffy.transform.localPosition.z);
            }
        }
    }

}