using DolphinCloud.Common.Attributes;
using DolphinCloud.Common.Enums;
using DolphinCloud.DataModel.System.User;
using DolphinCloud.OMS.WebApplication.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DolphinCloud.OMS.WebApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// 用户信息控制器
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// 用户信息首页
        /// </summary>
        /// <returns></returns>
        [Menu(MunuType.RootMenu, "用户管理", 98, "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [Menu(MunuType.Button_Function, "创建用户", 2, "Admin")]
        [HttpPost, ValidateAntiForgeryToken]        
        public IActionResult Create(UserCreateDataModel dataModel)
        {
            return new JsonResult("");
        }
    }
}
