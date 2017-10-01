using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using eKino.Core.Domain;
using eKino.Core.Repository;
using eKino.Infrastructure.Exceptions;
using eKino.Infrastructure.Services;
using Moq;
using Xunit;

namespace eKino.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_async_should_invoke_create_on_repository()
        {
            var repository = new Mock<IUserRepository>();
            var hashService = new Mock<IHashService>();
            var mapper = new Mock<IMapper>();
            var userService = new UserService(repository.Object, mapper.Object, hashService.Object);

            repository.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(false);
            hashService.Setup(x => x.Hash(It.IsAny<string>())).Returns("hash");
            
            await userService.RegisterAsync("user", "password", "user@gmail.com");
            repository.Verify(x => x.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            repository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }


        [Fact]
        public async Task register_async_should_throw_service_exception()
        {
            var repository = new Mock<IUserRepository>();
            var hashService = new Mock<IHashService>();
            var mapper = new Mock<IMapper>();
            var userService = new UserService(repository.Object, mapper.Object, hashService.Object);

            repository.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(true);

            hashService.Setup(x => x.Hash(It.IsAny<string>())).Returns("hash");
            await Assert.ThrowsAsync<ServiceException>(async () =>
                await userService.RegisterAsync("username", "password", "user@gmail.com"));
            repository.Verify(x => x.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task login_asnyc_should_login_user()
        {
            var repository = new Mock<IUserRepository>();
            var hashService = new Mock<IHashService>();
            var mapper = new Mock<IMapper>();
            var userService = new UserService(repository.Object, mapper.Object, hashService.Object);

            hashService.Setup(x => x.Hash(It.IsAny<string>())).Returns("hash");

            var user = new User
            {
                Username = "username",
                Password = "hash",
            };

            repository.Setup(x => x.GetUserByNameAsync(user.Username)).ReturnsAsync(user);
            await userService.LoginAsync(user.Username, user.Password);
            repository.Verify(x => x.GetUserByNameAsync(user.Username), Times.Once);
        }
    }
}