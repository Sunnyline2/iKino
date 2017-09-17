using iKino.API.Domain;
using iKino.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iKino.API.Repositories
{
    public class MovieRepository : IRepository<Movie>
    {
        private readonly IDatabase<CinemaContext> _database;

        public MovieRepository(IDatabase<CinemaContext> database)
        {
            _database = database;
        }


        public async Task<List<Movie>> GetAsync()
        {
            return await _database.Context.Movies.ToListAsync();
        }

        public async Task<IQueryable<Movie>> AsQueryable()
        {
            return _database.Context.Movies.AsQueryable();
        }


        public async Task<Movie> GetByIdAsync(Guid id)
        {
            return await _database.Context.Movies.SingleOrDefaultAsync(x => x.MovieId == id);
        }

        public async Task InsertAsync(Movie value)
        {
            await _database.Context.Movies.AddAsync(value);
            await _database.Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie value)
        {
            _database.Context.Movies.Update(value);
            await _database.Context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Guid id)
        {
            var movie = await GetByIdAsync(id);
            _database.Context.Movies.Remove(movie);
            await _database.Context.SaveChangesAsync();
        }

        public int Count => _database.Context.Users.Count();
    }
}