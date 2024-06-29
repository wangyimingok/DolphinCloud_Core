using AutoMapper;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.Common.Security;
using DolphinCloud.Common.Snowflake;
using DolphinCloud.DataEntity.System;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.DataModel.Account;
using DolphinCloud.DataModel.Base;
using DolphinCloud.DataModel.System.Role;
using DolphinCloud.DataModel.System.User;
using DolphinCloud.Framework.Session;
using DolphinCloud.Repository.System;
using Microsoft.Extensions.Logging;

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
        /// <summary>
        /// 当前用户信息
        /// </summary>
        private readonly ICurrentUserInfo _currentUser;
        /// <summary>
        /// 用户角色关系仓储
        /// </summary>
        private readonly UserRoleRelationRepository _userRoleRelationRepo;
        public UserDataService(ILogger<UserDataService> logger, IMapper mapper, UserRepository userRepository, ICurrentUserInfo currentUserInfo, UserRoleRelationRepository userRoleRelationRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepo = userRepository;
            _currentUser = currentUserInfo;
            _userRoleRelationRepo = userRoleRelationRepository;
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

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="resetPassword"></param>
        /// <returns></returns>
        public async Task<OperationMessage> ResetPasswordAsync(ResetPasswordDataModel resetPassword)
        {
            try
            {
                var oldPassword = SecurityUtil.MD5_HexConvert(SecurityUtil.Base64Encode(resetPassword.OldPassWord));
                if (!await _userRepo.Where(a => a.UserID == resetPassword.UserID && a.PassWord == oldPassword).AnyAsync())
                {
                    return new OperationMessage(ResponseCode.OperationWarning, "原密码错误");
                }
                var newPassWord = SecurityUtil.MD5_HexConvert(SecurityUtil.Base64Encode(resetPassword.NewPassWord));
                var result = await _userRepo.UpdateDiy.Set(a => a.PassWord, newPassWord).Set(a => a.LastModifyBy, _currentUser.UserName).Set(a => a.LastModifyDate, DateTimeOffset.Now).Where(a => a.UserID == resetPassword.UserID).ExecuteAffrowsAsync();
                return new OperationMessage(ResponseCode.OperationSuccess, "密码修改成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"修改密码操作异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"修改密码失败");
            }
        }

        /// <summary>
        /// 根据用户ID获得用户修改密码数据模型
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<ResultMessage<ResetPasswordDataModel>> GetResetPasswordDataModelAsync(long UserID)
        {
            try
            {
                var dataModel = await _userRepo.Select.Where(a => a.UserID == UserID).ToOneAsync(a => new ResetPasswordDataModel { UserID = a.UserID, UserName = a.UserName });
                return new ResultMessage<ResetPasswordDataModel>(ResponseCode.OperationSuccess, "查询成功", dataModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"根据用户ID获得用户修改密码数据模型异常,异常原因为:【{ex.Message}】");
                return new ResultMessage<ResetPasswordDataModel>(ResponseCode.ServerError, $"根据用户ID获得用户修改密码数据模型异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 根据用户数据主键获得用户基本信息数据模型
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<ResultMessage<BasicInfoDataModel>> GetBasicInfoDataModelAsync(long UserID)
        {
            try
            {
                if (UserID > 0)
                {
                    var DataEntity = await _userRepo.Select.Where(a => a.UserID == UserID).ToOneAsync();
                    if (DataEntity != null)
                    {
                        var DataModel = _mapper.Map<UserInfo, BasicInfoDataModel>(DataEntity);
                        return new ResultMessage<BasicInfoDataModel>(ResponseCode.OperationSuccess, "查询成功", DataModel);
                    }
                }
                return new ResultMessage<BasicInfoDataModel>(ResponseCode.OperationWarning, "未查询到符合条件的用户信息数据");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"根据用户数据主键获得用户基本信息数据模型查询异常,异常原因为:【{ex.Message}】");
                return new ResultMessage<BasicInfoDataModel>(ResponseCode.ServerError, $"根据用户数据主键获得用户基本信息数据模型查询异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public async Task<OperationMessage> UpdateUserBasicInfoDataAsync(BasicInfoDataModel dataModel)
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
                          .Set(a => a.LastModifyBy, _currentUser.UserName)
                          .Set(a => a.LastModifyDate, DateTimeOffset.Now)
                          .Where(a => a.UserID == dataModel.UserID).ExecuteAffrowsAsync();
                    return new OperationMessage(ResponseCode.OperationSuccess, "用户基本信息更新成功");
                }
                else
                {
                    return new OperationMessage(ResponseCode.OperationWarning, "未查询到符合条件的用户信息数据,更新失败");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"更新用户基本信息异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"更新用户基本信息异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 获取用户角色关系数据模型
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public async Task<ResultMessage<UserRoleRelationDataModel>> GetUserRoleRelationDataModelByUserIDAsync(long UserID)
        {
            try
            {
                var userDataModel = await _userRepo.Where(a => a.UserID == UserID && a.DeleteFG == false).ToOneAsync(a => new UserRoleRelationDataModel { UserID = a.UserID, UserName = a.UserName });
                if (userDataModel != null)
                {
                    var roleList = await _userRepo.Orm.Select<UserRoleRelationInfo, RoleInfo>()
                        .InnerJoin((relation, role) => relation.RoleID == role.RoleID && relation.UserID == UserID)
                        .ToListAsync((relation, role) => new LayuiTreeDataModel { TreeID = role.RoleID, NodeName = role.RoleName });
                    if (roleList.Any())
                    {
                        userDataModel.currentAlreadyRoleList = roleList;
                    }
                }
                return new ResultMessage<UserRoleRelationDataModel>(ResponseCode.OperationSuccess, "查询成功", userDataModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取用户角色关系数据模型异常,异常原因为:【{ex.Message}】");
                return new ResultMessage<UserRoleRelationDataModel>(ResponseCode.ServerError, $"获取用户角色关系数据模型异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 为用户配置角色
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public async Task<OperationMessage> GiveUserConfigRoleAsync(UserRoleRelationDataModel dataModel)
        {
            try
            {
                List<UserRoleRelationInfo> newRoleList = new List<UserRoleRelationInfo>();
                if (dataModel != null && dataModel.UserID > 0)
                {

                    var oldRoleList = await _userRoleRelationRepo.Select.Where(a => a.UserID == dataModel.UserID).ToListAsync();
                    //_userRoleRelationRepo
                    foreach (var item in dataModel.currentAlreadyRoleList)
                    {
                        UserRoleRelationInfo userRoleRelationInfo = new UserRoleRelationInfo();
                        userRoleRelationInfo.UserID = dataModel.UserID;
                        userRoleRelationInfo.RoleID = item.TreeID;
                        if (string.IsNullOrEmpty(_currentUser.UserName))
                        {
                            userRoleRelationInfo.CreateBy = "System";
                        }
                        else
                        {
                            userRoleRelationInfo.CreateBy = _currentUser.UserName;
                        }
                        userRoleRelationInfo.CreateDateTime = DateTimeOffset.Now;
                        userRoleRelationInfo.LastModifyBy = "System";
                        userRoleRelationInfo.LastModifyDate = DateTimeOffset.Now;
                        newRoleList.Add(userRoleRelationInfo);
                    }
                    using (var uow = _userRoleRelationRepo.Orm.CreateUnitOfWork())
                    {
                        _userRoleRelationRepo.UnitOfWork = uow;
                        if (newRoleList.Any())
                        {
                            await _userRoleRelationRepo.InsertAsync(newRoleList);
                        }
                        if (oldRoleList.Any())
                        {
                            await _userRoleRelationRepo.DeleteAsync(oldRoleList);
                        }
                        uow.Commit();
                        return new OperationMessage(ResponseCode.OperationSuccess, "用户角色配置成功");
                    }
                }
                else if (dataModel.currentAlreadyRoleList.Any())
                {
                    await _userRoleRelationRepo.Where(a => a.UserID == dataModel.UserID).ToDelete().ExecuteAffrowsAsync();
                    return new OperationMessage(ResponseCode.OperationSuccess, "用户角色配置成功");
                }
                else
                {
                    return new OperationMessage(ResponseCode.OperationWarning, "参数错误");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"为用户【{dataModel.UserName}】配置角色异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"为用户【{dataModel.UserName}】配置角色失败");
            }
        }

        /// <summary>
        /// 获取当前用户已经拥有的角色
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultMessage<List<int>>> GetCurrentUserAlreadyRole(long UserID)
        {
            try
            {
              var roleDataList=await  _userRoleRelationRepo.Where(a=>a.UserID==UserID).ToListAsync(a=>a.RoleID);
                return new ResultMessage<List<int>>(ResponseCode.OperationSuccess, "查询成功", roleDataList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"获取当前用户已经拥有的角色异常,异常原因为:【{ex.Message}】");
                return new ResultMessage<List<int>>(ResponseCode.ServerError, "获取当前用户已经拥有的角色查询失败");
            }
        }
    }
}
