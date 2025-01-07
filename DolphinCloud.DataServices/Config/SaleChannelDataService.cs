using AutoMapper;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.DataEntity.Config;
using DolphinCloud.DataInterFace.Config;
using DolphinCloud.DataModel.Base;
using DolphinCloud.DataModel.Config.SalesChannel;
using DolphinCloud.Framework.Session;
using DolphinCloud.Repository.Config;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataServices.Config
{
    /// <summary>
    /// 销售渠道数据服务
    /// </summary>
    public class SaleChannelDataService : BaseService, ISaleChannelDataInterFace
    {
        /// <summary>
        ///日志记录接口
        /// </summary>
        private readonly ILogger<SaleChannelDataService> _logger;
        /// <summary>
        /// 映射工具
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// 销售渠道数据仓储
        /// </summary>
        private readonly SalesChannelRepository _repo;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        private readonly ICurrentUserInfo _currentUser;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="logger">日志记录接口</param>
        /// <param name="mapper">映射工具</param>
        /// <param name="salesChannelRepository">销售渠道数据仓储</param>
        /// <param name="currentUserInfo">当前用户信息</param>
        public SaleChannelDataService(ILogger<SaleChannelDataService> logger, IMapper mapper, SalesChannelRepository salesChannelRepository, ICurrentUserInfo currentUserInfo)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = salesChannelRepository;
            _currentUser = currentUserInfo;
        }
        /// <summary>
        /// 创建销售渠道信息
        /// </summary>
        /// <param name="dataModel">传入参数 <see cref="SalesChannelCreateDataModel"/>类型 创建销售渠道数据模型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultMessage<string>> CreateSaleChannelAsync(SalesChannelCreateDataModel dataModel, CancellationToken cancellationToken)
        {
            try
            {
                if (await _repo.Select.Where(a => a.ChannelCode == dataModel.ChannelCode && a.ChannelName == dataModel.ChannelName && a.DeleteFG == false).AnyAsync(cancellationToken))
                {
                    return new ResultMessage<string>(ResponseCode.OperationWarning, "销售渠道已存在");
                }
                var dataEntity = _mapper.Map<SalesChannelInfo>(dataModel);
                dataEntity.CreateBy = _currentUser.UserName;
                dataEntity.CreateDateTime = DateTimeOffset.Now;
                dataEntity.LastModifyBy = _currentUser.UserName;
                dataEntity.LastModifyDateTime = DateTimeOffset.Now;
                await _repo.InsertAsync(dataEntity, cancellationToken);
                return new ResultMessage<string>(ResponseCode.OperationSuccess, "创建销售渠道成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"创建销售渠道异常,异常原因为:【{ex.Message}】");
                return new ResultMessage<string>(ResponseCode.ServerError, "创建销售渠道异常");
            }
        }

        /// <summary>
        /// 根据销售渠道ID获取销售渠道数据视图模型
        /// </summary>
        /// <param name="ChannelID">传入参数 <see cref="int"/>类型 销售渠道数据主键</param>
        /// <param name="cancellationToken">传入参数 <see cref="CancellationToken"/>类型 取消令牌</param>
        /// <returns></returns>
        public async Task<ResultMessage<SalesChannelDataViewModel>> GetChannelDataViewModelAsync(int ChannelID, CancellationToken cancellationToken)
        {
            try
            {
                var DataEntity = await _repo.Select.Where(a => a.ChannelID == ChannelID).ToOneAsync(cancellationToken);
                if (DataEntity == null)
                {
                    return new ResultMessage<SalesChannelDataViewModel>(ResponseCode.OperationWarning, "销售渠道数据不存在");
                }
                var dataViewModel = _mapper.Map<SalesChannelDataViewModel>(DataEntity);
                return new ResultMessage<SalesChannelDataViewModel>(ResponseCode.OperationSuccess, "查询成功", dataViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取销售渠道数据异常,异常原因为:【{ex.Message}】");
                return new ResultMessage<SalesChannelDataViewModel>(ResponseCode.ServerError, "获取销售渠道数据异常");
            }
        }

        /// <summary>
        /// 获取销售渠道下拉数据集
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResultMessage<List<OptionDataModel>>> GetChannelDataItemAsync(CancellationToken cancellationToken)
        {
            try
            {
                var dataList = await _repo.Select.Where(a => a.DeleteFG == false).ToListAsync(a => new OptionDataModel { OptionValue = a.ChannelID + string.Empty, OptionName = a.ChannelName }, cancellationToken);
                return new ResultMessage<List<OptionDataModel>>(ResponseCode.OperationSuccess, "查询成功", dataList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"获取销售渠道下拉数据集异常,异常原因为:【{ex.Message}】");
                return new ResultMessage<List<OptionDataModel>>(ResponseCode.ServerError, "获取销售渠道下拉数据集异常");
            }
        }

        /// <summary>
        /// 更新销售渠道信息
        /// </summary>
        /// <param name="dataModel">传入参数 <see cref="SalesChannelUpdateDataModel"/>类型 销售渠道更新数据模型</param>
        /// <param name="cancellationToken">传入参数 <see cref="CancellationToken"/>类型 取消令牌</param>
        /// <returns></returns>
        public async Task<ResultMessage<string>> UpdateChannelInfoAsync(SalesChannelUpdateDataModel dataModel, CancellationToken cancellationToken)
        {
            try
            {
                await _repo.Select.Where(a => a.ChannelID == dataModel.ChannelID)
                    .ToUpdate()
                    .SetIf(!string.IsNullOrWhiteSpace(dataModel.ChannelCode), a => a.ChannelCode, dataModel.ChannelCode)
                    .SetIf(!string.IsNullOrWhiteSpace(dataModel.ChannelName), a => a.ChannelName, dataModel.ChannelName)
                    .Set(a => a.LastModifyBy, _currentUser.UserName)
                    .Set(a => a.LastModifyDateTime, DateTimeOffset.Now).ExecuteAffrowsAsync(cancellationToken);
                return new ResultMessage<string>(ResponseCode.OperationSuccess, "更新销售渠道信息成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"更新销售渠道信息异常,异常原因为:【{ex.Message}】");
                throw;
            }
        }

        /// <summary>
        /// 删除销售渠道信息
        /// </summary>
        /// <param name="ChannelID">传入参数 <see cref="int"/>类型 销售渠道数据主键</param>
        /// <param name="cancellationToken">传入参数 <see cref="CancellationToken"/>类型 取消令牌</param>
        /// <returns></returns>
        public async Task<ResultMessage<string>> DeleteChannelInfoAsync(int ChannelID, CancellationToken cancellationToken)
        {
            try
            {
                await _repo.Select.Where(a => a.ChannelID == ChannelID)
                    .ToUpdate()
                    .Set(a => a.DeleteFG, true)
                    .Set(a => a.LastModifyBy, _currentUser.UserName)
                    .Set(a => a.LastModifyDateTime, DateTime.Now).ExecuteAffrowsAsync(cancellationToken);
                return new ResultMessage<string>(ResponseCode.OperationSuccess, "删除销售渠道信息成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"删除销售渠道信息异常,异常原因为:【{ex.Message}】");
                throw;
            }
        }

        /// <summary>
        /// 分页查询销售渠道信息
        /// </summary>
        /// <param name="dataModel">传入参数 <see cref="SaleChannelSearchPagination"/>类型 销售渠道搜索参数数据模型</param>
        /// <param name="cancellationToken">传入参数 <see cref="CancellationToken"/>类型 取消令牌</param>
        /// <returns></returns>
        public async Task<PaginationResult<List<SalesChannelDataViewModel>>> GetSaleChannelPaginationResultAsync(SaleChannelSearchPagination dataModel, CancellationToken cancellationToken)
        {
            try
            {
                long DataTotalCount = 0;
                var dataList = await _repo.Select.Where(a => a.DeleteFG == false)
                    .WhereIf(!string.IsNullOrWhiteSpace(dataModel.ChannelCode), a => a.ChannelCode.Contains(dataModel.ChannelCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(dataModel.ChannelName), a => a.ChannelName.Contains(dataModel.ChannelName))
                    .Count(out DataTotalCount)
                    .Page(dataModel.PageIndex, dataModel.PageSize)
                    .ToListAsync(cancellationToken);
                var dataViewModelList = _mapper.Map<List<SalesChannelDataViewModel>>(dataList);
                return new PaginationResult<List<SalesChannelDataViewModel>>(ResponseCode.OperationSuccess, "分页查询销售渠道信息成功", DataTotalCount, dataViewModelList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"分页查询销售渠道信息异常,异常原因为:【{ex.Message}】");
                return new PaginationResult<List<SalesChannelDataViewModel>>(ResponseCode.ServerError, "分页查询销售渠道信息异常", 0, null);
            }
        }
    }
}
