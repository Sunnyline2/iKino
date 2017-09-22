using System;
using iKino.API.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace iKino.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ICollection<UserDto>> BrowseAsync();
        Task<UserDto> GetByIdAsync(Guid userId);
        Task<UserDto> LoginAsync(string username, string password);
        Task<UserDto> RegisterAsync(string username, string password, string mail);
    }
}
