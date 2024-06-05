using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Configuration
{
    /// <summary>
    /// 身份验证配置
    /// </summary>
    public class AuthenticationConfiguration
    {
        /// <summary>
        /// Cookie配置项
        /// </summary>
        public CookieOptions CookieOptions { get; set; }

        /// <summary>
        /// JWT Bearer配置项
        /// </summary>
        public JwtBearerOptions JwtBearerOptions { get; set; }
    }
}
