using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.WebApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// 管理端主页
    /// </summary>
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
