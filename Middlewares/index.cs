namespace Middlewares
{

    using Microsoft.AspNetCore.Builder;

    public static class index
    {
        public static IApplicationBuilder UseFakeMoviesMiddleware(this IApplicationBuilder appBuilder)
        {
            return appBuilder.UseMiddleware<FakeMoviesMiddleware>();
        }

        public static IApplicationBuilder UseHelloWorldMiddleware(this IApplicationBuilder appBuilder) {
            return appBuilder.UseMiddleware<HelloWorldMiddleware>();
        }
    }
}