using AutoMapper;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.Common.Security;
using DolphinCloud.Common.Snowflake;
using DolphinCloud.DataEntity.System;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.DataModel.Account;
using DolphinCloud.DataModel.System.Menu;
using DolphinCloud.DataModel.System.User;
using DolphinCloud.Framework.Session;
using DolphinCloud.Repository.System;
using FreeScheduler;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataServices.System
{
    /// <summary>
    /// 用户数据服务
    /// </summary>
    public class UserDataService : BaseService, IUserDataInterFace
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private readonly ILogger<UserDataService> _logger;
        /// <summary>
        /// 映射工具接口
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// 用户数据仓储
        /// </summary>
        private readonly UserRepository _userRepo;

        private readonly ICurrentUserInfo _currentUser;

        public UserDataService(ILogger<UserDataService> logger, IMapper mapper, UserRepository userRepository, ICurrentUserInfo currentUserInfo)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepo = userRepository;
            _currentUser = currentUserInfo;
        }

        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public async Task<OperationMessage> CreateUser(UserCreateDataModel dataModel)
        {
            try
            {
                var UserData = _mapper.Map<UserCreateDataModel, UserInfo>(dataModel);
                if (await _userRepo.Where(a => a.UserName == UserData.UserName).AnyAsync())
                {
                    return new OperationMessage(ResponseCode.OperationWarning, $"用户名【{UserData.UserName}】已存在");
                }
                if (await _userRepo.Where(a => a.EMailAddress == UserData.EMailAddress).AnyAsync())
                {
                    return new OperationMessage(ResponseCode.OperationWarning, $"邮箱地址【{UserData.EMailAddress}】已存在");
                }
                if (await _userRepo.Where(a => a.MobileNumber == UserData.MobileNumber).AnyAsync())
                {
                    return new OperationMessage(ResponseCode.OperationWarning, $"手机号码【{UserData.MobileNumber}】已存在");
                }
                else
                {
                    UserData.UserID = IdHelper.GetLongId();
                    UserData.PassWord = SecurityUtil.MD5_HexConvert(SecurityUtil.Base64Encode(dataModel.PassWord));
                    if (string.IsNullOrEmpty(_currentUser.UserName))
                    {
                        UserData.CreateBy = "System";
                    }
                    else
                    {
                        UserData.CreateBy = _currentUser.UserName;
                    }
                    UserData.CreateDateTime = DateTimeOffset.Now;
                    UserData.LastModifyBy = "System";
                    UserData.LastModifyDate = DateTimeOffset.Now;
                    await _userRepo.InsertAsync(UserData);
                    return new OperationMessage(ResponseCode.OperationSuccess, $"用户【{UserData.UserName}】新增成功");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"创建用户异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"创建用户异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 逻辑删除用户数据
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public async Task<OperationMessage> DeleteUserAsync(UserDataViewModel dataModel)
        {
            try
            {
                var CurrentDataEntity = await _userRepo.Select.Where(a => a.UserID == dataModel.UserID && a.UserName == dataModel.UserName && a.EMailAddress == dataModel.EMailAddress && a.MobileNumber == dataModel.MobileNumber).ToUpdate().Set(a => a.DeleteFG, true).Set(a => a.LastModifyBy, _currentUser.UserName).Set(a => a.LastModifyDate, DateTimeOffset.Now).ExecuteAffrowsAsync();
                if (CurrentDataEntity > 0)
                {
                    return new OperationMessage(ResponseCode.OperationSuccess, "删除用户成功");
                }
                else
                {
                    return new OperationMessage(ResponseCode.OperationWarning, "未查询到符合条件的用户信息数据,删除失败");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"逻辑删除用户异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"逻辑删除用户异常,异常原因为:【{ex.Message}】");
            }
        }
        /// <summary>
        /// 检查邮箱地址是否被占用
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public async Task<OperationMessage> EMailAddressIsExistAsync(string emailAddress)
        {
            try
            {
                var isExist = await _userRepo.Select.Where(a => a.EMailAddress == emailAddress).AnyAsync();
                if (isExist)
                {
                    return new OperationMessage(ResponseCode.OperationWarning, $"邮箱地址【{emailAddress}】在系统中已存在,请更换一个邮箱地址再试!");
                }
                else
                {
                    return new OperationMessage(ResponseCode.OperationSuccess, "此邮箱地址未被占用,可以使用");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"检查邮箱地址是否被占用出现异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"检查邮箱地址是否被占用出现异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 生成管理员账号
        /// </summary>
        /// <returns></returns>
        public async Task<OperationMessage> GenerateAdmin()
        {
            try
            {
                if (!await _userRepo.Select.Where(a => a.UserName.Equals("Admin", StringComparison.OrdinalIgnoreCase)).AnyAsync())
                {
                    UserInfo user = new UserInfo();
                    user.UserName = "admin";
                    user.RealName = "超级管理员";
                    user.UserID = IdHelper.GetLongId();
                    user.EMailAddress = "admin@admin.com";
                    user.MobileNumber = "18588409774";
                    user.PassWord = SecurityUtil.MD5_HexConvert(SecurityUtil.Base64Encode("admin123"));
                    user.CreateBy = "System";
                    user.CreateDateTime = DateTimeOffset.Now;
                    user.LastModifyBy = "System";
                    user.LastModifyDate = DateTimeOffset.Now;
                    user.Status = 1;
                    await _userRepo.InsertAsync(user);
                    return new OperationMessage(ResponseCode.OperationSuccess, "创建超级管理员帐号成功");
                }
                else
                {
                    return new OperationMessage(ResponseCode.OperationWarning, "超级管理员帐号已存在");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"生成管理员账号异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"生成管理员账号异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 根据用户主键获取用户信息
        /// 用于更新
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<ResultMessage<UserModifyDataModel>> GetUserDataModelByUserIDAsync(long UserID)
        {
            try
            {
                if (UserID > 0)
                {
                    var DataEntity = await _userRepo.Select.Where(a => a.UserID == UserID).ToOneAsync();
                    if (DataEntity != null)
                    {
                        var DataModel = _mapper.Map<UserInfo, UserModifyDataModel>(DataEntity);
                        return new ResultMessage<UserModifyDataModel>(ResponseCode.OperationSuccess, "查询成功", DataModel);
                    }
                }
                return new ResultMessage<UserModifyDataModel>(ResponseCode.OperationWarning, "未查询到符合条件的用户信息数据");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"根据用户ID获得用户信息查询异常,异常原因为:【{ex.Message}】");
                return new ResultMessage<UserModifyDataModel>(ResponseCode.ServerError, $"根据用户ID获得用户信息查询异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 分页查询用户列表
        /// 异步方法
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<PaginationResult<List<UserDataViewModel>>> GetUserPaginationDataListAsync(UserPagination pagination, CancellationToken cancellationToken)
        {
            try
            {
                long totalDataCount = 0;
                var MenuList = await _userRepo.Select
                    .Page(pagination.PageIndex, pagination.PageSize)
                    .WhereIf(!string.IsNullOrWhiteSpace(pagination.SearchKey), a => a.UserName.Contains(pagination.SearchKey)
                        || a.MobileNumber.Contains(pagination.SearchKey) || a.EMailAddress.Contains(pagination.SearchKey)
                        || a.RealName.Contains(pagination.SearchKey))
                    //.WhereIf(!string.IsNullOrWhiteSpace(pagination.SearchKey), a => a.MobileNumber.Contains(pagination.SearchKey))
                    //.WhereIf(!string.IsNullOrWhiteSpace(pagination.SearchKey), a => a.EMailAddress.Contains(pagination.SearchKey))
                    //.WhereIf(!string.IsNullOrWhiteSpace(pagination.SearchKey), a => a.RealName.Contains(pagination.SearchKey))
                    .Where(a => a.DeleteFG == false)
                    .Count(out totalDataCount)
                    .ToListAsync(cancellationToken);
                var DataModel = _mapper.Map<List<UserInfo>, List<UserDataViewModel>>(MenuList);
                return new PaginationResult<List<UserDataViewModel>>(ResponseCode.OperationSuccess, "查询成功", totalDataCount, DataModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"分页查询用户列表异常,异常原因为:【{ex.Message}】");
                return new PaginationResult<List<UserDataViewModel>>(ResponseCode.ServerError, $"分页查询用户列表异常,异常原因为:【{ex.Message}】", 0, null);
            }
        }

        /// <summary>
        /// 手机号码是否被占用
        /// </summary>
        /// <param name="MobilePhone"></param>
        /// <returns></returns>
        public async Task<OperationMessage> MobilePhoneIsExistAsync(string MobilePhone)
        {
            try
            {
                var isExist = await _userRepo.Select.Where(a => a.MobileNumber == MobilePhone).AnyAsync();
                if (isExist)
                {
                    return new OperationMessage(ResponseCode.OperationWarning, $"手机号码【{MobilePhone}】在系统中已存在,请更换一个手机号码再试!");
                }
                else
                {
                    return new OperationMessage(ResponseCode.OperationSuccess, "此手机号码未被占用,可以使用");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"检查手机号码是否被占用出现异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"检查手机号码是否被占用出现异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public async Task<OperationMessage> UpdateUserDataAsync(UserModifyDataModel dataModel)
        {
            try
            {
                var CurrentDataEntity = await _userRepo.Select.Where(a => a.UserID == dataModel.UserID).ToOneAsync();
                if (CurrentDataEntity != null)
                {
                    var result = await _userRepo.UpdateDiy.SetIf(dataModel.UserName != CurrentDataEntity.UserName, a => a.UserName, dataModel.UserName)
                          .SetIf(dataModel.MobileNumber != CurrentDataEntity.MobileNumber, a => a.MobileNumber, dataModel.MobileNumber)
                          .SetIf(dataModel.EMailAddress != CurrentDataEntity.EMailAddress, a => a.EMailAddress, dataModel.EMailAddress)
                          .SetIf(dataModel.RealName != CurrentDataEntity.RealName, a => a.RealName, dataModel.RealName)
                          .SetIf(dataModel.Status != CurrentDataEntity.Status, a => a.Status, dataModel.Status)
                          .Set(a => a.LastModifyBy, _currentUser.UserName)
                          .Set(a => a.LastModifyDate, DateTimeOffset.Now)
                          .Where(a => a.UserID == dataModel.UserID).ExecuteAffrowsAsync();
                    return new OperationMessage(ResponseCode.OperationSuccess, "用户信息更新成功");
                }
                else
                {
                    return new OperationMessage(ResponseCode.OperationWarning, "未查询到符合条件的用户信息数据,更新失败");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"更新用户信息异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"更新用户信息异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 检查用户名是否已存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<OperationMessage> UserNameIsExistAsync(string userName)
        {
            try
            {
                var isExist = await _userRepo.Select.Where(a => a.UserName == userName).AnyAsync();
                if (isExist)
                {
                    return new OperationMessage(ResponseCode.OperationWarning, $"用户名【{userName}】在系统中已存在,请更换一个用户名再试!");
                }
                else
                {
                    return new OperationMessage(ResponseCode.OperationSuccess, "此用户名未被占用,可以使用");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"检查用户名是否被占用出现异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"检查用户名是否被占用出现异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public async Task<ResultMessage<LoginViewModel>> LoginValidateAsync(LoginDataModel dataModel)
        {
            try
            {
                var userData = await _userRepo.Select.Where(a => a.UserName == dataModel.UserName && a.DeleteFG == false).ToOneAsync();
                if (userData != null && userData != default)
                {
                    string passWord = SecurityUtil.MD5_HexConvert(SecurityUtil.Base64Encode(dataModel.PassWord));
                    if (userData.PassWord.Equals(passWord, StringComparison.OrdinalIgnoreCase))
                    {
                        var loginData = _mapper.Map<UserInfo, LoginViewModel>(userData);
                        return new ResultMessage<LoginViewModel>(ResponseCode.OperationSuccess, "登陆验证成功", loginData);
                    }
                    else
                    {
                        return new ResultMessage<LoginViewModel>(ResponseCode.OperationWarning, "密码错误");
                    }
                }
                else
                {
                    return new ResultMessage<LoginViewModel>(ResponseCode.OperationWarning, "用户名不存在");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"登陆验证出现异常,异常原因为:【{ex.Message}】");
                return new ResultMessage<LoginViewModel>(ResponseCode.ServerError, $"登陆验证出现异常,异常原因为:【{ex.Message}】");
            }
        }
    }
}
