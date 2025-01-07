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
        Task AddAuditLogs(AuditLogCreateDataModel auditLogs);


    }
}
