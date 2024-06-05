using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DolphinCloud.Framework.Session
{
    public static class PrincipalAccessorExtention
    {
        /// <summary>
        /// 注册身份凭据
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterPrincipal(this IServiceCollection services)
        {
            //加入HttpContext
            services.AddHttpContextAccessor();
            //注册身份凭证访问上下文
            services.TryAddSingleton<IPrincipalAccessor, DefaultPrincipalAccessor>();
            services.TryAddSingleton<ClaimsSession>();
            services.TryAddSingleton<ICurrentUserInfo, ClaimsSession>();           
        }
    }
}
