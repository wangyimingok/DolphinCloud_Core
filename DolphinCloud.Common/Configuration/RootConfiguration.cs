using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Configuration
{
    /// <summary>
    /// 配置项
    /// </summary>
    public class RootConfiguration : IRootConfiguration
    {
        /// <summary>
        /// 数据库连接字符串对象
        /// </summary>
        public ConnectionStrings ConnectionString { get; set; } = new ConnectionStrings();

        /// <summary>
        /// 身份验证配置
        /// </summary>
        public AuthenticationConfiguration AuthenConfiguration  { get; set; } = new AuthenticationConfiguration();
    }
}
