using ECommerce.Location.Api;
using ECommerce.Location.Api.Application.Queries;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Location.IntegrationTests
{
    public class LocationScenarios : BaseIntegrationTest
    {
        public LocationScenarios(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetByZipCode_ShouldReturnOkWithResult()
        {
            //Arrange
            var expectedResult = new List<GetLocationByZipCodeQueryResult>();

            //Act
            var response = await GetHttpClient().GetAsync("http://localhost:62140/api/Locations/ByZipCode/36090320");
            var result = JsonConvert.DeserializeObject<IEnumerable<GetLocationByZipCodeQueryResult>>(await response.Content.ReadAsStringAsync());

            //Assert
            response.EnsureSuccessStatusCode();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetByZipCode_ShouldReturnOkWithResult2()
        {
            //Arrange
            var expectedResult = new List<GetLocationByZipCodeQueryResult>();

            //Act
            var response = await GetHttpClient().GetAsync("http://localhost:62140/api/Locations/ByZipCode/36090320");
            var result = JsonConvert.DeserializeObject<IEnumerable<GetLocationByZipCodeQueryResult>>(await response.Content.ReadAsStringAsync());

            //Assert
            response.EnsureSuccessStatusCode();
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
