using DolphinCloud.DataEntity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Repository.Base
{
    /// <summary>
    /// 审计日志仓储
    /// </summary>
    public class AuditRepository : RepositoryCloud<AuditLogInfo>, IBaseRepository<AuditLogInfo>
    {
        public AuditRepository(UnitOfWorkManagerCloud uowm) : base(DbEnum.OMSDataBase, uowm)
        {
        }
    }
}
