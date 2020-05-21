using Dapper;
using ECommerce.Location.IntegrationTests.DTOs;
using ECommerce.Location.IntegrationTests.SqlCommands;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ECommerce.Location.IntegrationTests.Infra
{
    public class DatabaseAccess
    {
        private readonly DatabaseContext _databaseContext;

        public DatabaseAccess(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, DynamicParameters parameters = null)
        {
            using (var connection = new SqlConnection(_databaseContext.ApplicationConnectionString))
            {
                return await connection.QueryAsync<T>(sql, parameters);
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, DynamicParameters parameters = null)
        {
            using (var connection = new SqlConnection(_databaseContext.ApplicationConnectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }

        public async Task ExecuteAsync(string sql, DynamicParameters parameters)
        {
            using (var connection = new SqlConnection(_databaseContext.ApplicationConnectionString))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task<AddressDTO[]> Address(params AddressDTO[] addresses)
        {
            foreach (var address in addresses)
            {
                var parameters = new DynamicParameters();
                parameters.Add("StreetName", address.StreetName);
                parameters.Add("Number", address.Number);
                parameters.Add("ZipCode", address.ZipCode);
                parameters.Add("CityId", address.CityId);

                var id = await QueryFirstOrDefaultAsync<int>(AddressSqlCommands.Insert, parameters);
                address.Id = id;
            }

            return addresses;
        }
    }
}
