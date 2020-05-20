using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace ECommerce.Location.IntegrationTests
{
    public class DatabaseContext
    {
        private readonly static string _dbFilePath = Path.Combine(Environment.CurrentDirectory, "LocationDb.mdf");
        private readonly string _localConnectionString = $@"Data Source=(LocalDB)\mssqllocaldb;Initial Catalog=LocationDb;AttachDbFileName={_dbFilePath};Integrated Security=True;";

        public string ApplicationConnectionString { get; private set; }

        private static readonly Func<string, bool> _startsBatch = s => s.StartsWith("GO", StringComparison.OrdinalIgnoreCase);
        private static readonly Func<string, bool> _isUse = s => s.StartsWith("USE", StringComparison.OrdinalIgnoreCase);
        private static readonly Func<string, bool> _isSet = s => s.StartsWith("SET", StringComparison.OrdinalIgnoreCase);
        private static readonly Func<string, bool> _isComment = s => s.StartsWith("/*") && s.EndsWith("*/");

        public DatabaseContext(string applicationConnectionString)
        {
            ApplicationConnectionString = applicationConnectionString;
        }

        public void CreateDatabase()
        {
            ExecuteScript(_localConnectionString, "AuthorizationScript.sql");
            ExecuteScript(ApplicationConnectionString, "CreationSrcipt.sql");
            ExecuteScript(ApplicationConnectionString, "DefaultInsertScript.sql");
        }

        public void CleanData()
        {
            ExecuteScript(ApplicationConnectionString, "DeleteAllDataScript.sql");
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
