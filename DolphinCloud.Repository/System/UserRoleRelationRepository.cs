using DolphinCloud.DataEntity.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Repository.System
{
    /// <summary>
    /// 用户角色关系数据仓储
    /// </summary>
    public class UserRoleRelationRepository : RepositoryCloud<UserRoleRelationInfo>, IBaseRepository<UserRoleRelationInfo>
    {
        public UserRoleRelationRepository(UnitOfWorkManagerCloud uowm) : base(DbEnum.OMSDataBase, uowm)
        { }

    }
}
