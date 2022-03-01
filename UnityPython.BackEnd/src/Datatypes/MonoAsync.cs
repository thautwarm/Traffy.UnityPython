using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;

// using TElement = System.Int32;
// using TReturn = System.Int32;

namespace Traffy.Objects
{

    public static class ExtMonoAsyn
    {
        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static GeneratorAwaiter<TReturn> YieldFrom<TReturn>(this IEnumerator<TReturn> self)
        {
            return new GeneratorAwaiter<TReturn>(self);
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static GeneratorAwaiter<TReturn> YieldFrom<TReturn>(this IEnumerable<TReturn> self)
        {
            return new GeneratorAwaiter<TReturn>(self.GetEnumerator());
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static YieldAwaiter<TReturn> Yield<TReturn>(TReturn e) => new YieldAwaiter<TReturn>(e);
    }



    public abstract class Awaitable<T> : INotifyCompletion
    {
        public T m_Result;

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public T GetResult() => m_Result;
        public abstract bool MoveNext(ref T sent);

        public abstract bool IsCompleted { get; }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public Awaitable<T> GetAwaiter() => this;

        public void OnCompleted(Action continuation)
        {
            throw new NotSupportedException();
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            throw new InvalidProgramException("UnsafeOnCompleted is not supported");
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public bool MoveNext()
        {
            return MoveNext(ref m_Result);
        }
    }

    [AsyncMethodBuilder(typeof(MonoAsyncBuilder<>))]
    public class YieldAwaiter<T> : Awaitable<T>, INotifyCompletion
    {
        public bool m_IsCompleted = false;
        public override bool IsCompleted => m_IsCompleted;
        public override bool MoveNext(ref T value)
        {
            if (m_IsCompleted)
            {
                m_Result = value;
                return false;
            }
            value = m_Result;
            m_IsCompleted = true;
            return true;
        }

        public YieldAwaiter(T value)
        {
            m_Result = value;
        }
    }


    [AsyncMethodBuilder(typeof(MonoAsyncBuilder<>))]
    public sealed class GeneratorAwaiter<T> : Awaitable<T>, INotifyCompletion
    {
        public IEnumerator<T> m_Enumerator;
        public bool m_IsCompleted = false;
        public GeneratorAwaiter(IEnumerator<T> enumerator)
        {
            m_Enumerator = enumerator;
        }
        public override bool IsCompleted => m_IsCompleted;
        public override bool MoveNext(ref T value)
        {
            if (m_MoveNext())
            {
                value = m_Enumerator.Current;
                return true;
            }
            else
            {
                m_IsCompleted = true;
                return false;
            }
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        bool m_MoveNext() => m_Enumerator.MoveNext();
    }





    [AsyncMethodBuilder(typeof(MonoAsyncBuilder<>))]
    public sealed partial class MonoAsync<TElement> : Awaitable<TElement>, INotifyCompletion, IEnumerator<TElement>, IEnumerable<TElement>
    {
        [AllowNull] internal Awaitable<TElement> m_Nested;
        internal bool m_IsCompleted = false;
        internal IAsyncStateMachine StateMachine;
        // internal Handler<TElement> m_handler;
        public override bool IsCompleted => m_IsCompleted;

        public TElement Current => m_Result;

        object IEnumerator.Current => m_Result;

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public override bool MoveNext(ref TElement value)
        {
            if (m_IsCompleted)
                return false;

            if ((object)m_Nested != null && m_Nested.MoveNext(ref value))
            {
                return true;
            }
            StateMachine.MoveNext();
            if (m_IsCompleted)
                return false;
            else
            {
                value = m_Result;
                return true;
            }
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public TElement Run()
        {
            var value = default(TElement);
            while (!this.IsCompleted)
            {
                this.MoveNext(ref value);
            }
            return this.GetResult();
        }


        public bool MoveNext(TElement value)
        {
            m_Result = value;
            return MoveNext(ref value);
        }

        public void Reset()
        {
            throw new NotSupportedException($"Reset is not supported for {typeof(MonoAsync<TElement>)}");
        }

        public void Dispose()
        { }

        public IEnumerator<TElement> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}
