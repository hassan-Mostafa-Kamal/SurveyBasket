using SurveyBasket.Api.Contarcts.DTOs;

namespace SurveyBasket.Api.Contarcts.Validations
{
    public class CreatePollValidator :AbstractValidator<CreateOrUpdatePollDto>
    {
        public CreatePollValidator()
        {
            RuleFor(x => x.Titel)
            .NotEmpty()
           // .WithMessage("Please Add A {PropertyName}")
            .Length(3,100)
            //.WithMessage("titel should be at least {MinLength} char and maximum {MaxLength} char you entered {TotalLength} char")
            ;
            RuleFor(x => x.Description)
               .NotEmpty()
               .Length(3, 1000);
                
        }
    }
}
