using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DolphinCloud.Common.Pagination;
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
        /// <summary>
        /// 初始化菜单数据
        /// </summary>
        /// <returns></returns>
       // Task InitMenuData(Type baseController);

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
    }
}
