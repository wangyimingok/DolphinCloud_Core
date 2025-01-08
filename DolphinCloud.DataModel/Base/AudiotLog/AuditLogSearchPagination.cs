using DolphinCloud.Common.Pagination;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.Base.AudiotLog
{
    /// <summary>
    /// 审计日志分页查询条件
    /// </summary>
    public class AuditLogSearchPagination: BasePagination
    {
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

        /// <summary>
        /// 事件类型（例如：登录、登出、数据修改等）
        /// </summary>
        [JsonProperty("eventType")]
        public string EventType { get; set; }

        /// <summary>
        /// 执行方法名称
        /// </summary>
        [JsonProperty("methodName")]
        public string MethodName { get; set; }
    }
}
