using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace poclogger.CustomLogger
{
    public static class CustomLoggerMiddlewareExtension
    {
        public static void UseCustomLoggerProperties(this IApplicationBuilder app, Action<Dictionary<string, object>> act)
        {                                    
            app.UseCustomLoggerProperties((httpCtx, props) => act(props));
        }
        
        public static void UseCustomLoggerProperties(this IApplicationBuilder app, Action<HttpContext, Dictionary<string, object>> act)
        {
            app.UseMiddleware<CustomLoggerMiddleware>(act);    
        }        
    }
}