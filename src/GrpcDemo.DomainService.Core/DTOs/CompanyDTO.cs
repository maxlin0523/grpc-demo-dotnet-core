using System;
using System.Collections.Generic;
using System.Text;

namespace GrpcDemo.DomainService.Core.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Industry { get; set; }

        public string Address { get; set; }

        public int Phone { get; set; }
    }
}
