using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DolphinCloud.Common.Extentions
{
    public static class CoreExtension
    {
#nullable enable
        private static readonly ConcurrentDictionary<Type, object?> DefaultValues =
        new();

        public static object? GetDefaultValue(this Type type)
        {
            Guard.NotNull(type, nameof(type));
            return type.IsValueType && type != typeof(void)
                ? DefaultValues.GetOrAdd(type, Activator.CreateInstance)
                : null;
        }

        public static object? ToOrDefault(this object? @this, Type type)
        {
            Guard.NotNull(type, nameof(type));
            try
            {
                return @this.To(type);
            }
            catch (Exception)
            {
                return type.GetDefaultValue();
            }
        }

        public static object? To(this object? @this, Type type)
        {
            if (@this == null || @this == DBNull.Value)
            {
                return null;
            }

            var targetType = type.Unwrap();
            var sourceType = @this.GetType().Unwrap();

            if (sourceType == targetType)
            {
                return @this;
            }

            var converter = TypeDescriptor.GetConverter(sourceType);
            if (converter.CanConvertTo(targetType))
            {
                return converter.ConvertTo(@this, targetType);
            }

            converter = TypeDescriptor.GetConverter(targetType);
            if (converter.CanConvertFrom(sourceType))
            {
                return converter.ConvertFrom(@this);
            }

            return Convert.ChangeType(@this, targetType);
        }

        public static Type Unwrap(this Type type)
      => Nullable.GetUnderlyingType(Guard.NotNull(type, nameof(type))) ?? type;

        /// <summary>
        /// 转换标准日期时间格式
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToStandardTimeString(this DateTime @this)
        {
            return @this.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转换标准日期格式
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToStandardDateString(this DateTime @this)
        {
            return @this.ToString("yyyy-MM-dd");
        }
    }
}
