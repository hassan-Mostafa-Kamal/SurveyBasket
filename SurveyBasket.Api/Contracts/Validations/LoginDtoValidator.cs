namespace SurveyBasket.Api.Contracts.Validations
{
    public class LoginDtoValidator:AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(e => e.Password)
                .NotEmpty();
        }
    }
}
