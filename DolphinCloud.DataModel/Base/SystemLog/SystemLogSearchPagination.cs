using DolphinCloud.Common.Pagination;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.Base.SystemLog
{
    /// <summary>
    /// 系统日志分页搜索
    /// </summary>
    public class SystemLogSearchPagination:BasePagination
    {
        /// <summary>
        /// 异常信息
        /// </summary>
        [JsonProperty("exception")]
        public string Exception { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        [JsonProperty("level")]
        public string Level { get; set; }

        /// <summary>
        /// 日志消息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
