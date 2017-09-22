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

            //TODO wireup models

        }


        public DbSet<User> Users { get; protected set; }
        public DbSet<Movie> Movies { get; protected set; }
        public DbSet<Actor> Actors { get; protected set; }
        public DbSet<Vote> Votes { get; protected set; }
    }

}