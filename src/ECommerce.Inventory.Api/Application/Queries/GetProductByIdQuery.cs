using MediatR;

namespace ECommerce.Inventory.Api.Application.Queries
{
    public class GetProductByIdQuery : IRequest<GetProductByIdQueryResult>
    {
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    public class GetProductByIdQueryResult
    {
        public GetProductByIdQueryResult(int id, string name, int quantity)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}
