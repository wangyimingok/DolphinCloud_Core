using AutoMapper;
using DolphinCloud.DataEntity.Base;
using DolphinCloud.DataInterFace.Base;
using DolphinCloud.DataModel.Base.AudiotLog;
using DolphinCloud.Framework.Session;
using DolphinCloud.Repository.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataServices.Base
{
    /// <summary>
    /// 审计日志数据服务
    /// </summary>
    public class AuditLogDataService : BaseService, IAuditDataInterFace
    {
        /// <summary>
        /// api日志仓储
        /// </summary>
        private readonly AuditRepository _auditRepo;

        /// <summary>
        /// 映射工具
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// 当前用户接口信息
        /// </summary>
        private readonly ICurrentUserInfo _currentUser;

        /// <summary>
        /// 日志记录器
        /// </summary>
        private readonly ILogger<AuditLogDataService> _logger;

        public AuditLogDataService(AuditRepository auditRepo, IMapper mapper, ILogger<AuditLogDataService> logger, ICurrentUserInfo currentUser)
        {
            _auditRepo = auditRepo;
            _mapper = mapper;
            _currentUser = currentUser;
            _logger = logger;
        }

        /// <summary>
        /// 记录审计日志
        /// </summary>
        /// <param name="auditLogs">传入参数 <see cref="AuditLogCreateDataModel"/>类型 审计日志</param>
        /// <returns></returns>
        public async Task AddAuditLogs(AuditLogCreateDataModel auditLogs)
        {
            try
            {
                var LogDataEntity = _mapper.Map<AuditLogInfo>(auditLogs);
                await _auditRepo.InsertAsync(LogDataEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"记录审计日志操作异常,异常原因为:{ex.Message}");
            }
        }
    }
}