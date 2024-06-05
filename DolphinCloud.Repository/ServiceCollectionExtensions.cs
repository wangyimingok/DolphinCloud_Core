using DolphinCloud.Common.Configuration;
using DolphinCloud.Common.Enums;
using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DolphinCloud.Repository
{
    /// <summary>
    ///     服务集合扩展类
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     注册FreeSQL ORM框架
        /// </summary>
        /// <param name="services">传入参数 <see cref="IServiceCollection" />集合 </param>
        /// <param name="rootConfiguration">传入参数 <see cref="IRootConfiguration" />类型 配置文件对象</param>
        public static void AddFreeSQLORM(this IServiceCollection services, IRootConfiguration rootConfiguration)
        {
            //注入数据库操作
            var fsql = new FreeSqlCloud();
            fsql.DistributeTrace = log => Console.WriteLine(log.Split('\n')[0].Trim());
            fsql.Register(DbEnum.OMSDataBase,
                () => new FreeSqlBuilder().UseConnectionString(DataType.SqlServer,
                    rootConfiguration.ConnectionString?.OMSDataConnectionString).Build());
            services.AddSingleton<IFreeSql>(fsql);
            services.AddSingleton(fsql);
            services.AddScoped<UnitOfWorkManagerCloud>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(RepositoryCloud<>));
            services.AddScoped(typeof(BaseRepository<>), typeof(RepositoryCloud<>));
            services.AddScoped(typeof(IBaseRepository<,>), typeof(RepositoryCloud<,>));
            services.AddScoped(typeof(BaseRepository<,>), typeof(RepositoryCloud<,>));
            var typeList = typeof(RepositoryCloud<>).Assembly.GetTypes()
                .Where(a => a.IsAbstract == false && typeof(IBaseRepository).IsAssignableFrom(a));
            foreach (var repositoryType in typeList) services.AddScoped(repositoryType);
        }
    }
}