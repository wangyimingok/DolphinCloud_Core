using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace DolphinCloud.OMS.WebApplication.Initialization.CustomizeAuthen
{
    public class CustomerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement>
    {
        private readonly ILogger<CustomerAuthorizationHandler> _logger;
        public CustomerAuthorizationHandler(ILogger<CustomerAuthorizationHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement)
        {
            var currentOperationName = requirement.Name;
            if (string.IsNullOrWhiteSpace(currentOperationName))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
