using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.Role
{
    /// <summary>
    /// 角色列表展示数据模型
    /// </summary>
    public class RoleDataViewModel
    {
        /// <summary>
        /// 角色信息主键
        /// </summary>
        [JsonProperty("roleID")]
        public int RoleID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [JsonProperty("createBy")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("createDateTime")]
        public DateTimeOffset CreateDateTime { get; set; } = DateTimeOffset.Now;

        /// <summary>
        /// 逻辑删除标志
        /// </summary>
        [JsonProperty("deleteFG")]
        public bool DeleteFG { get; set; } = false;

        [JsonProperty]
        public string Field1 { get; set; }

        [JsonProperty]
        public string Field2 { get; set; }

        [JsonProperty]
        public string Field3 { get; set; }

        [JsonProperty]
        public string Field4 { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [JsonProperty("lastModifyBy")]
        public string LastModifyBy { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [JsonProperty("lastModifyDate")]
        public DateTimeOffset LastModifyDate { get; set; } = DateTimeOffset.Now;

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
