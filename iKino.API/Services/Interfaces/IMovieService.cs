using iKino.API.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iKino.API.Services.Interfaces
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