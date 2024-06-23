using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Constants;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.DataModel.System.Role;
using DolphinCloud.OMS.WebApplication.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.WebApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [Area("Admin")]
    [Authorize(Policy = PermissionPolicy.AdminArea)]
    public class RoleController : BaseController
    {
        private readonly IRoleDataInterFace _roleData;
        public RoleController(IRoleDataInterFace roleDataInterFace)
        {
            _roleData= roleDataInterFace;
        }
        /// <summary>
        /// 列表页视图
        /// </summary>
        /// <returns></returns>
        [Menu("角色管理", MunuType.ChildMenu, "Admin")]
        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }

        /// <summary>
        /// 分页获得角色列表数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [Menu("角色列表", MunuType.Button_Function, "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> GetRolePaginationDataList([FromBody] RolePagination pagination)
        { 
            var result=await _roleData.GetRoleDataPaginationAsync(pagination);
            return new JsonResult(result);
        }
        /// <summary>
        /// 角色创建页视图
        /// </summary>
        /// <returns></returns>
        [Menu("角色创建", MunuType.PageView, "Admin")]
        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View());
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [Menu("创建角色", MunuType.Button_Function, "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> CreateRole([FromBody] RoleCreateDataModel dataModel)
        {
            var result = await _roleData.CreateRole(dataModel);
            return new JsonResult(result);
        }

        /// <summary>
        /// 角色信息编辑页视图
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        [Menu("角色编辑", MunuType.PageView, "Admin")]
        public async Task<IActionResult> Edit(int RoleID)
        {
            var result=await _roleData.GetRoleDataModelByRoleIDAsync(RoleID);
            if (result.Code== ResponseCode.OperationSuccess)
            {
                return await Task.FromResult(View(result.Data));
            }
            return await Task.FromResult(View());
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [Menu("更新角色信息", MunuType.Button_Function, "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateRole([FromBody] RoleModifyDataModel dataModel)
        {
            var result = await _roleData.UpdateRoleDataAsync(dataModel);
            return new JsonResult(result);
        }


        /// <summary>
        /// 逻辑删除角色数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [Menu("删除角色信息", MunuType.Button_Function, "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteRole([FromBody] RoleDataViewModel dataModel)
        {
            var result = await _roleData.DeleteRoleAsync(dataModel);
            return new JsonResult(result);
        }

        /// <summary>
        /// 授权视图页
        /// </summary>
        /// <returns></returns>
        [Menu("角色授权", MunuType.PageView,"Admin")]
        public async Task<IActionResult> Authorization(int RoleID)
        {
            var result = await _roleData.GetRoleAuthorDataModelAsync(RoleID);
            if (result.Code == ResponseCode.OperationSuccess)
            {
                return await Task.FromResult(View(result.Data));
            }
            return await Task.FromResult(View());
        }
        /// <summary>
        /// 为角色授权
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> Authorization([FromBody] RoleAuthorDataModel dataModel)
        { 
            var result = await _roleData.ConfigPermissionByRoleAsync(dataModel);
            return  new JsonResult(new OperationMessage(ResponseCode.OperationSuccess, "授权成功"));
        }
    }
}
