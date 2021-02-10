using Microsoft.Extensions.Logging;

namespace poclogger.CustomLogger
{
    public static class CustoLoggerScopeDisposableExtension
    {
        public static void LogCritical(this CustomLoggerScopeDisposable scope, string msg, params object[] objs)
        {
            using (scope)
            {                                
                scope.Logger.LogCritical(msg, objs);
            }
        }

        public static void LogDebug(this CustomLoggerScopeDisposable scope, string msg, params object[] objs)
        {
            using (scope)
            {
                scope.Logger.LogDebug(msg, objs);
            }
        }

        public static void LogError(this CustomLoggerScopeDisposable scope, string msg, params object[] objs)
        {
            using (scope)
            {
                scope.Logger.LogError(msg, objs);
            }
        }

        public static void LogInformation(this CustomLoggerScopeDisposable scope, string msg, params object[] objs)
        {
            using (scope)
            {
                scope.Logger.LogInformation(msg, objs);
            }
        }

        public static void LogTrace(this CustomLoggerScopeDisposable scope, string msg, params object[] objs)
        {
            using (scope)
            {
                scope.Logger.LogTrace(msg, objs);
            }
        }

        public static void LogWarning(this CustomLoggerScopeDisposable scope, string msg, params object[] objs)
        {
            using (scope)
            {
                scope.Logger.LogWarning(msg, objs);
            }
        }
    }
}