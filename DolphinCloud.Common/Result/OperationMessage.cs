using DolphinCloud.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Result
{
    /// <summary>
    /// 操作信息
    /// </summary>
    public class OperationMessage : ResultMessage<object>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="responseCode">传入参数 <see cref="ResponseCode"/>类型 响应码枚举</param>
        /// <param name="messageStr">传入参数 <see cref="string"/>类型 相应信息</param>
        /// <param name="ResultData">传入参数 <see cref="object"/>类型 返回数据</param>
        public OperationMessage(ResponseCode responseCode, string messageStr, object ResultData = null)
        {
            this.Code = responseCode;
            this.Message = messageStr;
            this.Data = ResultData;
        }
    }
}
