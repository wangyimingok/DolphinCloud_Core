using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Pagination
{
    /// <summary>
    /// 分页请求基类
    /// </summary>
    public class BasePagination
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        [JsonProperty("page")]
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页数据条数
        /// </summary>
        [JsonProperty("limit")]
        public int PageSize { get; set; }
    }
}
