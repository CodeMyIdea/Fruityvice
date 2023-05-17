using Fruityvice;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Fruityvice.Tests.Integration
{
    public class FruitControllerIntegrationTests : IClassFixture<TestServerFixture>
    {
        private readonly HttpClient _httpClient;

        public FruitControllerIntegrationTests(TestServerFixture fixture)
        {
            _httpClient = fixture.HttpClient;
        }

        [Fact]
        public async Task GetAllFruits_ReturnsOkResult_WithListOfFruits()
        {
            // Arrange

            // Act
            HttpResponseMessage response = await _httpClient.GetAsync("/fruit/all");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // TODO: Validate the response content, e.g., deserialize and assert against the returned Fruit objects
        }
    }

    public class TestServerFixture
    {
        public HttpClient HttpClient { get; }

        public TestServerFixture()
        {
            // Create a TestServer with the Startup class
            var testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            // Create an HttpClient instance to send requests to the TestServer
            HttpClient = testServer.CreateClient();
        }
    }
}
