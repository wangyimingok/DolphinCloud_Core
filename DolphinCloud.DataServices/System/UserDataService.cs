using AutoMapper;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.Common.Security;
using DolphinCloud.Common.Snowflake;
using DolphinCloud.DataEntity.System;
using DolphinCloud.DataInterFace.System;
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
        public async Task GenerateAdmin()
        {
            try
            {
                UserInfo user = new UserInfo();
                user.UserName = "admin";
                user.RealName = "超级管理员";
                user.UserID = IdHelper.GetLongId();
                user.EMailAddress = "admin@admin.com";
                user.MobileNumber = "18588409774";
                user.PassWord = SecurityUtil.MD5Encrypt(SecurityUtil.Base64Encode("admin123"));
                user.CreateBy = "System";
                user.CreateDateTime = DateTimeOffset.Now;
                user.LastModifyBy = "System";
                user.LastModifyDate = DateTimeOffset.Now;
                user.Status = 1;
                await _userRepo.InsertAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"生成管理员账号异常,异常原因为:【{ex.Message}】");
            }
        }
    }
}
