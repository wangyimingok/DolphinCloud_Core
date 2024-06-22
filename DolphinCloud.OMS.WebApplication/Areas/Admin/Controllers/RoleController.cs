using DolphinCloud.Common.Constants;
using DolphinCloud.Common.Enums;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.DataModel.System.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.WebApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [Area("Admin")]
    [Authorize(Policy = PermissionPolicy.AdminArea)]
    public class RoleController : Controller
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
        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }

        /// <summary>
        /// 分页获得角色列表数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
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
        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View());
        }

        /// <summary>
        /// 分页获得角色列表数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
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
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteRole([FromBody] RoleDataViewModel dataModel)
        {
            var result = await _roleData.DeleteRoleAsync(dataModel);
            return new JsonResult(result);
        }
    }
}
