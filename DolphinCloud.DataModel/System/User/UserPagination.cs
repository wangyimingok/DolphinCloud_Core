using DolphinCloud.Common.Pagination;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.User
{
    /// <summary>
    /// 用户分页查询参数
    /// </summary>
    public class UserPagination: BasePagination
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [JsonProperty("searchKey")]
        public string SearchKey { get; set; }
    }
}
