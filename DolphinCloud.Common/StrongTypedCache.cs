using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common
{
    internal static class StrongTypedCache<T>
    {
#nullable enable
        public static readonly ConcurrentDictionary<PropertyInfo, Func<T, object?>?> PropertyValueGetters = new();

        public static readonly ConcurrentDictionary<PropertyInfo, Action<T, object?>?> PropertyValueSetters = new();
#nullable restore
    }
}
