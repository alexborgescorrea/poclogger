using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace poclogger.CustomLogger
{
    public interface ICustomLoggerScope
    {
        IDisposable Begin(string key, object value);
        CustomLoggerScopeDisposable Begin(ILogger logger, string key, object value);
        CustomLoggerScopeDisposable Begin(ILogger logger, IDictionary<string, object> values);
    }
}