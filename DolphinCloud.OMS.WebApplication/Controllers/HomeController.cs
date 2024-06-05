using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Enums;
using DolphinCloud.OMS.WebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DolphinCloud.OMS.WebApplication.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 首页视图
        /// </summary>
        /// <returns></returns>
        [Menu( MunuType.RootMenu,"首页",1)]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 控制台视图
        /// </summary>
        /// <returns></returns>
        [Menu(MunuType.RootMenu, "控制台", 2)]
        public IActionResult Console()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// 错误页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}