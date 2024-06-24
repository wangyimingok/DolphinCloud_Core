using DolphinCloud.Common.Configuration;
using DolphinCloud.Common.Enums;
using DolphinCloud.Common.Result;
using DolphinCloud.DataInterFace.Base;
using DolphinCloud.DataInterFace.System;
using DolphinCloud.DataModel.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DolphinCloud.OMS.AdminWeb.Controllers
{
    /// <summary>
    /// 账号控制器
    /// </summary>
    public class AccountController : BaseController
    {
        /// <summary>
        /// 验证码接口
        /// </summary>
        private readonly ICaptchaDataInterFace _captcha;
        /// <summary>
        /// http上下文
        /// </summary>
        private readonly HttpContext _httpContext;
        /// <summary>
        /// JWT Token配置
        /// </summary>
        private readonly JwtBearerOptions _jwtTokenConfig;
        /// <summary>
        /// 日志记录接口
        /// </summary>
        private readonly ILogger<AccountController> _logger;

        private readonly IUserDataInterFace _userData;
        public AccountController(ILogger<AccountController> logger, ICaptchaDataInterFace captchaDataInterFace, IHttpContextAccessor httpContextAccessor, IRootConfiguration rootConfiguration, IUserDataInterFace userDataInterFace)
        {
            _logger = logger;
            _captcha = captchaDataInterFace;
            _httpContext = httpContextAccessor?.HttpContext;
            _jwtTokenConfig = rootConfiguration.AuthenConfiguration.JwtBearerOptions;
            _userData = userDataInterFace;
        }


        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login()
        {
            // _httpContextAccessor.HttpContext.Session
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> Login(LoginDataModel loginData)
        {
            try
            {
                var result = await _userData.LoginValidateAsync(loginData);
                if (result.Code == ResponseCode.OperationSuccess)
                {
                    var userData = result.Data;
                    if (userData != null)
                    {
                        AuthenticationProperties properties = null;
                        if (loginData.RememberMe)
                        {
                            properties = new AuthenticationProperties
                            {
                                //是否记住我 持久化Cookie
                                IsPersistent = loginData.RememberMe,
                                //登录信息8小时有效
                                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                                //身份验证时间
                                IssuedUtc = DateTime.UtcNow,
                                //是否应允许刷新身份验证会话
                                AllowRefresh = true,
                                //登录完成后跳转地址
                                //RedirectUri = loginData.ReturnURLAddress
                            };
                        }
                        //配置用户的身份特征 
                        Claim[] claims = new Claim[] {
                                        //加入用户ID声明
                                        new Claim(ClaimTypes.NameIdentifier, userData.UserID.ToString()),
                                        //加入用户邮箱声明
                                        new Claim(ClaimTypes.Email, userData.EMailAddress),
                                        //加入用户名声明
                                        new Claim(ClaimTypes.Name,userData.UserName),
                                        //加入用户手机号码声明
                                    new Claim(ClaimTypes.MobilePhone,userData.MobileNumber),
                                    new Claim(ClaimTypes.GivenName,userData.RealName)
                                    // new Claim(ClaimTypes.PrimarySid,UserData.Phone)
                                    };
                        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
                        claimsPrincipal.AddIdentity(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, properties);
                        return new JsonResult(new OperationMessage(ResponseCode.OperationSuccess, "登录成功"));
                    }
                    else
                    {
                        return new JsonResult(new OperationMessage(ResponseCode.OperationWarning, $"登录失败,失败原因为;【未获得相关用户信息】"));
                    }
                }
                else
                {
                    return new JsonResult(new OperationMessage(ResponseCode.OperationWarning, $"登录失败,失败原因为;【{result.Message}】"));
                }
                //return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "登录验证出现异常");
                return new JsonResult(new OperationMessage(ResponseCode.ServerError, $"登录验证出现异常,异常原因为:【{ex.Message}】"));
            }
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous, HttpGet]
        public async Task<IActionResult> GetCaptchaCode()
        {
            var VerifyDataModel = await _captcha.CreateVerifyCodeAsync(4, VerifyCodeType.CHAR);
            //_httpContext.Session.SetString("CaptchaCode", VerifyDataModel.Code);
            return File(VerifyDataModel.Image, "image/gif");
        }

        /// <summary>
        ///无权限页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> AccessDenied()
        {
            return await Task.FromResult(View());
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous, HttpGet]
        public async Task<JsonResult> LoginOut()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            var result = new OperationMessage(ResponseCode.LoginOut, "注销成功");
            return Json(result);
            //return RedirectToAction("Console", "Home");
        }
    }
}
