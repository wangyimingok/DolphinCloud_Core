﻿using DolphinCloud.Common.Extentions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.ContractResolver
{
    /// <summary>
    /// 时间转换器
    /// </summary>
    public class MicrosecondEpochConverter : DateTimeConverterBase
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) { return null; }
            //return _epoch.AddMilliseconds((long)reader.Value / 1000d);
            long.TryParse(reader.Value.ToString(), out long timestampes);
            long mill = 0;
            if (timestampes <= 0)
            {
                DateTime.TryParse(reader.Value.ToString(), out DateTime currentTime);
                mill = currentTime.GetTimeStampMilliseconds();
            }
            else
            {
                mill = Convert.ToInt64(reader.Value);
            }
            return _epoch.AddMilliseconds(mill);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(((DateTime)value - _epoch).TotalMilliseconds + "");
        }
    }
}
