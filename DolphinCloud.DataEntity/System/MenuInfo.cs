using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace DolphinCloud.DataEntity.System
{

    /// <summary>
    /// 菜单信息表
    /// </summary>
    [JsonObject(MemberSerialization.OptIn), Table(Name = "System_MenuInfo", DisableSyncStructure = true)]
    public partial class MenuInfo
    {

        /// <summary>
        /// 菜单主键
        /// </summary>
        [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
        public int MenuID { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        [JsonProperty, Column(StringLength = 50)]
        public string ActionName { get; set; }

        /// <summary>
        /// 域名称
        /// </summary>
        [JsonProperty, Column(StringLength = 50)]
        public string AreaName { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        [JsonProperty, Column(StringLength = 50)]
        public string ControllerName { get; set; }

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

        [JsonProperty, Column(StringLength = 50)]
        public string Field1 { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string Field2 { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string Field3 { get; set; }

        [JsonProperty, Column(StringLength = 50)]
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

        /// <summary>
        /// 图标
        /// </summary>
        [JsonProperty, Column(StringLength = 50)]
        public string MenuIcon { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [JsonProperty, Column(StringLength = 50)]
        public string MenuName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [JsonProperty]
        public short MenuType { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        [JsonProperty, Column(StringLength = 50)]
        public string MenuUrlAddress { get; set; }

        /// <summary>
        /// 上级菜单主键
        /// </summary>
        [JsonProperty]
        public int? ParentID { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        [JsonProperty]
        public short SortNumber { get; set; }


        #region 外键 => 导航属性，OneToMany

        [Navigate("MenuID")]
        public virtual List<RoleAuthorityInfo> SystemRoleAuthorityInfos { get; set; }

        #endregion
    }

}
