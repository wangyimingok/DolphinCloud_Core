using DolphinCloud.Common.Enums;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Repository
{
    /// <summary>
    ///     默认数据仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryCloud<T> : DefaultRepository<T, int> where T : class
    {
        public RepositoryCloud(UnitOfWorkManagerCloud uomw) : this(DbEnum.OMSDataBase, uomw)
        {
        } //DI

        public RepositoryCloud(DbEnum db, UnitOfWorkManagerCloud uomw) : this(uomw.GetUnitOfWorkManager(db.ToString()))
        {
        }

        private RepositoryCloud(UnitOfWorkManager uomw) : base(uomw.Orm, uomw)
        {
            uomw.Binding(this);
        }
    }

    /// <summary>
    ///     默认数据仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class RepositoryCloud<T, TKey> : DefaultRepository<T, TKey> where T : class
    {
        public RepositoryCloud(UnitOfWorkManagerCloud uomw) : this(DbEnum.OMSDataBase, uomw)
        {
        } //DI

        public RepositoryCloud(DbEnum db, UnitOfWorkManagerCloud uomw) : this(uomw.GetUnitOfWorkManager(db.ToString()))
        {
        }

        private RepositoryCloud(UnitOfWorkManager uomw) : base(uomw.Orm, uomw)
        {
            uomw.Binding(this);
        }
    }
}
