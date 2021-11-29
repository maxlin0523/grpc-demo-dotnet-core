using Grpc.Core;
using Grpc.Core.Interceptors;
using GrpcDemo.Common.Models;
using GrpcDemo.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcDemo.DomainService.Utilities.Interceptors
{
    public class ExceptionInterceptor : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await base.UnaryServerHandler(request, context, continuation);
            }
            catch (Exception ex)
            {
                return GlobalExceptionFactory<TResponse>(ex.ToString());
            }
        }

        private TResponse GlobalExceptionFactory<TResponse>(string exception)
        {
            var type = typeof(TResponse);
            var code = ResponseCode.Exception;
            var message = exception;

            switch (type.Name)
            {
                case nameof(GeneralResponse): 
                    return (TResponse)Convert.ChangeType(new GeneralResponse { Code = code, Message = exception } , type);
                case nameof(CompanyResponse):
                    return (TResponse)Convert.ChangeType(new CompanyResponse { Code = code }, type);
                case nameof(CompaniesResponse):
                    return (TResponse)Convert.ChangeType(new CompaniesResponse { Code = code }, type);
                default:
                    return default(TResponse);
            }      
        }
    }
}
