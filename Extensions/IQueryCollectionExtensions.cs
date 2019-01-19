namespace Extensions
{
    using System.Linq;
    using System;
    using Microsoft.AspNetCore.Http;
    using Models;

    public static class IQueryCollectionExtensions
    {
        public static Movie ExtractMovieFromQueryCollection(this IQueryCollection queryCollection)
        {
            if (queryCollection == null || !queryCollection.Any())
                throw new ArgumentNullException(nameof(queryCollection));

            var validKeys = new [] { "name", "country", "releaseyear" };
            var validKeysNotPresent = validKeys.Any(x => !queryCollection.Keys.Contains(x));
            if (validKeysNotPresent)
                throw new NotSupportedException(nameof(queryCollection));

            string name = queryCollection["name"];
            string country = queryCollection["country"];

            DateTime releaseYear;
            if (!DateTime.TryParse(queryCollection["releaseyear"], out releaseYear))
            {
                throw new InvalidTimeZoneException(nameof(releaseYear));
            }

            return new Movie { Name = name, ReleaseYear = releaseYear, Country = country };
        }
    }
}