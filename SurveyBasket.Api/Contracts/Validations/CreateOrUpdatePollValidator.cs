using SurveyBasket.Api.Contarcts.DTOs;

namespace SurveyBasket.Api.Contarcts.Validations
{
    public class CreateOrUpdatePollValidator :AbstractValidator<CreateOrUpdatePollDto>
    {
        public CreateOrUpdatePollValidator()
        {
            RuleFor(x => x.Titel)
            .NotEmpty()
           // .WithMessage("Please Add A {PropertyName}")
            .Length(3,100)
            //.WithMessage("titel should be at least {MinLength} char and maximum {MaxLength} char you entered {TotalLength} char")
            ;
            RuleFor(x => x.Summary)
               .NotEmpty()
               .Length(3, 1000);
            RuleFor(x => x.StartsAt)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

            RuleFor(x => x.EndsAt)
                .NotEmpty();

            RuleFor(x => x)
                .Must(HasValidDate)
                .WithName(nameof(CreateOrUpdatePollDto.EndsAt))
                .WithMessage("{PropertyName} must be greater then or equals Start date ");


                
        }

        private bool HasValidDate(CreateOrUpdatePollDto dto)
        {
            return dto.EndsAt >= dto.StartsAt;
        }
    }
}
