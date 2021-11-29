using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrpcDemo.DomainService.Core.Utilities.Helpers
{
    public class MySqlConnectionHelper
    {
        public readonly string _connectionString;

        public MySqlConnectionHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
