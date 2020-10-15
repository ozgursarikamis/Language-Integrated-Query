using System;
using System.Collections.Generic;
using System.Linq;

namespace Queries
{
    internal class Program
    {
        private static void Main()
        {
            var movies = new List<Movie>
            {
                new Movie {Title = "The Dark Night", Rating = 8.9f, Year = 2008},
                new Movie {Title = "The King's Speech", Rating = 8.0f, Year = 2010},
                new Movie {Title = "Casablanca", Rating = 8.5f, Year = 1942},
                new Movie {Title = "Star wars V", Rating = 8.7f, Year = 1980},
            };

            var query = movies.Filter(x => x.Year > 2000);
            //foreach (var movie in query)
            //{
            //    //Console.WriteLine(movie.Title);
            //}

            Console.ReadLine();
        }
    }
}
