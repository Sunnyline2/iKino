using AutoMapper;
using eKino.Core.Domain;
using eKino.Core.Repository;
using eKino.Infrastructure.DTO;
using eKino.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eKino.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHashService _hashService;

        public UserService(IUserRepository userRepository, IMapper mapper, IHashService hashService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _hashService = hashService;
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync(int page, int size)
        {
            var users = await _userRepository.GetUsersAsync(page, size);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByNameAsync(username);
            if (user == null)
                throw new ServiceException("This user does not exist.");

            var hash = _hashService.Hash(password);
            if (!string.Equals(hash, user.Password))
                throw new ServiceException("The entered password is incorrect.");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> RegisterAsync(string username, string password, string mail, string role = SysRoles.User)
        {
            if (await _userRepository.AnyAsync(x =>
                string.Equals(x.Username, username, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(x.Mail, mail, StringComparison.InvariantCultureIgnoreCase)))
                throw new ServiceException("The email address or username is already used.");

            var user = new User
            {
                Username = username,
                Mail = mail,
                Password = _hashService.Hash(password),
                CreatedAt = DateTime.Now,
                IsActive = true,
                Role = role,
                UpdatedAt = DateTime.Now
            };

            await _userRepository.AddAsync(user);
            return _mapper.Map<UserDto>(user);
        }
    }

    public static class SysRoles
    {
        public const string Admin = "admin";
        public const string User = "user";
    }
}