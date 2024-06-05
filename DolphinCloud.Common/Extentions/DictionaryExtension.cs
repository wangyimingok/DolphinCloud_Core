using System;
using System.Collections.Generic;
using System.Text;

namespace DolphinCloud.Common.Extentions
{
    /// <summary>
    /// 字典的扩展方法
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 此方法用于尝试获取字典中的值（如果存在）
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="dictionary">The collection object</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value of the key (or default value if key not exists)</param>
        /// <returns>True if key does exists in the dictionary</returns>
        internal static bool TryGetValue<T>(this IDictionary<string, object> dictionary, string key, out T value)
        {
            object valueObj;
            if (dictionary.TryGetValue(key, out valueObj) && valueObj is T)
            {
                value = (T)valueObj;
                return true;
            }

            value = default(T);
            return false;
        }

        /// <summary>
        ///从具有给定键的字典中获取值。 如果找不到则返回默认值。
        /// </summary>
        /// <param name="dictionary">查字典并获取</param>
        /// <param name="key">Key to find the value</param>
        /// <typeparam name="TKey">Type of the key</typeparam>
        /// <typeparam name="TValue">Type of the value</typeparam>
        /// <returns>Value if found, default if can not found.</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue obj;
            return dictionary.TryGetValue(key, out obj) ? obj : default(TValue);
        }

        /// <summary>
        /// 从具有给定键的字典中获取值。 如果找不到则返回默认值。
        /// </summary>
        /// <param name="dictionary">查字典并获取</param>
        /// <param name="key">Key to find the value</param>
        /// <param name="factory">A factory method used to create the value if not found in the dictionary</param>
        /// <typeparam name="TKey">Type of the key</typeparam>
        /// <typeparam name="TValue">Type of the value</typeparam>
        /// <returns>Value if found, default if can not found.</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> factory)
        {
            TValue obj;
            if (dictionary.TryGetValue(key, out obj))
            {
                return obj;
            }

            return dictionary[key] = factory(key);
        }

        /// <summary>
        /// 从具有给定键的字典中获取值。 如果找不到则返回默认值。
        /// </summary>
        /// <param name="dictionary">查字典并获取</param>
        /// <param name="key">Key to find the value</param>
        /// <param name="factory">A factory method used to create the value if not found in the dictionary</param>
        /// <typeparam name="TKey">Type of the key</typeparam>
        /// <typeparam name="TValue">Type of the value</typeparam>
        /// <returns>Value if found, default if can not found.</returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)
        {
            return dictionary.GetOrAdd(key, k => factory());
        }

        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue value)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(new KeyValuePair<TKey, TValue>(key, value));
            }
            else
            {
                @this[key] = value;
            }

            return @this[key];
        }
    }
}
