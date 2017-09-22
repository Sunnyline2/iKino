using iKino.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace iKino.Tests.EndToEnd
{
    public abstract class ControllerBase
    {
        protected readonly TestServer TestServer;
        protected readonly HttpClient HttpClient;

        protected ControllerBase()
        {
            TestServer = new TestServer(new WebHostBuilder()
                                       .UseStartup<Startup>());
            HttpClient = TestServer.CreateClient();
        }


        protected StringContent CreatePayload(object value)
        {
            var jsonBody = JsonConvert.SerializeObject(value, Formatting.Indented);
            return new StringContent(jsonBody, Encoding.UTF8, "application/json");
        }
    }
}
