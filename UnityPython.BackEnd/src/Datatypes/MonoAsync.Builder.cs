using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Traffy.Objects
{

   public struct MonoAsyncBuilder<TReturn>
    {
        MonoAsync<TReturn> m_async;

        public MonoAsync<TReturn> Task => m_async;

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static MonoAsyncBuilder<TReturn> Create() => new MonoAsyncBuilder<TReturn>
        {
            m_async = new MonoAsync<TReturn>()
        };

        public void SetException(Exception exception)
        {
            throw exception;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public void SetResult(TReturn result)
        {
            Debug.Assert(m_async.StateMachine != null);
            m_async.m_Result = result;
            m_async.m_IsCompleted = true;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            m_async.StateMachine = stateMachine;
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter _awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {

            var awaiter = _awaiter as Awaitable<TReturn>;

            if ((object)awaiter == null)
            {
                throw new InvalidProgramException($"{nameof(AwaitOnCompleted)} requires {nameof(MonoAsync<TReturn>)}");
            }

            m_async.m_Nested = awaiter;
            if (!awaiter.MoveNext(ref m_async.m_Result)) // end
            {
                m_async.m_Nested = null;
                m_async.StateMachine.MoveNext();
            }
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter _awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            throw new InvalidProgramException("AwaitUnsafeOnCompleted is not supported");
        }
    }
}