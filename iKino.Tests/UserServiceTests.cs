using AutoMapper;
using iKino.API.Domain;
using iKino.API.Repositories.Interfaces;
using iKino.API.Services;
using iKino.API.Services.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace iKino.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_async_should_invoke_insert_on_repository()
        {
            var repositoryMock = new Mock<IRepository<User>>();
            var hashMock = new Mock<IHashService>();
            var mapperMock = new Mock<IMapper>();
            var userService = new UserService(repositoryMock.Object, hashMock.Object, mapperMock.Object);

            repositoryMock.Setup(x => x.GetAsync()).ReturnsAsync(() => new List<User>());
            hashMock.Setup(x => x.Hash(It.IsAny<string>())).Returns("hash");


            await userService.RegisterAsync("user", "password", "user@gmail.com");
            repositoryMock.Verify(x => x.GetAsync(), Times.Once);
            repositoryMock.Verify(x => x.InsertAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task register_async_should_throw_service_exception()
        {
            var user = new User("user", "user@gmail.com");

            var repositoryMock = new Mock<IRepository<User>>();
            var hashMock = new Mock<IHashService>();
            var mapperMock = new Mock<IMapper>();
            var userService = new UserService(repositoryMock.Object, hashMock.Object, mapperMock.Object);

            repositoryMock.Setup(x => x.GetAsync()).ReturnsAsync(() => new List<User> { user });
            hashMock.Setup(x => x.Hash(It.IsAny<string>())).Returns("hash");

            await Assert.ThrowsAsync<ServiceException>(async () => await userService.RegisterAsync(user.Username, "password", user.Mail));
            repositoryMock.Verify(x => x.GetAsync(), Times.Once);
        }
    }
}
