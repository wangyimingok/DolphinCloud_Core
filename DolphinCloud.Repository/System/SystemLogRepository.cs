using DolphinCloud.DataEntity.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Repository.System
{
    /// <summary>
    /// 系统日志数据仓储
    /// </summary>
    public class SystemLogRepository: RepositoryCloud<SystemLogInfo>, IBaseRepository<SystemLogInfo>
    {
        public SystemLogRepository(UnitOfWorkManagerCloud uowm) : base(DbEnum.OMSDataBase, uowm)
        {
        }
    }
}
