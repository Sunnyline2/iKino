using iKino.API.Domain;
using iKino.API.Repositories.Interfaces;
using iKino.API.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace iKino.API.Controllers
{
    public class VoteController : Controller
    {
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<User> _userRepository;


        public VoteController(IRepository<Movie> movieRepository, IRepository<User> userRepository)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            return NoContent();
        }

        [HttpGet]
        [Route("{movieId}/vote/{voteId}", Name = "GetVote")]
        public async Task<IActionResult> GetVote(Guid movieId, Guid voteId)
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie.MovieActors);
        }

        [HttpGet]
        [Route("{movieId}/votes")]
        public async Task<IActionResult> GetVotes(Guid movieId)
        {
            throw new NotImplementedException();
            //var movie = await _movieRepository.GetByIdAsync(movieId);
            //if (movie == null)
            //{
            //    return NotFound();
            //}

            //return Ok(movie.VoteMovies.Select(x => x.Vote));
        }

        [HttpGet]
        [Route("{movieId}/votes/{page}/{size}")]
        // new { query, startIndex = startIndex + pageSize, pageSize  
        public async Task<IActionResult> GetVotes(Guid movieId, int page, int size)
        {

            throw new NotImplementedException();

            //var movie = await _movieRepository.GetByIdAsync(movieId);
            //if (movie == null)
            //    return NotFound();

            //var X = movie.VoteMovies.Select(x => x.Vote);
            //var votes = X.Skip(page).Take(size);
            //return Ok(new Pagination { Content = votes, Page = page, Size = size });
        }

        [HttpPost]
        [Route("{movieId}/vote")]
        public async Task<IActionResult> CreateVote(Guid movieId, [FromBody]CreateVote createVote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = await _movieRepository.GetByIdAsync(movieId);
            if (movie == null)
            {
                return NotFound();
            }
            var vote = new Vote(createVote.Description, createVote.Rate);


            await _movieRepository.UpdateAsync(movie);
            return CreatedAtRoute("GetVote", new { movieId = movieId, voteId = vote.VoteId }, vote);
        }
    }
}