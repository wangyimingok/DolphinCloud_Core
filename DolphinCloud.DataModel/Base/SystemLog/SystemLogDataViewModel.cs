using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.Base.SystemLog
{
    /// <summary>
    /// 系统日志展示数据模型
    /// </summary>
    public class SystemLogDataViewModel
    {
        /// <summary>
        /// 日志数据主键
        /// </summary>
        [JsonProperty]
        public int Id { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        [JsonProperty]
        public string Exception { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        [JsonProperty]
        public string Level { get; set; }

        /// <summary>
        /// 日志消息
        /// </summary>
        [JsonProperty]
        public string Message { get; set; }

        /// <summary>
        /// 消息模版
        /// </summary>
        [JsonProperty]
        public string MessageTemplate { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        [JsonProperty]
        public string Properties { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        [JsonProperty]
        public DateTime? TimeStamp { get; set; }
    }
}
