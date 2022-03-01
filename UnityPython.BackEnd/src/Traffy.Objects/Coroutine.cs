using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Traffy.Annotations;

namespace Traffy.Objects
{

public sealed class TrTask : TrObject, IEnumerator<TrObject>
    {
        // at the end of such generator, result is set.
        public MonoAsync<TrObject> m_Generator;

        private TrTask(MonoAsync<TrObject> generator)
        {
            m_Generator = generator;
        }

        public TrObject Current => m_Generator.GetResult();

        object IEnumerator.Current => m_Generator.GetResult();

        [PyBind]
        public bool move_next()
        {
            return MoveNext();
        }
        public bool MoveNext()
        {
            m_Generator.m_Result = RTS.object_none;
            return m_Generator.MoveNext();
        }

        public void Dispose() { }

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;

        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError($"invalid invocation of {args[0].AsClass.Name}");
        }

        // check exceptions

        public static TrTask Create(MonoAsync<TrObject> gen)
        {
            return new TrTask(gen);
        }

        public static TrTask Create(MonoAsync<TrObject> gen, Frame frame)
        {
            async MonoAsync<TrObject> wrap(MonoAsync<TrObject> gen, Frame frame)
            {
                try
                {
                    return await gen;
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.WriteLine("====\n" + e.StackTrace + "\n====\n");
#endif
                    throw RTS.exc_wrap_frame(e, frame);
                }
            }

            var wrapped = wrap(gen, frame);
            return new TrTask(wrapped);
        }

        public void Reset()
        {
            throw new NotSupportedException("cannot reset Traffy coroutines");
        }

        public IEnumerator<TrObject> __iter__() => this;


        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrTask>("Generator");
            TrClass.TypeDict[typeof(TrTask)] = CLASS;
        }
        [Traffy.Annotations.Mark(typeof(TrTask))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }
    }
}