using AutoMapper;
using iKino.API.Attribute;
using iKino.API.Domain;
using iKino.API.Dto;
using iKino.API.Models;
using iKino.API.Repositories.Interfaces;
using iKino.API.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iKino.API.Controllers
{
    [Route("api/[Controller]")]
    public class MovieController : Controller
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieController(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieRepository.GetAsync();
            return Ok(movies.ToArray());
        }

        [HttpGet]
        [Route("{page}/{size}")]
        public async Task<IActionResult> GetMovies(int page, int size)
        {
            var movies = await _movieRepository.GetAsync();

            return Ok(Pagination.Create(movies.Skip(page).Take(size), page, size));
        }


        [HttpGet]
        [Route("{movieId}", Name = "GetMovie")]
        public async Task<IActionResult> GetMovie(Guid movieId)
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);
            return Ok(Mapper.Map<MovieDto>(movie));
        }


        [HttpPost]
        [RequireRole(Roles.Admin)]
        public async Task<IActionResult> CreateMovie([FromBody]CreateMovie createMovie)
        {
            if (!ModelState.IsValid)
                return Json(ModelState);

            //if (await _movieRepository.AnyAsync(x => string.Equals(x.Name.Trim().ToLowerInvariant(), createMovie.Name.Trim().ToLowerInvariant())))
            //    return BadRequest("This movie already exists.");





            var movie = new Movie(createMovie.Name, createMovie.Description, createMovie.Duration);
            await _movieRepository.InsertAsync(movie);
            return CreatedAtRoute("GetMovie", new { movieId = movie.MovieId }, movie);
        }


        [HttpDelete]
        [Route("{movieId}")]
        [RequireRole(Roles.Admin)]
        public async Task<IActionResult> DeleteMovie(Guid movieId)
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);
            if (movie == null)
                return NotFound();

            await _movieRepository.DeleteAsync(movie.MovieId);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMovie()
        {
            return Ok();
        }
    }
}