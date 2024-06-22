using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.User
{
    /// <summary>
    /// 重置密码数据模型
    /// </summary>
    public class ResetPasswordDataModel
    {
        /// <summary>
        /// 用户信息主键
        /// </summary>
        [JsonProperty]
        public long UserID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [JsonProperty]
        public string UserName { get; set; }

        /// <summary>
        /// 旧密码
        /// </summary>
        [JsonProperty]
        public string OldPassWord { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [JsonProperty]
        public string NewPassWord { get; set; }
    }
}
