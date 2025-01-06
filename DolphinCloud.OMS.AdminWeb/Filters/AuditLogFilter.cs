using DolphinCloud.Common.Attributes;
using DolphinCloud.Framework.Session;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;
using DolphinCloud.Common.Extentions;
using DolphinCloud.DataInterFace.Base;
using DolphinCloud.DataModel.Base.AudiotLog;

namespace DolphinCloud.OMS.AdminWeb.Filters
{
    /// <summary>
    /// 审计日志过滤器
    /// </summary>
    public class AuditLogFilter : IAsyncActionFilter
    {
        /// <summary>
        /// 审计日志记录接口
        /// </summary>
        private readonly IAuditDataInterFace _audit;

        /// <summary>
        /// 日志记录器
        /// </summary>
        private readonly ILogger<AuditLogFilter> _logger;

        /// <summary>
        /// 当前用户信息
        /// </summary>
        private readonly ICurrentUserInfo _currentUser;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="audit"></param>
        /// <param name="logger"></param>
        /// <param name="currentUser"></param>
        public AuditLogFilter(IAuditDataInterFace audit, ILogger<AuditLogFilter> logger, ICurrentUserInfo currentUser)
        {
            _audit = audit;
            _logger = logger;
            _currentUser = currentUser;
        }

        /// <summary>
        /// 执行动作
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 判断是否写日志
            if (!ShouldSaveAudit(context))
            {
                await next();
                return;
            }
            //接口Type
            var type = (context.ActionDescriptor as ControllerActionDescriptor).ControllerTypeInfo.AsType();
            //方法信息
            var method = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo;
            //方法参数
            var arguments = context.ActionArguments;
            //开始计时
            var stopwatch = Stopwatch.StartNew();

            var auditInfo = new AuditLogCreateDataModel
            {
                MethodName = method.Name,
                Parameters = ConvertArgumentsToJson(arguments),
                ExecutionTime = DateTime.Now,
                ExecutionDuration = 0,
                ReturnValue = "",
                ServiceName = type != null ? type.FullName.TruncateWithPostfix(80) : "",
                UserID = _currentUser.UserID + string.Empty,
                UserName = _currentUser.UserName
            };
            if (type != null && type.GetCustomAttribute<AuditedAttribute>() != null)
            {
                auditInfo.EventType = type.GetCustomAttribute<AuditedAttribute>()?.EventType;
            }
            else if (method != null && method.GetCustomAttribute<AuditedAttribute>() != null)
            {
                auditInfo.EventType = method.GetCustomAttribute<AuditedAttribute>()?.EventType;
            }
            ActionExecutedContext result = null;
            try
            {
                result = await next();
                if (result.Exception != null && !result.ExceptionHandled)
                {
                    auditInfo.Exception = result.Exception.ToJson();
                }
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex.ToJson();
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);

                if (result != null)
                {
                    switch (result.Result)
                    {
                        case ObjectResult objectResult:
                            auditInfo.ReturnValue = JsonConvert.SerializeObject(objectResult.Value);
                            break;

                        case JsonResult jsonResult:
                            auditInfo.ReturnValue = JsonConvert.SerializeObject(jsonResult.Value);
                            break;

                        case ContentResult contentResult:
                            auditInfo.ReturnValue = contentResult.Content;
                            break;
                    }
                }
                Console.WriteLine(auditInfo.ToString());
                auditInfo.ReturnValue = auditInfo.ReturnValue.TruncateWithPostfix(auditInfo.ReturnValue.Length);
                //保存审计日志
                await _audit.AddAuditLogs(auditInfo);
            }
        }

        /// <summary>
        /// 是否记录审计日志
        /// </summary>
        /// <param name="context"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static bool ShouldSaveAudit(ActionExecutingContext context, bool defaultValue = false)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor))
                return false;
            var methodInfo = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo;

            if (methodInfo == null)
            {
                return false;
            }

            if (!methodInfo.IsPublic)
            {
                return false;
            }

            if (methodInfo.IsDefined(typeof(AuditedAttribute), true))
            {
                return true;
            }

            if (methodInfo.IsDefined(typeof(DisableAuditingAttribute), true))
            {
                return false;
            }

            var classType = methodInfo.DeclaringType;
            if (classType != null)
            {
                if (classType.GetTypeInfo().IsDefined(typeof(AuditedAttribute), true))
                {
                    return true;
                }

                if (classType.GetTypeInfo().IsDefined(typeof(DisableAuditingAttribute), true))
                {
                    return false;
                }
            }
            return defaultValue;
        }

        private string ConvertArgumentsToJson(IDictionary<string, object> arguments)
        {
            try
            {
                if (arguments == null && arguments.Count == 0)
                {
                    return "{}";
                }

                var dictionary = new Dictionary<string, object>();

                foreach (var argument in arguments)
                {
                    dictionary[argument.Key] = argument.Value;
                }

                return JsonConvert.SerializeObject(dictionary);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString(), ex);
                return "{}";
            }
        }
    }
}
