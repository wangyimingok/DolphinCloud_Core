using AutoMapper;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.Common.Security;
using DolphinCloud.Common.Snowflake;
using DolphinCloud.DataEntity.System;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.DataModel.System.Menu;
using DolphinCloud.DataModel.System.User;
using DolphinCloud.Repository.System;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public UserDataService(ILogger<UserDataService> logger, IMapper mapper, UserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepo = userRepository;
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
                if (await _userRepo.Where(a => a.EMailAddress == UserData.EMailAddress || a.MobileNumber == UserData.MobileNumber || a.UserName == UserData.UserName).AnyAsync())
                {
                    return new OperationMessage(Common.Enums.ResponseCode.OperationWarning, $"用户【{UserData.UserName}】已存在");
                }
                else
                {
                    UserData.UserID = IdHelper.GetLongId();
                    UserData.CreateBy = "System";
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
                    .Count(out totalDataCount)
                    .Where(a => a.DeleteFG == false)
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
    }
}
