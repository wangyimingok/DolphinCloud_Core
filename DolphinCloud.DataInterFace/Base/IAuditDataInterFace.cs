using DolphinCloud.Common.Result;
using DolphinCloud.DataModel.Base;
using DolphinCloud.DataModel.Base.AudiotLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataInterFace.Base
{
    /// <summary>
    /// 审计日志数据接口
    /// </summary>
    public interface IAuditDataInterFace
    {
        /// <summary>
        /// 记录审计日志
        /// </summary>
        /// <param name="auditLogs">审计日志</param>
        /// <returns></returns>
        Task AddAuditLogs(AuditLogCreateDataModel auditLogs, CancellationToken cancellationToken = default);

        /// <summary>
        /// 分页查询审计日志
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PaginationResult<List<AuditLogDataModel>>> GetAuditLogPaginationResultAsync(AuditLogSearchPagination pagination, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获得审计日志时间类型下拉列表
        /// </summary>
        /// <returns></returns>
        Task<ResultMessage<List<OptionDataModel>>> GetAuditEventTypeSelectOptionAsync(CancellationToken cancellationToken);
    }
}
