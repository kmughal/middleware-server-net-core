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
                    Movie newMovie = context.Request.ReadMovieFromRequest();
                    FakeMoviesService.Instance.AddMovie(newMovie);
                    await context.Response.WriteAsync($"{newMovie.ToString()} added");
                });
            });
        }
    }
}