using DolphinCloud.Common.Constants;
using DolphinCloud.OMS.WebApplication.Initialization.CustomizeAuthen;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;

namespace DolphinCloud.OMS.WebApplication.Initialization
{
    /// <summary>
    /// 基于JWT Token的身份验证处理程序
    /// </summary>
    public class CustomizeAuthenticationHandler : IAuthenticationHandler
    {
        /// <summary>
        /// 鉴权架构
        /// </summary>
        private AuthenticationScheme _scheme;
        /// <summary>
        /// http上下文
        /// </summary>
        private HttpContext _context;

        /// <summary>
        /// 验证当前请求
        /// </summary>
        /// <returns></returns>
        public Task<AuthenticateResult> AuthenticateAsync()
        {
            //IsAuthenticated
            var User = _context.User;
            var CurrentAccessToken = _context.Request.Headers["access_token"];
            if (!string.IsNullOrWhiteSpace(CurrentAccessToken))
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Role, "Admin")
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CustomizeAuthenticationOptions.Scheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal, CustomizeAuthenticationOptions.Scheme);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            else
            {
                return Task.FromResult(AuthenticateResult.Fail("未登录"));
            }

        }

        /// <summary>
        /// 质疑当前请求
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            _context.Response.Redirect("/Account/Login");

            return Task.CompletedTask;
        }

        /// <summary>
        /// 禁止当前请求
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public Task ForbidAsync(AuthenticationProperties properties)
        {
            _context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            //_context.Response.Redirect(_scheme.);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 鉴权初始化
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _scheme = scheme;
            _context = context;
            return Task.CompletedTask;
        }
    }
}
