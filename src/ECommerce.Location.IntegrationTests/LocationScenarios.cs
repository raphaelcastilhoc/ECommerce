using ECommerce.Location.Api;
using ECommerce.Location.Api.Application.Queries;
using FizzWare.NBuilder;
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
            var expectedResult = Builder<GetLocationByZipCodeQueryResult>
                .CreateListOfSize(3)
                .TheFirst(1)
                .With(x => x.Number = 777)
                .TheNext(1)
                .With(x => x.Number = 888)
                .TheNext(1)
                .With(x => x.Number = 999)
                .All()
                .With(x => x.ZipCode = 36090320)
                .With(x => x.StreetName = "Diogo Alvares")
                .And(x => x.CityName = "Juiz de Fora")
                .And(x => x.StateName = "Minas Gerais")
                .And(x => x.CountryName = "Brazil")
                .Build();

            //Act
            var response = await HttpClient.GetAsync("http://localhost:62140/api/Locations/ByZipCode/36090320");
            var result = JsonConvert.DeserializeObject<IEnumerable<GetLocationByZipCodeQueryResult>>(await response.Content.ReadAsStringAsync());

            //    //Assert
            response.EnsureSuccessStatusCode();
            result.Should().BeEquivalentTo(expectedResult);
        }

        //[Fact]
        //public async Task GetByZipCode_ShouldReturnOkWithResult2()
        //{
        //    //Arrange
        //    var expectedResult = new List<GetLocationByZipCodeQueryResult>();

        //    //Act
        //    var response = await HttpClient.GetAsync("http://localhost:62140/api/Locations/ByZipCode/36090320");
        //    var result = JsonConvert.DeserializeObject<IEnumerable<GetLocationByZipCodeQueryResult>>(await response.Content.ReadAsStringAsync());

        //    //Assert
        //    response.EnsureSuccessStatusCode();
        //    result.Should().BeEquivalentTo(expectedResult);
        //}
    }
}
