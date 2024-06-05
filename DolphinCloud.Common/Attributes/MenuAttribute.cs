using DolphinCloud.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Attributes
{
    /// <summary>
    /// 菜单特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class MenuAttribute : Attribute
    {
        /// <summary>
        /// 排序值
        /// </summary>
        public int SortNumber { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 所属域名
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public MunuType MenuType { get; set; }

        /// <summary>
        /// 构建实例
        /// </summary>
        /// <param name="sortNumber">排序值</param>
        /// <param name="displayName">菜单显示名称</param>
        /// <param name="domainName">所属区域名称</param>
        /// <param name="menuType">菜单类型</param>
        public MenuAttribute(int sortNumber, string displayName, string domainName, MunuType menuType)
        {
            SortNumber = sortNumber;
            DisplayName = displayName;
            DomainName = domainName;
            MenuType = menuType;
        }

        /// <summary>
        /// 构建实例
        /// </summary>
        /// <param name="menuType">菜单类型</param>
        /// <param name="displayName">菜单显示名称</param>
        /// <param name="sortNumber">排序值</param>
        /// <param name="domainName">菜单类型</param>
        public MenuAttribute(MunuType menuType, string displayName, int sortNumber, string domainName = null)
        {
            SortNumber = sortNumber;
            DisplayName = displayName;
            DomainName = domainName;
            MenuType = menuType;
        }
    }
}
