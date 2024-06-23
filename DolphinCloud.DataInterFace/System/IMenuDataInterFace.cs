using DolphinCloud.Common.Result;
using DolphinCloud.DataModel.Base;
using DolphinCloud.DataModel.System.Menu;

namespace DolphinCloud.DataInterFace.System
{
    /// <summary>
    /// 菜单数据接口
    /// </summary>
    public interface IMenuDataInterFace
    {

        Task InitMenuData(IEnumerable<Type> assembly);

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> CreateMenu(MenuCreateDataModel dataModel);

        Task<PaginationResult<List<MenuDataViewModel>>> GetMenuTableAsync(MenuParameter pagination);

        /// <summary>
        /// 获得上级菜单下拉框选项
        /// </summary>
        /// <returns></returns>
        Task<ResultMessage<List<OptionDataModel>>> GetMenuSelectOptionAsync();

        /// <summary>
        /// 根据菜单数据主键获得菜单信息
        /// 用于更新菜单信息
        /// </summary>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        Task<ResultMessage<MenuModifyDataModel>> GetMenuDataModelByMenuIDAsync(int MenuID);
        /// <summary>
        /// 更新菜单信息
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> UpdateMenuDataAsync(MenuModifyDataModel dataModel);
        /// <summary>
        /// 获得导航栏数据模型
        /// </summary>
        /// <returns></returns>
        Task<List<SideBarNavDataModel>> GetSideBarNavDataModelsAsync(string AreaName = "");

        /// <summary>
        /// 逻辑删除菜单数据
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        Task<OperationMessage> DeleteMenuAsync(MenuDataViewModel dataModel);

        /// <summary>
        /// 获得权限树控件数据
        /// </summary>
        /// <returns></returns>
        Task<ResultMessage<List<LayuiTreeDataModel>>> GetPermissionTreeData();

        
    }
}
