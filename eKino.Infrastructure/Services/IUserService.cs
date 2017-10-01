using eKino.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eKino.Infrastructure.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> BrowseAsync();
        Task<IEnumerable<UserDto>> BrowseAsync(int page, int size);
        Task<UserDto> GetByIdAsync(Guid userId);
        Task<UserDto> LoginAsync(string username, string password);
        Task<UserDto> RegisterAsync(string username, string password, string mail, string role = SysRoles.User);
    }
}
