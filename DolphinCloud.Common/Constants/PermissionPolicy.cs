﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Constants
{
    /// <summary>
    /// 权限验证策略
    /// </summary>
    public class PermissionPolicy
    {
        /// <summary>
        /// 管理端权限
        /// </summary>
        public const string AdminArea = "Admin";

        /// <summary>
        /// 客户端权限
        /// </summary>
        public const string ClientArea = "Client";

        /// <summary>
        /// 自定义验证策略
        /// </summary>
        public const string Customize = "Customize";
    }
}
