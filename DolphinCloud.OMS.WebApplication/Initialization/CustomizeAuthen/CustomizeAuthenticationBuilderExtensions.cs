using Microsoft.AspNetCore.Authentication;

namespace DolphinCloud.OMS.WebApplication.Initialization.CustomizeAuthen
{
    /// <summary>
    /// 自定义身份验证扩展方法
    /// </summary>
    public static class CustomizeAuthenticationBuilderExtensions
    {
        /// <summary>
        /// 自定义身份认证
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        //public static AuthenticationBuilder AddCustomizeAuthentication(this AuthenticationBuilder builder, Action<CustomizeAuthenticationOptions> options=null)
        //{
        //    //return builder.AddScheme<CustomizeAuthenticationOptions, CustomizeAuthenticationHandler>(CustomizeAuthenticationOptions.Scheme, options);
        //    //return builder.AddScheme< CustomizeAuthenticationHandler>(CustomizeAuthenticationOptions.Scheme, options);
        //}
    }
}
