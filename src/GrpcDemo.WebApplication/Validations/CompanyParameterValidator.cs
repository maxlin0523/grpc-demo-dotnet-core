using FluentValidation;
using GrpcDemo.WebApplication.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcDemo.WebApplication.Validations
{
    public class CompanyParameterValidator : AbstractValidator<CompanyParameter>
    {
        public CompanyParameterValidator()
        {
            RuleFor(t => t.Id)
                .NotNull().WithMessage("{PropertyName} must be set")
                .Must(IsInt).WithMessage("{PropertyName} must be integer");

            RuleFor(t => t.Name)
                .NotNull().WithMessage("{PropertyName} must be set")
                .Must(value => value.Length < 50).WithMessage("Length of {PropertyName} must be less than 50");

            RuleFor(t => t.Industry)
                .NotNull().WithMessage("{PropertyName} must be set")
                .Must(value => value.Length < 25).WithMessage("Length of {PropertyName} must be less than 25");

            RuleFor(t => t.Address)
                .NotNull().WithMessage("{PropertyName} must be set")
                .Must(value => value.Length < 200).WithMessage("Length of {PropertyName} must be less than 200");

            RuleFor(t => t.Phone)
                .NotNull().WithMessage("{PropertyName} must be set")
                .Must(IsInt).WithMessage("{PropertyName} must be integer");
        }

        private Func<string, bool> IsInt => input => 
        {
            return int.TryParse(input, out int result);
        };
    }
}
