using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Constants;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.DataModel.System.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.AdminWeb.Controllers
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    [Authorize]
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
        [Menu(MunuType.ChildMenu, "菜单管理", 99)]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 创建菜单视图页
        /// </summary>
        /// <returns></returns>
        [Menu(MunuType.PageView, "创建菜单", 99)]
        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View());
        }
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [Menu(MunuType.Button_Function, "创建菜单", 1)]
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
        [Menu(MunuType.PageView, "编辑菜单", 2)]
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
        [Menu(MunuType.Button_Function, "编辑菜单", 2)]
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
        [Menu(MunuType.Button_Function, "获取菜单列表", 3)]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<PaginationResult<List<MenuDataViewModel>>> GetMenuTable([FromBody] MenuParameter pagination)
        {
            var result = await _menuData.GetMenuTableAsync(pagination);
            return result;
        }

        /// <summary>
        /// 页面下拉框选项数据获取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetMenuSelectOption()
        {
            var result = await _menuData.GetMenuSelectOptionAsync();
            return new JsonResult(result);
        }

        /// <summary>
        /// 逻辑删除菜单
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [Menu(MunuType.Button_Function, "删除菜单", 4)]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteMenu([FromBody] MenuDataViewModel dataModel)
        {
            var result = await _menuData.DeleteMenuAsync(dataModel);
            return Json(result);
        }

        /// <summary>
        /// 获得权限树控件数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<JsonResult> GetPermissionTreeData()
        {
            var result = await _menuData.GetPermissionTreeData();
            return new JsonResult(result);
        }
    }
}
