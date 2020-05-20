using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ECommerce.Location.IntegrationTests
{
    public class DatabaseAccess
    {
        private readonly DatabaseContext _databaseContext;

        public DatabaseAccess(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql)
        {
            using (var connection = new SqlConnection(_databaseContext.ApplicationConnectionString))
            {
                return await connection.QueryAsync<T>(sql);
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql)
        {
            using (var connection = new SqlConnection(_databaseContext.ApplicationConnectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql);
            }
        }

        public async Task ExecuteAsync(string sql, DynamicParameters parameters)
        {
            using (var connection = new SqlConnection(_databaseContext.ApplicationConnectionString))
            {
                await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
