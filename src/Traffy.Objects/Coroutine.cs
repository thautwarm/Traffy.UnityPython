using System;
using System.Collections;
using System.Collections.Generic;

namespace Traffy.Objects
{
    public class TraffyCoroutine : IEnumerator<TrObject>, TrObject
    {
        // at the end of such generator, result is set.
        private IEnumerator<TrObject> m_generator;
        public TrObject Result;
        public TrObject Sent;
        public IEnumerator<TrObject> generator { set => m_generator = value; }
        public bool MoveNext(TrObject o)
        {
            Sent = o;
            return m_generator.MoveNext();
        }
        public TraffyCoroutine()
        {
            this.Sent = RTS.object_none;
            this.Result = RTS.object_none;
        }

        public TrObject Current => m_generator.Current;

        object IEnumerator.Current => Current;

        public Dictionary<TrObject, TrObject> __dict__ => null;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError($"invalid invocation of {args[0].AsClass.Name}");
        }

        public IEnumerator<TrObject> ToIEnumerator()
        {
            Sent = RTS.object_none;
            while (m_generator.MoveNext())
            {
                yield return Current;
            }
        }

        public bool MoveNext()
        {
            return MoveNext(RTS.object_none);
        }

        public void Reset()
        {
            throw new NotSupportedException("cannot reset Traffy coroutines");
        }

        public void Dispose()
        {
            Sent = RTS.object_none;
        }

        public IEnumerator<TrObject> __iter__() => ToIEnumerator();
        public TrObject __next__() =>
            MoveNext() ? Current : throw new StopIteration(Result);

        [InitSetup(InitOrder.InitClassObjects)]
        static void _InitializeClasses()
        {
            CLASS = TrClass.FromPrototype<TraffyCoroutine>();
            CLASS.Name = "generator";
            CLASS.__new = TraffyCoroutine.datanew;
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }
}