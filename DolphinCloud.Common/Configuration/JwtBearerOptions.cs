using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Configuration
{
    /// <summary>
    ///  Jwt Bearer Token配置
    /// </summary>
    public class JwtBearerOptions
    {
        /// <summary>
        /// 令牌颁发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 令牌接受者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 加密密钥
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 过期时间(分钟)
        /// </summary>
        public int ExpireMinutes { get; set; }

        /// <summary>
        /// 是否启用Jwt Bearer
        /// </summary>
        public bool IsEnabledJwtBearer { get; set; }
    }
}
