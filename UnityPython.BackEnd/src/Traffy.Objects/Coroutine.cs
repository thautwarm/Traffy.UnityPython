using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


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

        public TrObject Current => m_generator.GetResult();

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
#if DEBUG
                    Console.WriteLine("====\n" + e.StackTrace + "\n====\n");
#endif
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
            return m_generator.MoveNext() ? m_generator.GetResult() : throw new StopIteration(m_generator.GetResult());
        }

        public TrObject __send__(TrObject v)
        {
            m_generator.m_Result = v;
            return m_generator.MoveNext() ? m_generator.GetResult() : throw new StopIteration(m_generator.GetResult());
        }

        public TrObject __send__(TrObject v, TrRef found)
        {
            m_generator.m_Result = v;
            var ret = MK.Bool(m_generator.MoveNext());
            found.value = m_generator.GetResult();
            return ret;
        }


        public static TrObject __send__(TrObject _self, TrObject v)
        {
            var self = (TrCoroutine)_self;
            return self.__send__(v);
        }

        public static TrObject __send__(TrObject _self, TrObject v, TrRef found)
        {
            var self = (TrCoroutine)_self;
            return self.__send__(v, found);
        }

        static TrObject _overloaded_send(BList<TrObject> args, Dictionary<TrObject, TrObject> kwargs)
        {
            if (kwargs != null)
                throw new TypeError("generator.send() doesn't accept keyword arguments");
            if (args.Count == 2)
                return __send__(args[0], args[1]);
            if (args.Count == 3)
            {
                return __send__(args[0], args[1], (TrRef)args[2]);
            }
            throw new ValueError("generator.send() takes 1 or 2 arguments");
        }

        public static TrObject _obj__send__ = TrSharpFunc.FromFunc("generator.send", _overloaded_send);

        [Traffy.Annotations.Mark(Initialization.TokenClassInit)]
        static void _Init()
        {
            CLASS = TrClass.FromPrototype<TrCoroutine>("generator");

            CLASS[CLASS.ic__new] = TrStaticMethod.Bind(TrSharpFunc.FromFunc("generator.__new__", TrCoroutine.datanew));
            CLASS.AddMethod("send", _obj__send__);
            TrClass.TypeDict[typeof(TrCoroutine)] = CLASS;
        }
        [Traffy.Annotations.Mark(typeof(TrCoroutine))]
        static void _SetupClasses()
        {
            CLASS.SetupClass();
            Initialization.Prelude(CLASS);
        }

        object IEnumerator.Current => m_generator.GetResult();
        public bool MoveNext()
        {
            m_generator.m_Result = RTS.object_none;
            return m_generator.MoveNext();
        }

        public void Dispose() { }
    }
}