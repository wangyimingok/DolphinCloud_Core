using DolphinCloud.DataInterFace.System;
using Microsoft.AspNetCore.Authentication;
using System.Net;

namespace DolphinCloud.OMS.AdminWeb.Initialization.CustomizeAuthen
{
    public class CustomizeAuthenticationHandler : IAuthenticationHandler
    {
        /// <summary>
        /// 当前认证架构
        /// </summary>
        private AuthenticationScheme _currentScheme;
        /// <summary>
        /// 当前http上下文
        /// </summary>
        private HttpContext _currentContext;

        private  IUserDataInterFace _userdata; 
        public async Task<AuthenticateResult> AuthenticateAsync()
        {
            var requestUrl = _currentContext.Request.Path.Value;
            var currentUser = _currentContext.User;
            if (currentUser.Identity.IsAuthenticated)
            {

                return await Task.FromResult(AuthenticateResult.Success(null));
            }
            else
            {
                return await Task.FromResult(AuthenticateResult.Fail("未登录"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            _currentContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 禁止访问处理
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public Task ForbidAsync(AuthenticationProperties properties)
        {
            _currentContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 认证初始化
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _currentScheme = scheme;
            _currentContext = context;
            _userdata= context.RequestServices.GetService<IUserDataInterFace>();
            return Task.CompletedTask;
        }
    }
}
