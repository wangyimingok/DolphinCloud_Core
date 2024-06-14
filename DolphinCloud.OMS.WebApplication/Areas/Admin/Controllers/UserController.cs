using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.DataModel.System.User;
using DolphinCloud.OMS.WebApplication.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.WebApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// 用户信息控制器
    /// </summary>
    [Area("Admin")]
    public class UserController : BaseController
    {
        private readonly IUserDataInterFace _user;
        public UserController(IUserDataInterFace userDataInterFace)
        {
            _user = userDataInterFace;
        }
        /// <summary>
        /// 用户信息首页
        /// </summary>
        /// <returns></returns>
        [Menu(MunuType.RootMenu, "用户管理", 98, "Admin")]
        public async Task<IActionResult> Index()
        {
            var result = await _user.GenerateAdmin();
            return View();
        }

        /// <summary>
        /// 创建用户视图
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View());
        }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [Menu(MunuType.Button_Function, "创建用户", 2, "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public Task<JsonResult> Create(UserCreateDataModel dataModel)
        {
            return Task.FromResult(Json(""));
        }

        /// <summary>
        /// 分页获得用户列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<PaginationResult<List<UserDataViewModel>>> GetUserPaginationDataList(UserPagination pagination, CancellationToken cancellationToken)
        {
            var result = await _user.GetUserPaginationDataListAsync(pagination, cancellationToken);
            return result;
        }

        /// <summary>
        /// 更新用户信息视图
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(long UserID)
        {
            var result = await _user.GetUserDataModelByUserIDAsync(UserID);
            if (result.Code == ResponseCode.OperationSuccess)
            {
                var UserDataModel = result.Data;
                return View(UserDataModel);
            }
            return View();
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit([FromBody] UserModifyDataModel dataModel)
        {
            var result = await _user.UpdateUserDataAsync(dataModel);
            return Json(result);
        }
    }
}
