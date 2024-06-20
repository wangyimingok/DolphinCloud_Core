using DolphinCloud.Common.Constants;
using DolphinCloud.DataInterFace.System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DolphinCloud.OMS.WebApplication.Initialization.CustomizeAuthen
{
    /// <summary>
    /// 自定义授权处理程序
    /// </summary>
    public class CustomizeAuthorizationHandler : AuthorizationHandler<CustomizeRequirement>
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private readonly ILogger<CustomizeAuthorizationHandler> _logger;
        /// <summary>
        /// 用户信息接口
        /// </summary>
        private readonly IUserDataInterFace _userData;
        public CustomizeAuthorizationHandler(ILogger<CustomizeAuthorizationHandler> logger, IUserDataInterFace userDataInter)
        {
            _logger = logger;
            _userData = userDataInter;
        }
        /// <summary>
        /// 处理鉴权
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomizeRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var UserID = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var UserName = context.User.FindFirst(ClaimTypes.Name)?.Value;
                var EMail = context.User.FindFirst(ClaimTypes.Email)?.Value;
                var MobilePhone = context.User.FindFirst(ClaimTypes.MobilePhone)?.Value;
                //验证客户端区域权限配置
                if (requirement.RequirementName == PermissionPolicy.ClientArea)
                {
                    if (UserName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        context.Succeed(requirement);
                    }
                }
                else if (requirement.RequirementName == PermissionPolicy.AdminArea)
                {
                    if (UserName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        context.Succeed(requirement);
                    }
                }
                else
                {
                    context.Fail();
                }
            }
           
            return Task.CompletedTask;
        }
    }
}
