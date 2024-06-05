using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.User
{
    /// <summary>
    /// 用户创建数据模型
    /// </summary>
    public class UserCreateDataModel
    {
        /// <summary>
        /// 用户信息主键
        /// </summary>
        [JsonProperty]
        public int UserID { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [JsonProperty]
        public string RealName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [JsonProperty]
        public string UserName { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        [JsonProperty]
        public string EMailAddress { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [JsonProperty]
        public string MobileNumber { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [JsonProperty]
        public string PassWord { get; set; }

       
    }
}
