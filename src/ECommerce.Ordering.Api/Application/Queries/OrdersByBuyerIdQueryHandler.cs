using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Api.Application.Queries
{
    public class OrdersByBuyerIdQueryHandler : IRequestHandler<OrdersByBuyerIdQuery, IEnumerable<OrdersByBuyerIdQueryResult>>
    {
        private const string query = @"SELECT o.Id OrderId, Date PurchaseDate, b.Name BuyerName
                                       FROM [dbo].[Order] o INNER JOIN [dbo].[Buyer] b ON o.BuyerId = b.Id
                                       WHERE b.Id = @BuyerId";

        private readonly IDbConnection _dbConnection;

        public OrdersByBuyerIdQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<OrdersByBuyerIdQueryResult>> Handle(OrdersByBuyerIdQuery request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("BuyerId", request.BuyerId);

            var orders = await _dbConnection.QueryAsync<OrdersByBuyerIdQueryResult>(query, parameters);

            return orders;
        }
    }
}
