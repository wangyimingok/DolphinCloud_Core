using DolphinCloud.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.WebApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [Area("Admin")]
    [Authorize(Policy = PermissionPolicy.AdminArea)]
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
