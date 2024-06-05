namespace DolphinCloud.Framework.Dependency
{
    /// <summary>
    /// 该接口用于按约定注册依赖项。
    /// </summary>
    /// <remarks>
    /// 实现该接口并注册到 <see cref="IocManager.AddConventionalRegistrar"/> 方法能够按照您自己的约定注册类
    /// </remarks>
    public interface IConventionalDependencyRegistrar
    {
        /// <summary>
        /// 按照约定注册给定程序集的类型
        /// </summary>
        /// <param name="context">登记上下文</param>
        void RegisterAssembly(IConventionalRegistrationContext context);
    }
}
