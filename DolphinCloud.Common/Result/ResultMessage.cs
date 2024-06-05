using DolphinCloud.Common.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Result
{
    /// <summary>
    /// 通用返回对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ResultMessage<T> where T : class
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ResultMessage()
        {
        }

        /// <summary>
        /// 响应码
        /// </summary>
        [JsonProperty("responseCode")]
        public ResponseCode Code { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="responseCode">传入参数 <see cref="ResponseCode"/>类型 响应码枚举</param>
        /// <param name="messageStr">传入参数 <see cref="string"/>类型 相应信息</param>
        /// <param name="ResultData">传入参数 <see cref="T"/>类型 返回数据</param>
        public ResultMessage(ResponseCode responseCode, string messageStr, T ResultData = null)
        {
            this.Code = responseCode;
            this.Message = messageStr;
            this.Data = ResultData;
        }
    }
}
