using GrpcDemo.Common.Enums;
using GrpcDemo.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GrpcDemo.WebApplication.Utilities.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var res = JsonSerializer.Serialize(new GeneralResponse<string>(ResponseCode.Exception, null, ex.ToString()));

                await context.Response.WriteAsync(res);
            }
        }
    }
}
