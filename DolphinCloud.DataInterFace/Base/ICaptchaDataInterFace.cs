using DolphinCloud.Common.Enums;
using DolphinCloud.DataModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.DataInterFace.Base
{
    /// <summary>
    /// 验证码接口
    /// </summary>
    public interface ICaptchaDataInterFace
    {
        /// <summary>
        /// 获取验证码
        /// 异步方法
        /// </summary>
        /// <param name="VerifyCodeLength">验证码长度</param>
        /// <param name="type">类型 0：数字 1：字符</param>
        /// <returns></returns>
        Task<VerifyCodeDataModel> CreateVerifyCodeAsync(int VerifyCodeLength, VerifyCodeType type);

        /// <summary>
        /// 获取验证码
        /// 异步方法
        /// </summary>
        /// <param name="VerifyCodeLength">验证码长度</param>
        /// <param name="type">类型 0：数字 1：字符</param>
        /// <returns></returns>
       VerifyCodeDataModel CreateVerifyCode(int VerifyCodeLength, VerifyCodeType type);
    }
}
