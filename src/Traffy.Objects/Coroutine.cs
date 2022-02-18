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

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError($"invalid invocation of {args[0].AsClass.Name}");
        }

        // check exceptions
        public void AsTopLevelGenerator(Frame frame)
        {
            IEnumerator<TrObject> wrap(IEnumerator<TrObject> gen, Frame frame)
            {
                bool test;
            loop_head:
                try
                {
                    test = gen.MoveNext();
                }
                catch (Exception e)
                {
                    throw RTS.exc_wrap_frame(e, frame);
                }
                if (test)
                {
                    yield return gen.Current;
                    goto loop_head;
                }
            }
            m_generator = wrap(m_generator, frame);
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

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TraffyCoroutine>();
            CLASS.Name = "generator";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("generator.__new__", TraffyCoroutine.datanew));
            TrClass.TypeDict[typeof(TraffyCoroutine)] = CLASS;
        }
        [Mark(typeof(TraffyCoroutine))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }
    }
}