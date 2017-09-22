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
    public class UserRepository : IUserRepository
    {
        private readonly IDatabase<CinemaContext> _database;

        public UserRepository(IDatabase<CinemaContext> database)
        {
            _database = database;
        }

        public DbSet<User> Users => _database.Context.Users;

        public async Task<ICollection<User>> GetUsersAsync()
        {
            return await Users.ToListAsync();
        }

        public async Task<ICollection<User>> GetUsersAsync(int page, int size)
        {
            return await Users.Skip(page * size).Take(size).ToListAsync();
        }

        public Task<ICollection<User>> SearchAsync(string value)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await Users.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            return await Users.SingleOrDefaultAsync(x => x.Username == name);
        }

        public async Task<User> GetUserByMailAsync(string mail)
        {
            return await Users.SingleOrDefaultAsync(x => x.Mail == mail);
        }

        public async Task CreateAsync(User movie)
        {
            Users.Add(movie);
            await _database.Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User movie)
        {
            Users.Update(movie);
            await _database.Context.SaveChangesAsync();
        }

        public async Task RemoveAsync(User movie)
        {
            Users.Remove(movie);
            await _database.Context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<User, bool>> expression)
        {
            return await Users.AnyAsync(expression);
        }


        public async Task<int> Count()
        {
            return await Users.CountAsync();
        }
    }
}
