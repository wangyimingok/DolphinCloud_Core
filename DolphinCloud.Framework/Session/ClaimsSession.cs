using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Framework.Session
{
    /// <summary>
    /// 身份声明
    /// </summary>
    public class ClaimsSession : SessionBase
    {
        /// <summary>
        /// 主要身份凭证访问器
        /// </summary>
        protected IPrincipalAccessor PrincipalAccessor { get; }

        /// <summary>
        /// HttpContext访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 身份凭证集合
        /// </summary>
        private List<Claim> ClaimList
        {
            get
            {
                if (PrincipalAccessor.Principal == null)
                {
                    return _httpContextAccessor.HttpContext?.User?.Claims.ToList();
                }
                else
                {
                    return PrincipalAccessor.Principal?.Claims.ToList();
                }
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="principalAccessor"></param>
        /// <param name="httpContextAccessor"></param>
        public ClaimsSession(IPrincipalAccessor principalAccessor, IHttpContextAccessor httpContextAccessor)
        {
            PrincipalAccessor = principalAccessor;
            _httpContextAccessor = httpContextAccessor;
        }
        /// <summary>
        /// 当前登录用户主键
        /// </summary>
        public override long UserID
        {
            get
            {
                var userIdClaim = ClaimList?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return 0;
                }
                long userId;
                if (!long.TryParse(userIdClaim.Value, out userId))
                {
                    return 0;
                }
                return userId;
            }
        }
        /// <summary>
        /// 当前用户真实姓名
        /// </summary>
        public override string UserRealName
        {
            get
            {
                var userNameClaim = ClaimList?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
                if (string.IsNullOrEmpty(userNameClaim?.Value))
                {
                    return null;
                }
                else
                {
                    return userNameClaim?.Value;
                }
            }
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        public override string UserName
        {
            get
            {
                var userNameClaim = ClaimList?.FirstOrDefault(c => c.Type == "name");
                if (string.IsNullOrEmpty(userNameClaim?.Value))
                {
                    var clientidClaim = ClaimList?.FirstOrDefault(c => c.Type == "client_id");
                    if (!string.IsNullOrEmpty(clientidClaim?.Value))
                    {
                        return clientidClaim?.Value;
                    }
                    return null;
                }
                else
                {
                    return userNameClaim?.Value;
                }
            }
        }
        /// <summary>
        /// 用户手机号码
        /// </summary>
        public override string MobilePhone
        {
            get
            {
                var UserMobilePhoneClaim = ClaimList?.FirstOrDefault(c => c.Type == "phone_number");
                if (string.IsNullOrEmpty(UserMobilePhoneClaim?.Value))
                {
                    return null;
                }
                else
                {
                    return UserMobilePhoneClaim?.Value;
                }
            }
        }
        /// <summary>
        /// 用户邮箱地址
        /// </summary>
        public override string EmailAddress
        {
            get
            {
                var UserEmailClaim = ClaimList?.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                if (string.IsNullOrEmpty(UserEmailClaim?.Value))
                {
                    return null;
                }
                else
                {
                    return UserEmailClaim?.Value;
                }
            }
        }

        /// <summary>
        /// 部门主键
        /// </summary>
        public override int DepartmentID
        {
            get
            {

                var userIdClaim = ClaimList?.FirstOrDefault(c => c.Type == "DepartmentID");
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return 0;
                }
                int departmentID;
                if (!int.TryParse(userIdClaim.Value, out departmentID))
                {
                    return 0;
                }
                return departmentID;

            }
        }

        /// <summary>
        /// 工号
        /// </summary>
        public override string EmployeeNumber
        {
            get
            {
                var EmployeeNumberClaim = ClaimList?.FirstOrDefault(c => c.Type == "EmployeeNumber");
                if (string.IsNullOrEmpty(EmployeeNumberClaim?.Value))
                {
                    return null;
                }
                else
                {
                    return EmployeeNumberClaim?.Value;
                }
            }
        }
    }
}
