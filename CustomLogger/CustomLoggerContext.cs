using Microsoft.Extensions.DependencyInjection;

namespace poclogger.CustomLogger
{
    public static class CustomLoggerContext
    {
        public static ICustomLoggerScope Scope { get; private set; }
        
        public static void AddCustomLogScope(this IServiceCollection services, ICustomLoggerScope scope)
        {
            Scope = scope;
        }
    }
}