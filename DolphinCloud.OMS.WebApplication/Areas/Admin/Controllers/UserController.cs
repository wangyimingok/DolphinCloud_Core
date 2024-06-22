using Castle.Facilities.TypedFactory;
using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Constants;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.DataModel.System.User;
using DolphinCloud.OMS.WebApplication.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.WebApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// 用户信息控制器
    /// </summary>
    [Area("Admin")]
    [Authorize(Policy = PermissionPolicy.AdminArea)]
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
        public async Task<JsonResult> CreateUser([FromBody] UserCreateDataModel dataModel)
        {
            if (string.IsNullOrWhiteSpace(dataModel.UserName))
            {
                var result = new OperationMessage(ResponseCode.OperationWarning, "用户名不能为空");
                return new JsonResult(result);
            }
            if (string.IsNullOrWhiteSpace(dataModel.MobileNumber))
            {
                var result = new OperationMessage(ResponseCode.OperationWarning, "手机号码不能为空");
                return new JsonResult(result);
            }
            if (string.IsNullOrWhiteSpace(dataModel.EMailAddress))
            {
                var result = new OperationMessage(ResponseCode.OperationWarning, "邮箱地址不能为空");
                return new JsonResult(result);
            }
            if (dataModel != null)
            {
                var result = await _user.CreateUser(dataModel);
                return new JsonResult(result);
            }
            else
            {
                var result = new OperationMessage(ResponseCode.OperationWarning, "参数错误");
                return new JsonResult(result);
            }

        }

        /// <summary>
        /// 分页获得用户列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<PaginationResult<List<UserDataViewModel>>> GetUserPaginationDataList([FromBody] UserPagination pagination, CancellationToken cancellationToken)
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

        /// <summary>
        /// 校验用户名是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> UserNameIsExist(string UserName)
        {
            var result = await _user.UserNameIsExistAsync(UserName);
            return Json(result);
        }

        /// <summary>
        /// 校验邮箱地址是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> EmailAddressIsExist(string emailAddress)
        {
            var result = await _user.EMailAddressIsExistAsync(emailAddress);
            return Json(result);
        }

        /// <summary>
        /// 校验手机号码是否存在
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> MobilePhoneIsExist(string mobilePhoneNumber)
        {
            var result = await _user.MobilePhoneIsExistAsync(mobilePhoneNumber);
            return Json(result);
        }

        /// <summary>
        /// 逻辑删除用户
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteUser([FromBody] UserDataViewModel dataModel)
        {
            var result = await _user.DeleteUserAsync(dataModel);
            return Json(result);
        }
    }
}
