using eKino.Infrastructure.Commands;
using eKino.Infrastructure.DTO;
using eKino.Infrastructure.Exceptions;
using eKino.Infrastructure.Services;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace eKino.Tests.EndToEnd
{
    public class MovieControllerTests : ControllerBase
    {
        private string _username = "username";
        private string _password = "password";
        private string _mail = "user@gmail.com";

        [Fact]
        public async Task register_should_create_user()
        {
            var responseMessage = await RegisterAsync("testing", "testingpass", "test@gmail.com");
            Assert.True(responseMessage.StatusCode == HttpStatusCode.Created);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var userDto = DeserializeObject<UserDto>(content);
            Assert.Equal(userDto.Username, "testing");
        }

        [Fact]
        public async Task register_should_throw_service_exception()
        {
            await Assert.ThrowsAsync<ServiceException>(async () => await RegisterAsync(_username, _password, _mail));
        }

        [Fact]
        public async Task login_should_return_jwt()
        {
            var responseMessage = await LoginAsync(_username, _password);
            Assert.True(responseMessage.StatusCode == HttpStatusCode.OK);
            var content = await responseMessage.Content.ReadAsStringAsync();
            var token = DeserializeObject<TokenService.Token>(content);
            Assert.NotEmpty(token.SecurityToken);
        }

        [Fact]
        public async Task fake()
        {
            var loginMessage = await LoginAsync("Sunnyline", "sunnyline123");
            var loginContent = await loginMessage.Content.ReadAsStringAsync();
            var token = DeserializeObject<TokenService.Token>(loginContent);

            var uers = await GetUsersAsync(token);




        }




        public async Task<HttpResponseMessage> LoginAsync(string username, string password) =>
            await HttpClient.PostAsync("api/v1/user/login", GetPayload(new LoginCommand { Username = username, Password = password }));

        public async Task<HttpResponseMessage> RegisterAsync(string username, string password, string email) =>
            await HttpClient.PostAsync("api/v1/user/register", GetPayload(new CreateUser { Username = username, Password = password, Mail = email }));

        public async Task<HttpResponseMessage> GetUsersAsync(TokenService.Token token) =>
            await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, "api/v1/user")
            {
                Headers =
                {
                    {"Authorization",$"Bearer {token.SecurityToken}"},
                }
            });

    }
}