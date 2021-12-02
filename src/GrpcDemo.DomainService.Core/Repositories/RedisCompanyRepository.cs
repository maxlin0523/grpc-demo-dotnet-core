using GrpcDemo.DomainService.Core.Entities;
using GrpcDemo.DomainService.Core.Interfaces.Repositories;
using GrpcDemo.DomainService.Core.Lua;
using GrpcDemo.DomainService.Core.Lua.Scripts;
using GrpcDemo.DomainService.Core.Utilities.DatabaseHelpers;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GrpcDemo.DomainService.Core.Repositories
{
    public class RedisCompanyRepository : ICompanyRepository
    {
        private readonly string _prefix;

        private readonly LuaScriptHelper _redisLua;

        public RedisCompanyRepository(RedisConnectionHelper helper)
        {
            _prefix = CompanyScript.Prefix;

            _redisLua = new LuaScriptHelper(helper.GetConnection(), helper.GetConnectionString());
        }

        public async Task<CompanyEntity> GetById(QueryCompanyEntity query)
        {
            var keys = new RedisKey[] { _prefix + query.Id };
            var array = (string[])await _redisLua.Execute(CompanyScript.GetById, keys);

            return array.Length == 0 
                ? null 
                : new CompanyEntity
                {
                    Id = int.Parse(array[0]),
                    Name = array[1],
                    Industry = array[2],
                    Address = array[3],
                    Phone = int.Parse(array[4])
                };
        }

        public async Task<IEnumerable<CompanyEntity>> GetAll()
        {
            var keys = new RedisKey[] { CompanyScript.Prefix };
            var json = (string)await _redisLua.Execute(CompanyScript.GetAll, keys);
            if (json == "{}")
            {
                // 無資料回傳空
                return Enumerable.Empty<CompanyEntity>();
            }

            var data = JsonSerializer.Deserialize<string[][]>(json);
            var result = data.Select(array => new CompanyEntity
            {
                Id = int.Parse(array[0]),
                Name = array[1],
                Industry = array[2],
                Address = array[3],
                Phone = int.Parse(array[4])
            });

            return result;
        }

        public async Task<bool> IsExists(QueryCompanyEntity query)
        {
            var keys = new RedisKey[] { _prefix + query.Id };
            var result = (int)await _redisLua.Execute(CompanyScript.IsExists, keys);

            return result > 0;
        }

        public async Task<bool> Create(CompanyEntity entity)
        {
            var keys = new RedisKey[] { _prefix + entity.Id };
            var values = new RedisValue[] { entity.Id, entity.Name, entity.Industry, entity.Address, entity.Phone };
            var result = (int)await _redisLua.Execute(CompanyScript.Create, keys, values);

            return result > 0;
        }

        public async Task<bool> Update(CompanyEntity entity)
        {
            var keys = new RedisKey[] { _prefix + entity.Id };
            var values = new RedisValue[] { entity.Id, entity.Name, entity.Industry, entity.Address, entity.Phone };
            var result = (int)await _redisLua.Execute(CompanyScript.Update, keys, values);

            return result > 0;
        }

        public async Task<bool> Delete(QueryCompanyEntity query)
        {
            var keys = new RedisKey[] { _prefix + query.Id };
            var result = (int)await _redisLua.Execute(CompanyScript.Delete, keys);

            return result > 0;
        }
    }
}
