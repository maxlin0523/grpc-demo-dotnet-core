using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrpcDemo.DomainService.Core.Utilities.Helpers
{
    public class SqlConnectionHelper
    {
        public readonly string _connectionString;

        public SqlConnectionHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
