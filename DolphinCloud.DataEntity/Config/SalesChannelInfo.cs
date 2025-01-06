using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataEntity.Config
{
    /// <summary>
	/// 销售渠道配置表
	/// </summary>
	[JsonObject(MemberSerialization.OptIn), Table(Name = "Config_SalesChannelInfo", DisableSyncStructure = true)]
    public class SalesChannelInfo
    {
        [JsonProperty, Column(IsPrimary = true)]
        public int ChannelID { get; set; }

        /// <summary>
        /// 订单来源编码
        /// </summary>
        [JsonProperty, Column(StringLength = 50)]
        public string ChannelCode { get; set; }

        /// <summary>
        /// 订单来源名称
        /// </summary>
        [JsonProperty, Column(StringLength = 50, IsNullable = false)]
        public string ChannelName { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [JsonProperty, Column(StringLength = 50, IsNullable = false)]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty]
        public DateTimeOffset CreateDateTime { get; set; } = DateTimeOffset.Now;


        /// <summary>
        /// 逻辑删除标志
        /// </summary>
        [JsonProperty]
        public bool DeleteFG { get; set; } = false;

        /// <summary>
        /// 最后修改人
        /// </summary>
        [JsonProperty, Column(StringLength = 50, IsNullable = false)]
        public string LastModifyBy { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [JsonProperty]
        public DateTimeOffset LastModifyDate { get; set; } = DateTimeOffset.Now;


    }
}
