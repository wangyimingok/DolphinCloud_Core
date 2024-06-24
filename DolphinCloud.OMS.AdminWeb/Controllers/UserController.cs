using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Constants;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.DataModel.System.User;
using DolphinCloud.Framework.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.AdminWeb.Controllers
{
    /// <summary>
    /// 用户信息控制器
    /// </summary>
    [Authorize(Policy = PermissionPolicy.AdminArea)]
    public class UserController : BaseController
    {
        /// <summary>
        /// 用户数据接口
        /// </summary>
        private readonly IUserDataInterFace _user;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        private readonly ICurrentUserInfo _currentUser;
        public UserController(IUserDataInterFace userDataInterFace, ICurrentUserInfo currentUserInfo)
        {
            _user = userDataInterFace;
            _currentUser = currentUserInfo;
        }
        /// <summary>
        /// 用户信息首页
        /// </summary>
        /// <returns></returns>
        [Menu(MunuType.ChildMenu, "用户管理", 98)]
        public async Task<IActionResult> Index()
        {
            var result = await _user.GenerateAdmin();
            return View();
        }

        /// <summary>
        /// 创建用户视图
        /// </summary>
        /// <returns></returns>
        [Menu("用户创建", MunuType.PageView)]
        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View());
        }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [Menu(MunuType.Button_Function, "创建用户", 2)]
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
        [Menu("用户列表", MunuType.Button_Function)]
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
        [Menu("更新用户信息", MunuType.PageView)]
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
        [Menu("更新用户信息", MunuType.Button_Function)]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [Menu("删除用户信息", MunuType.Button_Function)]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteUser([FromBody] UserDataViewModel dataModel)
        {
            var result = await _user.DeleteUserAsync(dataModel);
            return Json(result);
        }

        /// <summary>
        /// 修改密码页面视图
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ResetPassword()
        {
            var result = await _user.GetResetPasswordDataModelAsync(_currentUser.UserID);
            if (result.Code == ResponseCode.OperationSuccess)
            {
                var UserDataModel = result.Data;
                return View(UserDataModel);
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> ResetPassword([FromBody] ResetPasswordDataModel dataModel)
        {
            var result = await _user.ResetPasswordAsync(dataModel);
            return Json(result);
        }

        /// <summary>
        /// 用户基本信息页面视图
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> BasicInfo()
        {
            var result = await _user.GetBasicInfoDataModelAsync(_currentUser.UserID);
            if (result.Code == ResponseCode.OperationSuccess)
            {
                var UserDataModel = result.Data;
                return View(UserDataModel);
            }
            return View();
        }

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateBasicInfo([FromBody] BasicInfoDataModel dataModel)
        {
            var result = await _user.UpdateUserBasicInfoDataAsync(dataModel);
            return Json(result);
        }
    }
}
