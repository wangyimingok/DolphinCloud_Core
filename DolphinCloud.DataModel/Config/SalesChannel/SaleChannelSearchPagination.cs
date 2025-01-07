using DolphinCloud.Common.Pagination;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.Config.SalesChannel
{
    /// <summary>
    /// 销售渠道搜索条件
    /// </summary>
    public class SaleChannelSearchPagination: BasePagination
    {
        /// <summary>
        /// 订单来源编码
        /// </summary>
        [JsonProperty("channelCode")]
        public string ChannelCode { get; set; }

        /// <summary>
        /// 订单来源名称
        /// </summary>
        [JsonProperty("channelName")]
        public string ChannelName { get; set; }
    }
}
