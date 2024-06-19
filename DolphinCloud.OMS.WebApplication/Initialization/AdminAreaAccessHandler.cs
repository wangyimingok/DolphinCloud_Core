using Microsoft.AspNetCore.Authorization;

namespace DolphinCloud.OMS.WebApplication.Initialization
{
    /// <summary>
    /// Admin区域权限处理
    /// </summary>
    public class AdminAreaAccessHandler : AuthorizationHandler<AdminAreaRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminAreaRequirement requirement)
        {
           var currentUser= context.User;
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
