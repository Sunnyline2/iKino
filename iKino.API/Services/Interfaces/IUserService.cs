using iKino.API.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iKino.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> BrowseAsync();
        Task<UserDto> GetByIdAsync(Guid userId);
        Task<UserDto> LoginAsync(string username, string password);
        Task<UserDto> RegisterAsync(string username, string password, string mail);
    }
}
