
using eKino.Core.Domain;
using eKino.Core.Repository;
using eKino.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eKino.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaContext _database;

        public MovieRepository(CinemaContext database)
        {
            _database = database;
        }

        public async Task<ICollection<Movie>> GetMoviesAsync()
        {
            return await _database.Movies.ToListAsync();
        }

        public async Task<ICollection<Movie>> GetMoviesAsync(int page, int size)
        {
            return await _database.Movies.Skip(page * size).Take(size).ToListAsync();
        }

        public async Task<ICollection<Movie>> SearchAsync(string value)
        {
            return await _database.Movies.Where(x => x.Name.ToLowerInvariant().Contains(value.ToLowerInvariant())).ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(Guid movieId)
        {
            return await _database.Movies.SingleOrDefaultAsync(x => x.MovieId == movieId);
        }

        public async Task AddAsync(Movie movie)
        {
            _database.Movies.Add(movie);
            await _database.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie)
        {
            _database.Movies.Update(movie);
            await _database.SaveChangesAsync();
        }

        public async Task RemoveAsync(Movie movie)
        {
            _database.Movies.Remove(movie);
            await _database.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<Movie, bool>> expression)
        {
            return await _database.Movies.AnyAsync(expression);
        }

        public async Task<int> Count()
        {
            return await _database.Movies.CountAsync();
        }

    }
}