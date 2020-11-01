using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTestDemo.Infrastructure.Resources
{
    public class ScoreResourceValidator : AbstractValidator<ScoreResource>
    {
        public ScoreResourceValidator()
        {
            RuleFor(x => x.Employee)
                .NotNull()
                .WithName("Employee")
                .WithMessage("{PropertyName}must input")
                .MaximumLength(50)
                .WithMessage("{PropertyName}max length is {MaxLength}")
                .MinimumLength(2)
                .WithMessage("minlength|{PropertyName} min length is {MinLength}");
        }
    }
}
