using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GrpcDemo.Common.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ResponseCode
    {
        Success,
        Fail,
        Exception
    }
}
