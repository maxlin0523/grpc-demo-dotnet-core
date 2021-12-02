using GrpcDemo.DomainService.Core.Entities;
using GrpcDemo.DomainService.Core.Interfaces.Repositories;
using GrpcDemo.DomainService.Core.Lua;
using GrpcDemo.DomainService.Core.Lua.Scripts;
using GrpcDemo.DomainService.Core.Utilities.DatabaseHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcDemo.DomainService.Core.Repositories
{
    public class RedisCompanyRepository : ICompanyRepository
    {
        private readonly string _prefix;

        private readonly LuaScriptHelper _lua;

        public RedisCompanyRepository(RedisConnectionHelper helper)
        {
            _prefix = CompanyScr.Prefix;

            _lua = new LuaScriptHelper(helper.GetConnection(), helper.GetConnectionString());
        }

        public async Task<CompanyEntity> GetById(QueryCompanyEntity query)
        {
            return null;
        }

        public async Task<IEnumerable<CompanyEntity>> GetAll()
        {
            return null;
        }

        public async Task<bool> IsExists(QueryCompanyEntity query)
        {
            return default;
        }

        public async Task<bool> Create(CompanyEntity entity)
        {
            return default;
        }

        public async Task<bool> Update(CompanyEntity entity)
        {
            return default;
        }

        public async Task<bool> Delete(QueryCompanyEntity query)
        {
            return default;
        }
    }
}
