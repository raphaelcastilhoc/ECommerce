using ECommerce.Location.Api;
using ECommerce.Location.Api.Application.Queries;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Location.IntegrationTests
{
    [TestClass]
    public class LocationScenarios : BaseIntegrationTest<Startup>
    {
        [TestCleanup]
        public void Cleanup()
        {
            CleanData();
        }

        [TestMethod]
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
            var response = await Client.GetAsync($"{Get.ByZipCode}/36090320");
            var result = JsonConvert.DeserializeObject<IEnumerable<GetLocationByZipCodeQueryResult>>(await response.Content.ReadAsStringAsync());

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod]
        public async Task GetByZipCode_ShouldReturnNotFound()
        {
            //Arrange
            var expectedResult = new NotFoundResult();

            //Act
            var result = await Client.GetAsync($"{Get.ByZipCode}/36090777");

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        private class Get
        {
            public const string ByZipCode = "api/Locations/ByZipCode";
        }
    }
}
