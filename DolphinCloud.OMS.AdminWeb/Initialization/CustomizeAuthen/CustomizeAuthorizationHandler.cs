using DolphinCloud.Common.Constants;
using DolphinCloud.DataInterFace.System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DolphinCloud.OMS.AdminWeb.Initialization.CustomizeAuthen
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

        private readonly IRoleDataInterFace _roleData;
        public CustomizeAuthorizationHandler(ILogger<CustomizeAuthorizationHandler> logger, IUserDataInterFace userDataInter, IRoleDataInterFace roleData)
        {
            _logger = logger;
            _userData = userDataInter;
            _roleData = roleData;
        }
        /// <summary>
        /// 处理鉴权
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomizeRequirement requirement)
        {
            //context.
            if (context.User.Identity.IsAuthenticated)
            {
                if (context.Resource != null)
                {
                    HttpContext httpContext = (HttpContext)context.Resource;
                    if (httpContext != null)
                    {
                        var UserID = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        var UserName = context.User.FindFirst(ClaimTypes.Name)?.Value;
                        string urlAddress = httpContext.Request.Path.Value;
                        var routeData = httpContext.GetRouteData();
                        var controllerName = httpContext.GetRouteValue("controller") + string.Empty;
                        var actionName = httpContext.GetRouteValue("action") + string.Empty;
                        var checkedResult = await _roleData.CheckPermissionAsync(controllerName, actionName, urlAddress);
                        if (checkedResult)
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            _logger.LogWarning($"用户【{UserName}】,用户ID【{UserID}】访问【{controllerName}/{actionName}】权限校验不通过");
                            context.Fail();
                        }
                    }
                    else
                    {
                        _logger.LogWarning($"权限校验不通过,【httpContext】为空!");
                        context.Fail();
                    }
                }
                else
                {
                    _logger.LogWarning($"权限校验不通过,【context.Resource】为空!");
                    context.Fail();
                }
            }
            else
            {
                _logger.LogWarning($"权限校验不通过,用户未登录!");
                context.Fail();
                //context.
            }
        }
    }
}
