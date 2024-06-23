
using DolphinCloud.Common.Attributes;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.OMS.WebApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DolphinCloud.OMS.WebApplication.Initialization
{
    /// <summary>
    /// 初始化系统数据中间件
    /// </summary>
    public class InitSystemDataMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<InitSystemDataMiddleware> _logger;
        private readonly IMenuDataInterFace _menuData;
        public InitSystemDataMiddleware(RequestDelegate next,IServiceProvider serviceProvider, ILogger<InitSystemDataMiddleware> logger, IMenuDataInterFace menuDataInterFace)
        {
            _next= next;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _menuData = menuDataInterFace;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var controllerTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(BaseController).IsAssignableFrom(type));
           await _menuData.InitMenuData(controllerTypes);
            await _next(context);
        }
    }
}
