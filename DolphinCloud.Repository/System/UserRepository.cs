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
    /// 用户数据仓储
    /// </summary>
    public class UserRepository: RepositoryCloud<UserInfo>, IBaseRepository<UserInfo>
    {
        public UserRepository(UnitOfWorkManagerCloud uowm) : base(DbEnum.OMSDataBase, uowm)
        {
        }
    }
}
