namespace FakeServer
{
    using System.Text;
    using System;
    using Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Middlewares;
    using Models;
    using Services;
    using System.Net;

    internal class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseHelloWorldMiddleware();
            app.UseFakeMoviesMiddleware();
            AddMovieMiddleware(app);
        }

        private void AddMovieMiddleware(IApplicationBuilder app)
        {
            app.Map("/AddMovie", appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    Movie newMovie = null;
                    try
                    {
                        newMovie = context.Request.ReadMovieFromRequest();
                    }
                    catch (NotSupportedException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadGateway;
                        return;
                    }
                    catch (InvalidTimeZoneException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadGateway;
                        return;
                    }
                    FakeMoviesService.Instance.AddMovie(newMovie);
                    await context.Response.WriteAsync($"{newMovie.ToString()} added");
                });
            });
        }
    }
}