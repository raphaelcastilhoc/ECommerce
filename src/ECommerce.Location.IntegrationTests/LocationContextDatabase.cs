using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace ECommerce.Location.IntegrationTests
{
    public class LocationContextDatabase
    {
        private static readonly string _connectionString = @"Data Source=(LocalDB)\mssqllocaldb;Initial Catalog=Location;Integrated Security=True;";
        private readonly IDbConnection _dbConnection;

        private static readonly Func<string, bool> _startsBatch = s => s.StartsWith("GO", StringComparison.OrdinalIgnoreCase);
        private static readonly Func<string, bool> _isUse = s => s.StartsWith("USE", StringComparison.OrdinalIgnoreCase);
        private static readonly Func<string, bool> _isSet = s => s.StartsWith("SET", StringComparison.OrdinalIgnoreCase);
        private static readonly Func<string, bool> _isComment = s => s.StartsWith("/*") && s.EndsWith("*/");

        public LocationContextDatabase()
        {
            _dbConnection = new SqlConnection(_connectionString);
            CreateDatabase();
        }

        public void CreateDatabase()
        {
            using (var connection = _dbConnection)
            {
                connection.Open();

                Func<string, bool> isValidCommand = x => !_isUse(x) && !_isSet(x) && !_isComment(x);

                var dropScript = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DropScript.sql");

                var dropCommands = File.ReadAllLines(dropScript).Where(isValidCommand).ToList();

                var dropBatches = CreateBatches(dropCommands).ToList();

                foreach (var batch in dropBatches)
                {
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = string.Join(Environment.NewLine, batch);
                        query.ExecuteNonQuery();
                    }
                }



                var script = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CreationSrcipt.sql");
                var commands = File.ReadAllLines(script).Where(isValidCommand).ToList();

                var batches = CreateBatches(commands).ToList();

                foreach (var batch in batches)
                {
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = string.Join(Environment.NewLine, batch);
                        query.ExecuteNonQuery();
                    }
                }
            }
        }

        private static IEnumerable<IEnumerable<string>> CreateBatches(IEnumerable<string> commands)
        {
            var batch = new List<string>();
            foreach (var command in commands)
            {
                if (_startsBatch(command))
                {
                    if (batch.Any())
                    {
                        yield return batch;
                        batch = new List<string>();
                    }
                }
                else if (!string.IsNullOrWhiteSpace(command))
                {
                    batch.Add(command);
                }
            }

            if (batch.Any())
            {
                yield return batch;
            }
        }
    }
}
