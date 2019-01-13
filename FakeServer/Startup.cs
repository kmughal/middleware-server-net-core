namespace FakeServer
{
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Http;
  using Middlewares;

  internal class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseHelloWorldMiddleware();
            app.UseFakeMoviesMiddleware();
            app.Use(async(context,next)=>
            {
                 await context.Response.WriteAsync("Welcome to .net core");
            });
        }

    }
}