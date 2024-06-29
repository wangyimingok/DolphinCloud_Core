using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.Role
{
    public class RoleCheckBoxDataModel
    {
        // <summary>
        /// 角色信息主键
        /// </summary>
        [JsonProperty("roleID")]
        public int RoleID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [JsonProperty("roleName")]
        public string RoleName { get; set; }
    }
}
