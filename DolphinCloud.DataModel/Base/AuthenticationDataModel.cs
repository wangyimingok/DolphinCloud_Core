using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.Base
{
    /// <summary>
    /// 权限检查数据模型
    /// </summary>
    public class AuthenticationDataModel
    {
        /// <summary>
        /// 菜单主键
        /// </summary>
        [JsonProperty]
        public int MenuID { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        [JsonProperty]
        public string ActionName { get; set; }

        /// <summary>
        /// 域名称
        /// </summary>
        [JsonProperty]
        public string AreaName { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        [JsonProperty]
        public string ControllerName { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [JsonProperty]
        public string MenuName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [JsonProperty]
        public short MenuType { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        [JsonProperty]
        public string MenuUrlAddress { get; set; }

        /// <summary>
        /// 角色信息主键
        /// </summary>
        [JsonProperty("roleID")]
        public int RoleID { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [JsonProperty("roleName")]
        public string RoleName { get; set; }

        /// <summary>
        /// 用户信息主键
        /// </summary>
        [JsonProperty]
        public long UserID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [JsonProperty]
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [JsonProperty]
        public string RealName { get; set; }
    }
}
