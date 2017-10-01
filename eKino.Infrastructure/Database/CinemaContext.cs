using eKino.Core.Domain;
using eKino.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace eKino.Infrastructure.Database
{
    public class CinemaContext : DbContext
    {

        public CinemaContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MovieActor>()
                .HasKey(x => new { x.ActorId, x.MovieId });

            builder.Entity<MovieActor>()
                .HasOne(x => x.Actor)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.ActorId);

            builder.Entity<MovieActor>()
                .HasOne(x => x.Movie)
                .WithMany(x => x.Actors)
                .HasForeignKey(x => x.MovieId);
        }


        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<Video> Videos { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }

    public static class DbExtensions
    {
        public static void Seed(this CinemaContext context, IUserService userService)
        {
            Debug.WriteLine("Seeding database..");

            var actors = new[]
            {
                new Actor {Name = "Jack Nicholson"},
                new Actor {Name = "Marlon Brando"},
                new Actor {Name = "Robert De Niro"},
                new Actor {Name = "Al Pacino"},
                new Actor {Name = "Daniel Day-Lewis"},
                new Actor {Name = "Dustin Hoffman"},
                new Actor {Name = "Tom Hanks"},
                new Actor {Name = "Anthony Hopkins"},
                new Actor {Name = "Paul Newman"},
            };

            context.Actors.AddRange(actors);
            context.SaveChanges();

            var movies = new[]
            {
                new Movie{Name = "The Shawshank Redemption", Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", Duration = new TimeSpan(2,22,0)},
                new Movie{Name = "12 Angry Men", Description = "A jury holdout attempts to prevent a miscarriage of justice by forcing his colleagues to reconsider the evidence.", Duration = new TimeSpan(1,36,0)},
                new Movie{Name = "The Dark Knight", Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham, the Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.", Duration = new TimeSpan(2,32,0)},
                new Movie{Name = "The Godfather: Part II", Description = "The early life and career of Vito Corleone in 1920s New York is portrayed while his son, Michael, expands and tightens his grip on the family crime syndicate.", Duration = new TimeSpan(3, 22,0)},
                new Movie{Name = "Schindler's List", Description = "In German-occupied Poland during World War II, Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazi Germans.", Duration = new TimeSpan(3, 15, 0)},
            };
            context.Movies.AddRange(movies);
            context.SaveChanges();

            userService.RegisterAsync("Sunnyline", "sunnyline123", "damiancz112@gmail.com", SysRoles.Admin);

            //account for testing
            userService.RegisterAsync("username", "password", "user@gmail.com");
        }
    }
}

