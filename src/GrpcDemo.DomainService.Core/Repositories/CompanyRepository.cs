using GrpcDemo.DomainService.Core.Entities;
using GrpcDemo.DomainService.Core.Interfaces.Repositories;
using GrpcDemo.DomainService.Core.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace GrpcDemo.DomainService.Core.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly SqlConnectionHelper _databaseHelper;
        public CompanyRepository(SqlConnectionHelper helper)
        {
            _databaseHelper = helper;
        }

        public async Task<CompanyEntity> GetById(QueryCompanyEntity query)
        {
            using var conn = _databaseHelper.GetConnection();
            var sql = "SELECT Id, Name, Industry, Address, Phone FROM dbo.Company WHERE Id = @Id";
            var result = await conn.QueryFirstOrDefaultAsync<CompanyEntity>(sql, query);
            return result;
        }

        public async Task<IEnumerable<CompanyEntity>> GetAll()
        {
            using var conn = _databaseHelper.GetConnection();
            var sql = "SELECT Id, Name, Industry, Address, Phone FROM dbo.Company";
            var result = await conn.QueryAsync<CompanyEntity>(sql);
            return result;
        }

        public async Task<bool> IsExists(QueryCompanyEntity query)
        {
            using var conn = _databaseHelper.GetConnection();
            var sql = "SELECT COUNT(*) FROM dbo.Company WHERE Id = @Id";
            var result = await conn.QueryFirstOrDefaultAsync<int>(sql,query);
            return result > 0;
        }

        public async Task<bool> Create(CompanyEntity entity)
        {
            using var conn = _databaseHelper.GetConnection();
            var sql = "INSERT INTO dbo.Company(Id, Name, Industry, Address, Phone) VALUES( @Id, @Name, @Industry, @Address, @Phone)";
            var result = await conn.ExecuteAsync(sql,entity);
            return result > 0;
        }

        public async Task<bool> Update(CompanyEntity entity)
        {
            using var conn = _databaseHelper.GetConnection();
            var sql = "UPDATE dbo.Company SET Name = @Name, Industry = @Industry, Address = @Address, Phone = @Phone WHERE Id = @Id";
            var result = await conn.ExecuteAsync(sql, entity);
            return result > 0;
        }

        public async Task<bool> Delete(QueryCompanyEntity query)
        {
            using var conn = _databaseHelper.GetConnection();
            var sql = "DELETE FROM dbo.Company WHERE Id = @Id";
            var result = await conn.ExecuteAsync(sql, query);
            return result > 0;
        }
    }
}
