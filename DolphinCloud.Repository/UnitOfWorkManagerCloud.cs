using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Repository
{
    public class UnitOfWorkManagerCloud
    {
        private readonly FreeSqlCloud m_cloud;
        private readonly Dictionary<string, UnitOfWorkManager> m_managers = new();

        public UnitOfWorkManagerCloud(IServiceProvider serviceProvider)
        {
            m_cloud = serviceProvider.GetService<FreeSqlCloud>();
        }

        public void Dispose()
        {
            foreach (var uowm in m_managers.Values) uowm.Dispose();
            m_managers.Clear();
        }

        public IUnitOfWork Begin(string db, Propagation propagation = Propagation.Required,
            IsolationLevel? isolationLevel = null)
        {
            return GetUnitOfWorkManager(db).Begin(propagation, isolationLevel);
        }

        public UnitOfWorkManager GetUnitOfWorkManager(string db)
        {
            if (m_managers.TryGetValue(db, out var uowm) == false)
            {
                uowm = new UnitOfWorkManager(m_cloud.Use(db));
                m_managers.Add(db, uowm);
            }

            return uowm;
        }
    }
}
