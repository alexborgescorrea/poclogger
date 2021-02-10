using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace poclogger.CustomLogger
{
    public class CustomLoggerScopeDisposable : IDisposable
    {
        private readonly IDisposable[] _scopes;
        public ILogger Logger { get; }

        public CustomLoggerScopeDisposable(ILogger logger,
                                          IDisposable[] scopes)
        {
            _scopes = scopes;
            Logger = logger;
        }

        public CustomLoggerScopeDisposable(ILogger logger, 
                                          IDisposable scope)
            : this(logger, new [] { scope })
        {            
        }

        public void Dispose()
        {
            if (_scopes == null)
                return;

            foreach (var scope in _scopes.Reverse())
            {
                scope.Dispose();
            }
        }
    }
}