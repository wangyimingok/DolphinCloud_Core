using Castle.Windsor;
using System;

namespace DolphinCloud.Framework.Dependency
{
    /// <summary>
    /// IOC容器管理接口
    /// </summary>
    public interface IIocManager: IIocRegistrar, IIocResolver,IDisposable
    {
        /// <summary>
        /// 引用Castle Windsor Container.
        /// </summary>
        IWindsorContainer IocContainer { get; }

        /// <summary>
        /// 检查给定类型之前是否已注册
        /// </summary>
        /// <param name="type">Type to check</param>
        new bool IsRegistered(Type type);

        /// <summary>
        ///检查给定类型之前是否已注册
        /// </summary>
        /// <typeparam name="T">Type to check</typeparam>
        new bool IsRegistered<T>();
    }
}
