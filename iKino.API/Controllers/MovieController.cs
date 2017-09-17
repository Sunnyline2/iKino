using AutoMapper;
using iKino.API.Attribute;
using iKino.API.Dto;
using iKino.API.Models;
using iKino.API.Requests;
using iKino.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace iKino.API.Controllers
{
    [Route("api/[Controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;


        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.BrowseAsync();
            return Ok(movies.ToArray());
        }

        [HttpGet]
        [Route("{page}/{size}")]
        public async Task<IActionResult> GetMovies(int page, int size)
        {
            var movies = await _movieService.BrowseAsync(page, size);
            return Ok(Pagination.Create(movies, page, size));
        }


        [HttpGet]
        [Route("{movieId}", Name = "GetMovie")]
        public async Task<IActionResult> GetMovie(Guid movieId)
        {
            var movie = await _movieService.GetByIdAsync(movieId);
            return Ok(Mapper.Map<MovieDto>(movie));
        }


        [HttpPost]
        [RequireRole(Roles.Admin)]
        public async Task<IActionResult> CreateMovie([FromBody]CreateMovie createMovie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movie = await _movieService.CreateMovieAsync(createMovie.Name, createMovie.Description, createMovie.Duration);
            return CreatedAtRoute("GetMovie", new { movieId = movie.MovieId }, movie);
        }


        [HttpDelete]
        [Route("{movieId}")]
        [RequireRole(Roles.Admin)]
        public async Task<IActionResult> DeleteMovie(Guid movieId)
        {
            await _movieService.DeleteMovieAsync(movieId);
            return NoContent();
        }
    }
}