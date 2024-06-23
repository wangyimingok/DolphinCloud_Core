using DolphinCloud.DataModel.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.Role
{
    /// <summary>
    /// 角色授权数据模型
    /// </summary>
    public class RoleAuthorDataModel
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        [JsonProperty("roleID")]
        public int RoleID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [JsonProperty("roleName")]
        public string RoleName { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        [JsonProperty("permissions")]
        public List<LayuiTreeDataModel> Permissions { get; set; }
    }
}
