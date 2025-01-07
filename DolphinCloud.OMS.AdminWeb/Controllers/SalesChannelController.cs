using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.DataInterFace.Config;
using DolphinCloud.DataModel.Base;
using DolphinCloud.DataModel.Config.SalesChannel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.AdminWeb.Controllers
{
    /// <summary>
    /// 销售渠道配置
    /// </summary>
    [Authorize]
    public class SalesChannelController : BaseController
    {
        /// <summary>
        /// 销售渠道配置数据接口
        /// </summary>
        private readonly ISaleChannelDataInterFace _salesChannel;
        public SalesChannelController(ISaleChannelDataInterFace saleChannelDataInterFace)
        {
            _salesChannel = saleChannelDataInterFace;
        }
        /// <summary>
        /// 菜单首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 创建销售渠道页面
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateSaleChannel()
        {
            return View();
        }

        /// <summary>
        /// 创建销售渠道信息
        /// </summary>
        /// <param name="dataModel">传入参数 <see cref="SalesChannelCreateDataModel"/>类型 创建销售渠道数据模型</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ResultMessage<string>> CreateSaleChannel([FromBody] SalesChannelCreateDataModel dataModel, CancellationToken cancellationToken)
        {
            var result = await _salesChannel.CreateSaleChannelAsync(dataModel, cancellationToken);
            return result;
        }

        /// <summary>
        /// 获取销售渠道下拉数据集
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultMessage<List<OptionDataModel>>> GetChannelDataItem(CancellationToken cancellationToken)
        {
            var result = await _salesChannel.GetChannelDataItemAsync(cancellationToken);
            return result;
        }

        /// <summary>
        /// 根据销售渠道ID获取销售渠道数据视图模型
        /// </summary>
        /// <param name="channelID">传入参数 <see cref="int"/>类型 销售渠道数据主键</param>
        /// <param name="cancellationToken">传入参数 <see cref="CancellationToken"/>类型 取消令牌</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultMessage<SalesChannelDataViewModel>> GetChannelDataViewModel(int channelID, CancellationToken cancellationToken)
        {
            var result = await _salesChannel.GetChannelDataViewModelAsync(channelID, cancellationToken);
            return result;
        }

        /// <summary>
        /// 更新销售渠道信息
        /// </summary>
        /// <param name="dataModel">传入参数 <see cref="SaleChannelUpdateDataModel"/>类型 销售渠道更新数据模型</param>
        /// <param name="cancellationToken">传入参数 <see cref="CancellationToken"/>类型 取消令牌</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ResultMessage<string>> UpdateChannelInfo([FromBody] SalesChannelUpdateDataModel dataModel, CancellationToken cancellationToken)
        {
            var result = await _salesChannel.UpdateChannelInfoAsync(dataModel, cancellationToken);
            return result;
        }

        /// <summary>
        /// 删除销售渠道信息
        /// </summary>
        /// <param name="channelID">传入参数 <see cref="int"/>类型 销售渠道数据主键</param>
        /// <param name="cancellationToken">传入参数 <see cref="CancellationToken"/>类型 取消令牌</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultMessage<string>> DeleteChannelInfo(int channelID, CancellationToken cancellationToken)
        {
            var result = await _salesChannel.DeleteChannelInfoAsync(channelID, cancellationToken);
            return result;
        }

        /// <summary>
        /// 分页查询销售渠道信息
        /// </summary>
        /// <param name="dataModel">传入参数 <see cref="SaleChannelSearchPagination"/>类型 销售渠道搜索参数数据模型</param>
        /// <param name="cancellationToken">传入参数 <see cref="CancellationToken"/>类型 取消令牌</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<PaginationResult<List<SalesChannelDataViewModel>>> GetSaleChannelPaginationDataList([FromBody] SaleChannelSearchPagination dataModel, CancellationToken cancellationToken)
        {
            var result = await _salesChannel.GetSaleChannelPaginationResultAsync(dataModel, cancellationToken);
            return result;
        }
    }
}
