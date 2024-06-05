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
    /// 分页返回对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginationResult<T>
    {
        /// <summary>
        /// 响应码
        /// </summary>
        [JsonProperty("code")]
        public ResponseCode Code { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 数据总条数
        /// </summary>
        [JsonProperty("count")]
        public long DataCount { get; set; }

        /// <summary>
        /// 数据结果集
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public PaginationResult() { }
        /// <summary>
        /// 实例化一个分页返回对象
        /// </summary>
        /// <param name="code">传入参数 <see cref="ResponseCode"/>类型 返回响应码</param>
        /// <param name="msg">传入参数 <see cref="string"/>类型 返回消息</param>
        /// <param name="count">传入参数 <see cref="int"/>类型 数据结果集总条数</param>
        /// <param name="data">传入参数 <see cref="T"/>类型 泛型 数据结果集对象</param>
        public PaginationResult(ResponseCode code, string msg, long count, T data)
        {
            this.Code = code;
            this.Message = msg;
            this.DataCount = count;
            this.Data = data;
        }
    }
}
