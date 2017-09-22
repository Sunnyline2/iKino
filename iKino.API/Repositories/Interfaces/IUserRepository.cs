using iKino.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace iKino.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetUsersAsync();
        Task<ICollection<User>> GetUsersAsync(int page, int size);

        Task<ICollection<User>> SearchAsync(string value);

        Task<User> GetUserByIdAsync(Guid userId);
        Task<User> GetUserByNameAsync(string name);
        Task<User> GetUserByMailAsync(string mail);

        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task RemoveAsync(User user);


        Task<bool> AnyAsync(Expression<Func<User, bool>> expression);
        Task<int> Count();
    }
}