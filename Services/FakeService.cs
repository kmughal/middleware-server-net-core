namespace Services
{
    using System.Collections.Generic;
    using Models;
    using System;
     public class FakeMoviesService
    {
        public static FakeMoviesService Instance = new FakeMoviesService();
        private List<Movie> _movies = new List<Movie>
            {
                new Movie { Name = "Escape Room", ReleaseYear = new DateTime(2017, 02, 01), Country = "USA" },
                new Movie { Name = "Fake movie", ReleaseYear = new DateTime(2007, 02, 01), Country = "UK" }
            };
        public List<Movie> GetMovies()
        {
            return _movies;
        }

        public void AddMovie(Movie movie) {
            _movies.Add(movie);
        }
    }
}