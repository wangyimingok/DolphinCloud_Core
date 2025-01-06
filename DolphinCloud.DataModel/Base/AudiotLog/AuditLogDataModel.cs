using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.Base.AudiotLog
{
    /// <summary>
    /// 审计日志数据模型
    /// </summary>
    public class AuditLogDataModel
    {
        /// <summary>
        /// 数据主键
        /// </summary>
        [JsonProperty("logID")]
        public long LogID { get; set; }

        /// <summary>
        /// 事件描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// 事件类型（例如：登录、登出、数据修改等）
        /// </summary>
        [JsonProperty("EventType")]
        public string EventType { get; set; }

        /// <summary>
        /// 方法执行期间发生异常
        /// </summary>
        [JsonProperty("exception")]
        public string Exception { get; set; }

        /// <summary>
        /// 方法调用的总持续时间（毫秒）
        /// </summary>
        [JsonProperty("executionDuration")]
        public int? ExecutionDuration { get; set; }

        /// <summary>
        /// 方法执行的开始时间
        /// </summary>
        [JsonProperty("executionTime")]
        public DateTime? ExecutionTime { get; set; }

        /// <summary>
        /// 用户的IP地址
        /// </summary>
        [JsonProperty("iPAddress")]
        public string IPAddress { get; set; }

        /// <summary>
        /// 执行方法名称
        /// </summary>
        [JsonProperty("methodName")]
        public string MethodName { get; set; }

        /// <summary>
        /// 调用参数
        /// </summary>
        [JsonProperty("parameters")]
        public string Parameters { get; set; }

        /// <summary>
        /// 返回值
        /// </summary>
        [JsonProperty("returnValue")]
        public string ReturnValue { get; set; }

        /// <summary>
        /// 服务 (类/接口) 名
        /// </summary>
        [JsonProperty("serviceName")]
        public string ServiceName { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [JsonProperty("systemName")]
        public string SystemName { get; set; } = "AdminSystem";

        /// <summary>
        /// 执行操作的用户标识
        /// </summary>
        [JsonProperty("userID")]
        public string UserID { get; set; }

        /// <summary>
        /// 执行操作的用户名
        /// </summary>
        [JsonProperty("userName")]
        public string UserName { get; set; }
    }
}
