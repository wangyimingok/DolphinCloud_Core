using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Enums
{
    /// <summary>
    /// 响应码
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        OperationSuccess = 200,
        /// <summary>
        /// 操作警告
        /// </summary>
        [Description("操作警告")]
        OperationWarning = 210,
        /// <summary>
        /// 服务器异常
        /// </summary>
        [Description("服务器异常")]
        ServerError = 500,
    }
}
