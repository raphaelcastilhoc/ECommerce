using Dapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Location.Api.Application.Queries
{
    public class GetLocationByZipCodeQueryHandler : IRequestHandler<GetLocationByZipCodeQuery, IEnumerable<GetLocationByZipCodeQueryResult>>
    {
        private const string query = @"SELECT DISTINCT a.Id AddressId, a.StreetName, a.Number, a.ZipCode, c.Name CityName, s.Name StateName, co.Name CountryName
                                        FROM Address a
                                        INNER JOIN City c on a.CityId = c.Id
                                        INNER JOIN State s on c.StateId = c.Id
                                        INNER JOIN Country co on s.CountryId = co.Id
                                        WHERE ZipCode = @ZipCode";

        private readonly IDbConnection _dbConnection;
        private readonly IDistributedCache _cache;

        public GetLocationByZipCodeQueryHandler(IDbConnection dbConnection,
            IDistributedCache cache)
        {
            _dbConnection = dbConnection;
            _cache = cache;
        }

        public async Task<IEnumerable<GetLocationByZipCodeQueryResult>> Handle(GetLocationByZipCodeQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<GetLocationByZipCodeQueryResult> locations;

            var locationsJson = _cache.GetString($"location:{request.ZipCode}");
            if (locationsJson == null)
            {
                locations = await GetLocationsAsync(request);

                if (locations != null)
                {
                    await SaveLocationsOnCacheAsync(request, locations);
                }
            }
            else
            {
                locations = JsonConvert.DeserializeObject<IEnumerable<GetLocationByZipCodeQueryResult>>(locationsJson);
            }

            return locations;
        }

        private async Task<IEnumerable<GetLocationByZipCodeQueryResult>> GetLocationsAsync(GetLocationByZipCodeQuery request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ZipCode", request.ZipCode);

            var locations = await _dbConnection.QueryAsync<GetLocationByZipCodeQueryResult>(query, parameters);
            return locations;
        }

        private async Task SaveLocationsOnCacheAsync(GetLocationByZipCodeQuery request, IEnumerable<GetLocationByZipCodeQueryResult> locations)
        {
            string locationsJson = JsonConvert.SerializeObject(locations);
            var cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
            await _cache.SetStringAsync($"location:{request.ZipCode}", locationsJson, cacheOptions);
        }
    }
}
