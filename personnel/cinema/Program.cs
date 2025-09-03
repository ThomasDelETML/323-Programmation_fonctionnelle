using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cinema
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<movie> frenchMovies = new List<movie>() {
            new movie() { Title = "Le fabuleux destin d'Amélie Poulain", Genre = "Comédie", Rating = 8.3, Year = 2001, LanguageOptions = new string[] {"Français", "English"}, StreamingPlatforms = new string[] {"Netflix", "Hulu"} },
            new movie() { Title = "Intouchables", Genre = "Comédie", Rating = 8.5, Year = 2011, LanguageOptions = new string[] {"Français"}, StreamingPlatforms = new string[] {"Netflix", "Amazon"} },
            new movie() { Title = "The Matrix", Genre = "Science-Fiction", Rating = 8.7, Year = 1999, LanguageOptions = new string[] {"English", "Español"}, StreamingPlatforms = new string[] {"Hulu", "Amazon"} },
            new movie() { Title = "La Vie est belle", Genre = "Drame", Rating = 8.6, Year = 1946, LanguageOptions = new string[] {"Français", "Italiano"}, StreamingPlatforms = new string[] {"Netflix"} },
            new movie() { Title = "Gran Torino", Genre = "Drame", Rating = 8.2, Year = 2008, LanguageOptions = new string[] {"English"}, StreamingPlatforms = new string[] {"Hulu"} },
            new movie() { Title = "La Haine", Genre = "Drame", Rating = 8.1, Year = 1995, LanguageOptions = new string[] {"Français"}, StreamingPlatforms = new string[] {"Netflix"} },
            new movie() { Title = "Oldboy", Genre = "Thriller", Rating = 5/*8.4*/, Year = 2003, LanguageOptions = new string[] {"Coréen", "English"}, StreamingPlatforms = new string[] {"Amazon"} }
            };

            Console.WriteLine("Exo 1");

            // Exo 1
            List<movie> ComDraMovie = frenchMovies
                .Where(movie => movie.Genre != "Comédie" && movie.Genre != "Drame")
                .ToList();

            foreach (var movie in ComDraMovie)
            {
                Console.WriteLine($"Title : {movie.Title}");
            }

            // Espace
            Console.WriteLine("\n\nExo 2");

            // Exo 2
            List<movie> BadMovie = frenchMovies
                .Where(movie => movie.Rating < 7)
                .ToList();

            foreach (var movie in BadMovie)
            {
                Console.WriteLine($"Title : {movie.Title}");
            }

            // Espace
            Console.WriteLine("\n\nExo 3");

            // Exo 3
            List<movie> OldMovie = frenchMovies
                .Where (movie => movie.Year < 2000) 
                .ToList();

            foreach (var movie in OldMovie)
            {
                Console.WriteLine($"Title : {movie.Title}");
            }

            // Espace
            Console.WriteLine("\n\nExo 4");

            // Exo 4
            List<movie> ForeignMovie = frenchMovies
                .Where(movie => !movie.LanguageOptions.Contains("Français"))
                .ToList();

            foreach (var movie in ForeignMovie)
            {
                Console.WriteLine($"Title : {movie.Title}");
            }

            // Espace
            Console.WriteLine("\n\nExo 5");

            // Exo 5
            List<movie> NotflixMovie = frenchMovies
                .Where(movie => !movie.StreamingPlatforms.Contains("Netflix"))
                .ToList();

            foreach (var movie in ForeignMovie)
            {
                Console.WriteLine($"Title : {movie.Title}");
            }

            // Espace
            Console.WriteLine("\n\nVersion 2");

            // Version 2
            List<movie> CumulMovie = frenchMovies
                .Where(movie => movie.Genre != "Comédie" && movie.Genre != "Drame")
                .Where(movie => movie.Rating < 7)
                .Where(movie => movie.Year < 2000)
                .Where(movie => !movie.LanguageOptions.Contains("Français"))
                .Where(movie => !movie.StreamingPlatforms.Contains("Netflix"))
                .ToList();

            foreach (var movie in CumulMovie)
            {
                Console.WriteLine($"Title : {movie.Title}");
            }

            // Espace
            Console.WriteLine("\n\nVersion 3");

            // Version 3
            Console.WriteLine("Vos filtres...");
            Console.Write("Genre : ");
            string UserGenre = Console.ReadLine();

            Console.Write("Rating(min) : ");
            double UserRating = int.Parse(Console.ReadLine());

            Console.Write("Annee : ");
            int UserYear = int.Parse(Console.ReadLine());

            Console.Write("Doublage : ");
            string UserDub = Console.ReadLine();

            Console.Write("Fournisseur : ");
            string UserStream = Console.ReadLine();

            List<movie> UserMovie = frenchMovies
                .Where(movie => movie.Genre == UserGenre)
                .Where(movie => movie.Rating >= UserRating)
                .Where(movie => movie.Year >= UserYear)
                .Where(movie => movie.LanguageOptions.Contains(UserDub))
                .Where(movie => movie.StreamingPlatforms.Contains(UserStream))
                .ToList();

            foreach (var movie in UserMovie)
            {
                Console.WriteLine($"Title : {movie.Title}");
            }

            // Espace
            Console.WriteLine("\n\nVersion 3+");

            // Version 3 (avec types Action/func)
            List<Func<List<movie>, List<movie>>> filters = new List<Func<List<movie>, List<movie>>>();

            // Filtre Genre
            if (!string.IsNullOrWhiteSpace(UserGenre))
            {
                filters.Add(movies => movies.Where(m => m.Genre == UserGenre).ToList());
            }
        }
    }
}
