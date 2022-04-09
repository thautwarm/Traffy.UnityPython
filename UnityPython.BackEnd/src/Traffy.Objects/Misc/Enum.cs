using System;
using System.Collections.Generic;
using System.Linq;
using Traffy.Annotations;
using Traffy.InlineCache;

namespace Traffy.Objects
{

    [PyBuiltin]
    public partial class TrEnum : TrUserObjectBase
    {
        public static TrClass CLASS;
        public override TrClass Class => UserEnumClass;
        public TrClass UserEnumClass;
        public int EnumIndex;
        public override List<TrObject> __array__ { get; }
        public override string __repr__()
        {
            var (name, value) = UserEnumClass.EnumHelperField[EnumIndex];
            return $"<{UserEnumClass.Name}.{name.value}: {value.__repr__()}>";
        }
        [PyBind]
        public TrObject name => UserEnumClass.EnumHelperField[EnumIndex].Name;
        [PyBind]
        public TrObject value => UserEnumClass.EnumHelperField[EnumIndex].Value;
        public static void SetupEnumClass((TrStr Name, TrObject Value)[] many, TrClass cls)
        {
            cls.EnumHelperField = many;
            for (int i = 0; i < many.Length; i++)
            {
                cls[many[i].Name.value] = new TrEnum(cls, i);
            }
        }
        public TrEnum(TrClass cls, int i)
        {
            UserEnumClass = cls;
            EnumIndex = i;
        }
        [PyBind]
        public static void __init_subclass__(TrObject _, TrObject newclsobj)
        {
            if (newclsobj is TrClass newcls)
            {
                if (newcls.__getic_refl__(MK.Str(name_ClassAnnotation), out var ann) && ann is TrTuple ann_tuple)
                {
                    var enumerations = new List<(TrStr, TrObject)>();
                    for (int i = 0; i < ann_tuple.elts.UnList.Length; i++)
                    {
                        if (!(ann_tuple.elts[i] is TrStr name))
                        {
                            throw new ValueError($"invalid class fields ({newcls.Name}): {name_ClassAnnotation} should be a tuple of strings!");
                        }
                        TrObject enumInnerValue;
                        if (newcls.__getic_refl__(name, out var value) && !object.ReferenceEquals(value, Modules.TrModule_enum.auto()))
                        {
                            enumInnerValue = value;
                        }
                        else
                        {
                            enumInnerValue = MK.Int(i);
                        }
                        enumerations.Add((name, enumInnerValue));
                    }
                    SetupEnumClass(enumerations.ToArray(), newcls);
                }
            }
            else
            {
                throw new TypeError("__init_subclass__ must be called with a class as first argument");
            }
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrEnum>("Enum");
            CLASS.IsSealed = false;
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind("Enum.__new__", datanew);
        }

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError("Enum should be not used directly");
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsClassFixed = true;
        }
    }

}