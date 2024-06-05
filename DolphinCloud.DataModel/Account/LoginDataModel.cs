using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DolphinCloud.DataModel.Account
{
    /// <summary>
    /// 登录数据模型
    /// </summary>
    public class LoginDataModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerCode { get; set; }
        /// <summary>
        /// 是否记住我
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
