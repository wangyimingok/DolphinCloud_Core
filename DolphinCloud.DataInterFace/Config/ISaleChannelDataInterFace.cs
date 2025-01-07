using DolphinCloud.Common.Result;
using DolphinCloud.DataModel.Base;
using DolphinCloud.DataModel.Config.SalesChannel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataInterFace.Config
{
    /// <summary>
    /// 销售渠道数据接口
    /// </summary>
    public interface ISaleChannelDataInterFace
    {
        /// <summary>
        /// 创建销售渠道信息
        /// </summary>
        /// <param name="dataModel">传入参数 <see cref="CreateSaleChannelDataModel"/>类型 创建销售渠道数据模型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultMessage<string>> CreateSaleChannelAsync(SalesChannelCreateDataModel dataModel, CancellationToken cancellationToken);

        /// <summary>
        /// 获取销售渠道下拉数据集
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ResultMessage<List<OptionDataModel>>> GetChannelDataItemAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 根据销售渠道ID获取销售渠道数据视图模型
        /// </summary>
        /// <param name="ChannelID">传入参数 <see cref="int"/>类型 销售渠道数据主键</param>
        /// <param name="cancellationToken">传入参数 <see cref="CancellationToken"/>类型 取消令牌</param>
        /// <returns></returns>
        Task<ResultMessage<SalesChannelDataViewModel>> GetChannelDataViewModelAsync(int ChannelID, CancellationToken cancellationToken);

        /// <summary>
        /// 更新销售渠道信息
        /// </summary>
        /// <param name="dataModel">传入参数 <see cref="SaleChannelUpdateDataModel"/>类型 销售渠道更新数据模型</param>
        /// <param name="cancellationToken">传入参数 <see cref="CancellationToken"/>类型 取消令牌</param>
        /// <returns></returns>
        Task<ResultMessage<string>> UpdateChannelInfoAsync(SalesChannelUpdateDataModel dataModel, CancellationToken cancellationToken);

        /// <summary>
        /// 删除销售渠道信息
        /// </summary>
        /// <param name="ChannelID">传入参数 <see cref="int"/>类型 销售渠道数据主键</param>
        /// <param name="cancellationToken">传入参数 <see cref="CancellationToken"/>类型 取消令牌</param>
        /// <returns></returns>
        Task<ResultMessage<string>> DeleteChannelInfoAsync(int ChannelID, CancellationToken cancellationToken);

        /// <summary>
        /// 分页查询销售渠道信息
        /// </summary>
        /// <param name="dataModel">传入参数 <see cref="SaleChannelSearchPagination"/>类型 销售渠道搜索参数数据模型</param>
        /// <param name="cancellationToken">传入参数 <see cref="CancellationToken"/>类型 取消令牌</param>
        /// <returns></returns>
        Task<PaginationResult<List<SalesChannelDataViewModel>>> GetSaleChannelPaginationResultAsync(SaleChannelSearchPagination dataModel, CancellationToken cancellationToken);
    }
}
