namespace Middlewares
{
    using System.Threading.Tasks;
    using System;
    using Microsoft.AspNetCore.Http;

    public class HelloWorldMiddleware
    {
        private readonly RequestDelegate _next;

        public HelloWorldMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;

            if (!string.IsNullOrEmpty(path) && path == "/")
            {
                string message = $"Hello World {DateTime.Now.ToString()}";
                context.Response.ContentLength = System.Text.Encoding.UTF8.GetByteCount(message);
                await context.Response.WriteAsync(message);
                return;
            }
            await _next(context);
        }

    }
}