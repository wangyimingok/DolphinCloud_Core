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
    /// 销售渠道展示数据模型
    /// </summary>
    public class SalesChannelDataViewModel
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

        /// <summary>
        /// 创建人
        /// </summary>
        [JsonProperty]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty]
        public DateTimeOffset CreateDateTime { get; set; }


        /// <summary>
        /// 逻辑删除标志
        /// </summary>
        [JsonProperty]
        public bool DeleteFG { get; set; } = false;

        /// <summary>
        /// 最后修改人
        /// </summary>
        [JsonProperty]
        public string LastModifyBy { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [JsonProperty]
        public DateTimeOffset LastModifyDateTime { get; set; }
    }
}
