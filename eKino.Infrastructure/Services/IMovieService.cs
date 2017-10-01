using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eKino.Infrastructure.DTO;

namespace eKino.Infrastructure.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> BrowseAsync();
        Task<IEnumerable<MovieDto>> BrowseAsync(int page, int size);
        Task<MovieDto> GetByIdAsync(Guid movieId);
        Task<MovieDto> CreateMovieAsync(string name, string description, TimeSpan duration);
        Task DeleteMovieAsync(Guid movieId);
    }
}