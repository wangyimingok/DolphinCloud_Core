using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.ContractResolver
{
    /// <summary>
    /// Double类型转换器
    /// </summary>
    public class JsonConverterDouble : JsonConverter
    {
        /// <summary>
        /// 是否可以转换
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(double) || objectType == typeof(double?);
        }

        /// <summary>
        /// 读取原始Json
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
                double.TryParse(reader.Value != null ? reader.Value.ToString() : "", out double value);
                return value;
            }

        }
        /// <summary>
        /// 写入格式化之后的Json
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)

                writer.WriteValue(value);
            else
                writer.WriteValue(value + "");
        }
    }
}
