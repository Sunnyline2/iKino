using iKino.API.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace iKino.API.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<ICollection<Movie>> GetMoviesAsync();
        Task<ICollection<Movie>> GetMoviesAsync(int page, int size);

        Task<ICollection<Movie>> SearchAsync(string value);

        Task<Movie> GetMovieByIdAsync(Guid movieId);

        Task CreateAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task RemoveAsync(Movie movie);


        Task<bool> AnyAsync(Expression<Func<Movie, bool>> expression);
        Task<int> Count();
    }
}