using DolphinCloud.DataEntity.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Repository.System
{
    /// <summary>
    /// 角色权限关系数据仓储
    /// </summary>
    public class RoleAuthorityRepository: RepositoryCloud<RoleAuthorityInfo>, IBaseRepository<RoleAuthorityInfo>
    {
        public RoleAuthorityRepository(UnitOfWorkManagerCloud uowm) : base(DbEnum.OMSDataBase, uowm)
        { }
    }
}
