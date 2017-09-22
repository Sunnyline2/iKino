using AutoMapper;
using iKino.API.Domain;
using iKino.API.Repositories.Interfaces;
using iKino.API.Services;
using iKino.API.Services.Interfaces;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace iKino.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_async_should_invoke_create_on_repository()
        {
            var repository = new Mock<IUserRepository>();
            var hashService = new Mock<IHashService>();
            var mapper = new Mock<IMapper>();
            var userService = new UserService(repository.Object, hashService.Object, mapper.Object);

            repository.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(false); // czy u¿ytkownik istnieje
            hashService.Setup(x => x.Hash(It.IsAny<string>())).Returns("hash");


            await userService.RegisterAsync("user", "password", "user@gmail.com");
            repository.Verify(x => x.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            repository.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Once);
        }


        [Fact]
        public async Task register_async_should_throw_service_exception()
        {
            var repository = new Mock<IUserRepository>();
            var hashMock = new Mock<IHashService>();
            var mapperMock = new Mock<IMapper>();
            var userService = new UserService(repository.Object, hashMock.Object, mapperMock.Object);

            repository.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(true); // czy u¿ytkownik istnieje
            hashMock.Setup(x => x.Hash(It.IsAny<string>())).Returns("hash");

            await Assert.ThrowsAsync<ServiceException>(async () => await userService.RegisterAsync("username", "password", "mail"));
            repository.Verify(x => x.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task login()
        { }

    }
}
