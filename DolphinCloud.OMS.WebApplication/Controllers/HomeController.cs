using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Enums;
using DolphinCloud.OMS.WebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DolphinCloud.OMS.WebApplication.Controllers
{
    /// <summary>
    /// ��ҳ
    /// </summary>
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// ��ҳ��ͼ
        /// </summary>
        /// <returns></returns>
        [Menu( MunuType.RootMenu,"��ҳ",1)]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ����̨��ͼ
        /// </summary>
        /// <returns></returns>
        [Menu(MunuType.RootMenu, "����̨", 2)]
        public IActionResult Console()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// ����ҳ��
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