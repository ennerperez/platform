#if NETFX_45

using System;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif

        namespace Threading
        {
            public struct FuncAwaitable<TResult> : IAwaitable<TResult>
            {
                private readonly Func<TResult> function;

                public FuncAwaitable(Func<TResult> function)
                {
                    this.function = function;
                }

                public IAwaiter<TResult> GetAwaiter()
                {
                    return new FuncAwaiter<TResult>(this.function);
                }
            }
        }

#if PORTABLE
    }

#endif
}

#endif