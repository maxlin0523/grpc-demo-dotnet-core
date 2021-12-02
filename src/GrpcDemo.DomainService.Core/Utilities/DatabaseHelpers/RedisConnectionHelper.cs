using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace GrpcDemo.DomainService.Core.Utilities.DatabaseHelpers
{
    public class RedisConnectionHelper
    {
        private readonly string _connectionString;

        public RedisConnectionHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ConnectionMultiplexer GetConnection()
        {
            return ConnectionMultiplexer.Connect(_connectionString);
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
