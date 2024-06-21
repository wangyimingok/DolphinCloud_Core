using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Constants;
using DolphinCloud.Common.Enums;
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
        public MenuController(ILogger<MenuController> logger, IMenuDataInterFace menuDataInter)
        {
            _menuData = menuDataInter;
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
        /// 创建菜单视图页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View());
        }
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [Menu(MunuType.Button_Function, "创建菜单", 1, "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMenu([FromBody] MenuCreateDataModel dataModel)
        {
            if (string.IsNullOrWhiteSpace(dataModel.ControllerName) && string.IsNullOrWhiteSpace(dataModel.ActionName))
            {
                dataModel.MenuUrlAddress = "/";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(dataModel.AreaName))
                {
                    dataModel.MenuUrlAddress = $"/{dataModel.AreaName}/{dataModel.ControllerName}/{dataModel.ActionName}";
                }
                else
                {
                    dataModel.MenuUrlAddress = $"/{dataModel.ControllerName}/{dataModel.ActionName}";
                }
            }
            var result = await _menuData.CreateMenu(dataModel);
            return await Task.FromResult(new JsonResult(result));
        }

        /// <summary>
        /// 编辑菜单视图页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int MenuID)
        {
            var result = await _menuData.GetMenuDataModelByMenuIDAsync(MenuID);
            if (result.Code == ResponseCode.OperationSuccess)
            {
                var UserDataModel = result.Data;
                return View(UserDataModel);
            }
            return View();
        }

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] MenuModifyDataModel dataModel)
        {
            if (string.IsNullOrWhiteSpace(dataModel.ControllerName) && string.IsNullOrWhiteSpace(dataModel.ActionName))
            {
                dataModel.MenuUrlAddress = "/";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(dataModel.AreaName))
                {
                    dataModel.MenuUrlAddress = $"/{dataModel.AreaName}/{dataModel.ControllerName}/{dataModel.ActionName}";
                }
                else
                {
                    dataModel.MenuUrlAddress = $"/{dataModel.ControllerName}/{dataModel.ActionName}";
                }
            }
            var result = await _menuData.UpdateMenuDataAsync(dataModel);
            return await Task.FromResult(new JsonResult(result));
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

        /// <summary>
        /// 页面下拉框选项数据获取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetMenuSelectOption()
        {
            var result = await _menuData.GetMenuSelectOptionAsync();
            return new JsonResult(result);
        }

        /// <summary>
        /// 逻辑删除用户
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteMenu([FromBody] MenuDataViewModel dataModel)
        {
            var result = await _menuData.DeleteMenuAsync(dataModel);
            return Json(result);
        }
    }
}
