using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Attributes
{
    /// <summary>
    /// 审计特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class AuditedAttribute : Attribute
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// 实例化一个<see cref="AuditedAttribute"/>
        /// </summary>
        /// <param name="eventType"></param>
        public AuditedAttribute(string eventType)
        {
            this.EventType = eventType;
        }
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public AuditedAttribute()
        { }
    }
}
