using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Result
{
    /// <summary>
    /// 微信返回对象
    /// </summary>
    public  class WeChatResult
    {
        /// <summary>
        /// 异常编码
        /// </summary>
        [JsonProperty("errcode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("errmsg")]
        public string ErrorMessage { get; set; }

        ///// <summary>
        ///// 请求ID
        ///// </summary>
        //[JsonProperty("rid")]
        //public string RequesID { get; set; }
    }
}
