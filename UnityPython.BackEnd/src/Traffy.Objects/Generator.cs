using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace Traffy.Objects
{

    public sealed class TrGenerator : TrObject, IEnumerator<TrObject>
    {
        // at the end of such generator, result is set.
        public Awaitable<TrObject> m_Generator;

        private TrGenerator(Awaitable<TrObject> generator)
        {
            m_Generator = generator;
        }

        public TrObject Current => m_Generator.GetResult();

        public static TrClass CLASS;
        public TrClass Class => CLASS;

        public List<TrObject> __array__ => null;


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

        Awaitable<TrObject> TrObject.__await__() => this.m_Generator;
        IEnumerator<TrObject> TrObject.__iter__() => this;

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public TrObject __next__()
        {
            m_Generator.m_Result = RTS.object_none;
            return m_Generator.MoveNext() ? m_Generator.GetResult() : throw new StopIteration(m_Generator.GetResult());
        }

        public TrObject __send__(TrObject v)
        {
            m_Generator.m_Result = v;
            return m_Generator.MoveNext() ? m_Generator.GetResult() : throw new StopIteration(m_Generator.GetResult());
        }

        public TrObject __send__(TrObject v, TrRef found)
        {
            m_Generator.m_Result = v;
            var ret = MK.Bool(m_Generator.MoveNext());
            found.value = m_Generator.GetResult();
            return ret;
        }


        public static TrObject __send__(TrObject _self, TrObject v)
        {
            var self = (TrGenerator)_self;
            return self.__send__(v);
        }

        public static TrObject __send__(TrObject _self, TrObject v, TrRef found)
        {
            var self = (TrGenerator)_self;
            return self.__send__(v, found);
        }

        static TrObject _overloaded_send(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            if (kwargs != null)
                throw new TypeError("Generator.send() doesn't accept keyword arguments");
            if (args.Count == 2)
                return __send__(args[0], args[1]);
            if (args.Count == 3)
            {
                return __send__(args[0], args[1], (TrRef)args[2]);
            }
            throw new ValueError("Generator.send() takes 1 or 2 arguments");
        }

        public static TrObject _obj__send__ = TrSharpFunc.FromFunc("Generator.send", _overloaded_send);

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrGenerator>("Generator");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("Generator.__new__", TrGenerator.datanew));
            CLASS.AddMethod("send", _obj__send__);
            TrClass.TypeDict[typeof(TrGenerator)] = CLASS;
        }
        [Traffy.Annotations.Mark(typeof(TrGenerator))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }

        object IEnumerator.Current => m_Generator.GetResult();
        public bool MoveNext()
        {
            m_Generator.m_Result = RTS.object_none;
            return m_Generator.MoveNext();
        }

        public void Dispose() { }
    }
}