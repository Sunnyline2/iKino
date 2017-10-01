using eKino.Infrastructure;
using eKino.Infrastructure.Commands;
using eKino.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace eKino.API.Conrollers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }


        [HttpGet]
        [RequireRole(SysRoles.Admin)]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.BrowseAsync());
        }


        [HttpGet]
        [Route("{userId}", Name = "GetUser")]
        [RequireRole(SysRoles.Admin)]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            return Ok(await _userService.GetByIdAsync(userId));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.LoginAsync(loginCommand.Username, loginCommand.Password);
            var token = _tokenService.GenerateToken(user);

            return Ok(token);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CreateUser createUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.RegisterAsync(createUser.Username, createUser.Password, createUser.Mail);
            return CreatedAtRoute("GetUser", new { userId = user.UserId }, user);
        }
    }
}