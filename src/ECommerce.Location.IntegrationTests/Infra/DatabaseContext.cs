using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace ECommerce.Location.IntegrationTests.Infra
{
    public class DatabaseContext
    {
        private readonly string _localConnectionString;
        private readonly string _databaseName;

        public string ApplicationConnectionString { get; private set; }

        private static readonly Func<string, bool> _startsBatch = s => s.StartsWith("GO", StringComparison.OrdinalIgnoreCase);
        private static readonly Func<string, bool> _isUse = s => s.StartsWith("USE", StringComparison.OrdinalIgnoreCase);
        private static readonly Func<string, bool> _isSet = s => s.StartsWith("SET", StringComparison.OrdinalIgnoreCase);
        private static readonly Func<string, bool> _isComment = s => s.StartsWith("/*") && s.EndsWith("*/");

        public DatabaseContext(string applicationConnectionString, string databaseName)
        {
            ApplicationConnectionString = applicationConnectionString;
            _databaseName = databaseName;

            var _dbFilePath = Path.Combine(Environment.CurrentDirectory, "LocationDb.mdf");
            _localConnectionString = $@"Data Source=(LocalDB)\mssqllocaldb;Initial Catalog={databaseName};AttachDbFileName={_dbFilePath};Integrated Security=True;";
        }

        public void CreateDatabase()
        {
            ConfigureAuthorization();
            ExecuteScript(ApplicationConnectionString, "CreationSrcipt.sql");
            ExecuteScript(ApplicationConnectionString, "DefaultInsertScript.sql");
        }

        public void CleanData()
        {
            ExecuteScript(ApplicationConnectionString, "DeleteAllDataScript.sql");
        }

        private void ConfigureAuthorization()
        {
            var authorizationSql = $@"ALTER DATABASE [{_databaseName}] SET  READ_WRITE;
                                    ALTER LOGIN [sa] WITH PASSWORD=N'ECommerce@123';
                                    ALTER LOGIN [sa] ENABLE;
                                    ALTER AUTHORIZATION ON DATABASE::[{_databaseName}] TO [sa]";

            using (var connection = new SqlConnection(_localConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = authorizationSql;
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private void ExecuteScript(string connectionString, string scriptName)
        {
            Func<string, bool> isValidCommand = x => !_isUse(x) && !_isSet(x) && !_isComment(x);

            var script = Path.Combine(Environment.CurrentDirectory, "Scripts", scriptName);
            var commands = File.ReadAllLines(script).Where(isValidCommand).ToList();

            var batches = CreateBatches(commands).ToList();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var batch in batches)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = string.Join(Environment.NewLine, batch);
                        command.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }
        }

        private IEnumerable<IEnumerable<string>> CreateBatches(IEnumerable<string> commands)
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
