using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DolphinCloud.Common
{
    public static class Guard
    {
#nullable enable
        [return: NotNull]
        public static T NotNull<T>([NotNull] T? t,
           [CallerArgumentExpression(nameof(t))]
            string? paramName = default)
        {
#if NET6_0_OR_GREATER
            ArgumentNullException.ThrowIfNull(t, paramName);
#else
        if (t is null)
        {
            throw new ArgumentNullException(paramName);
        }
#endif
            return t;
        }
#nullable restore
    }
}
