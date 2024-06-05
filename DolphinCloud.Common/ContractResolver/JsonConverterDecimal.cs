using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.ContractResolver
{
    /// <summary>
    /// Decimal 转换器
    /// </summary>
    public class JsonConverterDecimal : JsonConverter
    {
        /// <summary>
        /// 是否转换成功
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal) || objectType == typeof(decimal?);
        }
        /// <summary>
        /// 读取json
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.ValueType == null && reader.Value == null)
            {
                return null;
            }
            else
            {
                decimal.TryParse(reader.Value != null ? reader.Value.ToString() : "", out decimal value);
                return value;
            }
        }

        /// <summary>
        /// 输出Json
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                writer.WriteValue(value);
            else
                writer.WriteValue(value + string.Empty);
        }
    }
}
