using System;
using System.Collections;
using System.Collections.Generic;
using Traffy.Annotations;

namespace Traffy.Objects
{

    [PyBuiltin]
    public sealed partial class TrIter : TrObject, IEnumerator<TrObject>
    {
        public IEnumerator<TrObject> iter;
        public override IEnumerator<TrObject> __iter__() => this;
        public override object Native => iter;

        public TrIter(IEnumerator<TrObject> itr)
        {
            iter = itr;
        }

        public static TrClass CLASS = null;
        public override TrClass Class => CLASS;
        public override List<TrObject> __array__ => null;

        public TrObject Current => iter.Current;

        object IEnumerator.Current => ((IEnumerator)iter).Current;

        [PyBind]
        public static TrObject __new__(TrObject _ /* class */, TrObject obj)
        {
            return MK.Iter(obj.__iter__());
        }

        public override bool __next__(TrRef refval)
        {
            if (iter.MoveNext())
            {
                refval.value = iter.Current;
                return true;
            }
            else
            {
                return false;
            }
        }



        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrIter>("iter");
            TrClass.TypeDict[typeof(TrIter)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrIter))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public bool MoveNext() => iter.MoveNext();
        public void Reset() => iter.Reset();

        public void Dispose() => iter.Dispose();
    }

}