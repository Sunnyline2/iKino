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
    public class UserRepository : IUserRepository
    {
        private readonly CinemaContext _database;

        public UserRepository(CinemaContext database)
        {
            _database = database;
        }

        public async Task<ICollection<User>> GetUsersAsync()
        {
            return await _database.Users.ToListAsync();
        }

        public async Task<ICollection<User>> GetUsersAsync(int page, int size)
        {
            return await _database.Users.Skip(page * size).Take(size).ToListAsync();
        }

        public Task<ICollection<User>> SearchAsync(string value)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await _database.Users.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            return await _database.Users.SingleOrDefaultAsync(x => x.Username == name);
        }

        public async Task<User> GetUserByMailAsync(string mail)
        {
            return await _database.Users.SingleOrDefaultAsync(x => x.Mail == mail);
        }

        public async Task AddAsync(User movie)
        {
            _database.Users.Add(movie);
            await _database.SaveChangesAsync();
        }

        public async Task UpdateAsync(User movie)
        {
            _database.Users.Update(movie);
            await _database.SaveChangesAsync();
        }

        public async Task RemoveAsync(User movie)
        {
            _database.Users.Remove(movie);
            await _database.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<User, bool>> expression)
        {
            return await _database.Users.AnyAsync(expression);
        }


        public async Task<int> Count()
        {
            return await _database.Movies.CountAsync();
        }
    }
}
