#if NETFX_45

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Support.Threading
{
    public static class FuncExtensions
    {
        public static IAwaiter<TResult> GetAwaiter<TResult>(this Func<TResult> function)
        {
            return new FuncAwaiter<TResult>(function);
        }
        //public static TaskAwaiter<TResult> GetAwaiter<TResult>(this Func<TResult> function)
        //{
        //    Task<TResult> task = new Task<TResult>(function);
        //    task.Start();
        //    return task.GetAwaiter(); // Returns a TaskAwaiter<TResult>.
        //}
    }

    //public static class ActionExtensions
    //{
    //    public static TaskAwaiter GetAwaiter(this Action action)
    //    {
    //        Task task = new Task(action);
    //        task.Start();
    //        return task.GetAwaiter(); // Returns a TaskAwaiter.
    //    }
    //}
}


#endif