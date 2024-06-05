using DolphinCloud.DataEntity.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Repository.System
{
    /// <summary>
    /// 角色数据仓储
    /// </summary>
    public class RoleRepository : RepositoryCloud<RoleInfo>, IBaseRepository<RoleInfo>
    {
        public RoleRepository(UnitOfWorkManagerCloud uowm) : base(DbEnum.OMSDataBase, uowm)
        {
        }
    }
}
