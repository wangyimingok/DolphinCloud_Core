using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Configuration
{
    /// <summary>
    /// Cookie 配置
    /// </summary>
    public class CookieOptions
    {
        /// <summary>
        /// 是否启用Cookie认证 默认为false
        /// </summary>
        public bool IsEnabledCookie { get; set; }
        /// <summary>
        /// Cookie名称
        /// </summary>
        public string CookieName { get; set; }
        /// <summary>
        /// Cookie有效域
        /// </summary>
        public string CookieDomain { get; set; }
        /// <summary>
        /// Cookie有效路径
        /// </summary>
        public string CookiePath { get; set; }
        /// <summary>
        /// 是否启用CookieHTTPOnly 默认为true
        /// </summary>
        public bool CookieHttpOnly { get; set; }
    }
}
