using MediatR;
using System;
using System.Collections.Generic;

namespace ECommerce.Ordering.Api.Application.Queries
{
    public class GetOrdersByBuyerIdQuery : IRequest<IEnumerable<OrdersByBuyerIdQueryResult>>
    {
        public GetOrdersByBuyerIdQuery(int buyerId)
        {
            BuyerId = buyerId;
        }

        public int BuyerId { get; private set; }
    }

    public class OrdersByBuyerIdQueryResult
    {
        public int OrderId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string BuyerName { get; set; }
    }
}
