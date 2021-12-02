using System;
using System.Collections.Generic;
using System.Text;

namespace GrpcDemo.DomainService.Core.Lua.Scripts
{
    public class CompanyScript
    {
        public const string Prefix = "Companies:";

        public const string GetById = @"
            local key = KEYS[1]
            return redis.call('HVALS', key)";

        public const string GetAll = @"
            local key = KEYS[1]
            local matches = redis.call('KEYS', key .. '*')
            local array = {}
            for index, value in ipairs(matches) do
                table.insert(array, redis.call('HVALS', value))
            end
            return cjson.encode(array)";

        public const string IsExists = @"
            local key = KEYS[1]
            local matches = redis.call('KEYS', key)
            local length = #matches           
            return length";


        public const string Create = @"
            local key = KEYS[1]
            local id = tonumber(ARGV[1])
            local name = ARGV[2]
            local industry = ARGV[3]
            local address = ARGV[4]
            local phone = ARGV[5]
            return redis.call('HSET', key, 'Id', id, 'Name', name, 'Industry', industry, 'Address', address, 'Phone', phone)";

        public const string Update = @"
            local key = KEYS[1]
            local id = tonumber(ARGV[1])
            local name = ARGV[2]
            local industry = ARGV[3]
            local address = ARGV[4]
            local phone = ARGV[5]
            return redis.call('HSET', key, 'Id', id, 'Name', name, 'Industry', industry, 'Address', address, 'Phone', phone)";

        public const string Delete = @"
            local key = KEYS[1]
            return redis.call('DEL', key)";
    }
}
