using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.System.User
{
    public class UserDataViewModel
    {
        /// <summary>
        /// 用户信息主键
        /// </summary>
        [JsonProperty]
        public long UserID { get; set; }

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
        /// 邮箱地址
        /// </summary>
        [JsonProperty]
        public string EMailAddress { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        [JsonProperty]
        public string LastModifyBy { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [JsonProperty]
        public DateTimeOffset LastModifyDate { get; set; }

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

        /// <summary>
        /// 真实姓名
        /// </summary>
        [JsonProperty]
        public string RealName { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        [JsonProperty]
        public short Status { get; set; } = 0;

        /// <summary>
        /// 用户名称
        /// </summary>
        [JsonProperty]
        public string UserName { get; set; }
    }
}
