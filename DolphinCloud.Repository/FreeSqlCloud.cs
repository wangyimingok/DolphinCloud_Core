using DolphinCloud.Common.Enums;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Repository
{
    // <summary>
    ///     FreeSqlCloud
    /// </summary>
    public class FreeSqlCloud : FreeSqlCloud<DbEnum> //DbEnum 换成 string 就是多租户管理
    {
        public FreeSqlCloud() : base(null)
        {
        }

        public FreeSqlCloud(string distributeKey) : base(distributeKey)
        {
        }
    }
}
