using DolphinCloud.DataInterFace.Base;
using DolphinCloud.DataModel.Base.AudiotLog;
using DolphinCloud.DataModel.Base.SystemLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.AdminWeb.Controllers
{
    /// <summary>
    /// 日志控制器
    /// </summary>
    [Authorize]
    public class LogsController : Controller
    {
        /// <summary>
        /// 审计日志数据接口
        /// </summary>
        private readonly IAuditDataInterFace _auditSvc;
        /// <summary>
        /// 系统日志数据接口
        /// </summary>
        private readonly ISystemLogDataInterFace _systemLogSvc;

        public LogsController(IAuditDataInterFace auditDataInterFace, ISystemLogDataInterFace systemLogDataInterFace)
        {
            _auditSvc = auditDataInterFace;
            _systemLogSvc = systemLogDataInterFace;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 系统日志
        /// </summary>
        /// <returns></returns>
        public IActionResult SystemLog()
        {
            return View();
        }

        /// <summary>
        /// 审计日志
        /// </summary>
        /// <returns></returns>
        public IActionResult AuditLog()
        {
            return View();
        }

        /// <summary>
        /// 分页获取审计日志
        /// </summary>
        /// <param name="searchPagination"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> GetAuditLogPaginationResult([FromBody] AuditLogSearchPagination searchPagination, CancellationToken cancellationToken)
        {
            var result = await _auditSvc.GetAuditLogPaginationResultAsync(searchPagination, cancellationToken);
            return Json(result);
        }

        /// <summary>
        /// 分页搜索系统日志
        /// </summary>
        /// <param name="searchPagination"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> GetSystemLogPaginationResult([FromBody] SystemLogSearchPagination searchPagination, CancellationToken cancellationToken)
        {
            var result = await _systemLogSvc.GetSystemLogPaginationResultAsync(searchPagination, cancellationToken);
            return Json(result);
        }

        /// <summary>
        /// 获取事件类型下拉选项
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet,AllowAnonymous]
        public async Task<JsonResult> GetAuditEventTypeSelectOption(CancellationToken cancellationToken)
        {
            var result = await _auditSvc.GetAuditEventTypeSelectOptionAsync(cancellationToken);
            return Json(result);
        }
    }
}
