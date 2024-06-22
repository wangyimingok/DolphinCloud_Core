using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.User
{
    /// <summary>
    /// 用户基本信息数据模型
    /// </summary>
    public class BasicInfoDataModel
    {
        /// <summary>
        /// 用户信息主键
        /// </summary>
        [JsonProperty("UserID")]
        public long UserID { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [JsonProperty("EMailAddress")]
        public string EMailAddress { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [JsonProperty("MobileNumber")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [JsonProperty("RealName")]
        public string RealName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [JsonProperty("UserName")]
        public string UserName { get; set; }
    }
}
