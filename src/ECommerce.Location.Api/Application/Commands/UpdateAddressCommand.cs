using MediatR;

namespace ECommerce.Location.Api.Application.Commands
{
    public class UpdateAddressCommand : IRequest
    {
        public int Id { get; set; }

        public string StreetName { get; set; }

        public int Number { get; set; }

        public int ZipCode { get; set; }
    }
}
