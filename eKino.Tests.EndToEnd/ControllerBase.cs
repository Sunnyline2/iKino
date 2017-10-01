using eKino.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;

namespace eKino.Tests.EndToEnd
{
    public abstract class ControllerBase
    {
        protected readonly HttpClient HttpClient;
        protected readonly TestServer TestServer;
        protected readonly IConfigurationRoot ConfigurationRoot;

        protected ControllerBase()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            ConfigurationRoot = builder.Build();
            TestServer = new TestServer(new WebHostBuilder().UseConfiguration(ConfigurationRoot).UseStartup(typeof(Startup)));
            HttpClient = TestServer.CreateClient();
        }

        protected StringContent GetPayload(object value)
        {
            var json = JsonConvert.SerializeObject(value);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected T DeserializeObject<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}