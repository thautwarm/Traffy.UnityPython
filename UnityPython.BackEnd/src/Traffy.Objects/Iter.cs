using System;
using System.Collections;
using System.Collections.Generic;

namespace Traffy.Objects
{

    [Serializable]
    public class TrIter : TrObject, IEnumerator<TrObject>
    {
        public IEnumerator<TrObject> iter;
        public IEnumerator<TrObject> __iter__ => this;
        public object Native => iter;

        public TrIter(IEnumerator<TrObject> itr)
        {
            iter = itr;
        }

        public static TrClass CLASS = null;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;

        IEnumerator<TrObject> TrObject.__iter__() => this;

        public TrObject Current => iter.Current;

        object IEnumerator.Current => ((IEnumerator)iter).Current;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            TrObject clsobj = args[0];
            var narg = args.Count;
            if (narg == 2 && kwargs == null)
            {
                var arg = args[1];
                return MK.Iter(arg.__iter__());
            }
            throw new TypeError($"{clsobj.AsClass.Name}.__new__() takes 2 positional argument(s) but {narg} were given");
        }


        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrIter>("iter");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("iter.__new__", TrIter.datanew));
            TrClass.TypeDict[typeof(TrIter)] = CLASS;
        }

        [Traffy.Annotations.Mark(typeof(TrIter))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        public bool MoveNext()
        {
            return iter.MoveNext();
        }

        public void Reset()
        {
            iter.Reset();
        }

        public void Dispose()
        {
            iter.Dispose();
        }
    }

}