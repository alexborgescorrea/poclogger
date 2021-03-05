using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace poclogger.CustomLogger
{
    public class CustomLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Action<HttpContext, Dictionary<string, object>> _act;

        public CustomLoggerMiddleware(RequestDelegate next, 
                                       Action<HttpContext, Dictionary<string, object>> act)
        {
            _next = next;
            _act = act;
        }

        public async Task Invoke(HttpContext httpCtx)
        {
            var props = new Dictionary<string, object>();
            _act(httpCtx, props);         
            var scopes = props.Select(p => CustomLoggerContext.Scope.Begin(p.Key, p.Value)).ToList();                                

            try
            {
                await _next(httpCtx);
            }
            finally
            {
                scopes.Reverse();
                scopes.ForEach(p => p.Dispose());
            }                       
        }
    }
}