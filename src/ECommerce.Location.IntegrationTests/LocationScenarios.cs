using ECommerce.Location.Api;
using ECommerce.Location.Api.Application.Queries;
using ECommerce.Location.IntegrationTests.DTOs;
using ECommerce.Location.IntegrationTests.Infra;
using ECommerce.Location.IntegrationTests.SqlCommands;
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
    public class LocationScenarios : BaseIntegrationTest
    {
        [TestInitialize]
        public void Initialize()
        {
            CleanData();
        }

        [TestMethod]
        public async Task GetByZipCode_ShouldReturnOkWithResult()
        {
            //Arrange
            var city = await DatabaseAccess.QueryFirstOrDefaultAsync<CityDTO>(CitySqlCommands.Get);

            var addresses = new AddressDTO[3]
            {
                new AddressDTO("Diogo Alvares", 777, 36090320, city.Id),
                new AddressDTO("Diogo Alvares", 888, 36090320, city.Id),
                new AddressDTO("Diogo Alvares", 999, 36090320, city.Id),
            };
            await DatabaseAccess.Address(addresses);

            var expectedResult = Builder<GetLocationByZipCodeQueryResult>
                .CreateListOfSize(3)
                .TheFirst(1)
                .With(x => x.Number = 777)
                .And(x => x.AddressId = addresses[0].Id)
                .TheNext(1)
                .With(x => x.Number = 888)
                .And(x => x.AddressId = addresses[1].Id)
                .TheNext(1)
                .With(x => x.Number = 999)
                .And(x => x.AddressId = addresses[2].Id)
                .All()
                .With(x => x.ZipCode = 36090320)
                .With(x => x.StreetName = "Diogo Alvares")
                .And(x => x.CityName = "Juiz de Fora")
                .And(x => x.StateName = "Minas Gerais")
                .And(x => x.CountryName = "Brazil")
                .Build();

            //Act
            var response = await Client.GetAsync($"{ApiEndpoints.Location.GetByZipCode}/36090320");
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
            var result = await Client.GetAsync($"{ApiEndpoints.Location.GetByZipCode}/36090320");

            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
