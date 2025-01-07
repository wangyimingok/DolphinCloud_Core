using Microsoft.Extensions.DependencyInjection;
using Rougamo.Context;
using Rougamo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DolphinCloud.Repository.AOP
{
    /// <summary>
    ///     事务特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionalAttribute : MoAttribute
    {
        private static readonly AsyncLocal<IServiceProvider> m_ServiceProvider = new();
        private readonly DbEnum m_db;

        private IUnitOfWork _uow;
        private IsolationLevel? m_IsolationLevel;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="db"></param>
        public TransactionalAttribute(DbEnum db)
        {
            m_db = db;
        }
        /// <summary>
        ///  事务传播方式
        /// </summary>
        public Propagation Propagation { get; set; } = Propagation.Required;

        /// <summary>
        ///  事务隔离级别
        /// </summary>
        public IsolationLevel IsolationLevel
        {
            get => m_IsolationLevel.Value;
            set => m_IsolationLevel = value;
        }

        /// <summary>
        /// 设置服务容器
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            m_ServiceProvider.Value = serviceProvider;
        }

        /// <summary>
        /// 方法调用时执行
        /// </summary>
        /// <param name="context"></param>
        public override void OnEntry(MethodContext context)
        {
            var uowManager = m_ServiceProvider.Value.GetService<UnitOfWorkManagerCloud>();
            _uow = uowManager.Begin(m_db.ToString(), Propagation, m_IsolationLevel);
        }

        /// <summary>
        /// 方法执行完毕进入
        /// </summary>
        /// <param name="context"></param>
        public override void OnExit(MethodContext context)
        {
            if (typeof(Task).IsAssignableFrom(context.TaskReturnType))
                ((Task)context.ReturnValue).ContinueWith(t => _OnExit());
            else _OnExit();

            void _OnExit()
            {
                try
                {
                    if (context.Exception == null) _uow.Commit();
                    else _uow.Rollback();
                }
                finally
                {
                    _uow.Dispose();
                }
            }
        }
    }
}
