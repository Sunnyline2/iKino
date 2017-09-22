using iKino.API.Domain;
using iKino.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace iKino.API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IDatabase<CinemaContext> _database;

        public DbSet<Movie> Movies => _database.Context.Movies;

        public MovieRepository(IDatabase<CinemaContext> database)
        {
            _database = database;
        }

        public async Task<ICollection<Movie>> GetMoviesAsync()
        {
            return await Movies.ToListAsync();
        }

        public async Task<ICollection<Movie>> GetMoviesAsync(int page, int size)
        {
            return await Movies.Skip(page * size).Take(size).ToListAsync();
        }

        public async Task<ICollection<Movie>> SearchAsync(string value)
        {
            return await Movies.Where(x => x.Name.ToLowerInvariant().Contains(value.ToLowerInvariant())).ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(Guid movieId)
        {
            return await Movies.SingleOrDefaultAsync(x => x.MovieId == movieId);
        }


        public async Task CreateAsync(Movie movie)
        {
            await Movies.AddAsync(movie);
            await _database.Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            Movies.Update(movie);
            await _database.Context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Movie movie)
        {
            Movies.Remove(movie);
            await _database.Context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<Movie, bool>> expression)
        {
            return await Movies.AnyAsync(expression);
        }

        public async Task<int> Count()
        {
            return await Movies.CountAsync();
        }
    }
}