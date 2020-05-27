using MediatR;
using System.Collections.Generic;

namespace ECommerce.Ordering.Api.Application.Queries
{
    public class GetBuyersQuery : IRequest<IEnumerable<GetBuyersQueryResult>>
    {
    }

    public class GetBuyersQueryResult
    {
        public GetBuyersQueryResult(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
