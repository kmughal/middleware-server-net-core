namespace Middlewares
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using Services;

    public class FakeMoviesMiddleware
    {
        private readonly RequestDelegate _next;

        public FakeMoviesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
           System.Console.WriteLine("path variable" , path);
            if (!string.IsNullOrEmpty(path) && path == "/movies")
            {
                var moviesInString = GetMoviesInString();
               context.Response.ContentLength = System.Text.Encoding.UTF8.GetByteCount(moviesInString);
                await context.Response.WriteAsync(moviesInString);
            }
            await _next(context);
        }

        public string GetMoviesInString()
        {
            var movies = FakeMoviesService.Instance.GetMovies();
            var result = JsonConvert.SerializeObject(movies);
            return result;
        }
    }
}