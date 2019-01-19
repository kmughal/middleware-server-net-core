namespace Extensions
{
    using Microsoft.AspNetCore.Http;
    using Models;

    public static class HttpRequestExtensions
    {
        public static Movie ReadMovieFromRequest(this HttpRequest request)
        {
            var query = request.Query;
            var body = request.Body;

            var method = request.Method.ToUpperInvariant();
            var newMovie = new Movie();

            if (method == "GET")
                newMovie = query.ExtractMovieFromQueryCollection();
            else if (method == "POST")
                newMovie = body.ReadResponseFromStreamAndDeserialize<Movie>();
            return newMovie;
        }
    }
}