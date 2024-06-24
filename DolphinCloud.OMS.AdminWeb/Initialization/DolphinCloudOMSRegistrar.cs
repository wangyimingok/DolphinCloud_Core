using Castle.MicroKernel.Registration;
using DolphinCloud.DataServices;
using DolphinCloud.Framework.Dependency;
using DolphinCloud.Framework.Session;
using System.Reflection;

namespace DolphinCloud.OMS.AdminWeb.Initialization
{
    public class DolphinCloudOMSRegistrar : IConventionalDependencyRegistrar
    {
        /// <summary>
        /// 扫描程序集以注册至依赖注入容器
        /// </summary>
        /// <param name="context"></param>
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            var ThisAssembly = Types.FromAssemblyInThisApplication(Assembly.GetAssembly(typeof(DolphinCloudOMSRegistrar)));
            context.IocManager.IocContainer.Register(ThisAssembly.BasedOn<BaseService>().WithServiceAllInterfaces().LifestyleTransient());
            context.IocManager.RegisterIfNot<IHttpContextAccessor, HttpContextAccessor>(DependencyLifeStyle.Transient);
            context.IocManager.RegisterIfNot<IPrincipalAccessor, DefaultPrincipalAccessor>(DependencyLifeStyle.Transient);
            context.IocManager.RegisterIfNot<ICurrentUserInfo, ClaimsSession>(DependencyLifeStyle.Transient);
        }
    }
}
