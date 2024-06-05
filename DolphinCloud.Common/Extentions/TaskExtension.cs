using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Extentions
{
    /// <summary>
    /// Task扩展
    /// </summary>
    public static class TaskExtension
    {
        public static Task<T> WrapTask<T>(this T t) => Task.FromResult(t);
    }
}
