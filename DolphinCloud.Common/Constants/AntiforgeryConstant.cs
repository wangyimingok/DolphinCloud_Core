using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Constants
{
    /// <summary>
    /// CSRF攻击常量
    /// </summary>
    public class AntiforgeryConstant
    {
        public const string TokenCookieName = "XSRF-TOKEN";

        public const string TokenHeaderName = "X-CSRF-Token";

        /// <summary>
        /// cookie名称
        /// </summary>
        public const string CookieName = "DolphinCloud.OMS.Cookie";

        /// <summary>
        /// Session名称
        /// </summary>
        public const string SessionName = "DolphinCloud.Session";
    }
}
