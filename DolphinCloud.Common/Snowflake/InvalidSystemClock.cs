using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Snowflake
{
    /// <summary>
    /// 无效的系统时间
    /// </summary>
    internal class InvalidSystemClock : Exception
    {
        public InvalidSystemClock(string message) : base(message) { }
    }
}
