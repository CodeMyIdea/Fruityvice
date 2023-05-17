using Fruityvice.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fruityvice.Tests.Integration
{
    [TestFixture]
    public class FruitControllerIntegrationTests
    {
        private TestServer _testServer;
        private HttpClient _httpClient;

        [OneTimeSetUp]
        public void Setup()
        {
            // Create a TestServer with the Startup class
            _testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());

            // Create an HttpClient instance to send requests to the TestServer
            _httpClient = _testServer.CreateClient();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            // Dispose the HttpClient and TestServer
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [Test]
        public async Task GetAllFruits_ReturnsOkResult_WithListOfFruits()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("api/fruit/all");

            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            string responseContent = await response.Content.ReadAsStringAsync();
            Fruit[] fruits = JsonConvert.DeserializeObject<Fruit[]>(responseContent);

            Assert.That(fruits, Is.Not.Empty);
        }

        [Test]
        public async Task GetFruitsByFamily_ReturnsOkResult_WithListOfFruits()
        {
            string fruitFamily = "Rutaceae";
            string requestBodyJson = JsonConvert.SerializeObject(new { fruitFamily });
            var httpContent = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/fruit/family", httpContent);

            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            string responseContent = await response.Content.ReadAsStringAsync();
            Fruit[] fruits = JsonConvert.DeserializeObject<Fruit[]>(responseContent);

            Assert.That(fruits, Is.Not.Empty);
        }
    }
}
