﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace DolphinCloud.DataEntity.System
{

    /// <summary>
    /// 角色与菜单关系表
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "System_RoleAuthorityInfo", DisableSyncStructure = true)]
    public partial class RoleAuthorityInfo
    {

        /// <summary>
        /// 角色菜单关系主键
        /// </summary>
        [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
        public int RelationID { get; set; }

        /// <summary>
        /// 菜单主键
        /// </summary>
        [JsonProperty]
        public int MenuID
        {
            get => _MenuID; set
            {
                if (_MenuID == value) return;
                _MenuID = value;
                SystemMenuInfo = null;
            }
        }
        private int _MenuID;

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
        [JsonProperty, Column(InsertValueSql = "getdate()")]
        public DateTimeOffset LastModifyDate { get; set; }


        #region 外键 => 导航属性，ManyToOne/OneToOne

        [Navigate("RoleID")]
        public virtual RoleInfo SystemRoleInfo { get; set; }

        [Navigate("MenuID")]
        public virtual MenuInfo SystemMenuInfo { get; set; }

        #endregion
    }

}
