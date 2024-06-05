using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common.Extentions
{
#nullable enable
    public static class EnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> ts, Action<T> action)
        {
            foreach (var t in ts)
            {
                action(t);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> ts, Action<T, int> action)
        {
            var i = 0;
            foreach (var t in ts)
            {
                action(t, i);
                i++;
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> ts, Func<T, Task> action)
        {
            foreach (var t in ts)
            {
                await action(t);
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> ts, Func<T, int, Task> action)
        {
            var i = 0;
            foreach (var t in ts)
            {
                await action(t, i);
                i++;
            }
        }
        public static string StringJoin<T>(this IEnumerable<T> @this, string separator)
        {
            return string.Join(separator, @this);
        }

        [MemberNotNull]
        [return: NotNull]
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : class
        => Guard.NotNull(source).Where(x => x != null)!;

        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query, bool condition, Func<T, bool> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }

        public static IEnumerable<T> TakeIf<T, TKey>(this IEnumerable<T> query, Func<T, TKey> orderBy, bool condition, int limit, bool orderByDescending = true)
        {
            // It is necessary sort items before it
            query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

            return condition
                ? query.Take(limit)
                : query;
        }
    }
}
