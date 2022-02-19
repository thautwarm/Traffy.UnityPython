using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Traffy.Objects.ExtMonoAsyn;

namespace Traffy.Objects
{

    public sealed class TrCoroutine : TrObject, IEnumerator<TrObject>
    {
        // at the end of such generator, result is set.
        public MonoAsync<TrObject> m_generator;

        private TrCoroutine(MonoAsync<TrObject> generator)
        {
            m_generator = generator;
        }

        public TrObject Current => m_generator.m_Result;

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;


        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError($"invalid invocation of {args[0].AsClass.Name}");
        }

        // check exceptions

        public static TrCoroutine Create(MonoAsync<TrObject> gen)
        {
            return new TrCoroutine(gen);
        }

        public static TrCoroutine Create(MonoAsync<TrObject> gen, Frame frame)
        {
            async MonoAsync<TrObject> wrap(MonoAsync<TrObject> gen, Frame frame)
            {
                try
                {
                    return await gen;
                }
                catch (Exception e)
                {
                    throw RTS.exc_wrap_frame(e, frame);
                }
            }

            var wrapped = wrap(gen, frame);
            return new TrCoroutine(wrapped);
        }

        public void Reset()
        {
            throw new NotSupportedException("cannot reset Traffy coroutines");
        }

        public IEnumerator<TrObject> __iter__() => this;

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public TrObject __next__()
        {
            m_generator.m_Result = RTS.object_none;
            return m_generator.MoveNext() ? m_generator.m_Result : throw new StopIteration(m_generator.m_Result);
        }

        public TrObject __send__(TrObject v)
        {
            m_generator.m_Result = v;
            return m_generator.MoveNext() ? m_generator.m_Result : throw new StopIteration(m_generator.m_Result);
        }

        [Mark(ModuleInit.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrCoroutine>();
            CLASS.Name = "generator";
            CLASS.InitInlineCacheForMagicMethods();
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("generator.__new__", TrCoroutine.datanew));
            TrClass.TypeDict[typeof(TrCoroutine)] = CLASS;
        }
        [Mark(typeof(TrCoroutine))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            ModuleInit.Prelude(CLASS);
        }

        object IEnumerator.Current => m_generator.m_Result;
        public bool MoveNext()
        {
            m_generator.m_Result = RTS.object_none;
            return m_generator.MoveNext();
        }

        public void Dispose(){ }
    }
}