using AutoMapper;
using GrpcDemo.WebApplication.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcDemo.Message;
using GrpcDemo.WebApplication.ViewModels;

namespace GrpcDemo.WebApplication.Utilities.Mappers
{
    public class ControllerProfile : Profile
    {
        public ControllerProfile()
        {
            CreateMap<CompanyParameter,CompanyMessage>();

            CreateMap<CompanyMessage, CompanyViewModel>();
        }
    }
}
