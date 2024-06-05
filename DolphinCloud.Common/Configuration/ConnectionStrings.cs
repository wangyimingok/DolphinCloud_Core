using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Configuration
{
    public class ConnectionStrings
    {
        /// <summary>
        /// OMS数据库连接字符串
        /// </summary>
        public string OMSDataConnectionString { get; set; }

        /// <summary>
        /// HOMS数据库连接字符串
        /// </summary>
        public string HOMSDataConnectionString { get; set; }

        /// <summary>
        /// 分布式缓存Redis服务连接字符串
        /// </summary>
        public string DataProtectionConnectionString { get; set; }
    }
}
