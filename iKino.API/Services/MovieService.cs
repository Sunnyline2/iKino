using AutoMapper;
using iKino.API.Domain;
using iKino.API.DTO;
using iKino.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iKino.API.Services.Interfaces;

namespace iKino.API.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;


        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieDto>> BrowseAsync()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            return Mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<IEnumerable<MovieDto>> BrowseAsync(int page, int size)
        {
            var movies = await _movieRepository.GetMoviesAsync(page, size);
            return Mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<MovieDto> GetByIdAsync(Guid movieId)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(movieId);
            return Mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> CreateMovieAsync(string name, string description, TimeSpan duration)
        {
            if (await _movieRepository.AnyAsync(x => string.Equals(x.Name.Trim(), name.Trim(), StringComparison.InvariantCultureIgnoreCase)))
                throw new ServiceException("The title given is already added.");

            var movie = new Movie(name, description, duration);
            await _movieRepository.CreateAsync(movie);
            return Mapper.Map<MovieDto>(movie);
        }

        public async Task DeleteMovieAsync(Guid movieId)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(movieId);
            if (movie == null)
                throw new ServiceException("This movie does not exist.");

            await _movieRepository.RemoveAsync(movie);
        }
    }
}
