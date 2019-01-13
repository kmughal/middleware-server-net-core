namespace Services
{
    using System.Collections.Generic;
    using Models;
    using System;
     public class FakeMoviesService
    {
        public static FakeMoviesService Instance = new FakeMoviesService();
        public List<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Name = "Escape Room", RelaseYear = new DateTime(2017, 02, 01), Country = "USA" },
                new Movie { Name = "Fake movie", RelaseYear = new DateTime(2007, 02, 01), Country = "UK" }
            };
        }
    }
}