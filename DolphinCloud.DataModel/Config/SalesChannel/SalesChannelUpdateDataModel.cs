using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.Config.SalesChannel
{
    /// <summary>
    /// 渠道信息更新数据模型
    /// </summary>
    public class SalesChannelUpdateDataModel
    {
        /// <summary>
        /// 渠道数据主键
        /// </summary>
        [JsonProperty]
        public int ChannelID { get; set; }

        /// <summary>
        /// 销售渠道编码
        /// </summary>
        [JsonProperty]
        public string ChannelCode { get; set; }

        /// <summary>
        /// 销售渠道名称
        /// </summary>
        [JsonProperty]
        public string ChannelName { get; set; }
    }
}
