using iKino.API.Attribute;
using iKino.API.Domain;
using iKino.API.Requests;
using iKino.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using iKino.API.Models;
using iKino.API.Services;

namespace iKino.API.Controllers
{
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }


        [HttpGet]
        [RequireRole(Roles.Admin)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.BrowseAsync());
        }


        [HttpGet]
        [Route("{userId}", Name = "GetUser")]
        [RequireRole(Roles.Admin)]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            return Ok(await _userService.GetByIdAsync(userId));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]Authenticate authenticate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.LoginAsync(authenticate.Username, authenticate.Password);
            var expiry = DateTime.Now.AddDays(1);
            var token = _jwtService.GenerateToken(user.UserId.ToString(), user.Username, user.Role, expiry);
            return Ok(AuthToken.Create(new JwtSecurityTokenHandler().WriteToken(token), expiry));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]CreateUser createUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.RegisterAsync(createUser.Username, createUser.Password, createUser.Mail);
            return CreatedAtRoute("GetUser", new { userId = user.UserId }, user);
        }
    }
}