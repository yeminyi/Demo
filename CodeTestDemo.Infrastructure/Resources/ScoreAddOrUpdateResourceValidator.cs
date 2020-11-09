using FluentValidation;

namespace CodeTestDemo.Infrastructure.Resources
{
    public class ScoreAddOrUpdateResourceValidator<T> : AbstractValidator<T> where T : ScoreAddOrUpdateResource
    {
        public ScoreAddOrUpdateResourceValidator()
        {
            RuleFor(x => x.GameTitle)
                .NotNull()
                .WithName("GameTitle")
                .WithMessage("required|The {PropertyName} must input")
                .MaximumLength(50)
                .WithMessage("maxlength|{PropertyName} max length is {MaxLength}")
                .MinimumLength(2)
                .WithMessage("minimumLength|{PropertyName} max length is {MinLength}");

            RuleFor(x => x.TeamA)
                .NotNull()
                .WithName("TeamA")
                .WithMessage("required|The {{PropertyName}  must input")
                .MaximumLength(50)
                .WithMessage("maxlength|{PropertyName} max length is {MaxLength}")
                .MinimumLength(2)
                .WithMessage("minlength|{PropertyName} min length is {MinLength}");

            RuleFor(x => x.TeamB)
                .NotNull()
                .WithName("TeamB")
                .WithMessage("required|The {{PropertyName}  must input")
                .MaximumLength(50)
                .WithMessage("maxlength|{PropertyName} max length is {MaxLength}")
                .MinimumLength(2)
                .WithMessage("minlength|{PropertyName} min length is {MinLength}");

            RuleFor(x => x.TeamAScore)
               .GreaterThanOrEqualTo(0)
               .WithName("TeamAScore")
               .WithMessage("The {PropertyName}  must > = 0");

            RuleFor(x => x.TeamBScore)
               .GreaterThanOrEqualTo(0)
               .WithName("TeamBScore")
               .WithMessage("The {PropertyName}  must > = 0");

            RuleFor(m => m.TeamA).Must((model, field) => field != model.TeamB)
                .WithMessage("The teams must be different");
        }
    }
}
