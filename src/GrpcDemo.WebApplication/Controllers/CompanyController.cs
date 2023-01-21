using AutoMapper;
using GrpcDemo.Common.Models;
using GrpcDemo.Message;
using GrpcDemo.WebApplication.Parameters;
using GrpcDemo.WebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResponseCode = GrpcDemo.Common.Enums.ResponseCode;

namespace GrpcDemo.WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyService.CompanyServiceClient _serviceClient;

        private readonly IMapper _mapper;

        public CompanyController(IMapper mapper, CompanyService.CompanyServiceClient serviceClient)
        {
            _mapper = mapper;
            _serviceClient = serviceClient;
        }

        [HttpGet]
        public async Task<GeneralResponse<CompanyViewModel>> GetById([FromQuery] QueryCompanyParameter param)
        {
            var result = await _serviceClient.GetByIdAsync(_mapper.Map<QueryCompanyMessage>(param));

            return new GeneralResponse<CompanyViewModel>(
                code: (ResponseCode)result.Code, data: _mapper.Map<CompanyViewModel>(result.Data));
        }

        [HttpGet]
        [Route("All")]
        public async Task<GeneralResponse<IEnumerable<CompanyViewModel>>> GetAll()
        {
            var result = await _serviceClient.GetAllAsync(new Empty());

            return new GeneralResponse<IEnumerable<CompanyViewModel>>(
                code: (ResponseCode)result.Code, data: _mapper.Map<IEnumerable<CompanyViewModel>>(result.Data));
        }

        [HttpPost]
        public async Task<GeneralResponse<string>> Create([FromBody] CompanyParameter param)
        {
            var result = await _serviceClient.CreateAsync(_mapper.Map<CompanyMessage>(param));

            return new GeneralResponse<string>(
                code: (ResponseCode)result.Code, data: string.Empty, message: result.Message);
        }

        [HttpPut]
        public async Task<GeneralResponse<string>> Update([FromBody] CompanyParameter param)
        {
            var result = await _serviceClient.UpdateAsync(_mapper.Map<CompanyMessage>(param));

            return new GeneralResponse<string>(
                code: (ResponseCode)result.Code, data: string.Empty, message: result.Message);
        }

        [HttpDelete]
        public async Task<GeneralResponse<string>> Delete([FromQuery] QueryCompanyParameter param)
        {
            var result = await _serviceClient.DeleteAsync(_mapper.Map<QueryCompanyMessage>(param));

            return new GeneralResponse<string>(
                code: (ResponseCode)result.Code, data: string.Empty, message: result.Message);
        }
    }
}
