using GrpcDemo.DomainService.Core.DTOs;
using GrpcDemo.DomainService.Core.Misc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrpcDemo.DomainService.Core.Interfaces.Services
{
    public interface ICompanyService
    {
        public Task<CompanyDTO> GetById(QueryCompanyDTO query);

        public Task<IEnumerable<CompanyDTO>> GetAll();

        public Task<Result> Create(CompanyDTO entity);

        public Task<Result> Update(CompanyDTO entity);

        public Task<Result> Delete(QueryCompanyDTO query);
    }
}
