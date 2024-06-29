using DolphinCloud.DataModel.Base;
using DolphinCloud.DataModel.System.Role;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.User
{
    /// <summary>
    /// 用户角色关系数据模型
    /// </summary>
    public class UserRoleRelationDataModel
    {
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
        /// 当前用户已经拥有的角色列表
        /// </summary>
        public List<LayuiTreeDataModel> currentAlreadyRoleList { get; set; }
    }
}
