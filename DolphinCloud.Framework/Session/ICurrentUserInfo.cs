using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Framework.Session
{
    /// <summary>
    /// 当前登录用户信息
    /// 模仿ABP伪Session方式
    /// 实际是从用户凭证获取
    /// </summary>
    public interface ICurrentUserInfo
    {
        /// <summary>
        /// 当前登录用户主键
        /// </summary>
        long UserID { get; }

        /// <summary>
        /// 当前用户账号
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        string UserRealName { get; }

        /// <summary>
        /// 部门主键
        /// </summary>
        int DepartmentID { get; }

        /// <summary>
        /// 用户手机号码
        /// </summary>
        string MobilePhone { get; }

        /// <summary>
        /// 用户邮箱地址
        /// </summary>
        string EmailAddress { get; }

        /// <summary>
        /// 工号
        /// </summary>
        string EmployeeNumber { get; }

    }
}
