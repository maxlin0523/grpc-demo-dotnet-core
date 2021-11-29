using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace GrpcDemo.DomainService.Core.Utilities.Helpers
{
    public class RedisConnectionHelper
    {
        public readonly string _connectionString;

        public RedisConnectionHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ConnectionMultiplexer GetConnection()
        {
            return ConnectionMultiplexer.Connect(_connectionString);
        }
    }
}
