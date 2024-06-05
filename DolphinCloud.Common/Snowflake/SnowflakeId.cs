using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Snowflake
{
    /// <summary>
    /// 雪花Id,全局唯一,性能高,取代GUID
    /// </summary>
    public struct SnowflakeId
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">long形式ID</param>
        public SnowflakeId(long id)
        {
            Id = id;
            var numBin = Convert.ToString(Id, 2).PadLeft(64, '0');
            long timestamp = Convert.ToInt64(new string(numBin.Copy(1, 41).ToArray()), 2) + IdWorker.Twepoch;
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(timestamp);
            Time = dateTime.ToLocalTime();
        }

        /// <summary>
        /// 获取long形式Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Id时间
        /// </summary>
        public DateTime Time { get; }

        /// <summary>
        /// 转为string形式Id
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
