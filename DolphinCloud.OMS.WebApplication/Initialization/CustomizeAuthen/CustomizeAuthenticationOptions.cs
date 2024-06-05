using Microsoft.AspNetCore.Authentication;

namespace DolphinCloud.OMS.WebApplication.Initialization.CustomizeAuthen
{
    /// <summary>
    /// 自定义身份验证配置选项
    /// </summary>
    public class CustomizeAuthenticationOptions: AuthenticationSchemeOptions
    {
        /// <summary>
        /// 方案名称
        /// </summary>
        public const string Scheme = "CustomizeAuthentication";
    }
}
