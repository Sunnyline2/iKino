using iKino.API.Domain;
using iKino.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iKino.API.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IDatabase<CinemaContext> _database;

        public UserRepository(IDatabase<CinemaContext> database)
        {
            _database = database;
        }

        public async Task<IQueryable<User>> GetAsync()
        {
            return _database.Context.Users.AsQueryable();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _database.Context.Users.SingleOrDefaultAsync(x => x.UserId == id);

        }

        public async Task InsertAsync(User value)
        {
            await _database.Context.Users.AddAsync(value);
            await _database.Context.SaveChangesAsync();

        }

        public async Task UpdateAsync(User value)
        {
            _database.Context.Users.Update(value);
            await _database.Context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            _database.Context.Users.Remove(user);
            await _database.Context.SaveChangesAsync();

        }

        public int Count => _database.Context.Users.Count();
    }
}