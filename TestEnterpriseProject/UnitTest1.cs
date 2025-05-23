using Xunit;
using EnterpriseProject;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TestEnterpriseProject
{
    public class UnitTest1 :IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public UnitTest1(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async void TestHomeIndexLoads() /* Testing to see if the home page loads */
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("/Home/Index");
            int code = (int)response.StatusCode;

            //Assert
            Assert.Equal(200, code);
        }

        [Theory]
        [InlineData("/Home/Privacy")]
        [InlineData("/Categories/Index")]
        [InlineData("/OurMission/Index")]
        public async Task TestAllPagesLoad(string URL) /* Testing to ensure that all available pages load */
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync(URL);
            int code = (int)response.StatusCode;

            //Assert
            Assert.Equal(200, code);
        }

        [Theory]
        [InlineData("Welcome")]
        public async Task TestHomePageDisplaysWelcome(string text) /* Testing to ensure that the home page displays the welcome text */
        {
            //Arrange

            //Act
            var response = await _httpClient.GetAsync("/Home/Index");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();

            //Assert
            Assert.Contains(text, contentString);
        }

        [Theory]
        [InlineData("About Enterprise Marketplace")]
        public async Task TestOurMissionPageDisplaysText(string text) /* Testing to ensure that our mission page displays the about enterprise text */
        {
            //Arrange

            //Act
            var response = await _httpClient.GetAsync("/OurMission/Index");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();

            //Assert
            Assert.Contains(text, contentString);
        }

        [Fact]
        public async void TestElectronicsCategory() /* Ensuring that the Categories pages work properly */
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("/Categories/Electronics");
            int code = (int)response.StatusCode;

            //Assert
            Assert.Equal(200, code);
        }

        [Fact]
        public async void TestEntertainmentCategory() /* Ensuring that the Categories pages work properly */
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("/Categories/Entertainment");
            int code = (int)response.StatusCode;

            //Assert
            Assert.Equal(200, code);
        }

        [Fact]
        public async void TestHomeImprovementSuppliesCategory() /* Ensuring that the Categories pages work properly */
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("/Categories/HomeImprovementSupplies");
            int code = (int)response.StatusCode;

            //Assert
            Assert.Equal(200, code);
        }

        [Theory]
        [InlineData("HP Laptop")]
        public async Task TestForListings(string listing)
        {
            //Arrange

            //Act
            var response = await _httpClient.GetAsync("ItemListings/Index");
            var pageContent = await response.Content.ReadAsStringAsync();
            var contentString = pageContent.ToString();

            //Assert
            Assert.Contains(listing, contentString);
        }
    }

}