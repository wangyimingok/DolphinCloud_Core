using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.Base.AudiotLog
{
    /// <summary>
    /// 审计日志视图数据模型
    /// </summary>
    public class AuditLogDataViewModel
    {
        /// <summary>
        /// 数据主键
        /// </summary>
        [JsonProperty]
        public long LogID { get; set; }

        /// <summary>
        /// 事件描述
        /// </summary>
        [JsonProperty]
        public string Description { get; set; }

        /// <summary>
        /// 事件类型（例如：登录、登出、数据修改等）
        /// </summary>
        [JsonProperty]
        public string EventType { get; set; }

        /// <summary>
        /// 方法执行期间发生异常
        /// </summary>
        [JsonProperty]
        public string Exception { get; set; }

        /// <summary>
        /// 方法调用的总持续时间（毫秒）
        /// </summary>
        [JsonProperty]
        public int? ExecutionDuration { get; set; }

        /// <summary>
        /// 方法执行的开始时间
        /// </summary>
        [JsonProperty]
        public DateTime? ExecutionTime { get; set; }

        /// <summary>
        /// 用户的IP地址
        /// </summary>
        [JsonProperty]
        public string IPAddress { get; set; }

        /// <summary>
        /// 执行方法名称
        /// </summary>
        [JsonProperty]
        public string MethodName { get; set; }

        /// <summary>
        /// 调用参数
        /// </summary>
        [JsonProperty]
        public string Parameters { get; set; }

        /// <summary>
        /// 返回值
        /// </summary>
        [JsonProperty]
        public string ReturnValue { get; set; }

        /// <summary>
        /// 服务 (类/接口) 名
        /// </summary>
        [JsonProperty]
        public string ServiceName { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [JsonProperty]
        public string SystemName { get; set; }

        /// <summary>
        /// 执行操作的用户标识
        /// </summary>
        [JsonProperty]
        public string UserID { get; set; }

        /// <summary>
        /// 执行操作的用户名
        /// </summary>
        [JsonProperty]
        public string UserName { get; set; }
    }
}
