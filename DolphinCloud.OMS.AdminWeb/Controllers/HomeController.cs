using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Constants;
using DolphinCloud.Common.Enums;
using DolphinCloud.OMS.AdminWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DolphinCloud.OMS.AdminWeb.Controllers
{
    /// <summary>
    /// �������ҳ
    /// </summary>
    [Authorize(Policy = PermissionPolicy.AdminArea)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// ��ҳ
        /// </summary>
        /// <returns></returns>
        [Menu("��ҳ", MunuType.RootMenu)]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ����̨
        /// </summary>
        /// <returns></returns>
        [Menu("����̨", MunuType.RootMenu)]
        public IActionResult Console()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
