using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace DolphinCloud.DataEntity.System
{

    /// <summary>
    /// 角色信息表
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "System_RoleInfo", DisableSyncStructure = true)]
    public partial class RoleInfo
    {

        /// <summary>
        /// 角色信息主键
        /// </summary>
        [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
        public int RoleID { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [JsonProperty]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty]
        public DateTimeOffset CreateDateTime { get; set; }= DateTimeOffset.Now;

        /// <summary>
        /// 逻辑删除标志
        /// </summary>
        [JsonProperty]
        public bool DeleteFG { get; set; } = false;

        [JsonProperty, Column(DbType = "nchar(10)")]
        public string Field1 { get; set; }

        [JsonProperty, Column(DbType = "nchar(10)")]
        public string Field2 { get; set; }

        [JsonProperty, Column(DbType = "nchar(10)")]
        public string Field3 { get; set; }

        [JsonProperty, Column(DbType = "nchar(10)")]
        public string Field4 { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [JsonProperty]
        public string LastModifyBy { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [JsonProperty]
        public DateTimeOffset LastModifyDate { get; set; }= DateTimeOffset.Now;

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty, Column(StringLength = 200)]
        public string Remarks { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [JsonProperty, Column(StringLength = 50)]
        public string RoleDescription { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [JsonProperty, Column(StringLength = 50, IsNullable = false)]
        public string RoleName { get; set; }


        #region 外键 => 导航属性，OneToMany

        [Navigate("RoleID")]
        public virtual List<RoleAuthorityInfo> SystemRoleAuthorityInfos { get; set; }

        [Navigate("RoleID")]
        public virtual List<UserRoleRelationInfo> SystemUserRoleRelationInfos { get; set; }

        #endregion
    }

}
