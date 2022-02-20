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
    public struct Handler<TElement>
    {
        [AllowNull] internal MonoAsync<TElement> top;
        public TElement YieldInValue
        {
            get => top.m_Result;
            set => top.m_Result = value;
        }

        public static implicit operator Handler<TElement>(MonoAsync<TElement> monoAsync)
        {
            return new Handler<TElement>() { top = monoAsync };
        }

        public static implicit operator MonoAsync<TElement>(Handler<TElement> handler)
        {
            return handler.top;
        }


    }



    public static class ExtMonoAsyn
    {
        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static AsyncEnumerator<TElement> Enum<TElement>(this MonoAsync<TElement> async)
        {
            return new AsyncEnumerator<TElement>(async);
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public static MonoAsync<TReturn> Yield<TReturn>(TReturn e) => new MonoAsync<TReturn>
        {
            m_Result = e
        };
    }

    public struct MonoAsyncBuilder<TReturn>
    {
        [NotNull] MonoAsync<TReturn> m_async;

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

            if (!(_awaiter is MonoAsync<TReturn> awaiter))
            {
                throw new InvalidProgramException($"{nameof(AwaitOnCompleted)} requires {nameof(MonoAsync<TReturn>)}");
            }

#nullable enable
            if (null != (object?)awaiter.StateMachine)
            {
                m_async.m_Nested = awaiter;
                m_async.YieldFrom(m_async.m_handler);
            }
            else
            {
                var handler = m_async.m_handler;
                // comment some lines to implement CPython generator behaviour
                // var tmp = handler.YieldInValue;
                handler.YieldInValue = awaiter.m_Result;
                m_async.m_handler = awaiter;
                // awaiter.m_Result = tmp;
                awaiter.m_IsCompleted = true;
            }
        }
#nullable disable
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter _awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            throw new InvalidProgramException("AwaitUnsafeOnCompleted is not supported");
        }

    }

    [AsyncMethodBuilder(typeof(MonoAsyncBuilder<>))]
    public sealed partial class MonoAsync<TElement> : INotifyCompletion
    {
        [AllowNull] internal MonoAsync<TElement> m_Nested;
        [AllowNull] internal TElement m_Result;
        internal bool m_IsCompleted;
        internal IAsyncStateMachine StateMachine;
        internal Handler<TElement> m_handler;
        public bool IsCompleted => m_IsCompleted;


        [MethodImpl(MethodImplOptionsCompat.Best)]
        public TElement GetResult()
        {
#nullable enable

            return m_Result;
#nullable disable
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public MonoAsync<TElement> GetAwaiter() => this;


        [MethodImpl(MethodImplOptionsCompat.Best)]
        public bool MoveNext()
        {
            return MoveNext(this);
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public void YieldFrom(Handler<TElement> handler)
        {
            m_Nested.MoveNext(handler);
            if (m_Nested.m_IsCompleted)
            {
                m_Nested = null;
                StateMachine.MoveNext();
            }
        }
        [MethodImpl(MethodImplOptionsCompat.Best)]
        public bool MoveNext(Handler<TElement> handler)
        {
            if (m_IsCompleted)
                return false;
            #nullable enable
            if (null == (object?)m_Nested)
            {
                if (m_handler.top != null)
                    m_handler.YieldInValue = handler.YieldInValue;
                m_handler = handler;
                StateMachine.MoveNext();
                return !m_IsCompleted;
            }
            #nullable disable
            YieldFrom(handler);
            return !m_IsCompleted;
        }

        public void OnCompleted(Action continuation)
        {
            throw new InvalidProgramException("OnCompleted is not supported");
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            throw new InvalidProgramException("UnsafeOnCompleted is not supported");
        }


        [MethodImpl(MethodImplOptionsCompat.Best)]
        public TElement Run()
        {
            while (!this.IsCompleted)
            {
                this.MoveNext();
            }
            return this.GetResult();
        }

    }

    public class AsyncEnumerator<TElement> : IEnumerator<TElement>, IEnumerable<TElement>
    {
        public MonoAsync<TElement> m_async;

        public Handler<TElement> m_handler;

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public AsyncEnumerator(MonoAsync<TElement> async)
        {
            m_async = async;
            m_handler = async;
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public bool MoveNext() => m_async.MoveNext(m_handler);


        [MethodImpl(MethodImplOptionsCompat.Best)]
        public bool MoveNext(TElement e)
        {
            m_handler.YieldInValue = e;
            return m_async.MoveNext(m_handler);
        }

        [MethodImpl(MethodImplOptionsCompat.Best)]
        public void Reset()
        {
            throw new NotSupportedException($"{nameof(MonoAsync<TElement>)} cannot be reset");
        }

        public void Dispose() { }

        public IEnumerator<TElement> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
        public TElement Current => m_handler.YieldInValue;
        object IEnumerator.Current => m_handler.YieldInValue;
    }
}

// using System;
// using System.Runtime.CompilerServices;
// using System.Collections.Generic;
// using System.Collections;
// using System.Diagnostics.CodeAnalysis;

// using TElement = System.Int32;

// public class Handler
// {
//     public TElement YieldInValue;
// }

// public static class MonoAsync
// {
//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public static MonoAsync<TElement> Yield(TElement e)
//     {
//         return MonoAsync<TElement>.Yield(e);
//     }

//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public static T Run<T>(this MonoAsync<T> self)
//     {
//         while (!self.IsCompleted)
//         {
//             self.MoveNext();
//         }
//         return self.GetResult();
//     }

// }
// public struct MonoAsyncBuilder<TReturn>
// {
//     [NotNull] MonoAsync<TReturn> m_async;

//     public MonoAsync<TReturn> Task => m_async;

//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public static MonoAsyncBuilder<TReturn> Create() => new MonoAsyncBuilder<TReturn>
//     {
//         m_async = new MonoAsync<TReturn>()
//     };

//     public void SetException(Exception exception)
//     {
//         throw exception;
//     }

//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public void SetResult(TReturn result)
//     {
//         m_async.m_Result = result;
//         m_async.m_IsCompleted = true;
//     }

//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
//     {
//         m_async.StateMachine = stateMachine;
//     }

//     public void SetStateMachine(IAsyncStateMachine stateMachine)
//     {
//         throw new InvalidOperationException();
//     }

//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public void AwaitOnCompleted<TAwaiter, TStateMachine>(
//         ref TAwaiter _awaiter, ref TStateMachine stateMachine)
//         where TAwaiter : INotifyCompletion
//         where TStateMachine : IAsyncStateMachine
//     {
//         # nullable enable
//         var awaiter = _awaiter as IMonoAsync;
//         if (awaiter == null)
//             throw new InvalidProgramException($"{nameof(AwaitOnCompleted)} requires {nameof(IMonoAsync)}");
//         if (null != (object?) awaiter.StateMachine)
//         {
//             m_async.m_Nested = awaiter;
//             m_async.YieldFrom(m_async.m_handler);
//         }
//         else
//         {
//             if (awaiter is MonoAsync<TElement> monoAsync)
//             {
//                 var handler = m_async.m_handler;
//                 if (handler != null)
//                 {
//                     var tmp = handler.YieldInValue;
//                     handler.YieldInValue = monoAsync.m_Result;
//                     monoAsync.m_Result = tmp;
//                 }
//                 monoAsync.m_IsCompleted = true;
//             }
//             else
//             {
//                 throw new InvalidProgramException("invalid program");
//             }
//         }
//         # nullable disable
//     }
//     public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
//         ref TAwaiter _awaiter, ref TStateMachine stateMachine)
//         where TAwaiter : ICriticalNotifyCompletion
//         where TStateMachine : IAsyncStateMachine
//     {
//         throw new InvalidProgramException("AwaitUnsafeOnCompleted is not supported");
//     }

// }

// public abstract class IMonoAsync
// {
//     [AllowNull] public IAsyncStateMachine StateMachine;
//     internal bool m_IsCompleted = false;


//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public abstract bool MoveNext([AllowNull] Handler handler);


// }

// [AsyncMethodBuilder(typeof(MonoAsyncBuilder<>))]
// public sealed class MonoAsync<TReturn> : IMonoAsync, INotifyCompletion
// {
//     [AllowNull] internal IMonoAsync m_Nested;
//     [AllowNull] internal TReturn m_Result;

//     public bool IsCompleted => m_IsCompleted;
//     [AllowNull] internal Handler m_handler;

//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public static MonoAsync<TElement> Yield(TElement e) => new MonoAsync<TElement>
//     {
//         m_Result = e
//     };
//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public TReturn GetResult() => m_Result;

//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public MonoAsync<TReturn> GetAwaiter() => this;


//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public bool MoveNext() => MoveNext(null);
//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public void YieldFrom([AllowNull] Handler handler)
//     {
//         m_Nested.MoveNext(m_handler);
//         if (m_Nested.m_IsCompleted)
//         {
//             m_Nested = null;
//             m_handler = handler;
//             StateMachine.MoveNext();
//         }
//     }
//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public override bool MoveNext([AllowNull] Handler handler)
//     {
//         if (m_IsCompleted)
//             return false;
//         if (m_Nested == null)
//         {
//             m_handler = handler;
//             StateMachine.MoveNext();
//             return !m_IsCompleted;
//         }
//         YieldFrom(handler);
//         return !m_IsCompleted;
//     }

//     public void OnCompleted(Action continuation)
//     {
//         throw new InvalidProgramException("OnCompleted is not supported");
//     }

//     public void UnsafeOnCompleted(Action continuation)
//     {
//         throw new InvalidProgramException("UnsafeOnCompleted is not supported");
//     }


//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public AsyncEnumerator<TReturn> Enum()
//     {
//         return new AsyncEnumerator<TReturn>(this);
//     }

// }

// public class AsyncEnumerator<TReturn> : IEnumerator<TElement>, IEnumerable<TElement>
// {
//     public MonoAsync<TReturn> m_async;

//     public Handler m_handler;

//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public AsyncEnumerator(MonoAsync<TReturn> async)
//     {
//         m_async = async;
//         m_handler = new Handler();
//     }

//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public bool MoveNext() => m_async.MoveNext(m_handler);


//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public bool MoveNext(TElement e)
//     {
//         m_handler.YieldInValue = e;
//         return m_async.MoveNext(m_handler);
//     }

//     [MethodImpl(MethodImplOptionsCompat.Best)]
//     public void Reset()
//     {
//         throw new NotSupportedException($"{nameof(MonoAsync<TReturn>)} cannot be reset");
//     }

//     public void Dispose() { }

//     public IEnumerator<TElement> GetEnumerator()
//     {
//         return this;
//     }

//     IEnumerator IEnumerable.GetEnumerator()
//     {
//         return this;
//     }


//     public TElement Current => m_handler.YieldInValue;
//     object IEnumerator.Current => m_handler.YieldInValue;
// }
