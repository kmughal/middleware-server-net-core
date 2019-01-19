namespace Infrastructure
{
    using System.IO;
    using System.Net.Http.Headers;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System;
    using Extensions;
    using Models;

    public class MovieHttpClient
    {
        public HttpClient Client { get; }

        public MovieHttpClient(HttpClient client)
        {
            Client = client;
            Client.BaseAddress = new Uri("http://localhost:3000");
            Client.Timeout = new TimeSpan(0, 0, 30);
            client.DefaultRequestHeaders.Clear();
        }

        public async Task<bool> AddNewMovie()
        {
            var addMovieUrl = $"/AddMovie";
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
                    requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

                    var response = await Client.SendAsync(requestMessage);
                    response.EnsureSuccessStatusCode();

                    var responseContents = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContents);

                }
            }
            return true;
        }
    }
}