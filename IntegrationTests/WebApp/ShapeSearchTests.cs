using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Web.Models;

namespace IntegrationTests.WebApp
{
    [TestFixture]
    public class ShapeSearchTests
    {
        IHost _inMemoryHost;
        private const string Endpoint = "api/shapesearch";

        [OneTimeSetUp]
        public async Task TestFixtureSetUp()
        {
            // Arrange
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    // SB - create an in memory test server which has same DI registrations
                    // as Web project but swap out the appsettings for one specific to this test proj
                    webHost.UseTestServer();
                    webHost.UseStartup<Web.Startup>();
                    webHost.ConfigureAppConfiguration(config =>
                        config.AddJsonFile("appsettings.test.json"));
                });

            // Create and start up the host
            _inMemoryHost = await hostBuilder.StartAsync();
        }

        [Test]
        public async Task ApiShapeSearch_InvalidSearchType_ReturnsBadRequest()
        {
            // Arrange
            var client = _inMemoryHost.GetTestClient();

            // Act
            var payload = new ShapeSearchRequestModel() { SearchBy = "invalid" };
            var httpContent = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Endpoint, httpContent);

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            StringAssert.Contains("Please specify a valid search type.", responseString);
        }

        [Test]
        public async Task ApiShapeSearch_CoordinateSearchWithMissingCoordinates_ReturnsBadRequest()
        {
            // Arrange
            var client = _inMemoryHost.GetTestClient();

            // Act
            var payload = new ShapeSearchRequestModel() { SearchBy = "coordinates" };
            var httpContent = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Endpoint, httpContent);

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            StringAssert.Contains("Please specify vertext coordinates.", responseString);
        }

        [Test]
        public async Task ApiShapeSearch_ValidSearch_ReturnsOk()
        {
            // Arrange
            var client = _inMemoryHost.GetTestClient();

            // Act
            var payload = new ShapeSearchRequestModel() 
            { 
                SearchBy = "name",
                Name = "A1"
            };

            var httpContent = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Endpoint, httpContent);

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            ShapeSearchResultModel responseModel = JsonSerializer.Deserialize<ShapeSearchResultModel>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            Assert.AreEqual("A1", responseModel.Name);
        }
    }
}
