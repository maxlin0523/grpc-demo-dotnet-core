using AutoMapper;
using GrpcDemo.DomainService.Core.DTOs;
using GrpcDemo.DomainService.Core.Entities;
using GrpcDemo.DomainService.Core.Interfaces.Repositories;
using GrpcDemo.DomainService.Core.Interfaces.Services;
using GrpcDemo.DomainService.Core.Misc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrpcDemo.DomainService.Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;

        private readonly ICompanyRepository _companyRepository;

        public CompanyService(IMapper mapper, ICompanyRepository companyRepository)
        {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDTO> GetById(QueryCompanyDTO query)
        {
            var result = await _companyRepository.GetById(_mapper.Map<QueryCompanyEntity>(query));

            return _mapper.Map<CompanyDTO>(result);       
        }

        public async Task<IEnumerable<CompanyDTO>> GetAll()
        {
            var result = await _companyRepository.GetAll();

            return _mapper.Map<IEnumerable<CompanyDTO>>(result);
        }

        public async Task<Result> Create(CompanyDTO entity)
        {
            var result = new Result();

            var query = _mapper.Map<QueryCompanyEntity>(new QueryCompanyDTO { Id = entity.Id });
            if (await _companyRepository.IsExists(query))
            {
                result.Message = "Duplicate company";
                return result;
            }

            var success = await _companyRepository.Create(_mapper.Map<CompanyEntity>(entity));

            result.Success = success;
            result.Message = success ? "Create success" : "Create failed";
            return result;

        }

        public async Task<Result> Update(CompanyDTO entity)
        {
            var result = new Result();

            var query = _mapper.Map<QueryCompanyEntity>(new QueryCompanyDTO { Id = entity.Id });
            if (!(await _companyRepository.IsExists(query)))
            {
                result.Message = "This company is no exists";
                return result;
            }

            var success = await _companyRepository.Update(_mapper.Map<CompanyEntity>(entity));

            result.Success = success;
            result.Message = success ? "Update success" : "Update failed";
            return result;
        }

        public async Task<Result> Delete(QueryCompanyDTO query)
        {
            var result = new Result();

            var queryEntity = _mapper.Map<QueryCompanyEntity>(query);
            if (!(await _companyRepository.IsExists(queryEntity)))
            {
                result.Message = "This company is no exists";
                return result;
            }

            var success = await _companyRepository.Delete(queryEntity);

            result.Success = success;
            result.Message = success ? "Delete success" : "Delete failed";
            return result;
        }
    }
}
