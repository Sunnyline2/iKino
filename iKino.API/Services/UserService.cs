using AutoMapper;
using iKino.API.Domain;
using iKino.API.Dto;
using iKino.API.Repositories.Interfaces;
using iKino.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iKino.API.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IHashService _hashService;

        public UserService(IRepository<User> userRepository, IHashService hashService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.GetAsync();
            return Mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return Mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> LoginAsync(string username, string password)
        {
            var users = await _userRepository.GetAsync();
            var user = await users.SingleOrDefaultAsync(x => x.Username == username);

            if (user == null)
                throw new ServiceException("This user does not exist.");

            var hash = _hashService.Hash(password);
            if (!string.Equals(hash, user.Password))
                throw new ServiceException("The entered password is incorrect.");

            return Mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> RegisterAsync(string username, string password, string mail)
        {
            var users = await _userRepository.GetAsync();

            if (await users.AnyAsync(x => x.Username == username) || await users.AnyAsync(x => x.Mail == mail))
            {
                throw new ServiceException("The email address or username is already used.");
            }



            var user = new User(username, mail);
            user.SetPassword(password, _hashService);

            await _userRepository.InsertAsync(user);
            return Mapper.Map<UserDto>(user);
        }
    }
}