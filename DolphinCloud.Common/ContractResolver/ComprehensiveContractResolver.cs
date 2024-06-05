using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.ContractResolver
{ 
    /// <summary>
  /// 综合Json处理类
  /// </summary>
    public class ComprehensiveContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// 对长整型做处理
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (objectType == typeof(long))
            {
                return new JsonConverterLong();
            }
            if (objectType == typeof(decimal))
            {
                return new JsonConverterDecimal();
            }
            if (objectType==typeof(double))
            {
                return new JsonConverterDouble();
            }
            return base.ResolveContractConverter(objectType);
        }

    }
}
