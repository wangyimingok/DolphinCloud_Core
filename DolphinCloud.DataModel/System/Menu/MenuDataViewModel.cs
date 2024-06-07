using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.Menu
{
    public class MenuDataViewModel
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
        /// 创建人
        /// </summary>
        [JsonProperty]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty]
        public DateTimeOffset CreateDateTime { get; set; }

        /// <summary>
        /// 逻辑删除标志
        /// </summary>
        [JsonProperty]
        public bool DeleteFG { get; set; } = false;
     
        /// <summary>
        /// 最后修改人
        /// </summary>
        [JsonProperty]
        public string LastModifyBy { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [JsonProperty]
        public DateTimeOffset LastModifyDate { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [JsonProperty]
        public string MenuIcon { get; set; }

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
        /// 上级菜单主键
        /// </summary>
        [JsonProperty]
        public int? ParentID { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        [JsonProperty]
        public short SortNumber { get; set; }
    }
}
