using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcDemo.WebApplication.Parameters
{
    public class CompanyParameter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Industry { get; set; }

        public string Address { get; set; }

        public int Phone { get; set; }
    }
}
