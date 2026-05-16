namespace SurveyBasket.Api.Contracts.Validations
{
    public class RefreshTokenRequestValidator : AbstractValidator<LoginDto>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(e => e.Password)
                .NotEmpty();
        }
   
    }
}
