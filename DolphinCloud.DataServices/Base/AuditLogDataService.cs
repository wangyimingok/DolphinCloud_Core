using AutoMapper;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.DataEntity.Base;
using DolphinCloud.DataInterFace.Base;
using DolphinCloud.DataModel.Base;
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
        public async Task AddAuditLogs(AuditLogCreateDataModel auditLogs, CancellationToken cancellationToken = default)
        {
            try
            {
                var LogDataEntity = _mapper.Map<AuditLogInfo>(auditLogs);
                await _auditRepo.InsertAsync(LogDataEntity, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"记录审计日志操作异常,异常原因为:{ex.Message}");
            }
        }

        /// <summary>
        /// 获得审计日志时间类型下拉列表
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultMessage<List<OptionDataModel>>> GetAuditEventTypeSelectOptionAsync(CancellationToken cancellationToken)
        {
            try
            {
                var dataList = await _auditRepo.Select.Distinct().ToListAsync(a => a.EventType);
                List<OptionDataModel> optionList = new List<OptionDataModel>();
                foreach (var item in dataList)
                {
                    OptionDataModel optionData = new OptionDataModel(item, item);
                    optionList.Add(optionData);
                }
                return new ResultMessage<List<OptionDataModel>>( ResponseCode.OperationSuccess,"", optionList); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"获取审计日志事件类型下拉选项异常,异常原因为:【{ex.Message}】");
                return new ResultMessage<List<OptionDataModel>>( ResponseCode.ServerError, "获取审计日志事件类型下拉选项异常");
            }
        }

        /// <summary>
        /// 分页查询审计日志
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PaginationResult<List<AuditLogDataModel>>> GetAuditLogPaginationResultAsync(AuditLogSearchPagination pagination, CancellationToken cancellationToken = default)
        {
            try
            {
                long DataTotalCount = 0;
                var logList = await _auditRepo.Select.WhereIf(!string.IsNullOrWhiteSpace(pagination.UserName), a => a.UserName.Contains(pagination.UserName))
                      .WhereIf(!string.IsNullOrWhiteSpace(pagination.MethodName), a => a.MethodName.Contains(pagination.MethodName))
                      .WhereIf(!string.IsNullOrWhiteSpace(pagination.EventType), a => a.EventType.Contains(pagination.EventType))
                      .WhereIf(!string.IsNullOrWhiteSpace(pagination.UserID), a => a.UserID.Contains(pagination.UserID))
                      .OrderByDescending(a => a.ExecutionTime)
                      .Count(out DataTotalCount)
                      .Page(pagination.PageIndex, pagination.PageSize)
                      .ToListAsync(cancellationToken);
                var dataModelList = _mapper.Map<List<AuditLogDataModel>>(logList);
                return new PaginationResult<List<AuditLogDataModel>>(ResponseCode.OperationSuccess, "查询成功", DataTotalCount, dataModelList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"分页查询审计日志异常,异常原因为:【{ex.Message}】");
                return new PaginationResult<List<AuditLogDataModel>>(ResponseCode.ServerError, "分页查询审计日志异常", 0, null);
            }
        }
    }
}