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
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页数据条数
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; } = 10;
    }
}
