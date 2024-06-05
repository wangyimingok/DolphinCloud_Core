using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DolphinCloud.Common.Extentions
{
    /// <summary>
    /// 枚举扩展方法类
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescriptionForEnum(this object value)
        {
            try
            {
                if (value == null) return string.Empty;
                var type = value.GetType();
                var field = type.GetField(System.Enum.GetName(type, value));

                if (field == null) return value.ToString();

                var des = CustomAttributeData.GetCustomAttributes(type.GetMember(field.Name)[0]);

                return des.Any() && des[0].ConstructorArguments.Any()
                    ? des[0].ConstructorArguments[0].Value.ToString()
                    : value.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDisplayForEnum(this object value)
        {
            try
            {
                if (value == null) return string.Empty;
                var type = value.GetType();
                var field = type.GetField(System.Enum.GetName(type, value));

                if (field == null) return value.ToString();

                var des = CustomAttributeData.GetCustomAttributes(type.GetMember(field.Name)[0]);

                return des.Any() && des[0].NamedArguments.Any()
                    ? des[0].NamedArguments[0].TypedValue.Value.ToString()
                    : value.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        ///     获取枚举项的Description特性的描述文字
        /// </summary>
        /// <param name="value">枚举对象</param>
        /// <returns> </returns>
        public static string ToDescription(this System.Enum value)
        {
            if (value == null)
                return string.Empty;
            return GetDescriptionForEnum(value);
        }

        /// <summary>
        ///     获取枚举项的Description特性的描述文字
        /// </summary>
        /// <param name="value">枚举对象</param>
        /// <returns> </returns>
        public static string ToDisplay(this System.Enum value)
        {
            if (value == null)
                return string.Empty;
            return GetDisplayForEnum(value);
        }

        
    }
}
