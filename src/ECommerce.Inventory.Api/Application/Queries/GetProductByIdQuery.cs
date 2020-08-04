using MediatR;

namespace ECommerce.Inventory.Api.Application.Queries
{
    public class GetProductByIdQuery : IRequest<GetProductByIdQueryResult>
    {
        public GetProductByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    public class GetProductByIdQueryResult
    {
        public GetProductByIdQueryResult(string id, string name, int quantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}
