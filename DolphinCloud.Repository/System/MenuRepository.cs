using DolphinCloud.Common.Enums;
using DolphinCloud.DataEntity.System;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Repository.System
{
    /// <summary>
    /// 菜单数据仓储
    /// </summary>
    public class MenuRepository : RepositoryCloud<MenuInfo>, IBaseRepository<MenuInfo>
    {
        public MenuRepository(UnitOfWorkManagerCloud uowm) : base(DbEnum.OMSDataBase, uowm)
        { }
    }
}
