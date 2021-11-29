using GrpcDemo.DomainService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrpcDemo.DomainService.Core.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        public Task<CompanyEntity> GetById(QueryCompanyEntity query);

        public Task<IEnumerable<CompanyEntity>> GetAll();

        public Task<bool> IsExists(QueryCompanyEntity query);

        public Task<bool> Create(CompanyEntity entity);

        public Task<bool> Update(CompanyEntity entity);

        public Task<bool> Delete(QueryCompanyEntity query);
    }
}
