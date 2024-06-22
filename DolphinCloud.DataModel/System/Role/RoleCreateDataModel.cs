using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.Role
{
    /// <summary>
    /// 角色创建数据模型
    /// </summary>
    public class RoleCreateDataModel
    {
        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [JsonProperty("roleDescription")]
        public string RoleDescription { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [JsonProperty("roleName")]
        public string RoleName { get; set; }
    }
}
