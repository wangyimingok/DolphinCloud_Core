using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Extentions
{
    /// <summary>
    /// 时间扩展方法
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// 【秒级】生成10位时间戳（北京时间）
        /// </summary>
        /// <param name="dt">时间</param>
        public static long GetTimeStampSeconds(this DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            return Convert.ToInt64((dt - dateStart).TotalSeconds);
        }
        /// <summary>
        /// 【秒级】获取时间（北京时间）
        /// </summary>
        /// <param name="timestamp">10位时间戳</param>
        public static DateTime GetDateTimeSeconds(this long timestamp)
        {
            long begtime = timestamp * 10000000;
            DateTime dt_1970 = new DateTime(1970, 1, 1, 8, 0, 0);
            long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度
            long time_tricks = tricks_1970 + begtime;//日志日期刻度
            DateTime dt = new DateTime(time_tricks);//转化为DateTime
            return dt;
        }

        /// <summary>
        /// 【毫秒级】获取时间（北京时间）
        /// </summary>
        /// <param name="timestamp">10位时间戳</param>
        public static DateTime GetDateTimeMilliseconds(this long timestamp)
        {
            long begtime = timestamp * 10000;
            DateTime dt_1970 = new DateTime(1970, 1, 1, 8, 0, 0);
            long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度
            long time_tricks = tricks_1970 + begtime;//日志日期刻度
            DateTime dt = new DateTime(time_tricks);//转化为DateTime
            return dt;
        }
        /// <summary>
        /// 【毫秒级】生成13位时间戳（北京时间）
        /// </summary>
        /// <param name="dt">时间</param>
        public static long GetTimeStampMilliseconds(this DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            return Convert.ToInt64((dt - dateStart).TotalMilliseconds);
        }/// <summary>
         /// 【秒级】获取时间（格林威治时间）
         /// </summary>
         /// <param name="timestamp">10位时间戳</param>
        public static DateTime GetUnixDateTimeSeconds(this long timestamp)
        {
            long begtime = timestamp * 10000000;
            DateTime dt_1970 = new DateTime(1970, 1, 1, 0, 0, 0);
            long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度
            long time_tricks = tricks_1970 + begtime;//日志日期刻度
            DateTime dt = new DateTime(time_tricks);//转化为DateTime
            return dt;
        }
        /// <summary>
        /// 【秒级】生成10位时间戳（格林威治时间）
        /// </summary>
        /// <param name="dt">时间</param>
        public static long GetUnixTimeStampSeconds(this DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 0, 0, 0);
            return Convert.ToInt64((dt - dateStart).TotalSeconds);
        }

        /// <summary>
        /// 【毫秒级】获取时间（格林威治时间）
        /// </summary>
        /// <param name="timestamp">10位时间戳</param>
        public static DateTime GetUnixDateTimeMilliseconds(this long timestamp)
        {
            long begtime = timestamp * 10000;
            DateTime dt_1970 = new DateTime(1970, 1, 1, 0, 0, 0);
            long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度
            long time_tricks = tricks_1970 + begtime;//日志日期刻度
            DateTime dt = new DateTime(time_tricks);//转化为DateTime
            return dt;
        }
        /// <summary>
        /// 【毫秒级】生成13位时间戳（格林威治时间）
        /// </summary>
        /// <param name="dt">时间</param>
        public static long GetUnixTimeStampMilliseconds(this DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 0, 0, 0);
            return Convert.ToInt64((dt - dateStart).TotalMilliseconds);
        }
    }
}
