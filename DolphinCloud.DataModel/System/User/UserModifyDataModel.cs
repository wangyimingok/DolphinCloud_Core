using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.User
{
    /// <summary>
    /// 用户信息更新数据模型
    /// </summary>
    public class UserModifyDataModel
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
        /// 密码
        /// </summary>
        [JsonProperty("PassWord")]
        public string PassWord { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [JsonProperty("RealName")]
        public string RealName { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        [JsonProperty("Status")]
        public short Status { get; set; } = 0;

        /// <summary>
        /// 用户名称
        /// </summary>
        [JsonProperty("UserName")]
        public string UserName { get; set; }
    }
}
