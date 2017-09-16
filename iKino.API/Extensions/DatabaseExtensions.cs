using iKino.API.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iKino.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task EnsureSeedAsync(this CinemaContext cinema)
        {
            if (await cinema.Movies.AnyAsync())
            {
                return;
            }

            var actors = new List<Actor>
            {
                 new Actor("Tim Robbins", new DateTime(1958, 10, 16)),
                new Actor("Morgan Freeman", new DateTime(1937, 6, 1)),
                 new Actor("Marlon Brando", new DateTime(1924, 4, 3)),
            };
            await cinema.Actors.AddRangeAsync(actors);

            var movies = new List<Movie>
            {
                new Movie("The Shawshank Redemption", "Adaptacja opowiadania Stephena Kinga. Niesłusznie skazany na dożywocie bankier, stara się przetrwać w brutalnym, więziennym świecie.", new TimeSpan(0, 2, 22, 0)),
                new Movie("Intouchables", "Sparaliżowany milioner zatrudnia do opieki młodego chłopaka z przedmieścia, który właśnie wyszedł z więzienia.", new TimeSpan(0, 1,52, 0)),
                new Movie("The Godfather", "Opowieść o nowojorskiej rodzinie mafijnej. Starzejący się Don Corleone pragnie przekazać władzę swojemu synowi.", new TimeSpan(0, 2,55, 0)),
            };

            await cinema.Movies.AddRangeAsync(movies);
            await cinema.SaveChangesAsync();
        }

        public static void SetCode(this HttpContext context, int code)
        {
            context.Response.StatusCode = code;
        }

        public static void SetAsJson(this HttpContext context, int code)
        {
            context.SetCode(code);
            context.SetContentType();
        }

        public static void SetContentType(this HttpContext context, string contentType = "application/json")
        {
            context.Response.ContentType = "application/json";
        }
    }
}
