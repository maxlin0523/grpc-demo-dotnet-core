using AutoMapper;
using Grpc.Core;
using GrpcDemo.DomainService.Core.DTOs;
using GrpcDemo.DomainService.Core.Interfaces.Services;
using GrpcDemo.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcDemo.DomainService.Implements
{
    public class CompanyServiceImp : CompanyService.CompanyServiceBase
    {
        private readonly IMapper _mapper;

        private readonly ICompanyService _companyService;

        public CompanyServiceImp(IMapper mapper, ICompanyService companyService)
        {
            _mapper = mapper;
            _companyService = companyService;
        }

        public override async Task<CompanyResponse> GetById(QueryCompanyMessage request, ServerCallContext context)
        {
            var data = await _companyService.GetById(_mapper.Map<QueryCompanyDTO>(request));

            var result = new CompanyResponse();
            result.Code = ResponseCode.Success;
            result.Data = _mapper.Map<CompanyMessage>(data);
            return result;
        }

        public override async Task<CompaniesResponse> GetAll(Empty request, ServerCallContext context)
        {
            var data = await _companyService.GetAll();

            var result = new CompaniesResponse();
            result.Code = ResponseCode.Success;
            result.Data.AddRange(data.Select(x => _mapper.Map<CompanyMessage>(x)));
            return result;
        }

        public override async Task<GeneralResponse> Create(CompanyMessage request, ServerCallContext context)
        {
            var data = await _companyService.Create(_mapper.Map<CompanyDTO>(request));

            var result = new GeneralResponse();
            result.Code = data.Success ? ResponseCode.Success : ResponseCode.Fail;
            result.Message = data.Message;
            return result;
        }

        public override async Task<GeneralResponse> Update(CompanyMessage request, ServerCallContext context)
        {
            var data = await _companyService.Update(_mapper.Map<CompanyDTO>(request));

            var result = new GeneralResponse();
            result.Code = data.Success ? ResponseCode.Success : ResponseCode.Fail;
            result.Message = data.Message;
            return result;
        }

        public override async Task<GeneralResponse> Delete(QueryCompanyMessage request, ServerCallContext context)
        {
            var data = await _companyService.Delete(_mapper.Map<QueryCompanyDTO>(request));

            var result = new GeneralResponse();
            result.Code = data.Success ? ResponseCode.Success : ResponseCode.Fail;
            result.Message = data.Message;
            return result;
        }
    }
}
