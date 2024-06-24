using DolphinCloud.DataInterFace.System;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.AdminWeb.Views.Shared.Components.SideBarNav
{
    /// <summary>
    /// 导航栏视图组件
    /// </summary>
    [ViewComponent(Name = "SideBarNav")]
    public class SideBarNavViewComponent : ViewComponent
    {
        /// <summary>
        /// 菜单数据接口
        /// </summary>
        private readonly IMenuDataInterFace _menuData;

        public SideBarNavViewComponent(IMenuDataInterFace menuData)
        {
            _menuData = menuData;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var menuList =await _menuData.GetSideBarNavDataModelsAsync();
            var model = new SideBarNavViewModel
            {
                MenuData = await _menuData.GetSideBarNavDataModelsAsync()
            };
            return View(model);
        }


    }
}
