using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DolphinCloud.Common.Extentions
{
    /// <summary>
    /// Json扩展
    /// </summary>
    public static class JsonExtentions
    {
        /// <summary>
        /// 对象转换Json字符串
        /// </summary>
        /// <param name="obj">传入参数 <see cref="object"/>类型</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
