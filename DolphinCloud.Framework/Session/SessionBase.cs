using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Framework.Session
{
    public abstract class SessionBase : ICurrentUserInfo
    {
        /// <summary>
        /// 当前登录用户主键
        /// </summary>
        public abstract long UserID { get; }
        /// <summary>
        /// 当前用户账号
        /// </summary>
        public abstract string UserName { get; }
        /// <summary>
        /// 用户编码
        /// </summary>
        public abstract string UserRealName { get; }

        /// <summary>
        /// 部门主键
        /// </summary>
        public abstract int DepartmentID { get; }
        /// <summary>
        /// 用户手机号码
        /// </summary>
        public abstract string MobilePhone { get; }

        /// <summary>
        /// 用户邮箱地址
        /// </summary>
        public abstract string EmailAddress { get; }

        /// <summary>
        /// 工号
        /// </summary>
        public abstract string EmployeeNumber { get; }
    }
}
