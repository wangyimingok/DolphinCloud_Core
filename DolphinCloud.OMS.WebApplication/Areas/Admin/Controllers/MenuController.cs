using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Constants;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Pagination;
using DolphinCloud.Common.Result;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.DataModel.System.Menu;
using DolphinCloud.OMS.WebApplication.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.WebApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    [Area("Admin")]
    [Authorize(Policy = PermissionPolicy.AdminArea)]
    public class MenuController : BaseController
    {
        private readonly ILogger<MenuController> _logger;

        private readonly IMenuDataInterFace _menuData;
        public MenuController(ILogger<MenuController> logger,IMenuDataInterFace menuDataInter)
        {
            _menuData=  menuDataInter;
            _logger = logger;
        }
        /// <summary>
        /// 菜单首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
        [Menu(MunuType.Button_Function, "创建菜单", 1, "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMenu(MenuCreateDataModel dataModel)
        {
            return await Task.FromResult(new JsonResult(""));
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<PaginationResult<List<MenuDataViewModel>>> GetMenuTable(MenuParameter pagination)
        {
            var result = await _menuData.GetMenuTableAsync(pagination);
            return result;
        }
    }
}
