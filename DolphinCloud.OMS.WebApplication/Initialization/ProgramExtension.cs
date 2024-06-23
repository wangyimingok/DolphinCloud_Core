using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using DolphinCloud.Common.Configuration;
using DolphinCloud.Common.Constants;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.Framework.Dependency;
using DolphinCloud.OMS.WebApplication.Areas.Admin.Controllers;
using DolphinCloud.OMS.WebApplication.Controllers;
using DolphinCloud.OMS.WebApplication.Initialization.CustomizeAuthen;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace DolphinCloud.OMS.WebApplication.Initialization
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ProgramExtension
    {
        /// <summary>
        /// 加载配置文件为对象
        /// </summary>
        /// <returns></returns>
        public static IRootConfiguration CreateRootConfiguration(this IServiceCollection services, IConfiguration ConfigurationProviderSource)
        {
            //services.
            var rootConfiguration = new RootConfiguration();
            ConfigurationProviderSource.GetSection(nameof(ConnectionStrings)).Bind(rootConfiguration.ConnectionString);
            ConfigurationProviderSource.GetSection(nameof(AuthenticationConfiguration)).Bind(rootConfiguration.AuthenConfiguration);
            return rootConfiguration;
        }
        /// <summary>
        /// 使用CastleWindsor依赖注入容器
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="windsorContainer"></param>
        /// <returns></returns>
        public static IHostBuilder UseCastleWindsor(this IHostBuilder hostBuilder, [NotNull] IWindsorContainer windsorContainer)
        {
            //Check.NotNull(windsorContainer, nameof(windsorContainer));

            return hostBuilder
                .ConfigureServices(services =>
                {
                    services.AddSingleton(windsorContainer);
                })
                .UseServiceProviderFactory(new WindsorServiceProviderFactory());
        }

        /// <summary>
        /// 配置分布式DataProtection缓存
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionStrings"></param>
        internal static void ConfigCustomerDataProtectionForRedisAndSessionCookie(this IServiceCollection services, IRootConfiguration Configuration)
        {
            var DataProtectionConnectionString = Configuration.ConnectionString.DataProtectionConnectionString;
            var redis = ConnectionMultiplexer.Connect(DataProtectionConnectionString);//建立Redis 连接
            var cookieOption = Configuration.AuthenConfiguration.CookieOptions;
            //添加数据保护服务，设置统一应用程序名称，并指定使用Reids存储私钥
            services.AddDataProtection()
                .SetApplicationName(Assembly.GetExecutingAssembly().GetName().Name)
                .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys");

            //添加Redis缓存用于分布式Session
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = DataProtectionConnectionString;
                options.InstanceName = Assembly.GetExecutingAssembly().GetName().Name;
            });
            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = string.IsNullOrWhiteSpace(cookieOption.CookieName) ? AntiforgeryConstant.CookieName : cookieOption.CookieName;
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.SameSite = SameSiteMode.None;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            //});
        }

        /// <summary>
        /// 配置基于Cookie的身份验证
        /// </summary>
        /// <param name="services"></param>
        internal static void ConfigAuthentication(this IServiceCollection services, AuthenticationConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                var DefaultPolicy = new AuthorizationPolicyBuilder();
                DefaultPolicy.Requirements.Add(new CustomizeRequirement(PermissionPolicy.ClientArea));
                options.DefaultPolicy = DefaultPolicy.Build();
                options.AddPolicy(PermissionPolicy.AdminArea, policy => policy.Requirements.Add(new CustomizeRequirement(PermissionPolicy.AdminArea)));
                options.AddPolicy(PermissionPolicy.ClientArea, policy => policy.Requirements.Add(new CustomizeRequirement(PermissionPolicy.ClientArea)));
                //options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
            });
            services.AddSingleton<IAuthorizationHandler, CustomizeAuthorizationHandler>();

            if (configuration.CookieOptions.IsEnabledCookie)
            {
                var cookieOption = configuration.CookieOptions;
                //身份验证配置
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                  .AddCookie(option =>
                  {
                      //无权限跳转页面地址
                      option.AccessDeniedPath = new PathString("/Account/AccessDenied");
                      option.LoginPath = new PathString("/Account/Login");
                      option.LogoutPath = new PathString("/Account/Logout");
                      option.Cookie = new CookieBuilder
                      {
                          //HttpOnly = cookieOption.CookieHttpOnly,
                          Name = string.IsNullOrWhiteSpace(cookieOption.CookieName) ? AntiforgeryConstant.CookieName : cookieOption.CookieName,
                          Path = string.IsNullOrWhiteSpace(cookieOption.CookiePath) ? "/" : cookieOption.CookiePath,
                          //SameSite = SameSiteMode.None,
                          //SecurePolicy = CookieSecurePolicy.SameAsRequest
                      };
                  });
            }
            if (configuration.JwtBearerOptions.IsEnabledJwtBearer)
            {
                var jwtBearerOption = configuration.JwtBearerOptions;
                //身份验证配置
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddCookie(option =>
                {
                    option.LoginPath = new PathString("/Account/Login");
                    option.LogoutPath = new PathString("/Account/Logout");
                    option.Cookie = new CookieBuilder
                    {
                        HttpOnly = true,
                        Name = AntiforgeryConstant.CookieName,
                        Path = "/",
                        SameSite = SameSiteMode.None,
                        SecurePolicy = CookieSecurePolicy.SameAsRequest
                    };
                }).AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtBearerOption.Issuer,
                        ValidAudience = jwtBearerOption.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                          Encoding.UTF8.GetBytes(jwtBearerOption.SecretKey)
                      ),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            }

        }

        /// <summary>
        /// 配置Kestrel https
        /// </summary>
        /// <param name="webBuilder"></param>
        /// <param name="configuration"></param>
        internal static void ConfigureKestrelHttps(this WebApplicationBuilder webBuilder, IConfiguration configuration)
        {
            webBuilder.WebHost.UseConfiguration(configuration)
                .UseKestrel();
        }

        internal static void Init(this IServiceCollection services)
        {
            var controllerTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type));
            foreach (var item in controllerTypes)
            {

            }
            //app.Use(async (context, next) =>
            //{
            //    var menuData = context.RequestServices.GetService<IMenuDataInterFace>(); //.Get<IMenuDataInterFace>();
            //    if (menuData != null)
            //    {
            //        var controllerList = Assembly.GetExecutingAssembly().GetTypes().Where(a => a.BaseType == typeof(BaseController));
            //        await menuData.InitMenuData(controllerList);
            //    }
            //    var userData = context.RequestServices.GetService<IUserDataInterFace>();
            //    if (userData != null)
            //    {
            //        await userData.GenerateAdmin();
            //    }
            //    await next();
            //});

        }
    }
}
