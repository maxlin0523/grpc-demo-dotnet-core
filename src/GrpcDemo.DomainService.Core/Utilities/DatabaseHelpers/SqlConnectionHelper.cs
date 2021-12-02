using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrpcDemo.DomainService.Core.Utilities.DatabaseHelpers
{
    public class SqlConnectionHelper
    {
        private readonly string _connectionString;

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
