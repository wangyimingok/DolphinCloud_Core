using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Constants;
using DolphinCloud.Common.Enums;
using DolphinCloud.OMS.WebApplication.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.WebApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// 管理端主页
    /// </summary>
    [Area("Admin")]
    [Authorize(Policy = PermissionPolicy.AdminArea)]
    public class HomeController : BaseController
    {
        [Menu("管理首页", MunuType.RootMenu, "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        
        [Menu("控制台", MunuType.RootMenu, "Admin")]
        public IActionResult Console()
        {
            return View();
        }
    }
}
