using AutoMapper;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.DataEntity.System;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.DataModel.Base;
using DolphinCloud.DataModel.System.Role;
using DolphinCloud.Framework.Session;
using DolphinCloud.Repository.System;
using Microsoft.Extensions.Logging;

namespace DolphinCloud.DataServices.System
{
    /// <summary>
    ///角色数据服务
    /// </summary>
    public class RoleDataService : BaseService, IRoleDataInterFace
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private readonly ILogger<RoleDataService> _logger;
        /// <summary>
        /// 映射工具接口
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// 角色数据仓储
        /// </summary>
        private readonly RoleRepository _roleRepo;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        private readonly ICurrentUserInfo _currentUser;
        /// <summary>
        /// 角色权限数据仓储
        /// </summary>
        private readonly RoleAuthorityRepository _roleAuthority;
        public RoleDataService(ILogger<RoleDataService> logger, IMapper mapper, RoleRepository roleRepository, ICurrentUserInfo currentUserInfo, RoleAuthorityRepository roleAuthorityRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _roleRepo = roleRepository;
            _currentUser = currentUserInfo;
            _roleAuthority = roleAuthorityRepository;
        }


        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public async Task<OperationMessage> CreateRole(RoleCreateDataModel dataModel)
        {
            try
            {
                if (await _roleRepo.Where(a => a.RoleName == dataModel.RoleName).AnyAsync())
                {
                    return new OperationMessage(ResponseCode.OperationWarning, $"角色名称【{dataModel.RoleName}】已存在,请更换一个再试");
                }
                var dataEntity = _mapper.Map<RoleInfo>(dataModel);
                if (string.IsNullOrEmpty(_currentUser.UserName))
                {
                    dataEntity.CreateBy = "System";
                }
                else
                {
                    dataEntity.CreateBy = _currentUser.UserName;
                }
                dataEntity.CreateDateTime = DateTimeOffset.Now;
                dataEntity.LastModifyBy = "System";
                dataEntity.LastModifyDate = DateTimeOffset.Now;
                await _roleRepo.InsertAsync(dataEntity);
                return new OperationMessage(ResponseCode.OperationSuccess, $"增加角色【{dataModel.RoleName}】成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"增加角色【{dataModel.RoleName}】操作失败,失败原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"增加角色【{dataModel.RoleName}】失败");
            }
        }

        /// <summary>
        /// 逻辑删除角色数据
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public async Task<OperationMessage> DeleteRoleAsync(RoleDataViewModel dataModel)
        {
            try
            {
                var result = await _roleRepo.Where(m => m.RoleID == dataModel.RoleID).ToUpdate()
                    .Set(a => a.LastModifyBy, _currentUser.UserName)
                    .Set(a => a.LastModifyDate, DateTimeOffset.Now)
                    .Set(a => a.DeleteFG, true)
                    .ExecuteAffrowsAsync();
                return new OperationMessage(ResponseCode.OperationSuccess, $"删除角色【{dataModel.RoleName}】成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"删除角色【{dataModel.RoleName}】操作异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"删除角色【{dataModel.RoleName}】失败");
            }
        }

        /// <summary>
        /// 根据角色数据主键获取角色授权数据模型
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public async Task<ResultMessage<RoleAuthorDataModel>> GetRoleAuthorDataModelAsync(int RoleID)
        {
            try
            {
                var DataEntity = await _roleRepo.Where(m => m.RoleID == RoleID).ToOneAsync();
                var result = _mapper.Map<RoleAuthorDataModel>(DataEntity);
                return new ResultMessage<RoleAuthorDataModel>(ResponseCode.OperationSuccess, "获取角色授权数据模型成功", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"根据角色ID获取角色授权数据模型异常,异常原因为:【{ex.Message}】");
                return new ResultMessage<RoleAuthorDataModel>(ResponseCode.ServerError, "获取角色授权数据模型失败");
            }
        }

        /// <summary>
        /// 根据角色ID获取角色信息
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public async Task<ResultMessage<RoleModifyDataModel>> GetRoleDataModelByRoleIDAsync(int RoleID)
        {
            try
            {
                var DataEntity = await _roleRepo.Where(m => m.RoleID == RoleID).ToOneAsync();
                var result = _mapper.Map<RoleModifyDataModel>(DataEntity);
                return new ResultMessage<RoleModifyDataModel>(ResponseCode.OperationSuccess, "获取角色详细信息成功", result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"根据角色ID获取角色详细信息异常,异常原因为:【{ex.Message}】");
                return new ResultMessage<RoleModifyDataModel>(ResponseCode.ServerError, "获取角色详细信息失败");
            }
        }

        /// <summary>
        /// 分页获取角色信息
        /// 用于角色列表展示
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<PaginationResult<List<RoleDataViewModel>>> GetRoleDataPaginationAsync(RolePagination pagination)
        {
            try
            {
                long totalDataCount = 0;
                var query = await _roleRepo.Where(m => m.DeleteFG == false)
                    .WhereIf(!string.IsNullOrEmpty(pagination.SearchKey), a => a.RoleName.Contains(pagination.SearchKey) || a.RoleDescription.Contains(pagination.SearchKey) || a.Remarks.Contains(pagination.SearchKey))
                    .Count(out totalDataCount)
                    .Page(pagination.PageIndex, pagination.PageSize)
                    .ToListAsync();
                var dataList = _mapper.Map<List<RoleDataViewModel>>(query);
                return new PaginationResult<List<RoleDataViewModel>>(ResponseCode.OperationSuccess, "获取角色数据成功", totalDataCount, dataList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"分页查询角色数据异常,异常原因为:【{ex.Message}】");
                return new PaginationResult<List<RoleDataViewModel>>(ResponseCode.ServerError, "获取角色数据失败", 0, null);
            }
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public async Task<OperationMessage> UpdateRoleDataAsync(RoleModifyDataModel dataModel)
        {
            try
            {
                var CurrentDataEntity = await _roleRepo.Select.Where(a => a.RoleID == dataModel.RoleID).ToOneAsync();
                if (CurrentDataEntity != null)
                {
                    var result = await _roleRepo.UpdateDiy.SetIf(dataModel.RoleName != CurrentDataEntity.RoleName, a => a.RoleName, dataModel.RoleName)
                          .SetIf(dataModel.RoleDescription != CurrentDataEntity.RoleDescription, a => a.RoleDescription, dataModel.RoleDescription)
                          .SetIf(dataModel.Remarks != CurrentDataEntity.Remarks, a => a.Remarks, dataModel.Remarks)
                          .Set(a => a.LastModifyBy, _currentUser.UserName)
                          .Set(a => a.LastModifyDate, DateTimeOffset.Now)
                          .Where(a => a.RoleID == dataModel.RoleID).ExecuteAffrowsAsync();
                    return new OperationMessage(ResponseCode.OperationSuccess, "角色信息更新成功");
                }
                else
                {
                    return new OperationMessage(ResponseCode.OperationWarning, "未查询到符合条件的角色信息数据,更新失败");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"更新角色信息异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"更新角色信息异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 为角色授权
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public async Task<OperationMessage> ConfigPermissionByRoleAsync(RoleAuthorDataModel dataModel)
        {
            try
            {
                List<RoleAuthorityDataModel> roleAuthorityDataModels = new List<RoleAuthorityDataModel>();
                //var oldPermissionDataEntityList = await _roleAuthority.Where(a => a.RoleID == dataModel.RoleID).ToListAsync();
                //var oldPermissionDataList = _mapper.Map<List<RoleAuthorityDataModel>>(dataEntityList);
                var newPermissionDataList = GetAuthorDataList(dataModel.RoleID, dataModel.Permissions, roleAuthorityDataModels);
                var newDataEntityList = _mapper.Map<List<RoleAuthorityInfo>>(newPermissionDataList);
                foreach (var item in newDataEntityList)
                {
                    if (string.IsNullOrEmpty(_currentUser.UserName))
                    {
                        item.CreateBy = "System";
                    }
                    else
                    {
                        item.CreateBy = _currentUser.UserName;
                    }
                    item.CreateDateTime = DateTimeOffset.Now;
                    item.LastModifyBy = "System";
                    item.LastModifyDate = DateTimeOffset.Now;
                }
                //var deleteList = oldPermissionDataList.Except(newPermissionDataList).ToList();
                //var addList = newPermissionDataList.Except(oldPermissionDataList).ToList();
                var deleDataCount = await _roleAuthority.DeleteAsync(a => a.RoleID == dataModel.RoleID);
                _logger.LogWarning($"删除旧权限数据【{deleDataCount}】条");
                var createDataList = await _roleAuthority.InsertAsync(newDataEntityList);
                _logger.LogWarning($"新增权限数据【{createDataList.Count}】条");
                return new OperationMessage(ResponseCode.OperationSuccess, $"授权成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"为角色【{dataModel.RoleName}】配置权限异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"授权失败");
            }
        }

        /// <summary>
        /// 树形结构转换为平行结构
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="Permission"></param>
        /// <param name="dataList"></param>
        /// <returns></returns>
        private List<RoleAuthorityDataModel> GetAuthorDataList(int RoleID, List<LayuiTreeDataModel> Permission, List<RoleAuthorityDataModel> dataList)
        {
            foreach (var item in Permission)
            {
                RoleAuthorityDataModel dataModel = new RoleAuthorityDataModel();
                dataModel.PermissionID = item.TreeID;
                dataModel.RoleID = RoleID;
                dataList.Add(dataModel);
                if (item.Children.Any())
                {
                    GetAuthorDataList(RoleID, item.Children, dataList);
                }
            }
            return dataList;
        }
    }
}
