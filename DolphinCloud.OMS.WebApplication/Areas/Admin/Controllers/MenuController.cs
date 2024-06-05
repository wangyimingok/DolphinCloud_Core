using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Enums;
using DolphinCloud.DataModel.System.Menu;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.WebApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    public class MenuController : Controller
    {
        /// <summary>
        /// 菜单首页
        /// </summary>
        /// <returns></returns>
        [Menu(MunuType.RootMenu, "菜单管理", 99, "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMenu(MenuCreateDataModel dataModel)
        {
            return await Task.FromResult(new JsonResult(""));
        }
    }
}
