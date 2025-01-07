using DolphinCloud.Common.Result;
using DolphinCloud.DataInterFace.Base;
using DolphinCloud.DataModel.Base.SystemLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.AdminWeb.Controllers
{
    /// <summary>
    /// 系统日志
    /// </summary>
    [Authorize]
    public class SystemLogController : Controller
    {
        /// <summary>
        /// 系统日志数据接口
        /// </summary>
        private readonly ISystemLogDataInterFace _systemLogSvc;

        public SystemLogController(ISystemLogDataInterFace systemLogDataInterFace)
        {
            _systemLogSvc = systemLogDataInterFace;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 分页搜索系统日志
        /// </summary>
        /// <param name="searchPagination"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> GetSystemLogPaginationResultAsync([FromBody] SystemLogSearchPagination searchPagination, CancellationToken cancellationToken)
        {
            var result = await _systemLogSvc.GetSystemLogPaginationResultAsync(searchPagination, cancellationToken);
            return Json(result);
        }
    }
}
