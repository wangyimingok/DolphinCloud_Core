using AutoMapper;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.DataInterFace.Base;
using DolphinCloud.DataModel.Base.SystemLog;
using DolphinCloud.Repository.System;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataServices.Base
{
    /// <summary>
    /// 系统日志数据服务
    /// </summary>
    public class SystemLogDataService : BaseService, ISystemLogDataInterFace
    {
        /// <summary>
        /// 日志记录接口
        /// </summary>
        private readonly ILogger<SystemLogDataService> _logger;
        /// <summary>
        /// 系统日志数据仓储
        /// </summary>
        private readonly SystemLogRepository _logRepo;

        private readonly IMapper _mapper;
        public SystemLogDataService(ILogger<SystemLogDataService> logger, SystemLogRepository systemLogRepository, IMapper mapper)
        {
            _logger = logger;
            _logRepo = systemLogRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 分页查询系统日志
        /// </summary>
        /// <param name="searchPagination"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaginationResult<List<SystemLogDataViewModel>>> GetSystemLogPaginationResultAsync(SystemLogSearchPagination searchPagination, CancellationToken cancellationToken)
        {
            try
            {
                long DataTotalCount = 0;
                var dataList = await _logRepo.Select//.Where(a => a.DeleteFG == false)
                   .WhereIf(!string.IsNullOrWhiteSpace(searchPagination.Level), a => a.Level == searchPagination.Level)
                   .WhereIf(!string.IsNullOrWhiteSpace(searchPagination.Message), a => a.Message.Contains(searchPagination.Message))
                   .WhereIf(!string.IsNullOrWhiteSpace(searchPagination.Exception), a => a.Exception.Contains(searchPagination.Exception))
                   .Count(out DataTotalCount)
                   .OrderByDescending(a => a.TimeStamp)//按时间倒序排列
                   .Page(searchPagination.PageIndex, searchPagination.PageSize)
                   .ToListAsync(cancellationToken);
                var dataViewModelList = _mapper.Map<List<SystemLogDataViewModel>>(dataList);
                return new PaginationResult<List<SystemLogDataViewModel>>(ResponseCode.OperationSuccess, "分页查询系统日志成功", DataTotalCount, dataViewModelList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"分页查询系统日志异常,异常原因为:【{ex.Message}】");
                return new PaginationResult<List<SystemLogDataViewModel>>(ResponseCode.ServerError, "分页查询系统日志异常", 0, null);
            }
        }
    }
}
