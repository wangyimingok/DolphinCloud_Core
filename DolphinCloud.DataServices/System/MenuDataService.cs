using AutoMapper;
using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Pagination;
using DolphinCloud.Common.Result;
using DolphinCloud.DataEntity.System;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.DataModel.System.Menu;
using DolphinCloud.Repository.System;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;

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
        public MenuDataService(ILogger<MenuDataService> logger, IMapper mapper, MenuRepository menuRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _munuRepo = menuRepository;
        }

        public async Task<OperationMessage> CreateMenu(MenuCreateDataModel dataModel)
        {
            try
            {
                var menuData = _mapper.Map<MenuCreateDataModel, MenuInfo>(dataModel);
                menuData.CreateBy = "System";
                menuData.CreateDateTime = DateTimeOffset.Now;
                menuData.LastModifyBy = "System";
                menuData.LastModifyDate = DateTimeOffset.Now;
                //menuData.
                await _munuRepo.InsertAsync(menuData);
                return new OperationMessage(Common.Enums.ResponseCode.OperationSuccess, "增加菜单成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"创建菜单异常,异常原因为:【{ex.Message}】");
                return new OperationMessage(Common.Enums.ResponseCode.ServerError, $"创建菜单异常,异常原因为:【{ex.Message}】");
            }
        }

        public async Task<PaginationResult<List<MenuDataViewModel>>> GetMenuTableAsync(BasePagination pagination)
        {
            try
            {
                var MenuList = await _munuRepo.Select.Page(pagination.PageIndex, pagination.PageSize).Where(a => a.DeleteFG == false).ToListAsync();
                var DataModel = _mapper.Map<List<MenuInfo>, List<MenuDataViewModel>>(MenuList);

            }
            catch (Exception ex)
            {

                throw;
            }
            throw new NotImplementedException();
        }

        public Task InitMenuData(Type baseController)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }

            throw new NotImplementedException();
        }

        public async Task InitMenuData(IEnumerable<Type> assemblyList)
        {
            try
            {
                List<MenuInfo> menuList = new List<MenuInfo>();

                //var ControllerList= assemblyList.Where(t=>t.GetCustomAttributes<MenuAttribute>().Any());
                foreach (var controller in assemblyList)
                {
                    var MethodsList = controller.GetMethods().Where(a => a.GetCustomAttributes<MenuAttribute>().Any());
                    foreach (var method in MethodsList)
                    {
                        MenuInfo menu = new MenuInfo();
                        var menuAttribute = method.GetCustomAttribute<MenuAttribute>();
                        menu.AreaName = menuAttribute.DomainName;
                        menu.MenuName = menuAttribute.DisplayName;
                        menu.ControllerName = controller.Name.Replace("Controller", "");
                        menu.ActionName = method.Name;
                        menu.SortNumber = menuAttribute.SortNumber == 0 ? (short)0 : (short)menuAttribute.SortNumber;
                        menu.MenuType = (short)menuAttribute.MenuType;
                        // menu.ParentID = menuAttribute.ParentID;
                        if (string.IsNullOrWhiteSpace(menuAttribute.DomainName))
                        {
                            menu.MenuUrlAddress = $"{menu.ControllerName}/{menu.ActionName}";
                        }
                        else
                        {
                            menu.MenuUrlAddress = $"{menu.AreaName}/{menu.ControllerName}/{menu.ActionName}";
                        }
                        menu.CreateBy = "System";
                        menu.CreateDateTime = DateTimeOffset.Now;
                        menu.LastModifyBy = "System";
                        menu.LastModifyDate = DateTimeOffset.Now;
                        menuList.Add(menu);
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

                throw;
            }
            throw new NotImplementedException();
        }
    }
}
