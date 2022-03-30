using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Traffy.Annotations;

namespace Traffy.Objects
{

    [PyBuiltin]
    [PyInherit(typeof(Traffy.Interfaces.Iterable), typeof(Traffy.Interfaces.Awaitable))]
    public sealed partial class TrGenerator : TrObject, IEnumerator<TrObject>
    {
        // at the end of such generator, result is set.
        public Awaitable<TrObject> m_Generator;

        private TrGenerator(Awaitable<TrObject> generator)
        {
            m_Generator = generator;
        }

        public TrObject Current => m_Generator.GetResult();

        public static TrClass CLASS;
        public override TrClass Class => CLASS;

        public override List<TrObject> __array__ => null;


        public static TrObject datanew(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            throw new TypeError($"invalid invocation of {args[0].AsClass.Name}");
        }

        // check exceptions

        public static TrGenerator Create(Awaitable<TrObject> gen)
        {
            return new TrGenerator(gen);
        }

        public static TrGenerator Create(MonoAsync<TrObject> gen, Frame frame)
        {
            async MonoAsync<TrObject> wrap(MonoAsync<TrObject> gen, Frame frame)
            {
                try
                {
                    await gen;
                    return frame.retval;
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
            return new TrGenerator(wrapped);
        }

        public void Reset()
        {
            throw new NotSupportedException("cannot reset Traffy coroutines");
        }

        public override Awaitable<TrObject> __await__() => this.m_Generator;
        public override IEnumerator<TrObject> __iter__() => this;

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public override bool __next__(TrRef refval)
        {
            if (m_Generator.IsCompleted)
            {
                return false;
            }
            m_Generator.m_Result = RTS.object_none;
            if (m_Generator.MoveNext())
            {
                refval.value = m_Generator.GetResult();
                return true;
            }
            return false;
        }

        [PyBind]
        public bool send(TrObject sent, TrRef refval = null)
        {
            if (m_Generator.IsCompleted)
            {
                return false;
            }
            m_Generator.m_Result = sent;
            if (m_Generator.MoveNext())
            {
                if (refval != null)
                    refval.value = m_Generator.GetResult();
                return true;
            }
            return false;
        }



        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.CreateRef)]
        internal static void _Create()
        {
            CLASS = TrClass.FromPrototype<TrGenerator>("Generator");
        }

        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.InitRef)]
        internal static void _Init()
        {
            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("Generator.__new__", TrGenerator.datanew));
            CLASS.IsSealed = true;
        }
        [Traffy.Annotations.SetupMark(Traffy.Annotations.SetupMarkKind.SetupRef)]
        internal static void _SetupClasses()
        {
            CLASS.SetupClass();
            CLASS.IsFixed = true;
            Initialization.Prelude(CLASS);
        }

        object IEnumerator.Current => m_Generator.GetResult();
        public bool MoveNext()
        {
            if (m_Generator.IsCompleted)
            {
                return false;
            }
            m_Generator.m_Result = RTS.object_none;
            return m_Generator.MoveNext();
        }

        public void Dispose() { }

        [PyBind]
        public TrObject is_completed => MK.Bool(m_Generator.IsCompleted);

        [PyBind]
        public TrObject result => m_Generator.IsCompleted ? m_Generator.GetResult() : RTS.object_none;
    }
}