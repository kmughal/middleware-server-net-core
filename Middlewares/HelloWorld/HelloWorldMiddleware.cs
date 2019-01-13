namespace Middlewares
{
    using System;
  using System.Threading.Tasks;
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
            System.Console.WriteLine("path variable is :" , path);
           
            if (!string.IsNullOrEmpty(path) && path == "/")
            {
                string message = $"Hello World {DateTime.Now.ToString()}";
                context.Response.ContentLength = System.Text.Encoding.UTF8.GetByteCount(message);
                await context.Response.WriteAsync(message);
            }
            await _next(context);
        }

    }
}