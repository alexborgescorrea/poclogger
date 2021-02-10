using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace poclogger.CustomLogger
{
    public class SerilogCustomLoggerScope : ICustomLoggerScope
    {        
        public IDisposable Begin(string key, object value)
        {
            return LogContext.PushProperty(key, value);
        }

        public CustomLoggerScopeDisposable Begin(ILogger logger, string key, object value)
        {            
            var scope = LogContext.PushProperty(key, value);
             return new CustomLoggerScopeDisposable(logger, scope);
        }

        public CustomLoggerScopeDisposable Begin(ILogger logger, IDictionary<string, object> values)
        {
            var scopes = values.Select(p => LogContext.PushProperty(p.Key, p.Value)).ToArray();            
             return new CustomLoggerScopeDisposable(logger, scopes);
        }
    }
}