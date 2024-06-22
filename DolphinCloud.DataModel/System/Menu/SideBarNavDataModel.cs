using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.Menu
{
    /// <summary>
    /// 导航栏数据模型
    /// </summary>
    public class SideBarNavDataModel
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

        /// <summary>
        /// 子菜单数据
        /// </summary>
        public List<SideBarNavDataModel> childMenuData { get; set; }
    }
}
