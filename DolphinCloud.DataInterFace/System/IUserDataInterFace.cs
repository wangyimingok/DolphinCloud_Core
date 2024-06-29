using DolphinCloud.Common.Result;
using DolphinCloud.DataModel.Account;
using DolphinCloud.DataModel.System.User;

namespace DolphinCloud.DataInterFace.System
{
    /// <summary>
    /// 用户数据接口
    /// </summary>
    public interface IUserDataInterFace
    {
        /// <summary>
        /// 生成管理员账号
        /// </summary>
        /// <returns></returns>
        Task<OperationMessage> GenerateAdmin();

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> CreateUser(UserCreateDataModel dataModel);

        /// <summary>
        /// 分页获得用户数据列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<PaginationResult<List<UserDataViewModel>>> GetUserPaginationDataListAsync(UserPagination pagination, CancellationToken cancellationToken);

        /// <summary>
        /// 根据用户数据主键获得用户可修改的数据字段
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Task<ResultMessage<UserModifyDataModel>> GetUserDataModelByUserIDAsync(long UserID);

        /// <summary>
        /// 更新用户信息
        /// 异步方法
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> UpdateUserDataAsync(UserModifyDataModel dataModel);


        /// <summary>
        /// 判断用户名是否已存在
        /// 异步方法
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> UserNameIsExistAsync(string userName);

        /// <summary>
        /// 判断邮箱地址是否已存在
        /// 异步方法
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> EMailAddressIsExistAsync(string emailAddress);

        /// <summary>
        /// 判断手机号码是否已存在
        /// 异步方法
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> MobilePhoneIsExistAsync(string MobilePhone);

        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<ResultMessage<LoginViewModel>> LoginValidateAsync(LoginDataModel dataModel);

        /// <summary>
        /// 逻辑删除用户数据
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> DeleteUserAsync(UserDataViewModel dataModel);


        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="resetPassword"></param>
        /// <returns></returns>
        Task<OperationMessage> ResetPasswordAsync(ResetPasswordDataModel resetPassword);

        /// <summary>
        /// 根据用户数据主键获得用户可修改的数据字段
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Task<ResultMessage<ResetPasswordDataModel>> GetResetPasswordDataModelAsync(long UserID);

        /// <summary>
        /// 根据用户数据主键获得用户基本信息数据模型
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Task<ResultMessage<BasicInfoDataModel>> GetBasicInfoDataModelAsync(long UserID);

        /// <summary>
        /// 更新用户基本信息
        /// 异步方法
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> UpdateUserBasicInfoDataAsync(BasicInfoDataModel dataModel);

        /// <summary>
        /// 获取用户角色关系数据模型
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Task<ResultMessage<UserRoleRelationDataModel>> GetUserRoleRelationDataModelByUserIDAsync(long UserID);

        /// <summary>
        /// 为用户配置角色
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> GiveUserConfigRoleAsync(UserRoleRelationDataModel dataModel);

        /// <summary>
        /// 获得当前用户已经拥有的角色
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        Task<ResultMessage<List<int>>> GetCurrentUserAlreadyRole(long UserID);

    }
}
