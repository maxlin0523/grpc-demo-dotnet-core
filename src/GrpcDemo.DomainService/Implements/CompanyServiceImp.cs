using AutoMapper;
using Grpc.Core;
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

        public CompanyServiceImp(IMapper mapper)
        {
            _mapper = mapper;
        
        }

        public override async Task<CompanyResponse> GetById(QueryCompanyMessage request, ServerCallContext context)
        {
            var result = new CompanyResponse();

            return result;
        }

        public override async Task<CompaniesResponse> GetAll(Empty request, ServerCallContext context)
        {

            return null;
        }

        public override async Task<GeneralResponse> Create(CompanyMessage request, ServerCallContext context)
        {
            return null;
        }

        public override async Task<GeneralResponse> Update(CompanyMessage request, ServerCallContext context)
        {
            return null;
        }

        public override async Task<GeneralResponse> Delete(QueryCompanyMessage request, ServerCallContext context)
        {
            return null;
        }
    }
}
