using DolphinCloud.DataInterFace.System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace DolphinCloud.OMS.AdminWeb.Initialization.CustomizeAuthen
{
    public class CustomizedAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private IUserDataInterFace _user;
        public CustomizedAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, IUserDataInterFace userDataInterFace) : base(options, logger, encoder)
        {
            _user=userDataInterFace;
        }

        public CustomizedAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IUserDataInterFace userDataInterFace) : base(options, logger, encoder, clock)
        {
            _user=userDataInterFace;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
           // Request.
           return Task.FromResult(AuthenticateResult.Fail("未登录"));
        }
    }
}
