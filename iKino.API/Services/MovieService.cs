using AutoMapper;
using iKino.API.Domain;
using iKino.API.Dto;
using iKino.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iKino.API.Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieDto>> BrowseAsync()
        {
            var movies = await _movieRepository.GetAsync();
            return Mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<IEnumerable<MovieDto>> BrowseAsync(int page, int size)
        {
            var movies = await _movieRepository.GetAsync();
            return Mapper.Map<IEnumerable<MovieDto>>(movies.Skip(page).Take(size));
        }

        public async Task<MovieDto> CreateMovieAsync(string name, string description, TimeSpan duration)
        {
            var movies = await _movieRepository.GetAsync();

            if (await movies.AnyAsync(x => string.Equals(x.Name.Trim(), name.Trim(), StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ServiceException("The title given is already added.");
            }

            var movie = new Movie(name, description, duration);
            await _movieRepository.InsertAsync(movie);
            return Mapper.Map<MovieDto>(movie);
        }
    }

    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> BrowseAsync();
        Task<IEnumerable<MovieDto>> BrowseAsync(int page, int size);


    }
}
