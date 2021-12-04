using FluentValidation;
using GrpcDemo.WebApplication.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcDemo.WebApplication.Validations
{
    public class QueryCompanyParameterValidator : AbstractValidator<QueryCompanyParameter>
    {
        public QueryCompanyParameterValidator()
        {
            RuleFor(t => t.Id)
                    .NotNull().WithMessage("'{PropertyName}' must be set")
                    .Must(IsInt).WithMessage("'{PropertyName}' must be integer");
        }

        private Func<string, bool> IsInt => input =>
        {
            return int.TryParse(input, out int result);
        };
    }
}
