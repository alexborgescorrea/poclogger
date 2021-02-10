using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace poclogger.CustomLogger
{
    public static class CustomLoggerMiddleware
    {
        public static void UseCustomLoggerProperties(this IApplicationBuilder app, Action<IDictionary<string, object>> act)
        {                                    
            app.UseCustomLoggerProperties((httpCtx, props) => act(props));
        }
        
        public static void UseCustomLoggerProperties(this IApplicationBuilder app, Action<HttpContext, IDictionary<string, object>> act)
        {
            app.Use(async (httpCtx, next) =>        
            {                    
                var props = new Dictionary<string, object>();
                act(httpCtx, props);         
                var scopes = props.Select(p => CustomLoggerContext.Scope.Begin(p.Key, p.Value)).ToList();                                

                try
                {
                    await next();
                }
                finally
                {
                    scopes.Reverse();
                    scopes.ForEach(p => p.Dispose());
                }
            });    
        }        
    }
}