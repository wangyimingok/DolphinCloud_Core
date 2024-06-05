using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Configuration
{
    /// <summary>
    /// 基础配置
    /// </summary>
    public interface IRootConfiguration
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        ConnectionStrings ConnectionString { get; }

        /// <summary>
        /// JwtBearerToken配置
        /// </summary>
        AuthenticationConfiguration AuthenConfiguration { get;}
    }
}
