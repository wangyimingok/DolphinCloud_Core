using DolphinCloud.Common.Result;
using DolphinCloud.DataModel.Base;
using DolphinCloud.DataModel.System.Role;

namespace DolphinCloud.DataInterFace.System
{
    /// <summary>
    /// 角色数据接口
    /// </summary>
    public interface IRoleDataInterFace
    {
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> CreateRole(RoleCreateDataModel dataModel);

        /// <summary>
        /// 角色分页查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<PaginationResult<List<RoleDataViewModel>>> GetRoleDataPaginationAsync(RolePagination pagination);

        /// <summary>
        /// 根据角色数据主键获得角色信息
        /// 用于更新角色信息
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        Task<ResultMessage<RoleModifyDataModel>> GetRoleDataModelByRoleIDAsync(int RoleID);

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> UpdateRoleDataAsync(RoleModifyDataModel dataModel);

        /// <summary>
        /// 逻辑删除角色数据
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> DeleteRoleAsync(RoleDataViewModel dataModel);

        /// <summary>
        /// 根据角色ID获取角色授权数据模型
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        Task<ResultMessage<RoleAuthorDataModel>> GetRoleAuthorDataModelAsync(int RoleID);

        /// <summary>
        /// 对角色进行授权
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> ConfigPermissionByRoleAsync(RoleAuthorDataModel dataModel);

        /// <summary>
        /// 权限校验
        /// </summary>
        /// <param name="ControllerName">传入参数 <see cref="string"/>类型 请求访问的控制器名称</param>
        /// <param name="ActionName">传入参数 <see cref="string"/>类型 请求访问的Action名称</param>
        /// <param name="requestUrlAddress">传入参数 <see cref="string"/>类型 请求访问的URL地址</param>
        /// <returns>返回值 <see cref="bool"/>类型 true:有权限访问 false:无权限访问</returns>
        Task<bool> CheckPermissionAsync(string ControllerName, string ActionName, string requestUrlAddress = null);

        /// <summary>
        /// 获得角色列表
        /// </summary>
        /// <returns></returns>
        Task<ResultMessage<List<LayuiTreeDataModel>>> GetRoleListAsync();

        /// <summary>
        /// 获得当前角色已经拥有的权限
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        Task<ResultMessage<List<int>>> GetCurrentRoleAlreadyPermissionAsync(int RoleID);
    }
}
