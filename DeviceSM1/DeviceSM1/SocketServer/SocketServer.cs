using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DeviceSM1.SocketServer
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SocketServer
    {
        private readonly RequestDelegate _next;

        public SocketServer(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SocketServerExtensions
    {
        public static IApplicationBuilder UseSocketServer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SocketServer>();
        }
    }
}
