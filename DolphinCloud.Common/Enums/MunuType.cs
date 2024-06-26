﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Enums
{
    /// <summary>
    /// 菜单类型
    /// </summary>
    public enum MunuType
    {
        /// <summary>
        /// 根菜单
        /// </summary>
        RootMenu = 1,
        /// <summary>
        /// 子菜单
        /// </summary>
        ChildMenu = 2,
        /// <summary>
        /// 页面
        /// </summary>
        PageView = 3,
        /// <summary>
        /// 按钮/功能
        /// </summary>
        Button_Function = 4
    }
}
