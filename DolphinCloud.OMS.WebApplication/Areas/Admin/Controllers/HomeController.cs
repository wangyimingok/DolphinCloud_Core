using DolphinCloud.Common.Constants;
using DolphinCloud.OMS.WebApplication.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.WebApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// 管理端主页
    /// </summary>
    [Area("Admin")]
    [Authorize(Policy = Permissions.AdminArea)]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Console()
        {
            return View();
        }
    }
}
