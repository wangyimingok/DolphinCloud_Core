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
// ָ��Ӧ������ʱ�����ļ�
builder.Configuration.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true).AddCommandLine(args).AddEnvironmentVariables();
var RootConfig = builder.Services.CreateRootConfiguration(builder.Configuration);
//ע�������ļ�����
builder.Services.AddSingleton(RootConfig);
builder.Host.UseCastleWindsor(IocManager.Instance.IocContainer);
//ʹ��Serilog��־��¼��
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
        //����WorkerId
        .SetWorkderId(buidler.Configuration["WorkerId"].ToLong())
        .Boot();
});
// Add services to the container.
builder.Services.AddControllersWithViews(option =>
{
    option.SuppressAsyncSuffixInActionNames = false;
    //������ڷ�CSRF�����Ĺ���������
    //option.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
}).AddNewtonsoftJson(option =>
{
    option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    //��ȡ�����������л��ͷ����л��ڼ���δ����ֵ
    //newtonsoftOption.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    //��ȡ�����������л��ͷ����л��ڼ���δ���Ĭ��ֵ
    //newtonsoftOption.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
    option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    //����ʱ�޶�ʱ���ʽ
    option.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
    option.SerializerSettings.ContractResolver = new ComprehensiveContractResolver
    {
        NamingStrategy = new CamelCaseNamingStrategy()//ת��ΪCamel����
    };
});
//���AutoMaperӳ���ϵ����
builder.Services.AddAutoMapperConfigrtion();
//builder.Services.ConfigCustomerDataProtectionForRedisAndSessionCookie(RootConfig);
//��CSRFע�빥������
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = AntiforgeryConstant.TokenHeaderName;
    options.Cookie.Name = AntiforgeryConstant.TokenCookieName;
});
//����JwtBearerToken�����֤
builder.Services.ConfigAuthentication(RootConfig.AuthenConfiguration);
//ҳ�����ܼ�����
builder.Services.AddMiniProfiler(option =>
{
    option.RouteBasePath = "/profiler";
    option.IgnoredPaths.Add("/lib");
    option.IgnoredPaths.Add("/css");
    option.IgnoredPaths.Add("/js");
});
//ע��FreeSQL ORM���
builder.Services.AddFreeSQLORM(RootConfig);
IocManager.Instance.AddConventionalRegistrar(new DolphinCloudOMSRegistrar());
IocManager.Instance.RegisterAssemblyByConvention(typeof(DolphinCloudOMSRegistrar).Assembly);
//builder.Services.InitSystemData();
builder.ConfigureKestrelHttps(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();
//����Ȩ����֤
app.UseAuthentication();
//�����֤
app.UseAuthorization();
//�������ܼ��
app.UseMiniProfiler();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    
    endpoints.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );
});
await app.RunAsync();
