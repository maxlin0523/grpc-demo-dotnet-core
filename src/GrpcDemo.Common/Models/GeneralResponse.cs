using GrpcDemo.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrpcDemo.Common.Models
{
    public class GeneralResponse<T>
    {
        public GeneralResponse(ResponseCode code, T data, string message = "None")
        {
            Code = code;
            Message = message;
            Data = data;
        }

        public ResponseCode Code { get; set; }      

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
