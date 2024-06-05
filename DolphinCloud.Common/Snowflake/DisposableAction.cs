using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Snowflake
{
    /// <summary>
    /// 资源释放动作
    /// </summary>
    internal class DisposableAction : IDisposable
    {
        /// <summary>
        /// 动作
        /// </summary>
        readonly Action _action;

        public DisposableAction(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            _action = action;
        }

        public void Dispose()
        {
            _action();
        }
    }
}
