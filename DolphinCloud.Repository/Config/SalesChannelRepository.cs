using DolphinCloud.DataEntity.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Repository.Config
{
    /// <summary>
    /// 销售渠道数据仓储
    /// </summary>
    public class SalesChannelRepository : RepositoryCloud<SalesChannelInfo>, IBaseRepository<SalesChannelInfo>
    {
        public SalesChannelRepository(UnitOfWorkManagerCloud uowm) : base(DbEnum.OMSDataBase, uowm)
        {
        }
    }
}
