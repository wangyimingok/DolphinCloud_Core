using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.Role
{
    /// <summary>
    /// 角色修改数据模型
    /// </summary>
    public class RoleModifyDataModel
    {
        /// <summary>
        /// 角色信息主键
        /// </summary>
        [JsonProperty("roleID")]
        public int RoleID { get; set; }       

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
