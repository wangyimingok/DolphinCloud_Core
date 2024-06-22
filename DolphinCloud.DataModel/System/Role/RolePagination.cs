using DolphinCloud.Common.Pagination;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.Role
{
    /// <summary>
    /// 角色分页查询数据模型
    /// </summary>
    public class RolePagination:BasePagination
    {       
        /// <summary>
        /// 搜索关键字
        /// </summary>
        [JsonProperty("searchKey")]
        public string SearchKey { get; set; }
    }
}
