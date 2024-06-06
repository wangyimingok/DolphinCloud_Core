using DolphinCloud.Common.Configuration;
using DolphinCloud.Common.Enums;
using DolphinCloud.DataInterFace.Base;
using DolphinCloud.DataModel.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DolphinCloud.OMS.WebApplication.Controllers
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
        public AccountController(ILogger<AccountController> logger, ICaptchaDataInterFace captchaDataInterFace, IHttpContextAccessor httpContextAccessor, IRootConfiguration rootConfiguration)
        {
            _logger = logger;
            _captcha = captchaDataInterFace;
            _httpContext = httpContextAccessor?.HttpContext;
            _jwtTokenConfig = rootConfiguration.AuthenConfiguration.JwtBearerOptions;
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
        public async Task<IActionResult> Login(LoginDataModel loginData)
        {
            AuthenticationProperties properties = null;
            if (loginData.RememberMe)
            {
                properties = new AuthenticationProperties
                {
                    //是否记住我 持久化Cookie
                    IsPersistent = loginData.RememberMe,
                    //登录信息15天有效
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(15),
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
                                        new Claim(ClaimTypes.NameIdentifier, "123456"),
                                        //加入用户邮箱声明
                                        new Claim(ClaimTypes.Email, "123456@123.com"),
                                        //加入用户名声明
                                        new Claim(ClaimTypes.Name,loginData.UserName),
                                        //加入用户手机号码声明
                                    new Claim(ClaimTypes.MobilePhone,"TestPhone")
                                    // new Claim(ClaimTypes.PrimarySid,UserData.Phone)
                                    };
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(identity);
            try
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, properties);
                //return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "登录验证出现异常");
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtTokenConfig.SecretKey)
            );
            var signCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtTokenConfig.Issuer,
                audience: _jwtTokenConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtTokenConfig.ExpireMinutes),
                signingCredentials: signCredential
            );

            return Ok(new
            {
                responseCode = 200,
                access_token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expiration = TimeZoneInfo.ConvertTimeFromUtc(jwtToken.ValidTo, TimeZoneInfo.Local)
            });
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
    }
}
