using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataModel.Base
{
    /// <summary>
    /// 验证码数据模型
    /// </summary>
    public class VerifyCodeDataModel
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 验证码数据流
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// base64
        /// </summary>
        public string Base64Str
        { get { return Convert.ToBase64String(Image); } }
    }
}
