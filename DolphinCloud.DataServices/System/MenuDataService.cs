using AutoMapper;
using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.DataEntity.System;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.DataModel.Base;
using DolphinCloud.DataModel.System.Menu;
using DolphinCloud.Framework.Session;
using DolphinCloud.Repository.System;
using Microsoft.Extensions.Logging;
using System.Reflection;
using static FreeSql.Internal.GlobalFilter;

namespace DolphinCloud.DataServices.System
{
    /// <summary>
    /// 菜单数据服务
    /// </summary>
    public class MenuDataService : BaseService, IMenuDataInterFace
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private readonly ILogger<MenuDataService> _logger;
        /// <summary>
        /// 映射工具接口
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// 菜单数据仓储
        /// </summary>
        private readonly MenuRepository _munuRepo;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        private readonly ICurrentUserInfo _currentUser;
        public MenuDataService(ILogger<MenuDataService> logger, IMapper mapper, MenuRepository menuRepository, ICurrentUserInfo currentUserInfo)
        {
            _logger = logger;
            _mapper = mapper;
            _munuRepo = menuRepository;
            _currentUser = currentUserInfo;
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public async Task<OperationMessage> CreateMenu(MenuCreateDataModel dataModel)
        {
            try
            {
                if (await _munuRepo.Select.Where(a => a.MenuName == dataModel.MenuName).AnyAsync())
                {
                    return new OperationMessage(ResponseCode.OperationWarning, "菜单名称已存在,请更换一个名称后再试!");
                }
                var menuData = _mapper.Map<MenuCreateDataModel, MenuInfo>(dataModel);
                menuData.CreateBy = string.IsNullOrWhiteSpace(_currentUser.UserName) ? "System" : _currentUser.UserName;
                menuData.CreateDateTime = DateTimeOffset.Now;
                menuData.LastModifyBy = string.IsNullOrWhiteSpace(_currentUser.UserName) ? "System" : _currentUser.UserName; ;
                menuData.LastModifyDate = DateTimeOffset.Now;
                //menuData.
                await _munuRepo.InsertAsync(menuData);
                return new OperationMessage(ResponseCode.OperationSuccess, "增加菜单成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"创建菜单异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"创建菜单异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 逻辑删除菜单
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public async Task<OperationMessage> DeleteMenuAsync(MenuDataViewModel dataModel)
        {
            try
            {
                var CurrentDataEntity = await _munuRepo.Select.Where(a => a.MenuID == dataModel.MenuID)
                    .ToUpdate()
                    .Set(a => a.DeleteFG, true)
                    .Set(a => a.LastModifyBy, _currentUser.UserName)
                    .Set(a => a.LastModifyDate, DateTimeOffset.Now)
                    .ExecuteAffrowsAsync();
                if (CurrentDataEntity > 0)
                {
                    return new OperationMessage(ResponseCode.OperationSuccess, "删除菜单成功");
                }
                else
                {
                    return new OperationMessage(ResponseCode.OperationWarning, "未查询到符合条件的菜单信息数据,删除失败");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"逻辑删除菜单异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"逻辑删除菜单异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 获得上级菜单下拉框选项
        /// </summary>
        /// <returns></returns>
        public async Task<ResultMessage<List<OptionDataModel>>> GetMenuSelectOptionAsync()
        {
            try
            {
                var dataList = await _munuRepo.Select.Where(a => a.MenuType <= 2 && a.DeleteFG == false).ToListAsync(a => new OptionDataModel { OptionName = a.MenuName, OptionValue = a.MenuID.ToString() });
                return new ResultMessage<List<OptionDataModel>>(ResponseCode.OperationSuccess, "获取上级菜单下拉框选项成功", dataList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取上级菜单下拉框选项异常,异常原因为:【{ex.Message}】");
                return new ResultMessage<List<OptionDataModel>>(ResponseCode.ServerError, $"获取上级菜单下拉框选项异常,异常原因为:【{ex.Message}】", null);
            }
        }

        /// <summary>
        /// 分页获得菜单列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<PaginationResult<List<MenuDataViewModel>>> GetMenuTableAsync(MenuParameter pagination)
        {
            try
            {
                long totalDataCount = 0;
                var MenuList = await _munuRepo.Select
                    .Page(pagination.PageIndex, pagination.PageSize)
                    .Where(a => a.MenuType >= 2 && a.DeleteFG == false)
                    .Count(out totalDataCount)
                    .ToListAsync();
                var DataModel = _mapper.Map<List<MenuInfo>, List<MenuDataViewModel>>(MenuList);
                return new PaginationResult<List<MenuDataViewModel>>(ResponseCode.OperationSuccess, "查询成功", totalDataCount, DataModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"分页查询菜单列表异常,异常原因为:【{ex.Message}】");
                return new PaginationResult<List<MenuDataViewModel>>(ResponseCode.ServerError, $"分页查询菜单列表异常,异常原因为:【{ex.Message}】", 0, null);
            }
        }

        /// <summary>
        /// 获得导航栏数据模型
        /// </summary>
        /// <returns></returns>
        public async Task<List<SideBarNavDataModel>> GetSideBarNavDataModelsAsync(string AreaName = "")
        {
            try
            {
                if (_currentUser.UserID > 0)
                {
                    var menuIdList = await _munuRepo.Orm.Select<RoleAuthorityInfo, UserRoleRelationInfo>()
                          .InnerJoin((roleAuthority, userRoleRelation) => userRoleRelation.RoleID == roleAuthority.RoleID && userRoleRelation.UserID == _currentUser.UserID)
                          .ToListAsync((roleAuthority, userRoleRelation) => roleAuthority.MenuID);
                    var currentMenuList = _munuRepo.Select.Where(a => menuIdList.Contains(a.MenuID)).ToTreeListAsync();
                    var DataModel = _mapper.Map<List<MenuInfo>, List<SideBarNavDataModel>>(currentMenuList.Result);
                    return DataModel;
                }
                else
                {
                    var MenuList = await _munuRepo.Select
                   .Where(a => a.MenuType <= 2 && a.DeleteFG == false)
                   .WhereIf(!string.IsNullOrWhiteSpace(AreaName), a => a.AreaName == AreaName)
                   .ToTreeListAsync();
                    var DataModel = _mapper.Map<List<MenuInfo>, List<SideBarNavDataModel>>(MenuList);
                    return DataModel;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获得导航栏数据模型异常,异常原因为:【{ex.Message}】");
                throw;
            }
        }

        /// <summary>
        /// 根据菜单数据主键获得菜单信息
        /// 用于更新菜单信息
        /// </summary>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        public async Task<ResultMessage<MenuModifyDataModel>> GetMenuDataModelByMenuIDAsync(int MenuID)
        {
            try
            {
                if (MenuID > 0)
                {
                    var DataEntity = await _munuRepo.Select.Where(a => a.MenuID == MenuID).ToOneAsync();
                    if (DataEntity != null)
                    {
                        var DataModel = _mapper.Map<MenuInfo, MenuModifyDataModel>(DataEntity);
                        return new ResultMessage<MenuModifyDataModel>(ResponseCode.OperationSuccess, "查询成功", DataModel);
                    }
                }
                return new ResultMessage<MenuModifyDataModel>(ResponseCode.OperationWarning, "未查询到符合条件的菜单信息数据");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"根据菜单ID获得菜单信息查询异常,异常原因为:【{ex.Message}】");
                return new ResultMessage<MenuModifyDataModel>(ResponseCode.ServerError, $"根据菜单ID获得菜单信息查询异常,异常原因为:【{ex.Message}】");
            }
        }
        /// <summary>
        /// 更新菜单信息
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        public async Task<OperationMessage> UpdateMenuDataAsync(MenuModifyDataModel dataModel)
        {
            try
            {
                var CurrentDataEntity = await _munuRepo.Select.Where(a => a.MenuID == dataModel.MenuID).ToOneAsync();
                if (CurrentDataEntity != null)
                {
                    var result = await _munuRepo.UpdateDiy.SetIf(dataModel.MenuName != CurrentDataEntity.MenuName, a => a.MenuName, dataModel.MenuName)
                          .SetIf(dataModel.MenuIcon != CurrentDataEntity.MenuIcon, a => a.MenuIcon, dataModel.MenuIcon)
                          .SetIf(dataModel.MenuUrlAddress != CurrentDataEntity.MenuUrlAddress, a => a.MenuUrlAddress, dataModel.MenuUrlAddress)
                          .SetIf(dataModel.SortNumber != CurrentDataEntity.SortNumber, a => a.SortNumber, dataModel.SortNumber)
                          .SetIf(dataModel.MenuType != CurrentDataEntity.MenuType, a => a.MenuType, dataModel.MenuType)
                          .SetIf(dataModel.ParentID != null && dataModel.ParentID != CurrentDataEntity.ParentID, a => a.ParentID, dataModel.ParentID)
                          .SetIf(dataModel.ControllerName != CurrentDataEntity.ControllerName, a => a.ControllerName, dataModel.ControllerName)
                          .SetIf(dataModel.AreaName != CurrentDataEntity.AreaName, a => a.AreaName, dataModel.AreaName)
                          .SetIf(dataModel.ActionName != CurrentDataEntity.ActionName, a => a.ActionName, dataModel.ActionName)
                          .Set(a => a.LastModifyBy, _currentUser.UserName)
                          .Set(a => a.LastModifyDate, DateTimeOffset.Now)
                          .Where(a => a.MenuID == dataModel.MenuID).ExecuteAffrowsAsync();
                    return new OperationMessage(ResponseCode.OperationSuccess, "菜单信息更新成功");
                }
                else
                {
                    return new OperationMessage(ResponseCode.OperationWarning, "未查询到符合条件的菜单信息数据,更新失败");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"更新菜单信息异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(ResponseCode.ServerError, $"更新菜单信息异常,异常原因为:【{ex.Message}】");
            }
        }

        /// <summary>
        /// 获得权限树控件数据
        /// </summary>
        /// <returns></returns>
        public async Task<ResultMessage<List<LayuiTreeDataModel>>> GetPermissionTreeData()
        {
            try
            {
                var DataEntityList = await _munuRepo.Select.Where(a => a.DeleteFG == false).ToTreeListAsync();
                var DataModel = _mapper.Map<List<MenuInfo>, List<LayuiTreeDataModel>>(DataEntityList);
                return new ResultMessage<List<LayuiTreeDataModel>>(ResponseCode.OperationSuccess, "查询成功", DataModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获得树控件数据异常,异常原因为：【{ex.Message}】");
                return new ResultMessage<List<LayuiTreeDataModel>>(ResponseCode.ServerError, $"获得树控件数据失败", null);
            }
        }
        public async Task InitMenuData(IEnumerable<Type> assemblyList)
        {
            try
            {
                List<MenuInfo> menuList = new List<MenuInfo>();
                foreach (var controller in assemblyList)
                {
                    var methodList = controller.GetMethods().Where(a => a.IsPublic == true).Where(m => m.Module.Name.StartsWith("DolphinCloud.OMS.WebApplication")).ToList();
                    foreach (var currentMethod in methodList)
                    {
                        foreach (Attribute attribute in currentMethod.GetCustomAttributes())
                        {
                            if (attribute is MenuAttribute menu)
                            {

                                MenuInfo menuInfo = new MenuInfo();
                                menuInfo.AreaName = menu.DomainName;
                                menuInfo.MenuName = menu.DisplayName;
                                menuInfo.ControllerName = controller.Name.Replace("Controller", "");
                                menuInfo.ActionName = currentMethod.Name;
                                menuInfo.SortNumber = menu.SortNumber == 0 ? (short)0 : (short)menu.SortNumber;
                                menuInfo.MenuType = (short)menu.MenuType;
                                //menuInfo.ParentID = menu.ParentID;
                                if (string.IsNullOrWhiteSpace(menu.DomainName))
                                {
                                    menuInfo.MenuUrlAddress = $"{menuInfo.ControllerName}/{menuInfo.ActionName}";
                                }
                                else
                                {
                                    menuInfo.MenuUrlAddress = $"{menuInfo.AreaName}/{menuInfo.ControllerName}/{menuInfo.ActionName}";
                                }
                                menuInfo.CreateBy = "System";
                                menuInfo.CreateDateTime = DateTimeOffset.Now;
                                menuInfo.LastModifyBy = "System";
                                menuInfo.LastModifyDate = DateTimeOffset.Now;
                                menuList.Add(menuInfo);
                            }
                        }
                    }
                }
                var MenuListData = await _munuRepo.Where(a => a.DeleteFG == false).ToListAsync();
                foreach (var item in menuList)
                {
                    //如果菜单项存在
                    if (MenuListData.Where(a => a.ActionName == item.AreaName && a.ControllerName == item.ControllerName && a.ActionName == item.ActionName && a.MenuUrlAddress == item.MenuUrlAddress).Any())
                    {
                        _logger.LogWarning($"菜单项【AreaName:{item.AreaName},ControllerName:{item.ControllerName},ActionName:{item.ActionName},UrlAddress:{item.MenuUrlAddress}】已存在");
                    }
                    else
                    {
                        //顶级菜单没有上级菜单ID
                        if (item.MenuType == (short)MunuType.RootMenu)
                        {
                            item.ParentID = 0;
                            item.MenuUrlAddress = "#";
                        }
                        await _munuRepo.InsertAsync(item);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"利用反射技术自动增加菜单异常,异常原因为:【{ex.Message}】");
            }
        }
    }
}
