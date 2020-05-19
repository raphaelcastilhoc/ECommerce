using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Location.Api.Application.Queries
{
    public class GetLocationByZipCodeQueryHandler : IRequestHandler<GetLocationByZipCodeQuery, IEnumerable<GetLocationByZipCodeQueryResult>>
    {
        private const string query = @"SELECT a.Id AddressId, a.StreetName, a.Number, a.ZipCode, c.Name CityName, s.Name StateName, co.Name CountryName
                                        FROM Address a
                                        INNER JOIN City c on a.CityId = c.Id
                                        INNER JOIN State s on c.StateId = c.Id
                                        INNER JOIN Country co on s.CountryId = co.Id
                                        WHERE ZipCode = @ZipCode";

        private readonly IDbConnection _dbConnection;

        public GetLocationByZipCodeQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<GetLocationByZipCodeQueryResult>> Handle(GetLocationByZipCodeQuery request, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ZipCode", request.ZipCode);

            var locations = await _dbConnection.QueryAsync<GetLocationByZipCodeQueryResult>(query, parameters);

            return locations;
        }
    }
}
