using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace poclogger.CustomLogger
{
    public static class CustomLoggerExtension
    {
        public static CustomLoggerScopeDisposable Scope(this ILogger logger, string key, object value)
        {
            return CustomLoggerContext.Scope.Begin(logger, key, value);
        }

        public static CustomLoggerScopeDisposable Scope(this ILogger logger, IDictionary<string, object> values)
        {
            return CustomLoggerContext.Scope.Begin(logger, values);
        }
    }
}