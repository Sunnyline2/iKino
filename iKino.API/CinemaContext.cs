using iKino.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace iKino.API
{
    public class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions options) : base(options)
        {
            base.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<MovieActor>().HasKey(x => new { x.ActorId, x.MovieId });

            model.Entity<MovieActor>()
                .HasOne(x => x.Actor)
                .WithMany(x => x.MovieActors)
                .HasForeignKey(x => x.ActorId);

            model.Entity<MovieActor>()
                .HasOne(x => x.Movie)
                .WithMany(x => x.MovieActors)
                .HasForeignKey(x => x.MovieId);

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }

}