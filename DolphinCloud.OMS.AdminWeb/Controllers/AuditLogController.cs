using DolphinCloud.DataInterFace.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.AdminWeb.Controllers
{
    /// <summary>
    /// 审计日志
    /// </summary>
    [Authorize]
    public class AuditLogController : Controller
    {
        /// <summary>
        /// 审计日志数据接口
        /// </summary>
        private readonly IAuditDataInterFace _auditSvc;

        public AuditLogController(IAuditDataInterFace auditDataInterFace)
        {
            _auditSvc = auditDataInterFace;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
