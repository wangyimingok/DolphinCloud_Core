using DolphinCloud.Common.Snowflake;
using DolphinCloud.Framework.Dependency;
using DolphinCloud.OMS.WebApplication.Initialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Serilog.Exceptions;
using DolphinCloud.Common.Extentions;
using Newtonsoft.Json.Serialization;
using DolphinCloud.Common.ContractResolver;
using DolphinCloud.Common.Constants;
using DolphinCloud.Repository;
using DolphinCloud.AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using DolphinCloud.DataInterFace.System;
using System.Reflection;
using DolphinCloud.OMS.WebApplication.Controllers;

var builder = WebApplication.CreateBuilder(args);
// 指定应用运行时配置文件
builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true).AddCommandLine(args).AddEnvironmentVariables();
var RootConfig = builder.Services.CreateRootConfiguration(builder.Configuration);
//注入配置文件对象
builder.Services.AddSingleton(RootConfig);
builder.Host.UseCastleWindsor(IocManager.Instance.IocContainer);
//使用Serilog日志记录器
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .Enrich.WithProperty("ApplicationName", context.HostingEnvironment.ApplicationName);
});
builder.Host.ConfigureServices((buidler, services) =>
{
    new IdHelperBootstrapper()
        //设置WorkerId
        .SetWorkderId(buidler.Configuration["WorkerId"].ToLong())
        .Boot();
});
// Add services to the container.
builder.Services.AddControllersWithViews(option =>
{
    option.SuppressAsyncSuffixInActionNames = false;
    //添加用于防CSRF攻击的过滤器特性
    //option.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
}).AddNewtonsoftJson(option =>
{
    option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    //获取或设置在序列化和反序列化期间如何处理空值
    //newtonsoftOption.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    //获取或设置在序列化和反序列化期间如何处理默认值
    //newtonsoftOption.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
    option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    //序列时限定时间格式
    option.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
    option.SerializerSettings.ContractResolver = new ComprehensiveContractResolver
    {
        NamingStrategy = new CamelCaseNamingStrategy()//转换为Camel规则
    };
});
//添加AutoMaper映射关系配置
builder.Services.AddAutoMapperConfigrtion();
builder.Services.ConfigCustomerDataProtectionForRedisAndSessionCookie(RootConfig);
//防CSRF注入攻击配置
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = AntiforgeryConstant.TokenHeaderName;
    options.Cookie.Name = AntiforgeryConstant.TokenCookieName;
});
//配置JwtBearerToken身份验证
builder.Services.ConfigAuthentication(RootConfig.AuthenConfiguration);
//页面性能监视器
builder.Services.AddMiniProfiler(option =>
{
    option.RouteBasePath = "/profiler";
    option.IgnoredPaths.Add("/lib");
    option.IgnoredPaths.Add("/css");
    option.IgnoredPaths.Add("/js");
});
//注册FreeSQL ORM框架
builder.Services.AddFreeSQLORM(RootConfig);
IocManager.Instance.AddConventionalRegistrar(new DolphinCloudOMSRegistrar());
IocManager.Instance.RegisterAssemblyByConvention(typeof(DolphinCloudOMSRegistrar).Assembly);
//builder.Services.InitSystemData();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
//app.Use(async (context, next) =>
//{
//    var menuData = context.RequestServices.GetService<IMenuDataInterFace>(); //.Get<IMenuDataInterFace>();
//    if (menuData != null)
//    {
//        var controllerList = Assembly.GetExecutingAssembly().GetTypes().Where(a=>a.BaseType==typeof(BaseController));
//        await menuData.InitMenuData(controllerList);
//    }
//    var userData = context.RequestServices.GetService<IUserDataInterFace>();
//    if (userData != null)
//    {
//        await userData.GenerateAdmin();
//    }
//    await next();
//});
app.UseRouting();
//启用权限验证
app.UseAuthentication();
//身份验证
app.UseAuthorization();

app.UseMiniProfiler();
//app.UseSession();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(
       name: "adminArea",
       areaName: "Admin",
       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
});

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );
//    endpoints.MapControllerRoute(
//       name: "default",
//       pattern: "{controller=Home}/{action=Index}/{id?}");
//});

app.Run();
