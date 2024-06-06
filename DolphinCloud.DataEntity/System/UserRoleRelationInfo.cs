using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace DolphinCloud.DataEntity.System
{

    /// <summary>
    /// 用户与角色关系表
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "System_UserRoleRelationInfo", DisableSyncStructure = true)]
    public partial class UserRoleRelationInfo
    {

        /// <summary>
        /// 用户角色信息主键
        /// </summary>
        [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
        public int RelationID { get; set; }

        /// <summary>
        /// 角色信息主键
        /// </summary>
        [JsonProperty]
        public int RoleID
        {
            get => _RoleID; set
            {
                if (_RoleID == value) return;
                _RoleID = value;
                SystemRoleInfo = null;
            }
        }
        private int _RoleID;

        /// <summary>
        /// 用户信息主键
        /// </summary>
        [JsonProperty]
        public int UserID
        {
            get => _UserID; set
            {
                if (_UserID == value) return;
                _UserID = value;
                SystemUserInfo = null;
            }
        }
        private int _UserID;

        /// <summary>
        /// 创建人
        /// </summary>
        [JsonProperty]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty, Column(InsertValueSql = "getdate()")]
        public DateTimeOffset CreateDateTime { get; set; }

        /// <summary>
        /// 逻辑删除标志
        /// </summary>
        [JsonProperty]
        public bool DeleteFG { get; set; } = false;

        /// <summary>
        /// 备用字段1
        /// </summary>
        [JsonProperty, Column(DbType = "nchar(10)")]
        public string Field1 { get; set; }

        /// <summary>
        /// 备用字段2
        /// </summary>
        [JsonProperty, Column(DbType = "nchar(10)")]
        public string Field2 { get; set; }

        /// <summary>
        /// 备用字段3
        /// </summary>
        [JsonProperty, Column(DbType = "nchar(10)")]
        public string Field3 { get; set; }

        /// <summary>
        /// 备用字段4
        /// </summary>
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
        [JsonProperty, Column(InsertValueSql = "getutcdate()")]
        public DateTimeOffset LastModifyDate { get; set; }


        #region 外键 => 导航属性，ManyToOne/OneToOne

        [Navigate("UserID")]
        public virtual UserInfo SystemUserInfo { get; set; }

        [Navigate("RoleID")]
        public virtual RoleInfo SystemRoleInfo { get; set; }

        #endregion
    }

}
