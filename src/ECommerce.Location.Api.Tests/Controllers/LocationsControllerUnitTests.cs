using ECommerce.Location.Api.Application.Queries;
using ECommerce.Location.Api.Controllers;
using FizzWare.NBuilder;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Location.Api.Tests.Controllers
{
    [TestClass]
    public class LocationsControllerUnitTests
    {
        private Mock<IMediator> _mediator;

        private LocationsController _locationsController;

        [TestInitialize]
        public void Initialize()
        {
            _mediator = new Mock<IMediator>();

            _locationsController = new LocationsController(_mediator.Object);
        }

        [TestMethod]
        public async Task GetByZipCode_ShouldReturnOkResultWithLocations()
        {
            //Arrange
            var locations = Builder<GetLocationByZipCodeQueryResult>.CreateListOfSize(3).Build();
            _mediator.Setup(x => x.Send(It.IsAny<GetLocationByZipCodeQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(locations);

            var expectedLocationsResult = Builder<GetLocationByZipCodeQueryResult>.CreateListOfSize(3).Build();
            var expectedResult = new OkObjectResult(expectedLocationsResult);

            //Act
            var result = await _locationsController.GetByZipCode(It.IsAny<int>());

            //Assert
            result.Should().BeEquivalentTo(result);
        }

        [TestMethod]
        public async Task GetByZipCode_ShouldReturnNotFoundIfNotExistsLocations()
        {
            //Arrange
            var locations = Enumerable.Empty<GetLocationByZipCodeQueryResult>();
            _mediator.Setup(x => x.Send(It.IsAny<GetLocationByZipCodeQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(locations);

            var expectedResult = new NotFoundResult();

            //Act
            var result = await _locationsController.GetByZipCode(It.IsAny<int>());

            //Assert
            result.Should().BeEquivalentTo(result);
        }
    }
}
