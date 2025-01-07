using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataEntity.System
{
    /// <summary>
    /// 系统日志表
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "System_Logs", DisableSyncStructure = true)]
    public class SystemLogInfo
    {
        /// <summary>
        /// 日志数据主键
        /// </summary>
        [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        [JsonProperty, Column(StringLength = -2)]
        public string Exception { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        [JsonProperty, Column(StringLength = -2)]
        public string Level { get; set; }

        /// <summary>
        /// 日志消息
        /// </summary>
        [JsonProperty, Column(StringLength = -2)]
        public string Message { get; set; }

        /// <summary>
        /// 消息模版
        /// </summary>
        [JsonProperty, Column(StringLength = -2)]
        public string MessageTemplate { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        [JsonProperty, Column(StringLength = -2)]
        public string Properties { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        [JsonProperty]
        public DateTime? TimeStamp { get; set; }

    }
}
