using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.WebApplication.Views.Shared.Components.SideBarNav
{
    /// <summary>
    /// 导航栏视图组件
    /// </summary>
    [ViewComponent(Name = "SideBarNav")]
    public class SideBarNavViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
