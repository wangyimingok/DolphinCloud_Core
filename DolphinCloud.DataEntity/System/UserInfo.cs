using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace DolphinCloud.DataEntity.System
{

    /// <summary>
    /// 用户信息表
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "System_UserInfo", DisableSyncStructure = true)]
    public partial class UserInfo
    {

        /// <summary>
        /// 用户信息主键
        /// </summary>
        [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
        public long UserID { get; set; }

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
        /// 邮箱地址
        /// </summary>
        [JsonProperty, Column(StringLength = 100, IsNullable = false)]
        public string EMailAddress { get; set; }

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

        /// <summary>
        /// 手机号码
        /// </summary>
        [JsonProperty, Column(StringLength = 30, IsNullable = false)]
        public string MobileNumber { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [JsonProperty, Column(StringLength = 50, IsNullable = false)]
        public string PassWord { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [JsonProperty, Column(StringLength = 10, IsNullable = false)]
        public string RealName { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        [JsonProperty]
        public short Status { get; set; } = 0;

        /// <summary>
        /// 用户名称
        /// </summary>
        [JsonProperty, Column(StringLength = 50, IsNullable = false)]
        public string UserName { get; set; }


        #region 外键 => 导航属性，OneToMany

        [Navigate("UserID")]
        public virtual List<UserRoleRelationInfo> SystemUserRoleRelationInfos { get; set; }

        #endregion
    }

}
