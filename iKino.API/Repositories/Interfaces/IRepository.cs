using System;
using System.Linq;
using System.Threading.Tasks;

namespace iKino.API.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<IQueryable<T>> GetAsync();
        Task<T> GetByIdAsync(Guid id);

        Task InsertAsync(T value);
        Task UpdateAsync(T value);
        Task DeleteAsync(Guid id);

        int Count { get; }
    }
}