using AutoMapper;
using iKino.API.Domain;
using iKino.API.DTO;
using iKino.API.Repositories.Interfaces;
using iKino.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iKino.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository, IHashService hashService, IMapper mapper)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _mapper = mapper;
        }


        public async Task<ICollection<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            return _mapper.Map<ICollection<UserDto>>(users);
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

        public async Task<UserDto> RegisterAsync(string username, string password, string mail)
        {
            if (await _userRepository.AnyAsync(x => string.Equals(x.Username, username, StringComparison.InvariantCultureIgnoreCase) || string.Equals(x.Mail, mail, StringComparison.InvariantCultureIgnoreCase)))
                throw new ServiceException("The email address or username is already used.");

            var user = new User(username, mail);
            user.SetPassword(password, _hashService);

            await _userRepository.CreateAsync(user);
            return _mapper.Map<UserDto>(user);
        }
    }
}