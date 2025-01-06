using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.AdminWeb.Controllers
{
    /// <summary>
    /// 店铺控制器
    /// </summary>
    [Authorize]
    public class ShopController : BaseController
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private readonly ILogger<ShopController> _logger;

        public ShopController(ILogger<ShopController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 列表页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
