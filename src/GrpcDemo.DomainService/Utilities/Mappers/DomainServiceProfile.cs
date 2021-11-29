using AutoMapper;
using GrpcDemo.DomainService.Core.DTOs;
using GrpcDemo.DomainService.Core.Entities;
using GrpcDemo.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcDemo.DomainService.Utilities.Mappers
{
    public class DomainServiceProfile : Profile
    {
        public DomainServiceProfile()
        {
            CreateMap<QueryCompanyMessage, QueryCompanyDTO>();
            CreateMap<QueryCompanyDTO, QueryCompanyEntity>();
            CreateMap<CompanyDTO,CompanyEntity>();
            CreateMap<CompanyEntity, CompanyDTO>();
            CreateMap<CompanyDTO, CompanyMessage>();
            CreateMap<CompanyMessage, CompanyDTO>();
        }
    }
}
