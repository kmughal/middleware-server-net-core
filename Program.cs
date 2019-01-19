namespace IoC.Decorator.Example
{
    using System.Collections.Generic;
    using System.Net.Http.Headers;
    using System.Net.Http;
    using System;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Models;

    using static System.Console;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        const string url = "http://localhost:3000";
        static void Main(string[] args)
        {
            var foo = IocProvider.ServiceProvider.GetService<IFoo>();
            foo.PrintHelloWorld();
            FakeServer.FakeServer.StartServer(url);
            ReadMoviesFromStream();
            ReadMoviesWithOutUsingStreams();
            Task.Delay(100);
            AddNewMovie().GetAwaiter().GetResult();
            Console.Read();
        }

        static async Task<bool> AddNewMovie()
        {
            var addMovieUrl = $"{url}/AddMovie";
            var newMovie = new Movie
            {
                Name = "Transformers",
                Country = "USA",
                ReleaseYear = new DateTime(2007, 04, 23)
            };

            var ms = new MemoryStream();
            ms.WriteObjectToStream(newMovie);
            ms.Position = 0;

            using(var requestMessage = new HttpRequestMessage(HttpMethod.Post, addMovieUrl))
            {
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using(var streamContent = new StreamContent(ms))
                {
                    requestMessage.Content = streamContent;
                    requestMessage.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

                    var response = await client.SendAsync(requestMessage);
                    response.EnsureSuccessStatusCode();

                    var responseContents = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContents);

                }
            }
            return true;
        }

        static async void ReadMoviesWithOutUsingStreams()
        {

            var sw = Stopwatch.StartNew();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{url}/movies");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using(var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var movies = await response.DeserializeResponse<List<Movie>>();
                PrintMovies(movies);
            }
            sw.Stop();
            Console.WriteLine("-------------------------------------------------------------------------");
            PrintStopWatchStats(sw, "Without using stream");
            Console.WriteLine("-------------------------------------------------------------------------");
        }

        static void PrintStopWatchStats(Stopwatch sw, string calledFrom)
        {
            Console.WriteLine($"{calledFrom} -> ElapsedMilliseconds: {sw.ElapsedMilliseconds},Average :{sw.ElapsedMilliseconds / 200}");
        }

        static void PrintMovies(List<Movie> movies)
        {
            foreach (var movie in movies)
            {
                WriteLine(movie.ToString());
            }
        }
        static async void ReadMoviesFromStream()
        {
            var sw = Stopwatch.StartNew();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{url}/movies");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using(var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var movies = stream.ReadResponseFromStreamAndDeserialize<List<Movie>>();
                PrintMovies(movies);
            }
            sw.Stop();
            Console.WriteLine("-------------------------------------------------------------------------");
            PrintStopWatchStats(sw, "Using Stream");
            Console.WriteLine("-------------------------------------------------------------------------");
        }
    }
}