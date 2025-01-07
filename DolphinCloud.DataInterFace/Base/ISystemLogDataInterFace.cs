using DolphinCloud.Common.Result;
using DolphinCloud.DataModel.Base.SystemLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataInterFace.Base
{
    /// <summary>
    /// 系统日志数据接口
    /// </summary>
    public interface ISystemLogDataInterFace
    {
        /// <summary>
        /// 分页搜索系统日志
        /// </summary>
        /// <param name="searchPagination"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PaginationResult<List<SystemLogDataViewModel>>> GetSystemLogPaginationResultAsync(SystemLogSearchPagination searchPagination, CancellationToken cancellationToken);
    }
}
