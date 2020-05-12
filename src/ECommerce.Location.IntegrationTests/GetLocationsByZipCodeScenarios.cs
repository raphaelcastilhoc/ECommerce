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
    public class GetLocationsByZipCodeScenarios : BaseIntegrationTest
    {
        public GetLocationsByZipCodeScenarios(WebApplicationFactory<Startup> factory) : base(factory)
        {
            new LocationContextDatabase();
        }

        [Fact]
        public async Task GetByZipCode_ShouldReturnOkWithResult()
        {
            //Arrange
            var expectedResult = new List<GetLocationByZipCodeQueryResult>();

            //Act
            var response = await HttpClient.GetAsync("http://localhost:62140/api/Locations/ByZipCode/36090320");
            var result = JsonConvert.DeserializeObject<IEnumerable<GetLocationByZipCodeQueryResult>>(await response.Content.ReadAsStringAsync());

            //Assert
            response.EnsureSuccessStatusCode();
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
