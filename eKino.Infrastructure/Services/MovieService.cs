using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using eKino.Core.Domain;
using eKino.Core.Repository;
using eKino.Infrastructure.DTO;
using eKino.Infrastructure.Exceptions;

namespace eKino.Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDto>> BrowseAsync()
        {
            var movies = await _movieRepository.GetMoviesAsync();
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
        }

        public async Task<IEnumerable<MovieDto>> BrowseAsync(int page, int size)
        {
            var movies = await _movieRepository.GetMoviesAsync(page, page);
            return _mapper.Map<IEnumerable<MovieDto>>(movies);
           
        }
        
        public async Task<MovieDto> GetByIdAsync(Guid movieId)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(movieId);
            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> CreateMovieAsync(string name, string description, TimeSpan duration)
        {
            if (await _movieRepository.AnyAsync(x => string.Equals(x.Name.Trim(), name.Trim(), StringComparison.InvariantCultureIgnoreCase)))
                throw new ServiceException("The title given is already added.");

            var movie = new Movie
            {
                Name = name,
                Duration = duration,
                Description = description,
            };
            
            await _movieRepository.AddAsync(movie);
            return _mapper.Map<MovieDto>(movie);         
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