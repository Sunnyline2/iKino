using iKino.API.Requests;
using System.Threading.Tasks;
using Xunit;

namespace iKino.Tests.EndToEnd
{
    public class UserControllerTests : ControllerBase
    {

        [Fact]
        public async Task login()
        {
            var loginBody = new Authenticate
            {
                Username = "username",
                Password = "password",
            };


            var response = await HttpClient.PostAsync("users", CreatePayload(loginBody));



        }
    }
}
