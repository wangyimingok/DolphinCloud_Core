using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.Config.SalesChannel
{
    /// <summary>
    /// 销售渠道创建数据模型
    /// </summary>
    public class SalesChannelCreateDataModel
    {
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
